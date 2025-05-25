using UnityEngine;
using UnityEngine.UI;

namespace App.UI.LayoutSpawners
{
    [ExecuteAlways]
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class HorizontalSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float objectsPerMeter = 1f;

        private void Start()
        {
            if (Application.isPlaying) 
                SpawnObjects();
        }

        public void SpawnObjects()
        {
            if (prefab == null) return;

            RectTransform rectTransform = GetComponent<RectTransform>();

            ClearChildren();

            float widthInMeters = rectTransform.rect.width;
            int count = Mathf.FloorToInt(widthInMeters * objectsPerMeter);

            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.name = prefab.name + "_" + i;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(obj);
#endif
            }
        }

        public void ClearChildren()
        {
            // Используем DestroyImmediate только в редакторе вне OnValidate
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    UnityEditor.Undo.DestroyObjectImmediate(child.gameObject);
                else
                    Destroy(child.gameObject);
#else
            Destroy(child.gameObject);
#endif
            }
        }
    }
}