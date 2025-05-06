using App.Entities.Player.SelectionPlayerEntity;
using Avastrad.Extensions;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.WarFog
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private NetworkBehaviour owner;
        [Space]
        [SerializeField] private StaticFieldOfViewConfig staticConfig;
        [SerializeField] private StaticFieldOfView staticFieldOfView;
        [Space]
        [SerializeField] private DynamicFieldOfView dynamicFieldOfView;
        [SerializeField] private DynamicFieldOfViewConfig defaultDynamicConfig;
        [Space]
        [SerializeField] private GameObject editorView;

        [Inject] private readonly SelectedPlayerEntityProvider _playerProvider;

        private void Start()
        {
#if !UNITY_EDITOR
            editorView.TrySetActive(false);
#endif
            dynamicFieldOfView.SetData(defaultDynamicConfig);
            SetDynamicVisibilityState(owner.HasInputAuthority);
        }

        private void LateUpdate()
        {
            if (_playerProvider.HasEntity)
            {
                var inRange = _playerProvider.Position.InYRange(owner.transform.position, staticConfig.Offset);
                staticFieldOfView.SetVisibility(inRange);
            }
        }

        public void SetDynamicVisibilityState(bool isVisible)
        {
            dynamicFieldOfView.gameObject.SetActive(isVisible);
        }
    }
}


