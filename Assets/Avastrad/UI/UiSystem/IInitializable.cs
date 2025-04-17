namespace Avastrad.UI.UiSystem
{
    public interface IInitializable
    {
        public int InitializePriority { get; }
        public void Initialize();
    }
}