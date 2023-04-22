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


    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("UI Manager instance already exists");
        }
    }

    private void OnEnable()
    {
        EventManager.OnKeyPickedUp += OnKeyEquipped;
        EventManager.OnLightsOffByGhost += ShowLightOffInstructions;
        EventManager.OnLightsOffByGhost += SetRedVignette;
        EventManager.OnRatRush += SetRedVignette;
        EventManager.OnSkullDrop += SetRedVignette;
        EventManager.OnPlayerEscaped += OnPlayerEscaped;
        EventManager.OnPlayerDeath += SetRedVignette;
        EventManager.OnPlayerDeath += OnPlayerDeath;

        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        tryAgainButton2.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton2.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        EventManager.OnKeyPickedUp -= OnKeyEquipped;
        EventManager.OnLightsOffByGhost -= ShowLightOffInstructions;
        EventManager.OnLightsOffByGhost -= SetRedVignette;
        EventManager.OnRatRush -= SetRedVignette;
        EventManager.OnSkullDrop -= SetRedVignette;
        EventManager.OnPlayerEscaped -= OnPlayerEscaped;
        EventManager.OnPlayerDeath -= SetRedVignette;
        EventManager.OnPlayerDeath -= OnPlayerDeath;
        // OnPlayerNearInteractable -= ShowInteractInstructions;
        // OnPlayerNotNearInteractable -= StopShowingInstructions;
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
        blackOutScreen.CrossFadeAlpha(setActive ? 1f : 0f, blackoutFadeDuration, true);
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

    private void OnKeyEquipped(int keys)
    {
        keysFoundText.SetText($"Keys Found: {keys}/3");
    }

    private void ShowLightOffInstructions()
    {
        if (instructionCoroutine != null)
            StopCoroutine(instructionCoroutine);

        instructionCoroutine = StartCoroutine(SetInstructions(InstructionType.LightsOff));
    }

    public void ShowInteractInstructions(bool shouldShow)
    {
        if (shouldShow)
        {
            if (instructionCoroutine != null)
                StopCoroutine(instructionCoroutine);

            instructionCoroutine = StartCoroutine(SetInstructions(InstructionType.Interact, false));

        }
        else
        {
            if (instructionCoroutine != null)
                StopCoroutine(instructionCoroutine);

            instructionsText.SetText(string.Empty);
            instructionPopup.SetActive(false);
        }
    }

    private void SetRedVignette()
    {
        redVignette.enabled = true;
        redVignette.canvasRenderer.SetAlpha(0.5f);
        redVignette.CrossFadeAlpha(0, 5, false);
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
