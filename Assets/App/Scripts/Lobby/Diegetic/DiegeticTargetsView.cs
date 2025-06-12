using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace App.Lobby.Diegetic
{
    public class DiegeticTargetsView : MonoBehaviour
    {
        [SerializeField] private TipView prevTargetTip;
        [SerializeField] private TipView nextTargetTip;
        [SerializeField] private DiegeticTargetsHolder diegeticTargetsHolder;

        private void Awake()
        {
            prevTargetTip.OnClick += PrevTarget;
            nextTargetTip.OnClick += NextTarget;
        }

        private void OnEnable()
        {
            prevTargetTip.Initialize();
            nextTargetTip.Initialize();
            
            diegeticTargetsHolder.OnTargetChanged += UpdateView;
            LocalizationSettings.SelectedLocaleChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            prevTargetTip.Dispose();
            nextTargetTip.Dispose();
            
            diegeticTargetsHolder.OnTargetChanged -= UpdateView;
            LocalizationSettings.SelectedLocaleChanged -= UpdateView;
        }
        
        private void UpdateView(Locale _) 
            => UpdateView();

        private void UpdateView()
        {
            if (!diegeticTargetsHolder.HasTarget)
                return;

            prevTargetTip.SetTipText(diegeticTargetsHolder.GetPrevTargetLocalizedString());
            nextTargetTip.SetTipText(diegeticTargetsHolder.GetNextTargetLocalizedString());
        }

        private void NextTarget() 
            => diegeticTargetsHolder.NextTarget();

        private void PrevTarget() 
            => diegeticTargetsHolder.PrevTarget();

        [Serializable]
        private struct TipView : IDisposable
        {
            [SerializeField] private GameObject tipHolder;
            [SerializeField] private TMP_Text tipTxt;
            [SerializeField] private Button tipBtn;

            public event Action OnClick;
            
            public void Initialize() 
                => tipBtn.onClick.AddListener(Click);

            public void SetTipText(string tip)
            {
                tipTxt.text = tip;
                SetVisibilityState(!string.IsNullOrEmpty(tip));
            }
            
            public void Dispose() 
                => tipBtn.onClick.RemoveListener(Click);
            
            private void SetVisibilityState(bool isVisible) 
                => tipHolder.SetActive(isVisible);

            private void Click() 
                => OnClick?.Invoke();
        }
    }
}