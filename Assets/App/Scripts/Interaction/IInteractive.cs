namespace App.Interaction
{
    public interface IInteractive
    {
        public bool IsInteractable { get; }
        public float MaxInteractTime { get; }
        public float CurrentInteractTime { get; }

        public void Interact(IInteractor interactor);
        public void AddInteractTime(IInteractor interactor, float value);
        public void SetInteractTime(float value);
    }
    
    public interface IInteractiveZone
    {
        public bool IsInteractable { get; }

        public void MakeVisible();
        public void Interact(IInteractor interactor);
    }
}