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

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Player.MovePlayer.performed += context => MovementInput = context.ReadValue<Vector2>();
        inputActions.Player.MovePlayer.canceled += context => MovementInput = Vector2.zero;

        inputActions.Player.EnginePulseButton.performed += context => IsFiringPulse = true;
        inputActions.Player.EnginePulseButton.canceled += context => IsFiringPulse = false;

        inputActions.Player.UseAttachmentButton.performed += context => IsUsingAttachment = true;
        inputActions.Player.UseAttachmentButton.canceled += context => IsUsingAttachment = false;

        inputActions.Player.SwitchAttachmentButton.performed += _ => OnSwitchAttachment?.Invoke();
        
        inputActions.Player.RotateClockwise.performed += _ => OnRotateButtonPressed?.Invoke(-1f);
        inputActions.Player.RotateClockwise.canceled += _ => OnStopRotateButtonPressed?.Invoke();

        inputActions.Player.RotateCounterclockwise.performed += _ => OnRotateButtonPressed?.Invoke(1f);
        inputActions.Player.RotateCounterclockwise.canceled += _ => OnStopRotateButtonPressed?.Invoke();

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}