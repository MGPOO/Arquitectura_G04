package ec.edu.banquito.service;

import ec.edu.banquito.dto.CuotaCreditoDTO;
import ec.edu.banquito.dto.EvaluacionCreditoRequest;
import ec.edu.banquito.dto.EvaluacionCreditoResponse;
import ec.edu.banquito.entity.*;
import jakarta.ejb.Stateless;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.TypedQuery;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.Period;
import java.util.ArrayList;
import java.util.List;

@Stateless
public class CreditoService {
    
    @PersistenceContext(unitName = "BanQuitoPU")
    private EntityManager em;
    
    public EvaluacionCreditoResponse evaluarCredito(EvaluacionCreditoRequest request) {
        EvaluacionCreditoResponse response = new EvaluacionCreditoResponse();
        
        try {
            // 1. Buscar cliente
            TypedQuery<Cliente> queryCliente = em.createQuery(
                "SELECT c FROM Cliente c WHERE c.cedula = :cedula", Cliente.class);
            queryCliente.setParameter("cedula", request.getCedula());
            List<Cliente> clientes = queryCliente.getResultList();
            
            if (clientes.isEmpty()) {
                response.setSujetoCredito(false);
                response.setAprobado(false);
                response.setMotivo("Cliente no registrado en el banco");
                return response;
            }
            
            Cliente cliente = clientes.get(0);
            
            // 2. Verificar cuenta activa
            if (cliente.getCuentas() == null || cliente.getCuentas().isEmpty()) {
                response.setSujetoCredito(false);
                response.setAprobado(false);
                response.setMotivo("Cliente no tiene cuenta activa");
                return response;
            }
            
            Cuenta cuenta = cliente.getCuentas().get(0);
            
            // 3. Verificar edad y estado civil
            LocalDate hoy = LocalDate.now();
            int edad = Period.between(cliente.getFechaNacimiento(), hoy).getYears();
            
            if (edad < 18) {
                response.setSujetoCredito(false);
                response.setAprobado(false);
                response.setMotivo("Cliente menor de edad");
                return response;
            }
            
            if ("SOLTERO".equals(cliente.getEstadoCivil()) && edad > 55) {
                response.setSujetoCredito(false);
                response.setAprobado(false);
                response.setMotivo("Cliente soltero mayor de 55 años");
                return response;
            }
            
            // 4. Verificar créditos activos
            TypedQuery<Long> queryCreditosActivos = em.createQuery(
                "SELECT COUNT(c) FROM Credito c WHERE c.cliente.idCliente = :idCliente AND c.estado = 'ACTIVO'", 
                Long.class);
            queryCreditosActivos.setParameter("idCliente", cliente.getIdCliente());
            long creditosActivos = queryCreditosActivos.getSingleResult();
            
            if (creditosActivos > 0) {
                response.setSujetoCredito(false);
                response.setAprobado(false);
                response.setMotivo("Cliente tiene créditos activos");
                return response;
            }
            
            // 5. Calcular promedios de depósitos y retiros (últimos 3 meses)
            LocalDateTime hace3Meses = LocalDateTime.now().minusMonths(3);
            
            TypedQuery<BigDecimal> queryDepositos = em.createQuery(
                "SELECT AVG(m.monto) FROM Movimiento m WHERE m.cuenta.idCuenta = :idCuenta " +
                "AND m.tipoMovimiento = 'DEPOSITO' AND m.fechaMovimiento >= :fecha", 
                BigDecimal.class);
            queryDepositos.setParameter("idCuenta", cuenta.getIdCuenta());
            queryDepositos.setParameter("fecha", hace3Meses);
            BigDecimal promedioDepositos = queryDepositos.getSingleResult();
            
            if (promedioDepositos == null) {
                promedioDepositos = BigDecimal.ZERO;
            }
            
            TypedQuery<BigDecimal> queryRetiros = em.createQuery(
                "SELECT AVG(m.monto) FROM Movimiento m WHERE m.cuenta.idCuenta = :idCuenta " +
                "AND m.tipoMovimiento = 'RETIRO' AND m.fechaMovimiento >= :fecha", 
                BigDecimal.class);
            queryRetiros.setParameter("idCuenta", cuenta.getIdCuenta());
            queryRetiros.setParameter("fecha", hace3Meses);
            BigDecimal promedioRetiros = queryRetiros.getSingleResult();
            
            if (promedioRetiros == null) {
                promedioRetiros = BigDecimal.ZERO;
            }
            
            // Verificar movimientos mínimos
            if (promedioDepositos.compareTo(BigDecimal.ZERO) == 0 && 
                promedioRetiros.compareTo(BigDecimal.ZERO) == 0) {
                response.setSujetoCredito(false);
                response.setAprobado(false);
                response.setMotivo("Sin movimientos en los últimos 3 meses");
                return response;
            }
            
            // 6. Calcular monto máximo: (PromedioDepósitos - PromedioRetiros) * 0.50 * PlazoMeses
            BigDecimal diferencia = promedioDepositos.subtract(promedioRetiros);
            BigDecimal montoMaximo = diferencia
                .multiply(new BigDecimal("0.50"))
                .multiply(new BigDecimal(request.getPlazoMeses()))
                .setScale(2, RoundingMode.HALF_UP);
            
            response.setSujetoCredito(true);
            response.setMontoMaximo(montoMaximo);
            
            // 7. Verificar si el precio <= montoMaximo
            if (request.getPrecioElectrodomestico().compareTo(montoMaximo) <= 0) {
                // Crear crédito
                Credito credito = new Credito();
                credito.setCliente(cliente);
                credito.setCuenta(cuenta);
                credito.setMontoCredito(request.getPrecioElectrodomestico());
                credito.setPlazoMeses(request.getPlazoMeses());
                credito.setTasaAnual(new BigDecimal("16.00"));
                credito.setFechaInicio(LocalDate.now());
                credito.setEstado("ACTIVO");
                
                em.persist(credito);
                em.flush();
                
                // Generar tabla de amortización
                generarTablaAmortizacion(credito);
                
                response.setAprobado(true);
                response.setIdCredito(credito.getIdCredito());
                response.setMotivo("Crédito aprobado exitosamente");
            } else {
                response.setAprobado(false);
                response.setMotivo("Monto solicitado excede el monto máximo autorizado");
            }
            
        } catch (Exception e) {
            response.setSujetoCredito(false);
            response.setAprobado(false);
            response.setMotivo("Error al evaluar crédito: " + e.getMessage());
        }
        
        return response;
    }
    
