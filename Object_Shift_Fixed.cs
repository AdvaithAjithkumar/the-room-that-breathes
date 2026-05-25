using UnityEngine;

public class ObjectShiftFixed : MonoBehaviour
{
    [Header("Shift Settings")]
    public Transform playerCamera;
    public float fovThreshold = 60f;
    public float shiftInterval = 3f;
    public bool useFixedShift = false;
    public Vector3 fixedShiftPosition;
    public Vector3 fixedShiftRotation;
    public float returnDelay = 10f;
    public float minZPosition = 2.7f; // minimum Z to prevent wall clipping

    private float timer = 0f;
    private float shiftedTimer = 0f;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isShifted = false;

    void Start()
{
    // Wait one frame before storing position to ensure all transforms are initialized
    StartCoroutine(InitializePosition());
}

System.Collections.IEnumerator InitializePosition()
{
    yield return null; // wait one frame
    originalPosition = transform.position;
    originalRotation = transform.rotation;
    Debug.Log("Original position stored: " + originalPosition);
}

    void Update()
    {
        timer += Time.deltaTime;
        if (isShifted)
        {
            if (!IsInFOV())
            {
                shiftedTimer += Time.deltaTime;
                if (shiftedTimer >= returnDelay)
                {
                    ReturnToOriginal();
                }
            }
            else
            {
                shiftedTimer = 0f;
            }
        }
        if (timer >= shiftInterval)
        {
            timer = 0f;
            if (!IsInFOV())
            {
                Shift();
            }
        }
        // Force all renderers visible
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }
    }

    bool IsInFOV()
    {
        if (playerCamera == null) return true;
        Vector3 dirToObject = (transform.position - playerCamera.position).normalized;
        float angle = Vector3.Angle(playerCamera.forward, dirToObject);
        return angle < fovThreshold;
    }

    void Shift()
    {
        if (useFixedShift)
        {
            if (!isShifted)
            {
                transform.position = originalPosition + fixedShiftPosition;
                transform.rotation = originalRotation * Quaternion.Euler(fixedShiftRotation);
                isShifted = true;
                shiftedTimer = 0f;
            }
        }
        else
        {
            float randomY = Random.Range(-0.02f, 0.02f);
            transform.position = new Vector3(
                originalPosition.x,
                originalPosition.y + randomY,
                originalPosition.z
            );
        }
    }

    void ReturnToOriginal()
    {
    transform.position = originalPosition;
    transform.rotation = originalRotation;
    isShifted = false;
    shiftedTimer = 0f;
    timer = 0f;
    Debug.Log("Returned to original position: " + transform.position);
    }

    public void ResetPosition()
    {
        ReturnToOriginal();
    }
}