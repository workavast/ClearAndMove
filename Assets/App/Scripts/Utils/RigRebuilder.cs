using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace App.Utils
{
    [RequireComponent(typeof(RigBuilder))]
    public class RigRebuilder : MonoBehaviour
    {
        [ContextMenu("Rebuild Rig")]
        public void Rebuild()
        {
            GetComponent<RigBuilder>().Build();
        }
    }
}