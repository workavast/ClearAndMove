using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(BoxCollider))]
public class RectToBoxCollider : MonoBehaviour
{
    private RectTransform rectTransform;
    private BoxCollider boxCollider;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void UpdateColliderSize()
    {
        if (!rectTransform) rectTransform = GetComponent<RectTransform>();
        if (!boxCollider) boxCollider = GetComponent<BoxCollider>();

        Vector2 size = rectTransform.rect.size;

        float width = size.x * rectTransform.lossyScale.x;
        float height = size.y * rectTransform.lossyScale.y;

        boxCollider.size = new Vector3(width, height, boxCollider.size.z);
        boxCollider.center = new Vector3(0,0, boxCollider.center.z);

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(boxCollider);
#endif
    }

    private void Update()
    {
        if (Application.isPlaying)
            UpdateColliderSize();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateColliderSize();
    }
#endif
}