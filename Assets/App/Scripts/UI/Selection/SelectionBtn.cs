using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Selection
{
    [RequireComponent(typeof(Button))]
    public class SelectionBtn<TId> : MonoBehaviour
    {
        [SerializeField] private Image selectMarker;
        [SerializeField] private TMP_Text tmpText;
        
        public TId Id { get; private set; }

        private Button _button;
        
        public event Action<TId> OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClick?.Invoke(Id));
        }

        public void SelectState(bool isSelect)
        {
            if (selectMarker != null)
                selectMarker.gameObject.SetActive(isSelect);
        }

        public void SetData(TId id, string textField)
        {
            Id = id;

            if (tmpText != null)
                tmpText.text = textField;
        }
    }
}