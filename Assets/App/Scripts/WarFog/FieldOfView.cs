using App.Entities.Player.SelectionPlayerEntity;
using Avastrad.Extensions;
using DG.Tweening;
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
        [SerializeField] private DynamicFieldOfViewConfig scopeDynamicConfig;
        [SerializeField] private float scopeTime = 0.25f;
        [Space]
        [SerializeField] private GameObject editorView;

        [Inject] private readonly SelectedPlayerEntityProvider _playerProvider;

        private Scope _dynamicScope;

        private void Awake()
        {
            _dynamicScope = new Scope(gameObject, dynamicFieldOfView, defaultDynamicConfig, scopeDynamicConfig);
        }

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
        
        public void SetScopeState(bool isScope) 
            => _dynamicScope.SetScopeState(isScope, scopeTime);

        private class Scope
        {
            private readonly GameObject _owner;
            private readonly DynamicFieldOfView _dynamicFieldOfView;
            private readonly DynamicFieldOfViewConfig _defaultDynamicConfig;
            private readonly DynamicFieldOfViewConfig _scopeDynamicConfig;

            private Tween _tween;
            private bool _isScope;
            
            public Scope(GameObject owner, DynamicFieldOfView dynamicFieldOfView, DynamicFieldOfViewConfig defaultDynamicConfig, DynamicFieldOfViewConfig scopeDynamicConfig)
            {
                _owner = owner;
                _dynamicFieldOfView = dynamicFieldOfView;
                _defaultDynamicConfig = defaultDynamicConfig;
                _scopeDynamicConfig = scopeDynamicConfig;
            }

            public void SetScopeState(bool isScope, float scopeTime)
            {
                if (_isScope == isScope)
                    return;

                _isScope = isScope;
                
                var startConfig = isScope ? _defaultDynamicConfig : _scopeDynamicConfig;
                var startFOV = _dynamicFieldOfView.FOV;
                var startViewDistance = _dynamicFieldOfView.ViewDistance;

                var targetConfig = _isScope ? _scopeDynamicConfig : _defaultDynamicConfig;
                var targetFOV = targetConfig.FOV;
                var targetViewDistance = targetConfig.ViewDistance;

                _dynamicFieldOfView.SetLayers(targetConfig.LayerMask);
                _dynamicFieldOfView.SetRaysPerAngle(targetConfig.RaysPerAngle);
                
                if (_tween != null && _tween.IsActive())
                {
                    _tween.Kill();
                    
                    var completedRatio = (_dynamicFieldOfView.FOV - startConfig.FOV) / (targetConfig.FOV - startConfig.FOV);
                    completedRatio = Mathf.Abs(completedRatio);

                    var remainRatio = 1f - completedRatio;
                    scopeTime *= remainRatio;
                }

                _tween = DOTween.Sequence()
                        .Join(DOTween.To(() => startFOV, x => _dynamicFieldOfView.SetFoV(x), targetFOV, scopeTime))
                        .Join(DOTween.To(() => startViewDistance, x => _dynamicFieldOfView.SetViewDistance(x), targetViewDistance, scopeTime))
                        .SetLink(_owner);
            }
        }
    }
}


