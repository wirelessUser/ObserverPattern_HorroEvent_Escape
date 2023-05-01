using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public bool isInteracted;

    private PlayerView playerView;
    private PlayerScriptableObject playerScriptableObject;
    private float Velocity;
    private float HorizontalAxis;
    private float VerticalAxis;
    private PlayerState playerState;
    public PlayerController(PlayerView _playerView, PlayerScriptableObject playerSO)
    {
        playerView = _playerView;
        playerView.SetController(this);
        playerScriptableObject = playerSO;
        playerScriptableObject.KeysEquipped = 0;
        playerState = PlayerState.InDark;
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracted = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            isInteracted = false;
        }
    }

    public void Jump(Rigidbody playerRigidbody, Transform transform)
    {
        bool IsGrounded = Physics.Raycast(transform.position, -transform.up, playerScriptableObject.raycastLength);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * playerScriptableObject.jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(Rigidbody playerRigidbody, Transform transform)
    {
        TakingInputs();

        Quaternion rotation = playerRigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * playerScriptableObject.sensitivity, 0));
        Vector3 position = transform.position + Time.fixedDeltaTime * Velocity * (transform.forward * VerticalAxis + transform.right * HorizontalAxis);

        playerRigidbody.MoveRotation(rotation);
        playerRigidbody.MovePosition(position);
    }

    public void SetKeys(int keys)
    {
        playerScriptableObject.KeysEquipped = keys;
    }

    public int GetKeys()
    {
        return playerScriptableObject.KeysEquipped;
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }
    public void SetPlayerState(PlayerState state)
    {
        playerState = state;
    }
    private void TakingInputs()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");
        VerticalAxis = Input.GetAxis("Vertical");
        Velocity = Input.GetKey(KeyCode.LeftShift) ? playerScriptableObject.sprintSpeed : playerScriptableObject.walkSpeed;
    }
}
