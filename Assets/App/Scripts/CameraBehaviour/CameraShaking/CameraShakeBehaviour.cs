using App.Entities.Player.SelectionPlayerEntity;
using Avastrad.CustomTimer;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace App.CameraBehaviour.CameraShaking
{
    public class CameraShakeBehaviour : MonoBehaviour
    {
        [SerializeField] private CinemachineBasicMultiChannelPerlin perlin;
        [SerializeField] private NoiseConfig shotConfig;

        [Inject] private readonly CameraShakeProvider _shakeProvider;
        [Inject] private readonly SelectedPlayerEntityProvider _selectedPlayerEntityProvider;

        private readonly Timer _noiseTimer = new(1, 1);

        private void Awake()
        {
            _selectedPlayerEntityProvider.OnWeaponShot += ShotShake;

            perlin.NoiseProfile = shotConfig.NoiseSettings;
            perlin.AmplitudeGain = 0;
        }

        private void Update()
        {
            if (_noiseTimer.TimerIsEnd)
                return;

            _noiseTimer.Tick(Time.deltaTime);
            perlin.AmplitudeGain = _shakeProvider.ShakePower * (1 - _noiseTimer.CurrentTime / _noiseTimer.MaxTime);
        }

        private void ShotShake()
        {
            perlin.NoiseProfile = shotConfig.NoiseSettings;
            _noiseTimer.SetMaxTime(shotConfig.TimeLenght);
        }
    }
}