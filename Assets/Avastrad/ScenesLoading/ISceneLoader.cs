using System;

namespace Avastrad.ScenesLoading
{
    public interface ISceneLoader
    {
        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public void ShowLoadScreen(bool showInstantly, Action onShowedCallback = null);
        public void HideLoadScreen(bool hideLoadScreenInstantly);
        public void LoadScene(int index, bool showLoadScreenInstantly = false, bool skipLoadingScene = false);
        public void LoadTargetScene();
    }
}