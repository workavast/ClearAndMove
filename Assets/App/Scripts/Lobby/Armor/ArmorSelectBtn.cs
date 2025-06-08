using App.UI.Selection;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.Armor
{
    public class ArmorSelectBtn : SelectionBtn<int>
    {
        [SerializeField] private Image icon;

        public void SetIcon(Sprite newIcon)
        {
            if (icon != null)
                icon.sprite = newIcon;
        }
    }
}