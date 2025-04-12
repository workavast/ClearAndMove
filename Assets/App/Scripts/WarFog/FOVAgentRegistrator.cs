using FOVMapping;
using UnityEngine;
using Zenject;

namespace App.WarFog
{
    public class FOVAgentRegistrator : MonoBehaviour
    {
        private FOVManagerProvider _fovManagerProvider;

        [Inject]
        public void Construct(FOVManagerProvider fovManagerProvider)
        {
            Debug.Log("Construct");
            _fovManagerProvider = fovManagerProvider;
        }
        
        private void Start()
        {
            var agents = GetComponentsInChildren<FOVAgent>();

            Debug.Log(_fovManagerProvider == null);
            Debug.Log(_fovManagerProvider.FOVManager == null);
            
            foreach (var agent in agents) 
                _fovManagerProvider.FOVManager.AddFOVAgent(agent);
        }
    }
}