using UnityEngine;
using TMPro;

public class KeypadManager : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;

    [Header("Code Settings")]
    private string correctCode = "427";
    private string currentInput = "";

    [Header("Display")]
    public TextMeshPro displayText;

    void Start()
    {
        UpdateDisplay();
    }

    public void PressButton(int digit)
    {
        if (currentInput.Length >= 3) return;

        currentInput += digit.ToString();
        UpdateDisplay();

        if (currentInput.Length == 3)
        {
            Invoke("CheckCode", 0.5f);
        }
    }

    void CheckCode()
    {
        if (currentInput == correctCode)
        {
            UpdateDisplayText("OK");
            Invoke("TriggerSymbols", 1f);
        }
        else
        {
            UpdateDisplayText("ERR");
            Invoke("ResetInput", 1f);
        }
    }

    void ResetInput()
    {
        currentInput = "";
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = currentInput.Length > 0 ? currentInput : "---";
    }

    void UpdateDisplayText(string msg)
    {
        if (displayText != null)
            displayText.text = msg;
    }

    void TriggerSymbols()
{
    PuzzleManager.Instance.ActivateSymbols();
}
public void HideDisplay()
{
    if (displayText != null)
        displayText.gameObject.SetActive(false);
}
}
