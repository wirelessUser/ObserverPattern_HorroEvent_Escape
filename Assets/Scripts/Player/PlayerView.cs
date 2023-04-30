using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Camera playerCamera;
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
        playerCamera = gameObject.GetComponentInChildren<Camera>();
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
        if (other.GetComponent<I_Interactable>() != null)
        {
            UIManager.Instance.ShowInteractInstructions(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<I_Interactable>() != null && playerController.isInteracted)
        {
            playerController.isInteracted = false;
            other.GetComponent<I_Interactable>().Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<I_Interactable>() != null)
        {
            UIManager.Instance.ShowInteractInstructions(false);
        }
    }

    #endregion Collision CallBacks

    public void SetController(PlayerController _playerController)
    {
        playerController = _playerController;
    }
}