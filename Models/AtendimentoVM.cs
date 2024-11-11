namespace PortifolioDEV.Models
{
    public class AtendimentoVM
    {
        public int Id { get; set; }

        public DateTime DtHoraAgendamento { get; set; }

        public DateOnly DataAtendimento { get; set; }

        public TimeOnly Horario { get; set; }

    }
}