    private void generarTablaAmortizacion(Credito credito) {
        BigDecimal monto = credito.getMontoCredito();
        int plazo = credito.getPlazoMeses();
        BigDecimal tasaMensual = credito.getTasaAnual()
            .divide(new BigDecimal("12"), 6, RoundingMode.HALF_UP)
            .divide(new BigDecimal("100"), 6, RoundingMode.HALF_UP);
        
        // Calcular cuota fija: M * [i(1+i)^n] / [(1+i)^n - 1]
        BigDecimal unoPlusI = BigDecimal.ONE.add(tasaMensual);
        BigDecimal unoPlusIPotN = unoPlusI.pow(plazo);
        BigDecimal numerador = monto.multiply(tasaMensual).multiply(unoPlusIPotN);
        BigDecimal denominador = unoPlusIPotN.subtract(BigDecimal.ONE);
        BigDecimal cuotaFija = numerador.divide(denominador, 2, RoundingMode.HALF_UP);
        
        BigDecimal saldoRestante = monto;
        LocalDate fechaBase = credito.getFechaInicio();
        
        for (int i = 1; i <= plazo; i++) {
            CuotaCredito cuota = new CuotaCredito();
            cuota.setCredito(credito);
            cuota.setNumeroCuota(i);
            cuota.setFechaVencimiento(fechaBase.plusMonths(i));
            
            BigDecimal interes = saldoRestante.multiply(tasaMensual).setScale(2, RoundingMode.HALF_UP);
            BigDecimal capital = cuotaFija.subtract(interes);
            
            // Ajuste para última cuota
            if (i == plazo) {
                capital = saldoRestante;
                cuotaFija = capital.add(interes);
            }
            
            saldoRestante = saldoRestante.subtract(capital);
            
            cuota.setValorCuota(cuotaFija);
            cuota.setInteresPagado(interes);
            cuota.setCapitalPagado(capital);
            cuota.setSaldoRestante(saldoRestante);
            
            em.persist(cuota);
        }
    }
    
    public List<CuotaCreditoDTO> obtenerTablaAmortizacion(Integer idCredito) {
        TypedQuery<CuotaCredito> query = em.createQuery(
            "SELECT c FROM CuotaCredito c WHERE c.credito.idCredito = :idCredito ORDER BY c.numeroCuota", 
            CuotaCredito.class);
        query.setParameter("idCredito", idCredito);
        List<CuotaCredito> cuotas = query.getResultList();
        
        List<CuotaCreditoDTO> resultado = new ArrayList<>();
        for (CuotaCredito cuota : cuotas) {
            CuotaCreditoDTO dto = new CuotaCreditoDTO();
            dto.setNumeroCuota(cuota.getNumeroCuota());
            dto.setFechaVencimiento(cuota.getFechaVencimiento());
            dto.setValorCuota(cuota.getValorCuota());
            dto.setInteresPagado(cuota.getInteresPagado());
            dto.setCapitalPagado(cuota.getCapitalPagado());
            dto.setSaldoRestante(cuota.getSaldoRestante());
            resultado.add(dto);
        }
        
        return resultado;
    }
    
    public Cliente obtenerClientePorCedula(String cedula) {
        TypedQuery<Cliente> query = em.createQuery(
            "SELECT c FROM Cliente c WHERE c.cedula = :cedula", Cliente.class);
        query.setParameter("cedula", cedula);
        List<Cliente> clientes = query.getResultList();
        return clientes.isEmpty() ? null : clientes.get(0);
    }
}
