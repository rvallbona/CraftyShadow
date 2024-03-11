using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;

    [SerializeField] private float speed = 5f;
    private Vector3 playerVelocity;

    private Vector2 moveInput;
    private Vector3 direction;

    private bool canInteractLight, interactingLight;

    private CharacterInputSystem characterInputSystem;
    private CharacterController characterController;
    private void Awake()
    {
        inputManager = InputManager._INPUT_MANAGER;
        characterInputSystem = new CharacterInputSystem();
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        Movement();
        characterController.Move(playerVelocity * Time.deltaTime);

        //Change to NewInputSistem
        if (canInteractLight && Input.GetKeyDown(KeyCode.E))
        {
            LightLogic();
        }
    }
    private void FixedUpdate()
    {
        moveInput = inputManager.GetLeftAxisUpdate();
    }
    private void Movement()
    {
        direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        playerVelocity = direction * speed;

        //Rotation
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 1000f);
        }
    }
    private void OnEnable()
    {
        characterInputSystem.Character.Enable();
    }
    private void OnDisable()
    {
        characterInputSystem.Character.Disable();
    }
    private void LightLogic()
    {
        interactingLight = true;
    }
    public void SetCanInteractLight(bool can)
    {
        canInteractLight = can;
    }
    public void SetInteractLight(bool can)
    {
        interactingLight = can;
    }
    public bool GetInteractingLight() { return interactingLight; }
}