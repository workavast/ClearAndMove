using System.Linq;
using UnityEngine;

namespace Avastrad.UI.UiSystem
{
    public class ScreenBase : MonoBehaviour
    {
        public virtual void Initialize() 
            => InitializeChildren();

        protected void InitializeChildren()
        {
            var initializables = GetComponentsInChildren<IInitializable>(true).ToList();
            initializables.Sort(SortInitializables);
            foreach (var initializable in initializables) 
                initializable.Initialize();
        }
        
        public virtual void Show()
            => gameObject.SetActive(true);

        public virtual void Hide()
            => HideInstantly();
        
        public virtual void HideInstantly()
            => gameObject.SetActive(false);
        
        private static int SortInitializables(IInitializable x, IInitializable y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else // If x is null and y is not null, y is greater.
                    return -1;
            }
            else
            {
                // If x is not null...
                if (y == null)// ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    if (x.InitializePriority == y.InitializePriority)
                        return 0;

                    if (x.InitializePriority > y.InitializePriority)
                        return 1;
                    else
                        return -1;
                }
            }
        }
    }
}