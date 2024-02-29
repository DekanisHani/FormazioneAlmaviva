using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication2WebApi.Models.DB;
namespace WebApplication2WebApi.Handlers.QueryHandlers
{
    public sealed record GetPersonaQuery() : IRequest<List<Persona>>;
    public sealed record GetPersonaByIdQuery(int id) : IRequest<Persona>;
    public sealed record GetPersonaConDapperQuery() : IRequest<List<Persona>>;
    public sealed record GetPersonaByIdConDapperQuery(int id) : IRequest<Persona>;


    public sealed class PersonaQueryHandler :
        IRequestHandler<GetPersonaQuery, List<Persona>>,
        IRequestHandler<GetPersonaByIdQuery, Persona>,
        IRequestHandler<GetPersonaConDapperQuery, List<Persona>>,
        IRequestHandler<GetPersonaByIdConDapperQuery, Persona>
    {
        private readonly masterContext _masterContext;
        private readonly string _connectionString;

        public PersonaQueryHandler(masterContext masterContext)
        {
            _masterContext = masterContext;
            _connectionString = masterContext.Database.GetConnectionString();
        }

        public async Task<List<Persona>> Handle(GetPersonaQuery request, CancellationToken cancellationToken)
        {
            return await _masterContext.Persona.ToListAsync();
        }

        public async Task<Persona> Handle(GetPersonaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _masterContext.Persona.FindAsync(request.id);
        }

        public async Task<List<Persona>> Handle(GetPersonaConDapperQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM Persona";
            using var connection = new SqlConnection(_connectionString);
            var risultato = (await connection.QueryAsync<Persona>(query,
                new {param = (int?)1})).ToList();
            return risultato;
        }


        public async Task<Persona> Handle(GetPersonaByIdConDapperQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM Persona WHERE Persona.Id = @param";
            using var connection = new SqlConnection(_connectionString);
            var risultato = (await connection.QueryAsync<Persona>(query,
                new { param = request.id})).SingleOrDefault();
            return risultato;
        }
    }
}
