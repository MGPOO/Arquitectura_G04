package com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.vista

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.Image
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.unit.dp
import androidx.compose.foundation.layout.RowScope
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.example.conuni_soap_java_g04_cliente_movil.R
import com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.modelo.Unidad
import com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.servicios.ConversorService
import androidx.compose.ui.window.Dialog     // ← usamos Dialog (no AlertDialog)

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent { App() }
    }
}

/* ====== Tema ====== */
private val BluePrimary = Color(0xFF0D47A1)
private val GreenBtn = Color(0xFF2E7D32)
private val RedBtn = Color(0xFFC62828)
private val CardGray = Color(0xFFF5F5F5) // gris claro para contenedores y selector

@Composable
private fun AppTheme(content: @Composable () -> Unit) {
    val scheme = lightColorScheme(
        primary = BluePrimary,
        onPrimary = Color.White,
        background = Color.White,
        surface = Color.White
    )
    MaterialTheme(colorScheme = scheme, content = content)
}

/* ========================== Navegación ========================== */
@Composable
private fun App() {
    AppTheme {
        val nav = rememberNavController()
        NavHost(navController = nav, startDestination = "login") {
            composable("login") {
                LoginScreen {
                    nav.navigate("conv") {
                        popUpTo("login") { inclusive = true }
                    }
                }
            }
            composable("conv") {
                ConverterScreen(
                    onLogout = {
                        nav.navigate("login") {
                            popUpTo(0) { inclusive = true }
                        }
                    }
                )
            }
        }
    }
}

/* ========================== Barra superior ========================== */
@Composable
private fun AppTopBar(
    title: String,
    actions: @Composable RowScope.() -> Unit = {}
) {
    Surface(color = BluePrimary, contentColor = Color.White, tonalElevation = 4.dp) {
        Row(
            Modifier
                .fillMaxWidth()
                .height(56.dp)
                .padding(horizontal = 16.dp),
            verticalAlignment = Alignment.CenterVertically
        ) {
            Text(title, style = MaterialTheme.typography.titleMedium, modifier = Modifier.weight(1f))
            Row(content = actions)
        }
    }
}

/* ========================== Login ========================== */
@Composable
private fun LoginScreen(onSuccess: () -> Unit) {
    var user by remember { mutableStateOf("") }
    var pass by remember { mutableStateOf("") }
    var error by remember { mutableStateOf<String?>(null) }

    Scaffold(topBar = { AppTopBar("Iniciar Sesión") }) { inner ->
        Column(
            Modifier
                .fillMaxSize()
                .padding(inner)
                .padding(16.dp),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            Image(
                painter = painterResource(id = R.drawable.liga_logo),
                contentDescription = "Logo LDU",
                modifier = Modifier
                    .size(140.dp)
                    .padding(bottom = 20.dp)
            )

            OutlinedTextField(
                value = user,
                onValueChange = { user = it },
                label = { Text("Usuario") },
                singleLine = true,
                modifier = Modifier.fillMaxWidth()
            )
            Spacer(Modifier.height(8.dp))
            OutlinedTextField(
                value = pass,
                onValueChange = { pass = it },
                label = { Text("Contraseña") },
                singleLine = true,
                visualTransformation = PasswordVisualTransformation(),
                modifier = Modifier.fillMaxWidth()
            )
            Spacer(Modifier.height(16.dp))
            Button(
                onClick = {
                    if (user.trim() == "MONSTER" && pass == "monster9") {
                        error = null; onSuccess()
                    } else error = "Usuario o contraseña inválidos."
                },
                colors = ButtonDefaults.buttonColors(
                    containerColor = GreenBtn, contentColor = Color.White
                ),
                modifier = Modifier.fillMaxWidth()
            ) { Text("Ingresar") }

            error?.let {
                Spacer(Modifier.height(8.dp))
                Text(it, color = MaterialTheme.colorScheme.error)
            }
        }
    }
}

/* ========================== Conversor ========================== */
@Composable
private fun ConverterScreen(
    onLogout: () -> Unit,
    svc: ConversorService = ConversorService()
) {
    Scaffold(
        topBar = {
            AppTopBar(
                title = "Conversor",
                actions = {
                    Button(
                        onClick = onLogout,
                        colors = ButtonDefaults.buttonColors(
                            containerColor = RedBtn, contentColor = Color.White
                        )
                    ) { Text("Cerrar sesión") }
                }
            )
        }
    ) { inner ->
        Column(
            Modifier
                .fillMaxSize()
                .padding(inner)
                .padding(16.dp)
                .verticalScroll(rememberScrollState())
        ) {
            Text("Conversor de Unidades (Servicio SOAP)",
                style = MaterialTheme.typography.titleLarge)
            Spacer(Modifier.height(16.dp))

            Card(
                Modifier.fillMaxWidth(),
                colors = CardDefaults.cardColors(containerColor = CardGray)
            ) {
                Section(
                    titulo = "Longitud",
                    opciones = listOf(Unidad.METROS, Unidad.KILOMETROS, Unidad.MILLAS, Unidad.YARDAS)
                ) { v, de, a -> svc.longitudes(v, de, a) }
            }
            Spacer(Modifier.height(12.dp))
            Card(
                Modifier.fillMaxWidth(),
                colors = CardDefaults.cardColors(containerColor = CardGray)
            ) {
                Section(
                    titulo = "Masa",
                    opciones = listOf(Unidad.GRAMOS, Unidad.KILOGRAMOS, Unidad.LIBRAS, Unidad.ONZAS)
                ) { v, de, a -> svc.masas(v, de, a) }
            }
            Spacer(Modifier.height(12.dp))
            Card(
                Modifier.fillMaxWidth(),
                colors = CardDefaults.cardColors(containerColor = CardGray)
            ) {
                Section(
                    titulo = "Temperatura",
                    opciones = listOf(Unidad.CELSIUS, Unidad.FAHRENHEIT, Unidad.KELVIN)
                ) { v, de, a -> svc.temperaturas(v, de, a) }
            }
        }
    }
}

