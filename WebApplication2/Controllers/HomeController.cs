using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IPersonaService<PersonaItaliaService> _personaItaliaService;
        private readonly IPersonaService<PersonaFranciaService> _personaFranciaService;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IPersonaService<PersonaItaliaService> personaItaliaService, IPersonaService<PersonaFranciaService> personaFranciaService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _personaItaliaService = personaItaliaService;
            _personaFranciaService = personaFranciaService;
        }

        public async Task<IActionResult> Index()
        {

            //var prefissoFrancese = _personaFranciaService.AggiungiPrefisso("1234567890");
            //_logger.LogInformation(prefissoFrancese);
            //var prefissoItaliano = _personaItaliaService.AggiungiPrefisso("0987654321");
            //_logger.LogInformation(prefissoItaliano);

            var persona = new PersonaViewModel();
            persona.Nome = "Mario";
            persona.Cognome = "Rossi";
            persona.NumeroTelefono = 320123;

            var persona2 = new PersonaViewModel();
            persona2.Nome = "Marta";
            persona2.Cognome = "Verdi";
            persona2.NumeroTelefono = 324156;

            var persona3 = new PersonaViewModel();
            persona3.Nome = "Marco";
            persona3.Cognome = "Bianchi";
            persona3.NumeroTelefono = 388857;

            var list = new List<PersonaViewModel>
            {
                persona,
                persona2,
                persona3,
            };

            ViewBag.Count = list.Count;
            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> _ListPersone()
        {
            var response = await _httpClient.GetAsync("https://localhost:7031/api/Persona");
            _ = response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var persone = JsonConvert.DeserializeObject<List<PersonaViewModel>>(responseString);

            ViewBag.Count = persone.Count;
            return PartialView(persone);
        }
        [HttpPost]
        public IActionResult SubmitForm(PersonaViewModel persona)
        {
            persona.Id_ComuneNascita = 1;
            var persona_json = JsonConvert.SerializeObject(persona);
            var data = new StringContent(persona_json.ToString(), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("https://localhost:7031/api/Persona", data).Result;

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(PersonaViewModel persona, int id)
        {
            var response = await _httpClient.GetAsync("https://localhost:7031/api/Persona/" + id);
            var responseString = await response.Content.ReadAsStringAsync();
            var persone = JsonConvert.DeserializeObject<PersonaViewModel>(responseString);
            return PartialView(persone);
        }

        public async Task<IActionResult> Edited(PersonaViewModel persona, int id)
        {
            var persona_json = JsonConvert.SerializeObject(persona);
            var data = new StringContent(persona_json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7031/api/Persona/" + id, data);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(PersonaViewModel persona, int id)
        {
            var personaDeleted = await _httpClient.DeleteAsync("https://localhost:7031/api/Persona/" + id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult _FormPersone()
        {
            return PartialView();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
