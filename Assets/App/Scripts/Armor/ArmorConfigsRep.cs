using App.Utils.ConfigsRepositories;
using UnityEngine;

namespace App.Armor
{
    [CreateAssetMenu(fileName = nameof(ArmorConfigsRep), menuName = Consts.AppName + "/Configs/Armor/" + nameof(ArmorConfigsRep))]
    public class ArmorConfigsRep : ConfigsRepository<ArmorConfig>
    {
        public int MaxArmorLevel => Configs.Capacity;
        
        public ArmorConfig GetArmor(int armorLevel)
        {
            if (armorLevel < 0)
            {
                Debug.LogError($"Armor level less then 0: [{armorLevel}]");
                armorLevel = 0;
            }

            if (armorLevel >= Configs.Count)
            {
                Debug.LogError($"Armor level equal or higher then max armor level: [{armorLevel}] [{Configs.Count}]");
                armorLevel = Configs.Count - 1;
            }
            
            return Configs[armorLevel];
        }

        public Sprite GetIcon(int armorLevel) 
            => GetArmor(armorLevel).Icon;
    }
}