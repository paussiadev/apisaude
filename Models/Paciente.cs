using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace apisaude.Models;

public partial class Paciente
{
    public int CodPaciente { get; set; }

    public long Cpf { get; set; }

    public string NomePac { get; set; } = null!;

    public long CartaoDoSus { get; set; }

    public string? Rua { get; set; }

    public int? NumeroCasa { get; set; }

    public string? Bairro { get; set; }

    public string? Cidade { get; set; }

    public long? Telefone { get; set; }

    public string Email { get; set; } = null!;

    public int? CodMed { get; set; }

    [JsonIgnore]
    public virtual Medico? CodMedNavigation { get; set; }

    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
