using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class PersonaItaliaService: IPersonaService<PersonaItaliaService>
    {
        public string AggiungiPrefisso(string numeroTelefonico) {

            return "+39" + numeroTelefonico;

        }
    }
}
