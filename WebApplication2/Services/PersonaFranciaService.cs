using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class PersonaFranciaService: IPersonaService<PersonaFranciaService>
    {
        public string AggiungiPrefisso(string numeroTelefonico)
        {

            return "+33" + numeroTelefonico;

        }
    }
}
