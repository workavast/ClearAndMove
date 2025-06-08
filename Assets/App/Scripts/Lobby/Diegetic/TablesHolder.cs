using System.Collections.Generic;
using App.Tools.Attributes.Button;
using UnityEngine;

namespace App.Lobby.Diegetic
{
    public class TablesHolder : MonoBehaviour
    {
        [SerializeField] private int tableIndex;
        [SerializeField] private DiegeticCamera diegeticCamera;
        [SerializeField] private List<Table> tables;

        [Button]
        public void SetTable()
        {
            SetTable(tableIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                SetTable(0);
            }
            if (Input.GetKeyDown("2"))
            {
                SetTable(1);
            }
            if (Input.GetKeyDown("3"))
            {
                SetTable(2);
            }
        }
        
        [Button]
        public void SetTable(int index)
        {
            var table = tables[index];

            foreach (var t in tables) 
                t.Deactivate();

            table.Activate();
            
            // diegeticCamera.SetData(table.lookPoint, table.cameraPosition);
        }
    }
}