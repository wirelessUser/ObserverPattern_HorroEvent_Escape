using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstructionView : MonoBehaviour
{
    [SerializeField] InstructionSciprtableObject playerSpawnedInstruction;
    [SerializeField] InstructionSciprtableObject interactionInstruction;
    [SerializeField] InstructionSciprtableObject lightOffByGhostInstruction;


    [Header("Instruction Popup")]
    [SerializeField]
    private GameObject instructionPopup;
    [SerializeField]
    private TextMeshProUGUI instructionsText;


    private Coroutine instructionCoroutine;

    void Start()
    {
        ShowInstruction(playerSpawnedInstruction);
    }

    public void ShowInstruction(InstructionSciprtableObject instruction)
    {
        stopCoroutine(instructionCoroutine);
        instructionCoroutine = StartCoroutine(SetInstructions(instruction));
    }
    public void ShowInstruction(InstructionType type)
    {
        stopCoroutine(instructionCoroutine);
        switch (type)
        {
            case InstructionType.PlayerSpawned:
                instructionCoroutine = StartCoroutine(SetInstructions(playerSpawnedInstruction));
                break;
            case InstructionType.Interact:
                instructionCoroutine = StartCoroutine(SetInstructions(interactionInstruction));
                break;
            case InstructionType.LightsOff:
                instructionCoroutine = StartCoroutine(SetInstructions(lightOffByGhostInstruction));
                break;
        }
    }

    public void HideInstruction()
    {
        HideInstructionPopup();
    }

    private IEnumerator SetInstructions(InstructionSciprtableObject instruction)
    {
        yield return new WaitForSeconds(instruction.waitToTriggerDuration);
        ShowInstructionPopup(instruction);

        yield return new WaitForSeconds(instruction.displayDuration);
        HideInstructionPopup();
    }

    private void HideInstructionPopup()
    {
        instructionsText.SetText(string.Empty);
        instructionPopup.SetActive(false);
        stopCoroutine(instructionCoroutine);
    }

    private void ShowInstructionPopup(InstructionSciprtableObject instruction)
    {
        instructionsText.SetText(instruction.instruction);
        instructionPopup.SetActive(true);
    }

    private void stopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
