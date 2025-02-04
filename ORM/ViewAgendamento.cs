using System;
using System.Collections.Generic;

namespace PortifolioDEV.ORM;

public partial class ViewAgendamento
{
    public string? Nome { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public string? Senha { get; set; }

    public int? TipoUsuario { get; set; }

    public string? TipoServico { get; set; }

    public decimal? Valor { get; set; }

    public int IdAgendamento { get; set; }

    public DateTime? DtHoraAgendamento { get; set; }

    public DateOnly? DataAtendimento { get; set; }

    public TimeOnly? Horario { get; set; }
}
