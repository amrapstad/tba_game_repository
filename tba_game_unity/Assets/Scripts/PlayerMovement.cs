using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;
    public CharacterController characterController;

    public float walkSpeed = 2f;
    public float sprintSpeed = 4f;
    public float gravity = 10f;
    public float jumpForce = 5f;

    public float viewSensitivity = 120f;
    public float bobbingSpeed = 0.24f;
    public float bobbingAmount = 0.06f;

    private Vector3 moveDirection;
    private Vector3 previousPosition;

    private bool isRunning;
    private bool isGrounded;

    float xRotation;
    float timer = 0.0f;
    float midpoint = 0.7f;

    private float originalHeight = 2f;
    private float targetHeight;
    private float crouchSpeed = 5f;
    private bool canStandUp = true;

    private float rotationSpeed = 5f;
    private float targetZRotation;
    private float currentZRotation;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        Move();
        View();

        if (characterController.height < originalHeight)
            CheckObstaclesAbove();
    }

    void Move()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1;
        if (Keyboard.current.sKey.isPressed) input.y -= 1;
        if (Keyboard.current.aKey.isPressed) input.x -= 1;
        if (Keyboard.current.dKey.isPressed) input.x += 1;

        if (characterController.isGrounded)
        {
            isGrounded = true;

            moveDirection = transform.TransformDirection(new Vector3(input.x, 0, input.y));

            isRunning = Keyboard.current.leftShiftKey.isPressed;
            moveDirection *= isRunning ? sprintSpeed : walkSpeed;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                moveDirection.y = jumpForce;
                isGrounded = false;
            }

            if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
            {
                targetHeight = characterController.height == originalHeight
                    ? originalHeight - 1f
                    : (canStandUp ? originalHeight : characterController.height);

                StartCoroutine(ChangeHeightSmoothly());
            }

            // Lean effect
            if (Keyboard.current.dKey.isPressed) targetZRotation = -1.5f;
            else if (Keyboard.current.aKey.isPressed) targetZRotation = 1.5f;
            else targetZRotation = 0f;

            currentZRotation = Mathf.Lerp(currentZRotation, targetZRotation, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, currentZRotation);

            if (input.magnitude > 0.1f)
                ViewHeadBobbing(input);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void View()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float inputX = mouseDelta.x * viewSensitivity * Time.deltaTime;
        float inputY = mouseDelta.y * viewSensitivity * Time.deltaTime;

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * inputX);
    }

    void ViewHeadBobbing(Vector2 input)
    {
        timer += bobbingSpeed * Time.deltaTime * 60f;
        if (timer > Mathf.PI * 2) timer -= Mathf.PI * 2;

        float waveSlice = Mathf.Sin(timer);
        float translateChange = waveSlice * bobbingAmount * input.magnitude;

        Vector3 camPos = camera.transform.localPosition;
        camPos.y = midpoint + translateChange;
        camera.transform.localPosition = camPos;
    }

    void CheckObstaclesAbove()
    {
        canStandUp = !Physics.Raycast(transform.position, Vector3.up, 1.5f);
    }

    IEnumerator ChangeHeightSmoothly()
    {
        float startHeight = characterController.height;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * crouchSpeed;
            characterController.height = Mathf.Lerp(startHeight, targetHeight, elapsed);
            yield return null;
        }

        characterController.height = targetHeight;
    }
}
