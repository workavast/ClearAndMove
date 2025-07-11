using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Lobby.PlayersView
{
    public class PinMaterialChanger : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material[] materials;
        
        private void Start()
        {
            var activeMaterials = meshRenderer.materials;
            activeMaterials[0] = GetRandomMaterial();
            meshRenderer.materials = activeMaterials;
        }

        private Material GetRandomMaterial()
        {
            var materialIndex = Random.Range(0, materials.Length);
            return materials[materialIndex];
        }
    }
}