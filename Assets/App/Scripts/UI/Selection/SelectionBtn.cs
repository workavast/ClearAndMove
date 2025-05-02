using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Selection
{
    public class SelectionBtn<TId> : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        
        public TId ID { get; private set; }

        private Button _button;
        
        public event Action<TId> OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClick?.Invoke(ID));
        }

        public void SetData(TId id, string textField)
        {
            ID = id;
            tmpText.text = textField;
        }
    }
}