using FanSelector.Core;
using FanSelector.Models.Input;
using FluentValidation;

namespace FanSelector.Data.Core.Command.Contest
{
    public class EnterContestCommand : ICommand
    {
        public string UserId { get; set; }
        public ContestEntry ContestEntry { get; set; }

        public class EnterContestCommandValidator : AbstractValidator<EnterContestCommand>
        {
            public EnterContestCommandValidator()
            {
                RuleFor(x => x.UserId).NotEmpty();
                RuleFor(x => x.ContestEntry).NotEmpty();
            }
        }
    }
}