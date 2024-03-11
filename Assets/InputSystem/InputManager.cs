using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager _INPUT_MANAGER;
    private CharacterInputSystem characterInputSystem;

    private bool lightButtonPressed;

    private Vector2 leftAxisValue = Vector2.zero;
    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            characterInputSystem = new CharacterInputSystem();
            characterInputSystem.Character.Enable();

            characterInputSystem.Character.Move.performed += LeftAxisUpdate;

            characterInputSystem.Character.Light.started += LightButtonPressed;

            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Update()
    {
        InputSystem.Update();
    }
    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }
    public Vector2 GetLeftAxisUpdate() { return leftAxisValue; }
    private void LightButtonPressed(InputAction.CallbackContext context)
    {
        lightButtonPressed = true;
    }
    public bool GetLightButtonPressed(){ return lightButtonPressed; }
}
