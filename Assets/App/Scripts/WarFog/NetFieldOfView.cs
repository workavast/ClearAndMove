using Fusion;
using UnityEngine;

namespace App.WarFog
{
    public class NetFieldOfView : NetworkBehaviour
    {
        [SerializeField] private GameObject editorView;
        [SerializeField] private FieldOfView fieldOfView;
        [SerializeField] private FieldOfViewConfig defaultConfig;

        private void Awake()
        {
#if !UNITY_EDITOR
            editorView.TrySetActive(false);
#endif
        }

        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                fieldOfView.gameObject.SetActive(true);
                fieldOfView.SetData(defaultConfig);
            }
            else
            {
                fieldOfView.gameObject.SetActive(false);
                fieldOfView.SetData(0, 0, 0, 0);
            }
        }
    }
}


