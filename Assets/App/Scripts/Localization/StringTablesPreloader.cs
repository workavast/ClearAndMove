using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace App.Localization
{
    [Serializable]
    public class StringTablesPreloader
    {
        [SerializeField] private StringTablesConfig config;

        private IReadOnlyList<TableReference> TableReferences => config.TableReferences;

        public async Task Preload()
        {
            var selectedLocale = LocalizationSettings.SelectedLocale;
            var handle = LocalizationSettings.StringDatabase.PreloadTables(TableReferences.ToList(), selectedLocale);
            await handle.Task;
        }

        public void Release()
        {
            foreach (var tableReference in TableReferences)
                LocalizationSettings.AssetDatabase.ReleaseTable(tableReference);
        }
    }
}