using System;
using System.Collections.Generic;

namespace PortifolioDEV.ORM;

public partial class TbAtendimento
{
    public int IdAtendimento { get; set; }

    public DateTime DtHoraAgendamento { get; set; }

    public DateOnly DataAtendimento { get; set; }

    public TimeOnly Horario { get; set; }

    public int IdUsuario { get; set; }

    public int IdServico { get; set; }

    public virtual TbServico IdServicoNavigation { get; set; } = null!;

    public virtual TbUsuario IdUsuarioNavigation { get; set; } = null!;
}
