using FOVMapping;

namespace App.WarFog
{
    public class FOVManagerProvider
    {
        public readonly FOVManager FOVManager;
        
        public FOVManagerProvider(FOVManager fovManager) 
            => FOVManager = fovManager;
    }
}