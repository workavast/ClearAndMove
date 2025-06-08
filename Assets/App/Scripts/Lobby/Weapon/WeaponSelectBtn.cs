using App.UI.Selection;
using App.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.Weapon
{
    public class WeaponSelectBtn : SelectionBtn<WeaponId>
    {
        [SerializeField] private Image icon;

        public void SetIcon(Sprite newIcon)
        {
            if (icon != null)
                icon.sprite = newIcon;
        }
    }
}