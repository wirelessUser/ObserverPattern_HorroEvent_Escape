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
    private Image m_BlackOutScreen;
    [SerializeField]
    private float m_BlackoutFadeDuration;
    private Coroutine m_BlackoutCoroutine;

    [Header("Instruction Popup")]
    [SerializeField]
    private GameObject m_InstructionPopup;
    [SerializeField]
    private TextMeshProUGUI m_InstructionsText;
    [SerializeField]
    private List<Instruction> m_Instructions;
    [SerializeField]
    private float m_InstructionDisplayDuration;
    private Coroutine m_InstructionCoroutine;

    [Header("Player Sanity")]
    [SerializeField] GameObject m_RootViewPanel;
    [SerializeField] Image m_InsanityImage;
    [SerializeField] Image m_RedVignette;

    [Header("Keys UI")]
    [SerializeField] TextMeshProUGUI m_KeysFoundText;

    [Header("Game Over Panel")]
    [SerializeField] GameObject m_GameOverPanel;
    [SerializeField] Button m_TryAgainButton;
    [SerializeField] Button m_QuitButton;

    [Header("Game Won Panel")]
    [SerializeField] GameObject m_GameWonPanel;
    [SerializeField] Button m_TryAgainButton2;
    [SerializeField] Button m_QuitButton2;

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

        m_TryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        m_QuitButton.onClick.AddListener(OnQuitButtonClicked);
        m_TryAgainButton2.onClick.AddListener(OnTryAgainButtonClicked);
        m_QuitButton2.onClick.AddListener(OnQuitButtonClicked);
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
        if (m_BlackoutCoroutine != null)
            StopCoroutine(m_BlackoutCoroutine);

        m_BlackoutCoroutine = StartCoroutine(ToggleBlackoutScreen(false, () =>
        {
            if (m_InstructionCoroutine != null)
                StopCoroutine(m_InstructionCoroutine);

            m_InstructionCoroutine = StartCoroutine(SetInstructions(InstructionType.PlayerSpawned));
        }));
    }

    private IEnumerator ToggleBlackoutScreen(bool setActive, Action callback = null)
    {
        m_BlackOutScreen.CrossFadeAlpha(setActive?1f:0f, m_BlackoutFadeDuration, true);
        yield return new WaitForSeconds(m_BlackoutFadeDuration);
        callback?.Invoke();
    }
    
    private IEnumerator SetInstructions(InstructionType type, bool oneShot = true)
    {
        string instructionToSet = "";
        foreach (Instruction instruction in m_Instructions)
        {
            if (instruction.instructionType == type && !instruction.displayed)
            {
                instructionToSet = instruction.instruction;
                instruction.displayed = oneShot;
                break;
            }
        }

        m_InstructionPopup.SetActive(true);
        m_InstructionsText.SetText(instructionToSet);

        yield return new WaitForSeconds(m_InstructionDisplayDuration);

        m_InstructionsText.SetText(string.Empty);
        m_InstructionPopup.SetActive(false);
    }

    public void UpdateInsanity(float playerSanity)
    {
        m_InsanityImage.rectTransform.localScale = new Vector3(1, playerSanity, 1);
    }

    private void OnKeyEquipped()
    {
        m_KeysFoundText.SetText($"Keys Found: {PlayerController.KeysEquipped}/3");
    }

    private void ShowLightOffInstructions()
    {
        if (m_InstructionCoroutine != null)
            StopCoroutine(m_InstructionCoroutine);

        m_InstructionCoroutine = StartCoroutine(SetInstructions(InstructionType.LightsOff));
    }

    private void ShowInteractInstructions()
    {
        if (m_InstructionCoroutine != null)
            StopCoroutine(m_InstructionCoroutine);

        m_InstructionCoroutine = StartCoroutine(SetInstructions(InstructionType.Interact, false));
    }

    private void StopShowingInstructions()
    {
        if (m_InstructionCoroutine != null)
            StopCoroutine(m_InstructionCoroutine);

        m_InstructionsText.SetText(string.Empty);
        m_InstructionPopup.SetActive(false);
    }

    private void SetRedVignette()
    {
        m_RedVignette.enabled = true;
        m_RedVignette.canvasRenderer.SetAlpha(0.5f);
        m_RedVignette.CrossFadeAlpha(0,5,false);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(ToggleGameOverPanel());
    }

    private IEnumerator ToggleGameOverPanel()
    {
        yield return new WaitForSeconds(2f);
        m_GameOverPanel.SetActive(true);
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
        m_GameWonPanel.SetActive(true);
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
