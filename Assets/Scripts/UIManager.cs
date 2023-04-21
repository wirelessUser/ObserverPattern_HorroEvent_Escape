using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Blackout Screen")] 
    [SerializeField]
    private Image blackOutScreen;
    [SerializeField]
    private float blackoutFadeDuration;
    private Coroutine blackoutCoroutine;

    [Header("Instruction Popup")]
    [SerializeField]
    private GameObject instructionPopup;
    [SerializeField]
    private TextMeshProUGUI instructionsText;
    [SerializeField]
    private List<Instruction> instructions;
    [SerializeField]
    private float instructionDisplayDuration;
    private Coroutine instructionCoroutine;

    [Header("Player Sanity")]
    [SerializeField] GameObject rootViewPanel;
    [SerializeField] Image insanityImage;
    [SerializeField] Image redVignette;

    [Header("Keys UI")]
    [SerializeField] TextMeshProUGUI keysFoundText;

    [Header("Game Over Panel")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button quitButton;

    [Header("Game Won Panel")]
    [SerializeField] GameObject gameWonPanel;
    [SerializeField] Button tryAgainButton2;
    [SerializeField] Button quitButton2;

    public static Action OnPlayerNearInteractable;
    public static Action OnPlayerNotNearInteractable;

    private void OnEnable()
    {
        PlayerController.OnKeyEquipped += OnKeyEquipped;
        LightsOffEventTrigger.OnLightsOff += ShowLightOffInstructions;
        LightsOffEventTrigger.OnLightsOff += SetRedVignette;
        RatRushEventTrigger.OnRatRush += SetRedVignette;
        SkullDropEventTrigger.OnSkullDrop += SetRedVignette;
        PlayerSanity.OnPlayerDeath += SetRedVignette;
        PlayerSanity.OnPlayerDeath += OnPlayerDeath;
        PlayerEscapedEventTrigger.OnPlayerEscaped += OnPlayerEscaped;
        OnPlayerNearInteractable += ShowInteractInstructions;
        OnPlayerNotNearInteractable += StopShowingInstructions;

        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        tryAgainButton2.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton2.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        PlayerController.OnKeyEquipped -= OnKeyEquipped;
        LightsOffEventTrigger.OnLightsOff -= ShowLightOffInstructions;
        LightsOffEventTrigger.OnLightsOff -= SetRedVignette;
        RatRushEventTrigger.OnRatRush -= SetRedVignette;
        SkullDropEventTrigger.OnSkullDrop -= SetRedVignette;
        PlayerSanity.OnPlayerDeath -= SetRedVignette;
        PlayerSanity.OnPlayerDeath -= OnPlayerDeath;
        PlayerEscapedEventTrigger.OnPlayerEscaped -= OnPlayerEscaped;
        OnPlayerNearInteractable -= ShowInteractInstructions;
        OnPlayerNotNearInteractable -= StopShowingInstructions;
    }

    private void Start()
    {
        if (blackoutCoroutine != null)
            StopCoroutine(blackoutCoroutine);

        blackoutCoroutine = StartCoroutine(ToggleBlackoutScreen(false, () =>
        {
            if (instructionCoroutine != null)
                StopCoroutine(instructionCoroutine);

            instructionCoroutine = StartCoroutine(SetInstructions(InstructionType.PlayerSpawned));
        }));
    }

    private IEnumerator ToggleBlackoutScreen(bool setActive, Action callback = null)
    {
        blackOutScreen.CrossFadeAlpha(setActive?1f:0f, blackoutFadeDuration, true);
        yield return new WaitForSeconds(blackoutFadeDuration);
        callback?.Invoke();
    }
    
    private IEnumerator SetInstructions(InstructionType type, bool oneShot = true)
    {
        string instructionToSet = "";
        foreach (Instruction instruction in instructions)
        {
            if (instruction.instructionType == type && !instruction.displayed)
            {
                instructionToSet = instruction.instruction;
                instruction.displayed = oneShot;
                break;
            }
        }

        instructionPopup.SetActive(true);
        instructionsText.SetText(instructionToSet);

        yield return new WaitForSeconds(instructionDisplayDuration);

        instructionsText.SetText(string.Empty);
        instructionPopup.SetActive(false);
    }

    public void UpdateInsanity(float playerSanity)
    {
        insanityImage.rectTransform.localScale = new Vector3(1, playerSanity, 1);
    }

    private void OnKeyEquipped()
    {
        keysFoundText.SetText($"Keys Found: {PlayerController.KeysEquipped}/3");
    }

    private void ShowLightOffInstructions()
    {
        if (instructionCoroutine != null)
            StopCoroutine(instructionCoroutine);

        instructionCoroutine = StartCoroutine(SetInstructions(InstructionType.LightsOff));
    }

    private void ShowInteractInstructions()
    {
        if (instructionCoroutine != null)
            StopCoroutine(instructionCoroutine);

        instructionCoroutine = StartCoroutine(SetInstructions(InstructionType.Interact, false));
    }

    private void StopShowingInstructions()
    {
        if (instructionCoroutine != null)
            StopCoroutine(instructionCoroutine);

        instructionsText.SetText(string.Empty);
        instructionPopup.SetActive(false);
    }

    private void SetRedVignette()
    {
        redVignette.enabled = true;
        redVignette.canvasRenderer.SetAlpha(0.5f);
        redVignette.CrossFadeAlpha(0,5,false);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(ToggleGameOverPanel());
    }

    private IEnumerator ToggleGameOverPanel()
    {
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnTryAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPlayerEscaped()
    {
        gameWonPanel.SetActive(true);
    }

}

[Serializable]
public class Instruction
{
    public InstructionType instructionType;
    public string instruction;
    public bool displayed;
}

[Serializable]
public enum InstructionType
{
    PlayerSpawned,
    LightsOff,
    Interact,
    OpenDoor
}
