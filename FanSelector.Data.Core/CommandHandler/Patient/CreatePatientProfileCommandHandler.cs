using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.Core.Command.Patient;

namespace FanSelector.Data.Core.CommandHandler.Patient
{
    //public class CreatePatientProfileCommandHandler : ICommandHandler<CreatePatientProfileCommand>
    //{
    //    private readonly IPatientCommandRepository _patientCommandRepository;

    //    public CreatePatientProfileCommandHandler(IPatientCommandRepository patientCommandRepository)
    //    {
    //        _patientCommandRepository = patientCommandRepository;
    //    }

    //    public async Task<CommandResult> Execute(CreatePatientProfileCommand command)
    //    {
    //        var commandResult = new CommandResult();
    //        //Validate
    //        await _patientCommandRepository.CreatePatientProfile(command.UserId, command.PatientProfile);
    //        commandResult.Success = true;
    //        return commandResult;
    //    }
    //}
}
