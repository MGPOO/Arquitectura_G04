/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */
package ec.edu.monster.view;

import ec.edu.monster.controller.ConversorController;
import javax.swing.JOptionPane;

/**
 *
 * @author Rabedon1
 */
public class ConvensorVista extends javax.swing.JFrame {

    
    public ConvensorVista() {
        
        initComponents();
        controller = new ConversorController();
        
//        cboxLongitudOrigen.setModel(new javax.swing.DefaultComboBoxModel<>(
//            new String[] { "Kilometros, Metros, Millas" }
//        ));
        
//        cboxLongitudDestino.setModel(new javax.swing.DefaultComboBoxModel<>(
//            new String[] { "Kilometros, Metros, Millas" }
//        ));
    }

    private ConversorController controller;
    
    //Longitud
    private String tipoLongitud = "longitud"; 
    private String valorOrigenLongitud;
    private String valorDestinoLongitud;

    //Masa
    private String tipoMasa = "masa";
    private String valorOrigenMasa;
    private String valorDestinoMasa;
    
    //Temperatura
    private String tipoTemperatura = "temperatura";
    private String valorOrigenTemperatura;
    private String valorDestinoTemperatura;
    
    
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jPanel1 = new javax.swing.JPanel();
        jLabel1 = new javax.swing.JLabel();
        jPanel2 = new javax.swing.JPanel();
        jPanel3 = new javax.swing.JPanel();
        jLabel2 = new javax.swing.JLabel();
        jLabel5 = new javax.swing.JLabel();
        jLabel6 = new javax.swing.JLabel();
        jLabel7 = new javax.swing.JLabel();
        btnCalcularLongitud = new javax.swing.JButton();
        cboxLongitudOrigen = new javax.swing.JComboBox<>();
        cboxLongitudDestino = new javax.swing.JComboBox<>();
        txtValorLongitud = new javax.swing.JTextField();
        jPanel4 = new javax.swing.JPanel();
        jLabel3 = new javax.swing.JLabel();
        jLabel8 = new javax.swing.JLabel();
        txtValorMasa = new javax.swing.JTextField();
        jLabel9 = new javax.swing.JLabel();
        cboxMasaOrigen = new javax.swing.JComboBox<>();
        jLabel13 = new javax.swing.JLabel();
        cboxMasaDestino = new javax.swing.JComboBox<>();
        btnCalcularMasa = new javax.swing.JButton();
        jPanel5 = new javax.swing.JPanel();
        jLabel4 = new javax.swing.JLabel();
        jLabel10 = new javax.swing.JLabel();
        jLabel11 = new javax.swing.JLabel();
        txtValorTemperatura = new javax.swing.JTextField();
        cboxTemperaturaOrigen = new javax.swing.JComboBox<>();
        jLabel12 = new javax.swing.JLabel();
        cboxTemperaturaDestino = new javax.swing.JComboBox<>();
        btnCalcularTemperatura = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        jPanel1.setBackground(new java.awt.Color(51, 102, 255));

