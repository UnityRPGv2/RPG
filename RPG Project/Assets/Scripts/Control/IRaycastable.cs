namespace RPG.Control
{
    public interface IRaycastable
    {
        bool HandleRaycast(PlayerController callingController);
    }
}