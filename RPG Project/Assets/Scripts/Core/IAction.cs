namespace RPG.Core
{
    public interface IAction {
        void Activate();
        void Cancel();
    }
}