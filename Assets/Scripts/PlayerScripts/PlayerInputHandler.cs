using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls inputActions;
    private Vector2 movementInput;

    public Vector2 MovementInput
    {
        get { return movementInput.normalized; }
        private set { movementInput = value; }
    }

    public bool IsFiringPulse { get; private set; }
    public bool IsUsingAttachment { get; private set; }
    public float RotationDirection { get; private set; }

    public event Action<float> OnRotateButtonPressed;
    public event Action OnStopRotateButtonPressed;

    public event Action OnSwitchAttachment;

    public event Action<InputAction.CallbackContext> OnMenuButtonPressed;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Player.MovePlayer.performed += context => MovementInput = context.ReadValue<Vector2>();
        inputActions.Player.MovePlayer.canceled += context => MovementInput = Vector2.zero;

        inputActions.Player.EnginePulseButton.performed += _ => IsFiringPulse = true;
        inputActions.Player.EnginePulseButton.canceled += _ => IsFiringPulse = false;

        inputActions.Player.UseAttachmentButton.performed += _ => IsUsingAttachment = true;
        inputActions.Player.UseAttachmentButton.canceled += _ => IsUsingAttachment = false;

        inputActions.Player.SwitchAttachmentButton.performed += _ => OnSwitchAttachment?.Invoke();
        
        inputActions.Player.RotateClockwise.performed += _ => OnRotateButtonPressed?.Invoke(-1f);
        inputActions.Player.RotateClockwise.canceled += _ => OnStopRotateButtonPressed?.Invoke();

        inputActions.Player.RotateCounterclockwise.performed += _ => OnRotateButtonPressed?.Invoke(1f);
        inputActions.Player.RotateCounterclockwise.canceled += _ => OnStopRotateButtonPressed?.Invoke();

        inputActions.Player.Pause.performed += context => OnMenuButtonPressed?.Invoke(context);

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}