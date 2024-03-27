using UnityEngine;
public class PlayerController : MonoBehaviour
{
    //Character Logic
    private InputManager inputManager;

    [SerializeField] private float speed = 5f;
    private Vector3 playerVelocity;

    private Vector2 moveInput;
    private Vector3 direction;

    private CharacterInputSystem characterInputSystem;
    private CharacterController characterController;

    private Animator animator;

    //Light Controller
    private bool lightPress;

    private bool canInteractLight, interactingLight;
    private bool energy;

    //Laser Logic
    private bool key;

    private void Awake()
    {
        characterInputSystem = new CharacterInputSystem();
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        energy = false;

        animator = this.gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        Inputs();
        Movement();
        characterController.Move(playerVelocity * Time.deltaTime);
        LightLogic();
    }
    private void FixedUpdate()
    {
        moveInput = InputManager._INPUT_MANAGER.GetLeftAxisUpdate();
    }
    private void Inputs()
    {
        lightPress = InputManager._INPUT_MANAGER.GetLightButtonPressed();
    }
    private void OnEnable()
    {
        characterInputSystem.Character.Enable();
    }
    private void OnDisable()
    {
        characterInputSystem.Character.Disable();
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
    private void LightLogic()
    {
        //Change to NewInputSistem
        if (canInteractLight && Input.GetKeyDown(KeyCode.E) || canInteractLight && Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            interactingLight = true;
        }
    }

    /*--- SETTERS || GETTERS ---*/
    public void SetCanInteractLight(bool can) { canInteractLight = can; }

    public void SetInteractLight(bool can) { interactingLight = can; }
    public bool GetInteractingLight() { return interactingLight; }
    
    public void SetEnergy(bool e) { energy = e; }
    public bool GetEnergy() { return energy; }

    public void SetKey(bool e) { key = e; }
    public bool KeyEnergy() { return key; }
}