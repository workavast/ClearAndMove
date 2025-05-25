using Fusion;
using UnityEngine;

namespace App.Interaction
{
    public class NetInteractiveZone : NetworkBehaviour, IInteractiveZone
    {
        [SerializeField] private InterfaceReference<IInteractive> interactive;

        public bool IsVisible { get; private set; }
        private bool _isVisible;
        private bool _isUsed;
        
        public bool IsInteractable => interactive.Value.IsInteractable;
        public float InteractPercentage => interactive.Value.CurrentInteractTime / interactive.Value.MaxInteractTime;

        public void MakeVisible()
        {
            IsVisible = _isVisible = true;
        }
        
        public void Interact(IInteractor interactor)
        {
            _isUsed = true;
            interactive.Value.AddInteractTime(interactor, Runner.DeltaTime);
        }

        public override void FixedUpdateNetwork()
        {
            if (_isUsed)
                _isUsed = false;
            else
                interactive.Value.SetInteractTime(0);
        }

        public override void Render()
        {
            if (!_isVisible)
                IsVisible = false;

            if (_isVisible) 
                _isVisible = false;
        }
    }
}