using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//TODO - UIManager -> All Naming Should Be Service -> Not Managers , We need to Reduce LOC of UIService to Less than 100
// Todo -> We will not use generic singleton, as scope is very less, we directly take the serialized refference in the respected script

// Todo->This UI manager will become GameUI which will only contain UI of Sanity and Keys Holder i.e. Root UI objects
public class UIManager : GenericMonoSingleton<UIManager>
{
    [Header("Blackout Screen")]
    [SerializeField]
    private Image blackOutScreen;
    [SerializeField]
    private float blackoutFadeDuration;
    private Coroutine blackoutCoroutine;

    // Todo -> Move Instructions stuff into seperate InsturctionsView Mono
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

    // TODO -> Single UI Panel , Only Text will get updated on event callback 
    [Header("Game Over Panel")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button quitButton;

    [Header("Game Won Panel")]
    [SerializeField] GameObject gameWonPanel;
    [SerializeField] Button tryAgainButton2;
    [SerializeField] Button quitButton2;


    private void OnEnable()
    {
        // TODO -> Make Every Event in EventService , Remove EventManager
        EventService.Instance.KeyPickedUpEvent.AddListener(OnKeyEquipped);

        EventManager.Instance.OnLightsOffByGhost += ShowLightOffInstructions;
        EventManager.Instance.OnLightsOffByGhost += SetRedVignette;
        EventManager.OnRatRush += SetRedVignette;
        EventManager.OnSkullDrop += SetRedVignette;
        EventManager.OnPlayerEscaped += OnPlayerEscaped;
        EventManager.OnPlayerDeath += SetRedVignette;
        EventManager.OnPlayerDeath += OnPlayerDeath;

        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        tryAgainButton2.onClick.AddListener(OnTryAgainButtonClicked); //Todo -> These should not be two buttons if funcationality is same
        quitButton2.onClick.AddListener(OnQuitButtonClicked);          // There will be single one GameEnd Panel
    }

    private void OnDisable()
    {

        EventService.Instance.KeyPickedUpEvent.AddListener(OnKeyEquipped);

        EventManager.Instance.OnLightsOffByGhost -= ShowLightOffInstructions;
        EventManager.Instance.OnLightsOffByGhost -= SetRedVignette;
        EventManager.OnRatRush -= SetRedVignette;
        EventManager.OnSkullDrop -= SetRedVignette;
        EventManager.OnPlayerEscaped -= OnPlayerEscaped;
        EventManager.OnPlayerDeath -= SetRedVignette;
        EventManager.OnPlayerDeath -= OnPlayerDeath;
    }

    private void Start()
    {
        if (blackoutCoroutine != null)
            StopCoroutine(blackoutCoroutine);

        blackoutCoroutine = StartCoroutine(ToggleBlackoutScreen(false, () =>
        {
            if (instructionCoroutine != null)
                StopCoroutine(instructionCoroutine);

            instructionCoroutine = StartCoroutine(SetInstructions(Instruction.InstructionType.PlayerSpawned));
        }));
    }

    private IEnumerator ToggleBlackoutScreen(bool setActive, Action callback = null)
    {
        blackOutScreen.CrossFadeAlpha(setActive ? 1f : 0f, blackoutFadeDuration, true);
        yield return new WaitForSeconds(blackoutFadeDuration);
        callback?.Invoke();
    }

    // All the instructions code should move to IntructionsView and there should be Scriptable Object
    private IEnumerator SetInstructions(Instruction.InstructionType type, bool oneShot = true)
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
        Debug.Log("UI Manager - OnKeyEquipped");
        keysFoundText.SetText($"Keys Found: {keys}/3");
    }

    private void ShowLightOffInstructions()
    {
        if (instructionCoroutine != null)
            StopCoroutine(instructionCoroutine);

        instructionCoroutine = StartCoroutine(SetInstructions(Instruction.InstructionType.LightsOff));
    }

    public void ShowInteractInstructions(bool shouldShow)
    {
        if (shouldShow)
        {
            if (instructionCoroutine != null)
                StopCoroutine(instructionCoroutine);

            instructionCoroutine = StartCoroutine(SetInstructions(Instruction.InstructionType.Interact, false));

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
        StartCoroutine(ToggleGameOverPanel()); //Todo -> Incorrect Way to Call Courotuine, there should be a null check 
                                               // and reference to the courotuine
    }

    private IEnumerator ToggleGameOverPanel()
    {
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
    }

    private void OnQuitButtonClicked() => Application.Quit();

    private void OnTryAgainButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void OnPlayerEscaped() => gameWonPanel.SetActive(true);

}

//refactor
/*[Serializable]
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
}*/
