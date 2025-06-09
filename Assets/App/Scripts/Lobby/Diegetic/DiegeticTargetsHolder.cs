using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Lobby.Diegetic
{
    public class DiegeticTargetsHolder : MonoBehaviour
    {
        [SerializeField] private DiegeticTarget defaultDiegeticTarget;
        [SerializeField] private List<DiegeticTarget> tables;

        private DiegeticTarget _diegeticTarget;

        private void Start()
        {
            SetTable(defaultDiegeticTarget);
        }

        private void NextTable()
        {
            if (_diegeticTarget == null)
                throw new NullReferenceException($"Table is null");

            if (_diegeticTarget.TryGetNextTable(out var table)) 
                SetTable(table);
            else
                Debug.Log($"Current table doesnt have next table");
        }
        
        private void PrevTable()
        {
            if (_diegeticTarget == null)
                throw new NullReferenceException($"Table is null");

            if (_diegeticTarget.TryGetPrevTable(out var table)) 
                SetTable(table);
            else
                Debug.Log($"Current table doesnt have prev table");
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) 
                PrevTable();
            if (Input.GetKeyDown(KeyCode.E))
                NextTable();
        }
        
        public void SetTable(int index)
        {
            var table = tables[index];
            SetTable(table);
        }

        private void SetTable(DiegeticTarget diegeticTarget)
        {
            foreach (var t in tables) 
                t.Deactivate();
            
            _diegeticTarget = diegeticTarget;
            _diegeticTarget.Activate();
        }
    }
}