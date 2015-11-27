using FanSelector.Core;

namespace FanSelector.Data.Core.Command.Appointment
{
    public class DeletePatientAppointmentCommand : ICommand
    {
        public int Id { get; set; }
    }
}
