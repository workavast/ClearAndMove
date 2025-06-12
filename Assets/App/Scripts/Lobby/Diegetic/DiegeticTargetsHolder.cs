using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Lobby.Diegetic
{
    public class DiegeticTargetsHolder : MonoBehaviour
    {
        [SerializeField] private DiegeticTarget defaultDiegeticTarget;
        [SerializeField] private List<DiegeticTarget> tables;

        public bool HasTarget => _diegeticTarget != null;
        
        private DiegeticTarget _diegeticTarget;

        public event Action OnTargetChanged;
            
        private void Start()
        {
            SetTarget(defaultDiegeticTarget);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) 
                PrevTarget();
            if (Input.GetKeyDown(KeyCode.E))
                NextTarget();
        }
        
        public void SetTarget(int index)
        {
            var table = tables[index];
            SetTarget(table);
        }

        public string GetPrevTargetLocalizedString()
        {
            if (_diegeticTarget.TryGetPrevTarget(out var target))
                return target.GetLocalizedName();

            return string.Empty;
        }
        
        public string GetNextTargetLocalizedString()
        {
            if (_diegeticTarget.TryGetNextTarget(out var target))
                return target.GetLocalizedName();

            return string.Empty;
        }
        
        public void NextTarget()
        {
            if (_diegeticTarget == null)
                throw new NullReferenceException($"Table is null");

            if (_diegeticTarget.TryGetNextTarget(out var table)) 
                SetTarget(table);
            else
                Debug.Log($"Current table doesnt have next table");
        }
        
        public void PrevTarget()
        {
            if (_diegeticTarget == null)
                throw new NullReferenceException($"Table is null");

            if (_diegeticTarget.TryGetPrevTarget(out var table)) 
                SetTarget(table);
            else
                Debug.Log($"Current table doesnt have prev table");
        }

        private void SetTarget(DiegeticTarget diegeticTarget)
        {
            foreach (var t in tables) 
                t.Deactivate();
            
            _diegeticTarget = diegeticTarget;
            _diegeticTarget.Activate();
            OnTargetChanged?.Invoke();
        }
    }
}