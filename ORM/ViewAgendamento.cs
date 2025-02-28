using System;
using System.Collections.Generic;

namespace PortifolioDEV.ORM;

public partial class ViewAgendamento
{
    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public int TipoUsuario { get; set; }

    public string TipoServico { get; set; } = null!;

    public decimal Valor { get; set; }

    public int IdAgendamento { get; set; }

    public DateTime DtHoraAgendamento { get; set; }

    public DateOnly DataAtendimento { get; set; }

    public TimeOnly Horario { get; set; }
}
