using System;
using System.Collections.Generic;

namespace WebApplication2WebApi.Models.DB;

public partial class Comune
{
    public int Id { get; set; }

    public string Descrizione { get; set; } = null!;

    public virtual ICollection<Persona> Persona { get; set; } = new List<Persona>();
}
