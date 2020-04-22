using System;

namespace VehicleAgency.Commands

{
    public abstract class CommandBase<R> : ICommand
    {
        public Command GetCommand() { return (Command)Enum.Parse(typeof(Command), GetType().Name); }

        public abstract R Execute(CommandContext context);

        public object ExecuteCommand(CommandContext context)
        {
            return Execute(context);
        }
    }
}
