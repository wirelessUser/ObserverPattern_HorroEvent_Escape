using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO -> Make PlayerController Non-Mono and Make PlayerView Seperate , Only for Player We will use Controller and View
// Business Logic Inside PlayerController and Unity Stuff Inside PlayerView 

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : GenericMonoSingleton<PlayerController>//why mono
{
    // Todo -> Make PlayerScriptable Object and move this things
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

    // Todo -> Make PlayerScriptable Object and move Keys Counter
    public int KeysEquipped { get; private set; }

    private float Velocity { get => Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed; }
    private float HorizontalAxis { get => Input.GetAxis("Horizontal"); }
    private float VerticalAxis { get => Input.GetAxis("Vertical"); }
    private bool IsGrounded { get => Physics.Raycast(transform.position, -transform.up, raycastLength); }


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

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // Todo -> Follow consitant Declarations , if we are doing everything in Awake,
                                                     // it should be in Awake Everywhere
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        KeysEquipped = 0;
    }

    private void Update()
    {
        Cursor.visible = showCursor;

        Move();
    }


    private void Move()
    {
        //movement should happen on player GameObject not on rigidbody

        //Todo ->Make it readable code
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0)));
        playerRigidbody.MovePosition(transform.position + Time.fixedDeltaTime * Velocity * (transform.forward * VerticalAxis + transform.right * HorizontalAxis));

        //Camera rotation. ->Todo This should be in CameraController
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
        Debug.Log("PlayerController - OnKeyEquipped");
        KeysEquipped = keys;
    }

    private void DisableControls()
    {
        enabled = false; //Todo -> PlayerController.Disable()
    }

}