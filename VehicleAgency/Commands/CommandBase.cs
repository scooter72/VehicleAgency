namespace VehicleAgency.Commands

{
    public abstract class CommandBase<R>
    {
        public abstract R Execute(CommandContext context);
    }
}
