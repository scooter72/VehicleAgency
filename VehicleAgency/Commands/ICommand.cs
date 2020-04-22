namespace VehicleAgency.Commands
{
    interface ICommand
    {
        Command GetCommand();
        object ExecuteCommand(CommandContext context);
    }
}
