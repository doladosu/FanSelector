using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.Core.Command.Appointment;

namespace FanSelector.Data.Core.CommandHandler.Appointment
{
    //public class UpdatePatientAppointmentCommandHandler : ICommandHandler<UpdatePatientAppointmentCommand>
    //{
    //    private readonly IAppointmentCommandRepository _appointmentCommandRepository;

    //    public UpdatePatientAppointmentCommandHandler(IAppointmentCommandRepository appointmentCommandRepository)
    //    {
    //        _appointmentCommandRepository = appointmentCommandRepository;
    //    }

    //    public async Task<CommandResult> Execute(UpdatePatientAppointmentCommand command)
    //    {
    //        var commandResult = new CommandResult();
    //        var commandValidator = new UpdatePatientAppointmentCommand.UpdatePatientAppointmentCommandValidator();
    //        var result = commandValidator.Validate(command);
    //        if (result.IsValid)
    //        {
    //            await _appointmentCommandRepository.UpdatePatientAppointment(command.Appointment);
    //            commandResult.Success = true;
    //            if (!commandResult.Success)
    //            {
    //                commandResult.Message = "Error occured updating appointment!";
    //            }
    //        }
    //        else
    //        {
    //            commandResult.Success = false;
    //            var error = result.Errors.FirstOrDefault();
    //            commandResult.Message = error != null ? error.ErrorMessage : "Error occured updating appointment!";
    //        }
    //        return commandResult;
    //    }
    //}
}