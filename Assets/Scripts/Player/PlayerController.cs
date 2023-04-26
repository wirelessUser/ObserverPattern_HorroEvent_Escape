using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for Player and Camera Movement based on User Input.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : GenericMonoSingleton<PlayerController>//why mono
{
    [Header("Adjustments:")]
    public bool showCursor;
    public float jumpForce = 5f;
    public float raycastLength;

    [Header("Player Movement Speeds:")]
    [Range(0.01f, 10.0f)] public float sensitivity = 5f;
    [Range(0.01f, 32.0f)] public float walkSpeed = 0.50f, sprintSpeed = 0.85f;// inspector TODO



    private Rigidbody playerRigidbody;
    private Camera playerCamera;
    private const float rotationLimit = 0.5f;

    public int KeysEquipped { get; private set; }

    private float Velocity { get => Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed; }
    private float HorizontalAxis { get => Input.GetAxis("Horizontal"); }
    private float VerticalAxis { get => Input.GetAxis("Vertical"); }
    private bool IsGrounded { get => Physics.Raycast(transform.position, -transform.up, raycastLength); }

    private void OnEnable()
    {
        EventManager.OnKeyPickedUp += OnKeyPickedUp;

        EventManager.OnPlayerEscaped += DisableControls;
    }

    private void OnDisable()
    {
        EventManager.OnKeyPickedUp -= OnKeyPickedUp;
        EventManager.OnPlayerEscaped -= DisableControls;
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // awake
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        KeysEquipped = 0;
    }

    private void Update()
    {
        Cursor.visible = showCursor;

        Movement();
    }


    private void Movement()
    {
        //movement should happen on player not on rigidbody
        // readable code
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0)));
        playerRigidbody.MovePosition(transform.position + Time.fixedDeltaTime * Velocity * (transform.forward * VerticalAxis + transform.right * HorizontalAxis));

        //Camera rotation.
        float velocity = sensitivity * -Input.GetAxis("Mouse Y");
        playerCamera.transform.Rotate(velocity, 0f, 0f);

        float rotationX = playerCamera.transform.localRotation.x;
        if (rotationX > rotationLimit || rotationX < -rotationLimit)
        {
            playerCamera.transform.Rotate(-velocity, 0, 0);
        }

        //Jump:
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnKeyPickedUp(int keys)
    {
        KeysEquipped = keys;//check
    }

    private void DisableControls()
    {
        enabled = false; //fix - PlayerController.Disable()
    }

}