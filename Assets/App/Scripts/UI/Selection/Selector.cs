using System;
using System.Collections.Generic;
using App.UI.WindowsSwitching;
using UnityEngine;

namespace App.UI.Selection
{
    public abstract class Selector<TId, TButton> : MonoBehaviour, IWindow
        where TButton : SelectionBtn<TId>
    {
        [field: SerializeField] public string Id { get; private set; }
        [SerializeField] private TButton weaponSelectBtnPrefab;
        [SerializeField] private Transform holder;

        private TButton[] _buttons;
        private SelectionBtn<TId> _lastSelectedBtn;
        
        private void Awake() 
            => Initialize();

        protected virtual void Initialize()
        {
            var allIds = GetIds();
            _buttons = new TButton[allIds.Count];

            var activeId = GetCurrentActiveId();
            for (int i = 0; i < allIds.Count; i++)
            {
                var id = allIds[i];
                
                var button = Instantiate(weaponSelectBtnPrefab, holder);
                UpdateBtn(id, button);
                button.OnClick += Select;
                _buttons[i] = button;

                if (Is(activeId, button.Id)) 
                    SetSelectedBtn(activeId);
                else
                    button.SelectState(false);
            }
        }
        
        public virtual void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);

        public void UpdateBtns()
        {
            foreach (var button in _buttons)
                UpdateBtn(button.Id, button);
        }

        protected virtual void UpdateBtn(TId id, TButton button)
        {
            button.SetData(id, GetName(button.Id));
        }
        
        protected abstract IReadOnlyList<TId> GetIds();

        protected abstract string GetName(TId id);

        protected abstract bool Is(TId a, TId b);

        protected abstract TId GetCurrentActiveId();
        
        protected abstract void OnSelect(TId id);
        
        private void Select(TId id)
        {
            SetSelectedBtn(id);
            OnSelect(id);
        }

        private void SetSelectedBtn(TId id)
        {
            if (_lastSelectedBtn != null)
            {
                if (Is(_lastSelectedBtn.Id, id))
                    return;
                
                _lastSelectedBtn.SelectState(false);
            }

            _lastSelectedBtn = GetButton(id);
            _lastSelectedBtn.SelectState(true);
        }

        private TButton GetButton(TId id)
        {
            foreach (var button in _buttons)
                if (Is(button.Id, id))
                    return button;

            throw new NullReferenceException($"Cant find button with id: [{id}]");
        }
    }
}