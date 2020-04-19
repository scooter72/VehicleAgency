namespace VehicleAgency.Command
{
    public abstract class CommandBase<R>
    {
        public abstract R Execute(CommandContext context);
    }
}
