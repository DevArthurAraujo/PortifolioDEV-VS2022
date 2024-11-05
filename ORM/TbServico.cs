using System;
using System.Collections.Generic;

namespace PortifolioDEV.ORM;

public partial class TbServico
{
    public int IdServico { get; set; }

    public string TipoServico { get; set; } = null!;

    public decimal Valor { get; set; }

    public virtual ICollection<TbAtendimento> TbAtendimentos { get; set; } = new List<TbAtendimento>();
}