/* ====== Sección de conversión ====== */
@Composable
private fun Section(
    titulo: String,
    opciones: List<Unidad>,
    onConvert: (Double, Unidad, Unidad) -> String
) {
    var valorTxt by remember { mutableStateOf("0") }
    var de by remember { mutableStateOf(opciones.first()) }
    var a by remember { mutableStateOf(opciones.last()) }
    var res by remember { mutableStateOf<String?>(null) }
    var formError by remember { mutableStateOf<String?>(null) }

    Column(Modifier.padding(16.dp)) {
        Text(titulo, style = MaterialTheme.typography.titleMedium)
        Spacer(Modifier.height(8.dp))
        OutlinedTextField(
            value = valorTxt,
            onValueChange = { valorTxt = it },
            label = { Text("Valor") },
            singleLine = true,
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(Modifier.height(8.dp))

        Row(Modifier.fillMaxWidth(), horizontalArrangement = Arrangement.spacedBy(12.dp)) {
            UnidadDropdown("De", opciones, de, modifier = Modifier.weight(1f)) { de = it }
            UnidadDropdown("A", opciones, a, modifier = Modifier.weight(1f)) { a = it }
        }

        formError?.let {
            Spacer(Modifier.height(6.dp))
            Text(it, color = MaterialTheme.colorScheme.error)
        }

        Spacer(Modifier.height(12.dp))
        Button(
            onClick = {
                formError = null
                val num = valorTxt.toDoubleOrNull()
                if (num == null) {
                    formError = "Ingrese un número válido."
                    return@Button
                }
                if (de == a) {
                    formError = "Seleccione unidades distintas."
                    return@Button
                }
                res = onConvert(num, de, a)
            },
            colors = ButtonDefaults.buttonColors(
                containerColor = GreenBtn, contentColor = Color.White
            ),
            modifier = Modifier.fillMaxWidth()
        ) { Text("Calcular") }

        res?.let {
            Spacer(Modifier.height(8.dp))
            Text(it)
        }
    }
}

/* ====== Dropdown con Dialog (fondo gris claro, sin container por defecto) ====== */
@Composable
private fun UnidadDropdown(
    label: String,
    opts: List<Unidad>,
    selected: Unidad,
    modifier: Modifier = Modifier,
    onSel: (Unidad) -> Unit
) {
    var open by remember { mutableStateOf(false) }

    // Botón-campo para abrir el selector (seguro al click)
    OutlinedButtonField(
        label = label,
        value = unidadLabel(selected),
        onClick = { open = true },
        modifier = modifier
    )

    if (open) {
        Dialog(onDismissRequest = { open = false }) {
            // Todo el contenido del diálogo lo controlamos nosotros:
            Surface(
                color = CardGray,                    // ← gris claro uniforme
                shape = RoundedCornerShape(12.dp),
                tonalElevation = 0.dp,
                shadowElevation = 6.dp,
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(16.dp)
            ) {
                Column(Modifier.padding(12.dp)) {
                    Text("Selecciona $label",
                        style = MaterialTheme.typography.titleMedium,
                        color = Color.Black)
                    Spacer(Modifier.height(8.dp))

                    opts.forEach { op ->
                        Surface(
                            color = Color.White,       // opciones blancas simples
                            shape = RoundedCornerShape(6.dp),
                            tonalElevation = 0.dp,
                            shadowElevation = 0.dp,
                            modifier = Modifier
                                .fillMaxWidth()
                                .padding(vertical = 4.dp)
                                .clickable {
                                    onSel(op)
                                    open = false
                                }
                        ) {
                            Text(
                                unidadLabel(op),
                                modifier = Modifier.padding(10.dp),
                                color = Color.Black
                            )
                        }
                    }

                    Spacer(Modifier.height(8.dp))
                    Row(Modifier.fillMaxWidth(), horizontalArrangement = Arrangement.End) {
                        TextButton(onClick = { open = false }) {
                            Text("Cerrar", color = BluePrimary)
                        }
                    }
                }
            }
        }
    }
}

/* ====== Campo “falso” que luce como selector ====== */
@Composable
private fun OutlinedButtonField(
    label: String,
    value: String,
    onClick: () -> Unit,
    modifier: Modifier = Modifier
) {
    Column(modifier) {
        Text(label, style = MaterialTheme.typography.labelSmall, color = Color.Gray)
        Spacer(Modifier.height(4.dp))
        OutlinedButton(
            onClick = onClick,
            modifier = Modifier.fillMaxWidth(),
            contentPadding = PaddingValues(horizontal = 12.dp, vertical = 12.dp)
        ) {
            Row(Modifier.fillMaxWidth(), horizontalArrangement = Arrangement.SpaceBetween) {
                Text(value)
                Text("▼")
            }
        }
    }
}

/* ====== Etiquetas de unidades ====== */
private fun unidadLabel(u: Unidad) = when (u) {
    Unidad.METROS -> "Metros"
    Unidad.KILOMETROS -> "Kilómetros"
    Unidad.MILLAS -> "Millas"
    Unidad.YARDAS -> "Yardas"
    Unidad.GRAMOS -> "Gramos"
    Unidad.KILOGRAMOS -> "Kilogramos"
    Unidad.LIBRAS -> "Libras"
    Unidad.ONZAS -> "Onzas"
    Unidad.CELSIUS -> "°C"
    Unidad.FAHRENHEIT -> "°F"
    Unidad.KELVIN -> "K"
}
