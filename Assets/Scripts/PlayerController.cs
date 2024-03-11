using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 playerVelocity;

    private Vector2 moveInput;
    private Vector3 direction;

    private CharacterInputSystem characterInputSystem;
    private CharacterController characterController;
    private void Awake()
    {
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
    }
    private void FixedUpdate()
    {
        moveInput = characterInputSystem.Character.Move.ReadValue<Vector2>();
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
}
