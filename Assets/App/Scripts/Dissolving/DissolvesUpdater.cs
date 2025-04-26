using UnityEngine;

namespace App.Dissolving
{
    public class DissolvesUpdater : MonoBehaviour
    {
        [SerializeField] private DissolvesOwner dissolvesOwner;
        [SerializeField] private float duration;

        public bool Dissolve { get; private set; }
        public float Timer { get; private set; }

        private void LateUpdate()
        {
            if (Dissolve && Timer > duration)
                return;

            if (!Dissolve && Timer < 0)
                return;

            if (Dissolve)
                Timer += Time.deltaTime;
            else
                Timer -= Time.deltaTime;

            dissolvesOwner.ManualUpdate(Timer / duration);
        }

        public void SetVisibilityState(bool isVisible)
        {
            Dissolve = !isVisible;
        }
    }
}