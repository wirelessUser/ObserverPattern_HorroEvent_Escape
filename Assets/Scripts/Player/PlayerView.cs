using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private PlayerController playerController;

    private void OnEnable()
    {
        EventService.Instance.KeyPickedUpEvent.AddListener(OnKeyPickedUp);
        EventService.Instance.PlayerEscapedEvent.AddListener(DisableControls);
    }

    private void OnDisable()
    {
        EventService.Instance.KeyPickedUpEvent.RemoveListener(OnKeyPickedUp);
        EventService.Instance.PlayerEscapedEvent.RemoveListener(DisableControls);
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerController.Move(playerRigidbody, transform);
        playerController.Jump(playerRigidbody, transform);
        playerController.Interact();
    }

    #region Event CallBacks
    private void OnKeyPickedUp(int keys)
    {
        Debug.Log("On Key Picked Up");
        playerController.SetKeys(keys);
    }

    private void DisableControls()
    {
        this.enabled = false;
    }

    #endregion Event CallBacks

    #region Collision CallBacks
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            GameService.Instance.GetInstructionView().ShowInstruction(InstructionType.Interact);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IInteractable interactable;
        if (other.TryGetComponent(out interactable) && playerController.isInteracted)
        {
            playerController.isInteracted = false;
            interactable.Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            GameService.Instance.GetInstructionView().HideInstruction();
        }
    }

    #endregion Collision CallBacks

    public void SetController(PlayerController _playerController)
    {
        playerController = _playerController;
    }
}