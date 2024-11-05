using System;
using System.Collections.Generic;

namespace PortifolioDEV.ORM;

public partial class ViewAtendimento
{
    public DateTime DtHoraAgendamento { get; set; }

    public DateOnly DataAtendimento { get; set; }

    public TimeOnly Horario { get; set; }

    public string TipoServico { get; set; } = null!;

    public decimal Valor { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public int IdAtendimento { get; set; }
}
