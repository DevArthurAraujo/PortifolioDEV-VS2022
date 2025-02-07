using System;
using System.Collections.Generic;

namespace PortifolioDEV.ORM;

public partial class TbUsuario
{
    public int IdUsuario { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public int TipoUsuario { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public virtual ICollection<TbAgendamento> TbAgendamentos { get; set; } = new List<TbAgendamento>();
}
