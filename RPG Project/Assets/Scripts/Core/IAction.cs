namespace RPG.Core
{
    public interface IAction {
        void Activated();
        void Cancel();
    }
}