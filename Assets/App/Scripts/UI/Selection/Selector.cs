using System.Collections.Generic;
using App.UI.WindowsSwitching;
using UnityEngine;

namespace App.UI.Selection
{
    public abstract class Selector<TId> : MonoBehaviour, IWindow
    {
        [field: SerializeField] public string Id { get; private set; }
        [SerializeField] private SelectionBtn<TId> weaponSelectBtnPrefab;
        [SerializeField] private Transform holder;

        private SelectionBtn<TId>[] _buttons;
        
        private void Awake() 
            => Initialize();

        protected virtual void Initialize()
        {
            var allIds = GetIds();
            _buttons = new SelectionBtn<TId>[allIds.Count];

            for (int i = 0; i < allIds.Count; i++)
            {
                var id = allIds[i];
                
                var view = Instantiate(weaponSelectBtnPrefab, holder);
                view.SetData(id, GetName(id));
                view.OnClick += Select;
                _buttons[i] = view;
            }
        }
        
        public virtual void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);

        public void UpdateBtns()
        {
            foreach (var button in _buttons) 
                button.SetData(button.ID, GetName(button.ID));
        }
        
        protected abstract IReadOnlyList<TId> GetIds();

        protected abstract string GetName(TId id);

        protected abstract void Select(TId id);
    }
}