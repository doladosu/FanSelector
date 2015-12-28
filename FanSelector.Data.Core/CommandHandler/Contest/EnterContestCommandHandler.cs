using System.Linq;
using System.Threading.Tasks;
using FanSelector.Core;
using FanSelector.Data.CommandService;
using FanSelector.Data.Core.Command.Contest;

namespace FanSelector.Data.Core.CommandHandler.Contest
{
    public class EnterContestCommandHandler : ICommandHandler<EnterContestCommand>
    {
        private readonly IContestCommandRepository _contestCommandRepository;

        public EnterContestCommandHandler(IContestCommandRepository contestCommandRepository)
        {
            _contestCommandRepository = contestCommandRepository;
        }

        public async Task<CommandResult> Execute(EnterContestCommand command)
        {
            var commandResult = new CommandResult();
            var commandValidator = new EnterContestCommand.EnterContestCommandValidator();
            var result = commandValidator.Validate(command);
            if (result.IsValid)
            {
                await _contestCommandRepository.EnterContestTask();
                commandResult.Success = true;
                if (!commandResult.Success)
                {
                    commandResult.Message = "Error occured creating appointment!";
                }
            }
            else
            {
                commandResult.Success = false;
                var error = result.Errors.FirstOrDefault();
                commandResult.Message = error != null ? error.ErrorMessage : "Error occured creating appointment!";
            }
            return commandResult;
        }
    }
}