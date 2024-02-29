using System;
using System.Collections.Generic;

namespace WebApplication2WebApi.Models.DB;

public partial class Persona
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string? NumeroTelefonico { get; set; }

    public int? Id_ComuneNascita { get; set; }

    public virtual Comune? Id_ComuneNascitaNavigation { get; set; }
}
