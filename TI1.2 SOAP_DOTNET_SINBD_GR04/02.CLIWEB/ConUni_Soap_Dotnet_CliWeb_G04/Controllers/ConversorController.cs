using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ConUni_Soap_Dotnet_CliWeb_G04.Services; // Asegúrate de que coincida con tu namespace real

namespace ConUni_Soap_Dotnet_CliWeb_G04.Controllers
{
    public class ConversorController : Controller
    {
        private readonly ConversorSoapClient _api;

        public ConversorController(ConversorSoapClient api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // --- CONVERSIONES DE PESO ---

        [HttpPost]
        public async Task<IActionResult> KgALibras(double kg)
        {
            if (kg <= 0)
            {
                ViewBag.Error = "Por favor, ingresa un valor válido para kilogramos.";
                return View("Index");
            }

            try
            {
                double lb = await _api.KgALibrasAsync(kg);
                ViewBag.ResultadoPeso = $"{kg} kilogramos equivalen a {lb:F2} libras.";
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al comunicarse con el servicio SOAP: " + ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LibrasAKg(double lb)
        {
            if (lb <= 0)
            {
                ViewBag.Error = "Por favor, ingresa un valor válido para libras.";
                return View("Index");
            }

            try
            {
                double kg = await _api.LibrasAKgAsync(lb);
                ViewBag.ResultadoPeso = $"{lb} libras equivalen a {kg:F2} kilogramos.";
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al comunicarse con el servicio SOAP: " + ex.Message;
            }

            return View("Index");
        }

        // --- CONVERSIONES DE DISTANCIA ---

        [HttpPost]
        public async Task<IActionResult> MetrosAYardas(double metros)
        {
            if (metros <= 0)
            {
                ViewBag.Error = "Por favor, ingresa un valor válido para metros.";
                return View("Index");
            }

            try
            {
                double yardas = await _api.MetrosAYardasAsync(metros);
                ViewBag.ResultadoDistancia = $"{metros} metros equivalen a {yardas:F2} yardas.";
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al comunicarse con el servicio SOAP: " + ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CentimetrosAPies(double cm)
        {
            if (cm <= 0)
            {
                ViewBag.Error = "Por favor, ingresa un valor válido para centímetros.";
                return View("Index");
            }

            try
            {
                double pies = await _api.CentimetrosAPiesAsync(cm);
                ViewBag.ResultadoDistancia = $"{cm} cm equivalen a {pies:F2} pies.";
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al comunicarse con el servicio SOAP: " + ex.Message;
            }

            return View("Index");
        }
    }
}