        jLabel1.setBackground(new java.awt.Color(255, 255, 255));
        jLabel1.setFont(new java.awt.Font("Segoe UI Variable", 1, 18)); // NOI18N
        jLabel1.setForeground(new java.awt.Color(255, 255, 255));
        jLabel1.setText("CONVENSOR DE UNIDADES  (CLIENTE WEB SOAP)");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(374, 374, 374)
                .addComponent(jLabel1)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(27, 27, 27)
                .addComponent(jLabel1)
                .addContainerGap(32, Short.MAX_VALUE))
        );

        jPanel2.setBackground(new java.awt.Color(204, 204, 204));
        jPanel2.setToolTipText("");

        jLabel2.setFont(new java.awt.Font("Segoe UI Variable", 1, 18)); // NOI18N
        jLabel2.setForeground(new java.awt.Color(51, 102, 255));
        jLabel2.setText("LONGITUD");

        jLabel5.setText("VALOR");

        jLabel6.setText("UNIDAD ORIGEN");

        jLabel7.setText("UNIDAD DESTINO");

        btnCalcularLongitud.setBackground(new java.awt.Color(204, 0, 0));
        btnCalcularLongitud.setForeground(new java.awt.Color(255, 255, 255));
        btnCalcularLongitud.setText("CALCULAR");
        btnCalcularLongitud.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnCalcularLongitudActionPerformed(evt);
            }
        });

        cboxLongitudOrigen.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Kilometros", "Metros", "Millas" }));
        cboxLongitudOrigen.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cboxLongitudOrigenActionPerformed(evt);
            }
        });

        cboxLongitudDestino.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Kilometros", "Metros", "Millas" }));
        cboxLongitudDestino.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cboxLongitudDestinoActionPerformed(evt);
            }
        });

        txtValorLongitud.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                txtValorLongitudActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel3Layout = new javax.swing.GroupLayout(jPanel3);
        jPanel3.setLayout(jPanel3Layout);
        jPanel3Layout.setHorizontalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel3Layout.createSequentialGroup()
                .addGap(0, 139, Short.MAX_VALUE)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                        .addComponent(jLabel7)
                        .addComponent(jLabel6)
                        .addComponent(txtValorLongitud, javax.swing.GroupLayout.PREFERRED_SIZE, 90, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(btnCalcularLongitud)
                            .addComponent(cboxLongitudDestino, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addComponent(jLabel2))
                    .addGroup(jPanel3Layout.createSequentialGroup()
                        .addGap(7, 7, 7)
                        .addComponent(cboxLongitudOrigen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addGap(139, 139, 139))
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addGap(158, 158, 158)
                .addComponent(jLabel5, javax.swing.GroupLayout.PREFERRED_SIZE, 64, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        jPanel3Layout.setVerticalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addGap(29, 29, 29)
                .addComponent(jLabel2)
                .addGap(18, 18, 18)
                .addComponent(jLabel5)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(txtValorLongitud, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel6)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(cboxLongitudOrigen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel7)
                .addGap(18, 18, 18)
                .addComponent(cboxLongitudDestino, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(btnCalcularLongitud)
                .addContainerGap(38, Short.MAX_VALUE))
        );

        jLabel3.setFont(new java.awt.Font("Segoe UI Variable", 1, 18)); // NOI18N
        jLabel3.setForeground(new java.awt.Color(51, 102, 255));
        jLabel3.setText("MASA");

        jLabel8.setText("VALOR");

        txtValorMasa.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                txtValorMasaActionPerformed(evt);
            }
        });

        jLabel9.setText("UNIDAD ORIGEN");

        cboxMasaOrigen.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Gramos", "Kilogramos", "Libras" }));
        cboxMasaOrigen.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cboxMasaOrigenActionPerformed(evt);
            }
        });

        jLabel13.setText("UNIDAD DESTINO");

        cboxMasaDestino.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Gramos", "Kilogramos", "Libras" }));
        cboxMasaDestino.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cboxMasaDestinoActionPerformed(evt);
            }
        });

        btnCalcularMasa.setBackground(new java.awt.Color(204, 0, 0));
        btnCalcularMasa.setForeground(new java.awt.Color(255, 255, 255));
        btnCalcularMasa.setText("CALCULAR");
        btnCalcularMasa.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnCalcularMasaActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel4Layout = new javax.swing.GroupLayout(jPanel4);
        jPanel4.setLayout(jPanel4Layout);
        jPanel4Layout.setHorizontalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel4Layout.createSequentialGroup()
                .addContainerGap(119, Short.MAX_VALUE)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel4Layout.createSequentialGroup()
                        .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(cboxMasaOrigen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                .addComponent(jLabel13)
                                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(btnCalcularMasa)
                                    .addComponent(cboxMasaDestino, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addComponent(txtValorMasa, javax.swing.GroupLayout.PREFERRED_SIZE, 90, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addComponent(jLabel9)))
                        .addGap(109, 109, 109))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel4Layout.createSequentialGroup()
                        .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(jLabel8, javax.swing.GroupLayout.PREFERRED_SIZE, 45, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel3))
                        .addGap(130, 130, 130))))
        );
        jPanel4Layout.setVerticalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel4Layout.createSequentialGroup()
                .addGap(24, 24, 24)
                .addComponent(jLabel3)
                .addGap(18, 18, 18)
                .addComponent(jLabel8)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(txtValorMasa, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel9)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(cboxMasaOrigen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel13)
                .addGap(18, 18, 18)
                .addComponent(cboxMasaDestino, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(btnCalcularMasa)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        jLabel4.setFont(new java.awt.Font("Segoe UI Variable", 1, 18)); // NOI18N
        jLabel4.setForeground(new java.awt.Color(51, 102, 255));
        jLabel4.setText("TEMPERATURA");

        jLabel10.setText("UNIDAD ORIGEN");

        jLabel11.setText("VALOR");

        txtValorTemperatura.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                txtValorTemperaturaActionPerformed(evt);
            }
        });

        cboxTemperaturaOrigen.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Celsius", "Fahrenheit", "Kelvin" }));
        cboxTemperaturaOrigen.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cboxTemperaturaOrigenActionPerformed(evt);
            }
        });

        jLabel12.setText("UNIDAD DESTINO");

        cboxTemperaturaDestino.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Celsius", "Fahrenheit", "Kelvin" }));
        cboxTemperaturaDestino.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cboxTemperaturaDestinoActionPerformed(evt);
            }
        });

        btnCalcularTemperatura.setBackground(new java.awt.Color(204, 0, 0));
        btnCalcularTemperatura.setForeground(new java.awt.Color(255, 255, 255));
        btnCalcularTemperatura.setText("CALCULAR");
        btnCalcularTemperatura.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnCalcularTemperaturaActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel5Layout = new javax.swing.GroupLayout(jPanel5);
        jPanel5.setLayout(jPanel5Layout);
        jPanel5Layout.setHorizontalGroup(
            jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel5Layout.createSequentialGroup()
                .addContainerGap(127, Short.MAX_VALUE)
                .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel5Layout.createSequentialGroup()
                        .addComponent(jLabel4)
                        .addGap(91, 91, 91))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel5Layout.createSequentialGroup()
                        .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(jLabel12)
                            .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                .addGroup(jPanel5Layout.createSequentialGroup()
                                    .addGap(6, 6, 6)
                                    .addComponent(cboxTemperaturaOrigen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addComponent(jLabel10)
                                .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                    .addComponent(jLabel11, javax.swing.GroupLayout.PREFERRED_SIZE, 62, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addComponent(txtValorTemperatura, javax.swing.GroupLayout.PREFERRED_SIZE, 90, javax.swing.GroupLayout.PREFERRED_SIZE)))
                            .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                .addComponent(cboxTemperaturaDestino, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addComponent(btnCalcularTemperatura)))
                        .addGap(105, 105, 105))))
        );
        jPanel5Layout.setVerticalGroup(
            jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel5Layout.createSequentialGroup()
                .addGap(29, 29, 29)
                .addComponent(jLabel4)
                .addGap(18, 18, 18)
                .addComponent(jLabel11)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(txtValorTemperatura, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(13, 13, 13)
                .addComponent(jLabel10)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(cboxTemperaturaOrigen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel12)
                .addGap(18, 18, 18)
                .addComponent(cboxTemperaturaDestino, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(btnCalcularTemperatura)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addGap(19, 19, 19)
                .addComponent(jPanel3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 35, Short.MAX_VALUE)
                .addComponent(jPanel4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(37, 37, 37)
                .addComponent(jPanel5, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addGap(38, 38, 38)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jPanel4, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jPanel5, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jPanel3, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap(18, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(0, 29, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jPanel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void btnCalcularLongitudActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnCalcularLongitudActionPerformed
        
        try {
            // 1️⃣ Leer los valores ingresados
            double valor = Double.parseDouble(txtValorLongitud.getText().trim());
            // 3️⃣ Llamar al controlador
            double resultado = controller.convertir(tipoLongitud, valor, valorOrigenLongitud, valorDestinoLongitud);

            // 4️⃣ Mostrar el resultado
            JOptionPane.showMessageDialog(
                this,
                "Resultado: " + resultado,
                "Conversión exitosa",
                JOptionPane.INFORMATION_MESSAGE
            );

        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(
                this,
                "Por favor ingresa un valor numérico válido",
                "Error",
                JOptionPane.ERROR_MESSAGE
            );
        } catch (Exception e) {
            JOptionPane.showMessageDialog(
                this,
                "Error al convertir: " + e.getMessage(),
                "Error",
                JOptionPane.ERROR_MESSAGE
            );
        }
    }//GEN-LAST:event_btnCalcularLongitudActionPerformed

    private void cboxLongitudOrigenActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cboxLongitudOrigenActionPerformed
        valorOrigenLongitud = cboxLongitudOrigen.getSelectedItem().toString().toLowerCase();
        System.out.println("El valor origen es: " + valorOrigenLongitud);
        
    }//GEN-LAST:event_cboxLongitudOrigenActionPerformed

    private void cboxLongitudDestinoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cboxLongitudDestinoActionPerformed
        valorDestinoLongitud = cboxLongitudDestino.getSelectedItem().toString().toLowerCase();
        System.out.println("El valor destino es: " + valorDestinoLongitud);
    }//GEN-LAST:event_cboxLongitudDestinoActionPerformed

    private void txtValorLongitudActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_txtValorLongitudActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_txtValorLongitudActionPerformed

    private void txtValorMasaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_txtValorMasaActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_txtValorMasaActionPerformed

    private void cboxMasaOrigenActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cboxMasaOrigenActionPerformed
        valorOrigenMasa = cboxMasaOrigen.getSelectedItem().toString().toLowerCase();
        System.out.println("El valor origen es: " + valorOrigenMasa);
    }//GEN-LAST:event_cboxMasaOrigenActionPerformed

    private void cboxMasaDestinoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cboxMasaDestinoActionPerformed
        valorDestinoMasa = cboxMasaDestino.getSelectedItem().toString().toLowerCase();
        System.out.println("El valor destino es: " + valorDestinoMasa);
    }//GEN-LAST:event_cboxMasaDestinoActionPerformed

    private void btnCalcularMasaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnCalcularMasaActionPerformed
        
        try {
            // 1️⃣ Leer los valores ingresados
            double valor = Double.parseDouble(txtValorMasa.getText().trim());
            // 3️⃣ Llamar al controlador
            double resultado = controller.convertir(tipoMasa, valor, valorOrigenMasa, valorDestinoMasa);

            // 4️⃣ Mostrar el resultado
            JOptionPane.showMessageDialog(
                this,
                "Resultado: " + resultado,
                "Conversión exitosa",
                JOptionPane.INFORMATION_MESSAGE
            );

        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(
                this,
                "Por favor ingresa un valor numérico válido",
                "Error",
                JOptionPane.ERROR_MESSAGE
            );
        } catch (Exception e) {
            JOptionPane.showMessageDialog(
                this,
                "Error al convertir: " + e.getMessage(),
                "Error",
                JOptionPane.ERROR_MESSAGE
            );
        }
    }//GEN-LAST:event_btnCalcularMasaActionPerformed

    private void txtValorTemperaturaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_txtValorTemperaturaActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_txtValorTemperaturaActionPerformed

    private void cboxTemperaturaOrigenActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cboxTemperaturaOrigenActionPerformed
        valorOrigenTemperatura = cboxTemperaturaOrigen.getSelectedItem().toString().toLowerCase();
        System.out.println("El valor origen es: " + valorOrigenTemperatura);
    }//GEN-LAST:event_cboxTemperaturaOrigenActionPerformed

    private void cboxTemperaturaDestinoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cboxTemperaturaDestinoActionPerformed
        valorDestinoTemperatura = cboxTemperaturaDestino.getSelectedItem().toString().toLowerCase();
        System.out.println("El valor destino es: " + valorOrigenTemperatura);
    }//GEN-LAST:event_cboxTemperaturaDestinoActionPerformed

    private void btnCalcularTemperaturaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnCalcularTemperaturaActionPerformed
        
        try {
            // 1️⃣ Leer los valores ingresados
            double valor = Double.parseDouble(txtValorTemperatura.getText().trim());
            // 3️⃣ Llamar al controlador
            double resultado = controller.convertir(tipoTemperatura, valor, valorOrigenTemperatura, valorDestinoTemperatura);

            // 4️⃣ Mostrar el resultado
            JOptionPane.showMessageDialog(
                this,
                "Resultado: " + resultado,
                "Conversión exitosa",
                JOptionPane.INFORMATION_MESSAGE
            );

        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(
                this,
                "Por favor ingresa un valor numérico válido",
                "Error",
                JOptionPane.ERROR_MESSAGE
            );
        } catch (Exception e) {
            JOptionPane.showMessageDialog(
                this,
                "Error al convertir: " + e.getMessage(),
                "Error",
                JOptionPane.ERROR_MESSAGE
            );
        }
    }//GEN-LAST:event_btnCalcularTemperaturaActionPerformed

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(ConvensorVista.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(ConvensorVista.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(ConvensorVista.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(ConvensorVista.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ConvensorVista().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton btnCalcularLongitud;
    private javax.swing.JButton btnCalcularMasa;
    private javax.swing.JButton btnCalcularTemperatura;
    private javax.swing.JComboBox<String> cboxLongitudDestino;
    private javax.swing.JComboBox<String> cboxLongitudOrigen;
    private javax.swing.JComboBox<String> cboxMasaDestino;
    private javax.swing.JComboBox<String> cboxMasaOrigen;
    private javax.swing.JComboBox<String> cboxTemperaturaDestino;
    private javax.swing.JComboBox<String> cboxTemperaturaOrigen;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel10;
    private javax.swing.JLabel jLabel11;
    private javax.swing.JLabel jLabel12;
    private javax.swing.JLabel jLabel13;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JLabel jLabel9;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel jPanel3;
    private javax.swing.JPanel jPanel4;
    private javax.swing.JPanel jPanel5;
    private javax.swing.JTextField txtValorLongitud;
    private javax.swing.JTextField txtValorMasa;
    private javax.swing.JTextField txtValorTemperatura;
    // End of variables declaration//GEN-END:variables
}
