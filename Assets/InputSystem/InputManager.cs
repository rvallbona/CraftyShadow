using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager _INPUT_MANAGER;
    private CharacterInputSystem characterInputSystem;

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
}
