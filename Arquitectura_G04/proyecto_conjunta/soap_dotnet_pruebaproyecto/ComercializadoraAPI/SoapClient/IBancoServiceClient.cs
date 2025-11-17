using System.ServiceModel;
using System.Runtime.Serialization;

namespace ComercializadoraAPI.SoapClient
{
    [ServiceContract]
    public interface IBancoServiceClient
    {
        [OperationContract]
        Task<EvaluateCreditResponse> EvaluateCreditAsync(EvaluateCreditRequest request);

        [OperationContract]
        Task<GetAmortizationScheduleResponse> GetAmortizationScheduleAsync(GetAmortizationScheduleRequest request);

        [OperationContract]
        Task<GetClientInfoResponse> GetClientInfoAsync(GetClientInfoRequest request);
    }

    // DTOs para comunicación SOAP
    [DataContract]
    public class EvaluateCreditRequest
    {
        [DataMember]
        public string Cedula { get; set; } = string.Empty;

        [DataMember]
        public decimal PrecioElectrodomestico { get; set; }

        [DataMember]
        public int PlazoMeses { get; set; }
    }

    [DataContract]
    public class EvaluateCreditResponse
    {
        [DataMember]
        public bool SujetoCredito { get; set; }

        [DataMember]
        public decimal MontoMaximo { get; set; }

        [DataMember]
        public bool Aprobado { get; set; }

        [DataMember]
        public int IdCredito { get; set; }

        [DataMember]
        public string Mensaje { get; set; } = string.Empty;
    }

    [DataContract]
    public class GetAmortizationScheduleRequest
    {
        [DataMember]
        public int IdCredito { get; set; }
    }

    [DataContract]
    public class AmortizationCuota
    {
        [DataMember]
        public int NumeroCuota { get; set; }

        [DataMember]
        public DateTime FechaVencimiento { get; set; }

        [DataMember]
        public decimal ValorCuota { get; set; }

        [DataMember]
        public decimal InteresPagado { get; set; }

        [DataMember]
        public decimal CapitalPagado { get; set; }

        [DataMember]
        public decimal SaldoRestante { get; set; }
    }

    [DataContract]
    public class GetAmortizationScheduleResponse
    {
        [DataMember]
        public List<AmortizationCuota> Cuotas { get; set; } = new List<AmortizationCuota>();

        [DataMember]
        public string Mensaje { get; set; } = string.Empty;
    }

    [DataContract]
    public class GetClientInfoRequest
    {
        [DataMember]
        public string Cedula { get; set; } = string.Empty;
    }

    [DataContract]
    public class ClientInfo
    {
        [DataMember]
        public string Cedula { get; set; } = string.Empty;

        [DataMember]
        public string NombreCompleto { get; set; } = string.Empty;

        [DataMember]
        public DateTime FechaNacimiento { get; set; }

        [DataMember]
        public string EstadoCivil { get; set; } = string.Empty;

        [DataMember]
        public List<CuentaInfo> Cuentas { get; set; } = new List<CuentaInfo>();
    }

    [DataContract]
    public class CuentaInfo
    {
        [DataMember]
        public string NumeroCuenta { get; set; } = string.Empty;

        [DataMember]
        public string TipoCuenta { get; set; } = string.Empty;

        [DataMember]
        public decimal Saldo { get; set; }
    }

    [DataContract]
    public class GetClientInfoResponse
    {
        [DataMember]
        public ClientInfo? Cliente { get; set; }

        [DataMember]
        public string Mensaje { get; set; } = string.Empty;
    }
}
