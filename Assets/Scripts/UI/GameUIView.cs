using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIView : MonoBehaviour
{
    public List<InstructionSciprtableObject> instructionsSO;
    [SerializeField] private float instructionDisplayDuration;
    private Coroutine instructionCoroutine;

    [Header("Player Sanity")]
    [SerializeField] GameObject rootViewPanel;
    [SerializeField] Image insanityImage;
    [SerializeField] Image redVignette;

    [Header("Keys UI")]
    [SerializeField] TextMeshProUGUI keysFoundText;

    [Header("Game End Panel")]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TextMeshProUGUI gameEndText;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button quitButton;


    private void OnEnable()
    {
        EventService.Instance.KeyPickedUpEvent.AddListener(OnKeyEquipped);
        EventService.Instance.LightsOffByGhostEvent.AddListener(ShowLightOffInstructions);
        EventService.Instance.LightsOffByGhostEvent.AddListener(SetRedVignette);
        EventService.Instance.PlayerEscapedEvent.AddListener(OnPlayerEscaped);
        EventService.Instance.PlayerDeathEvent.AddListener(SetRedVignette);
        EventService.Instance.PlayerDeathEvent.AddListener(OnPlayerDeath);
        EventService.Instance.RatRushEvent.AddListener(SetRedVignette);
        EventService.Instance.SkullDropEvent.AddListener(SetRedVignette);

        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        EventService.Instance.KeyPickedUpEvent.AddListener(OnKeyEquipped);
        EventService.Instance.LightsOffByGhostEvent.RemoveListener(ShowLightOffInstructions);
        EventService.Instance.LightsOffByGhostEvent.RemoveListener(SetRedVignette);
        EventService.Instance.PlayerEscapedEvent.RemoveListener(OnPlayerEscaped);
        EventService.Instance.PlayerDeathEvent.RemoveListener(SetRedVignette);
        EventService.Instance.PlayerDeathEvent.RemoveListener(OnPlayerDeath);
        EventService.Instance.RatRushEvent.RemoveListener(SetRedVignette);
        EventService.Instance.SkullDropEvent.RemoveListener(SetRedVignette);
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

        GameService.Instance.GetInstructionView().ShowInstruction((InstructionType.LightsOff));
    }

    private void SetRedVignette()
    {
        redVignette.enabled = true;
        redVignette.canvasRenderer.SetAlpha(0.5f);
        redVignette.CrossFadeAlpha(0, 5, false);
    }

    private void OnPlayerDeath()
    {
        gameEndPanel.SetActive(true);
        gameEndText.SetText("Game Over");
    }

    private void OnPlayerEscaped()
    {
        gameEndPanel.SetActive(true);
        gameEndText.SetText("You Escaped");
    }

    private void OnQuitButtonClicked() => Application.Quit();

    private void OnTryAgainButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

}

