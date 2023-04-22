using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for Player and Camera Movement based on User Input.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //Public Variables:
    [Header("Adjustments:")]
    public bool showCursor;
    public float jumpForce = 5f;
    public float raycastLength;

    [Header("Player Movement Speeds:")]
    [Range(0.01f, 10.0f)] public float sensitivity = 5f;
    [Range(0.01f, 32.0f)] public float walkSpeed = 0.50f, sprintSpeed = 0.85f;

    [Header("Player Death references")]
    [SerializeField] private Transform slenderMan;
    [SerializeField] private Vector3 slenderManOffset;
    [SerializeField] private Vector3 slenderManLookAtOffset;
    [SerializeField] private Transform jumpScareLookAtTarget;
    [SerializeField] private float jumpScareRotationSpeed;

    // Private fields:
    private Rigidbody playerRigidbody;
    private Camera playerCamera;
    private const float rotationLimit = 0.5f;
    private static int keysEquipped = 0;

    //Properties:
    private float Velocity { get => Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed; }
    private float HorizontalAxis { get => Input.GetAxis("Horizontal"); }
    private float VerticalAxis { get => Input.GetAxis("Vertical"); }
    private bool IsGrounded { get => Physics.Raycast(transform.position, -transform.up, raycastLength); }
    public static int KeysEquipped { get => keysEquipped; }

    private void OnEnable()
    {
        EventManager.OnKeyPickedUp += OnKeyPickedUp;
        EventManager.OnPlayerEscaped += DisableControls;
        PlayerSanity.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        EventManager.OnKeyPickedUp -= OnKeyPickedUp;
        EventManager.OnPlayerEscaped -= DisableControls;
        PlayerSanity.OnPlayerDeath -= OnPlayerDeath;
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        slenderMan.gameObject.SetActive(false);
        keysEquipped = 0;
    }

    private void Update()
    {
        Cursor.visible = showCursor;

        Movement();
    }


    private void Movement()
    {
        //Rigidbody movement and rotation.
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0)));
        playerRigidbody.MovePosition(transform.position + Time.fixedDeltaTime * Velocity * (transform.forward * VerticalAxis + transform.right * HorizontalAxis));

        //Camera rotation.
        float velocity = sensitivity * -Input.GetAxis("Mouse Y");
        playerCamera.transform.Rotate(velocity, 0f, 0f);
        float rotationX = playerCamera.transform.localRotation.x;

        //Cancel out any rotational velocity if the player tries to rotate camera beyond Rotation Limit.
        if (rotationX > rotationLimit || rotationX < -rotationLimit) { playerCamera.transform.Rotate(-velocity, 0, 0); }

        //Jump:
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded) { playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); }
    }

    private void OnKeyPickedUp(int keys)
    {
        keysEquipped = keys;
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player Controller, OnPlayerDeath");
        PlayerSanity.OnPlayerDeath -= OnPlayerDeath;
    }

    private void DisableControls()
    {
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            Debug.Log("Player Inside Interation Trigger");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            Debug.Log("Player outside interaction trigger");
        }
    }
}