using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication2WebApi.Models.DB;


namespace WebApplication2WebApi.Handlers.CommandHandlers
{
    public sealed record PostPersonaCommand(Persona persona) : IRequest<Persona>;
    public sealed record PutPersonaCommand(int id) : IRequest<Persona>;
    public sealed record DeletePersonaCommand(int id) : IRequest;

    public sealed class PersonaCommandHandler :
        IRequestHandler<PostPersonaCommand, Persona>,
        IRequestHandler<PutPersonaCommand, Persona>,
        IRequestHandler<DeletePersonaCommand>

    {
        private readonly masterContext _masterContext;
        private Persona persona;

        public PersonaCommandHandler(masterContext masterContext)
        {
            _masterContext = masterContext;
            
        }

        public async Task<Persona> Handle(PostPersonaCommand request, CancellationToken cancellationToken)
        {
            _masterContext.Persona.Add(request.persona);
            await _masterContext.SaveChangesAsync();

            return request.persona;
        }

        public async Task<Persona> Handle(PutPersonaCommand request, CancellationToken cancellationToken)
        {
            var personaTrovata = await _masterContext.Persona.FindAsync(persona.Id);
            if (personaTrovata != null)
            {
                personaTrovata.Nome = persona.Nome;
                personaTrovata.Cognome = persona.Cognome;
                personaTrovata.NumeroTelefonico = persona.NumeroTelefonico;
                personaTrovata.Id_ComuneNascita = persona.Id_ComuneNascita;


                var updatedPersona = _masterContext.Persona.Update(personaTrovata);
            }
            return await _masterContext.Persona.FindAsync(request.id);
        }

        public async Task Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona= await _masterContext.Persona.FindAsync(request.id);
            if (persona != null)
            {
                _masterContext.Persona.Remove(persona);
            }
            await _masterContext.SaveChangesAsync();
        }
    }
}