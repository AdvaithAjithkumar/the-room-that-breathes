using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public RoomShrink roomShrink;
    public AudioEscalation audioEscalation;
    public GameObject door;
    public Light roomLight;

    [Header("Props")]
    public GameObject lampMesh;

    [Header("Timer Settings")]
    public float gameDuration = 600f;

    private float timer = 0f;
    private bool gameOver = false;
    private bool puzzleSolved = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TitleCardManager.Instance.ShowOpeningCard();
    }

    void Update()
    {
        if (gameOver) return;

        timer += Time.deltaTime;

        if (timer >= gameDuration && !puzzleSolved)
        {
            TriggerBadEnding();
        }
    }

    public void PuzzleSolved()
    {
        puzzleSolved = true;
        TriggerGoodEnding();
    }

    void TriggerGoodEnding()
    {
        gameOver = true;
        roomShrink.StopShrinking();
        audioEscalation.StopAudio();
        StartCoroutine(GoodEndingSequence());
    }

    void TriggerBadEnding()
    {
        gameOver = true;
        StartCoroutine(BadEndingSequence());
    }

    System.Collections.IEnumerator GoodEndingSequence()
    {
        // Flash white
        if (roomLight != null)
        {
            roomLight.intensity = 10f;
            roomLight.color = Color.white;
        }

        // Hold brightness for 3 seconds
        yield return new WaitForSeconds(3f);

        // Hide everything
        if (door != null)
            door.SetActive(false);

        PuzzleManager.Instance.HideAllSymbols();
        FindFirstObjectByType<KeypadManager>().HideDisplay();

        foreach (Transform child in roomShrink.transform)
        {
            child.gameObject.SetActive(false);
        }

        if (lampMesh != null)
            lampMesh.SetActive(false);

        // Hold for another 2 seconds
        yield return new WaitForSeconds(2f);

        // Very slowly fade to pitch black over 6 seconds
        float elapsed = 0f;
        float fadeDuration = 6f;
        float startIntensity = 10f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            float smoothT = t * t * (3f - 2f * t);
            if (roomLight != null)
                roomLight.intensity = Mathf.Lerp(startIntensity, 0f, smoothT);
            yield return null;
        }

        if (roomLight != null)
            roomLight.intensity = 0f;

        TitleCardManager.Instance.ShowGoodEndingCard();
    }

    System.Collections.IEnumerator BadEndingSequence()
    {
        audioEscalation.StopAudio();

        // Hide keypad display
        FindFirstObjectByType<KeypadManager>().HideDisplay();

        // Hide all room objects
        foreach (Transform child in roomShrink.transform)
        {
            child.gameObject.SetActive(false);
        }

        if (lampMesh != null)
            lampMesh.SetActive(false);

        // Kill the light slowly over 5 seconds
        float elapsed = 0f;
        float fadeDuration = 5f;
        float startIntensity = roomLight != null ? roomLight.intensity : 1f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            float smoothT = t * t * (3f - 2f * t);
            if (roomLight != null)
                roomLight.intensity = Mathf.Lerp(startIntensity, 0f, smoothT);
            yield return null;
        }

        if (roomLight != null)
            roomLight.intensity = 0f;

        TitleCardManager.Instance.ShowBadEndingCard();
    }
}