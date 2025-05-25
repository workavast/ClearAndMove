using DCFApixels;
using Fusion;
using UnityEngine;

namespace App.Interaction
{
    public class NetInteractorZone : NetworkBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] public LayerMask layerMask;

        private bool _isVisible;
        private readonly Collider[] _collidersBuffer = new Collider[8];
        private int _collisions;
        
        public void SetVisibility(bool isVisible)
        {
            _isVisible = isVisible;
        }
        
        public void TtyInteract(IInteractor interactor, bool interact)
        {
            if (!HasStateAuthority && !HasInputAuthority)
                return;

            IInteractiveZone zone = null;
            _collisions = Runner.GetPhysicsScene().OverlapSphere(transform.position, radius, _collidersBuffer, 
                layerMask, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < _collisions; i++)
            {
                zone = _collidersBuffer[i].GetComponent<IInteractiveZone>();
                if (zone != null && zone.IsInteractable)
                    break;
            }

            if (interact && zone != null) 
                zone.Interact(interactor);
        }

        public override void Render()
        {
            if (_isVisible)
            {
                for (int i = 0; i < _collisions; i++)
                {
                    var locZone = _collidersBuffer[i].GetComponent<IInteractiveZone>();
                    if (locZone != null && locZone.IsInteractable)
                    {
                        if (_isVisible)
                            locZone.MakeVisible();
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (radius > 0)
                DebugX.Draw().WireSphere(transform.position, radius);
        }
    }
}