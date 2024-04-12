using System;
using System.Collections.Generic;

namespace apisaude.Models;

public partial class Medico
{
    public int CodMedico { get; set; }

    public int CodCrm { get; set; }

    public string NomeMedico { get; set; } = null!;

    public virtual ICollection<Paciente> Pacientes { get; } = new List<Paciente>();
}
