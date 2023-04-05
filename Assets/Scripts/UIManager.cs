using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Image m_InsanityImage;

    private void Start()
    {
        if (m_BlackoutCoroutine != null)
            StopCoroutine(m_BlackoutCoroutine);

        m_BlackoutCoroutine = StartCoroutine(ToggleBlackoutScreen(false, () =>
        {
            if (m_InstructionCoroutine != null)
                StopCoroutine(m_InstructionCoroutine);

            m_InstructionCoroutine = StartCoroutine(SetInstructions(m_Instructions.Find(x => x.instructionType == InstructionType.PlayerSpawned).instruction));
        }));
    }

    private IEnumerator ToggleBlackoutScreen(bool setActive, Action callback = null)
    {
        m_BlackOutScreen.CrossFadeAlpha(setActive?1f:0f, m_BlackoutFadeDuration, true);
        yield return new WaitForSeconds(m_BlackoutFadeDuration);
        callback?.Invoke();
    }

    private IEnumerator SetInstructions(string instructionToSet)
    {
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

}

[Serializable]
public struct Instruction
{
    public InstructionType instructionType;
    public string instruction;
}

[Serializable]
public enum InstructionType
{
    PlayerSpawned,
    LightsOff,
    OpenDoor
}
