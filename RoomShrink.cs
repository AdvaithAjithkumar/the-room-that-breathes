using UnityEngine;

public class RoomShrink : MonoBehaviour
{
    [Header("Shrink Settings")]
    public float shrinkDuration = 600f; // 10 minutes total
    public float minScale = 0.4f;       // how small the room gets

    private float elapsed = 0f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (elapsed >= shrinkDuration) return;

        elapsed += Time.deltaTime;
        float t = elapsed / shrinkDuration;

        float currentScale = Mathf.Lerp(1f, minScale, t);
        transform.localScale = new Vector3(
            originalScale.x * currentScale,
            originalScale.y,
            originalScale.z * currentScale
        );
    }

    public void StopShrinking()
    {
        elapsed = shrinkDuration;
    }
}
