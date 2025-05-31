using App.Settings;
using App.Settings.CameraShakePower;

namespace App.CameraBehaviour.CameraShaking
{
    public class CameraShakeProvider
    {
        public float ShakePower => _cameraShakePowerSettingsModel.ShakePower;
        
        private readonly CameraShakePowerSettingsModel _cameraShakePowerSettingsModel;
        
        public CameraShakeProvider(SettingsModel settingsModel)
        {
            _cameraShakePowerSettingsModel = settingsModel.CameraShakePowerSettingsModel;
        }
    }
}