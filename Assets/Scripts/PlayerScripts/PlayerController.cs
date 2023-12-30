using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region
    // Move
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float dragFactor = .85f;
    private Vector2 moveAmount;

    //Move Flags
    bool canMove;

    //Power Ups
    //***


    //Game Objects
    Rigidbody2D playerRb;
    SpriteRenderer spriteRenderer;


    //Outside Scripts
    private RotatePlayerController rotationController;
    private TractorBeamController tractorBeamController;
    private PulseEngineController pulseEngine;
    private FuelController fuelController;

    private PlayerInputHandler inputHandler;

    private AttachmentsController attachmentsController;
    [SerializeField] private LevelController levelController;

    #endregion


    private void Awake()
    {
        //Initialize Game Components
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fuelController = GetComponent<FuelController>();
        rotationController = GetComponent<RotatePlayerController>();
        tractorBeamController = GetComponentInChildren<TractorBeamController>();
        pulseEngine = GetComponent<PulseEngineController>();

        attachmentsController = GetComponentInChildren<AttachmentsController>();

        inputHandler = GetComponent<PlayerInputHandler>();



    }

    private void OnEnable()
    {
        //Event subscriptions for rotation
        inputHandler.OnRotateButtonPressed += rotationController.TryRotatePlayer;
        inputHandler.OnStopRotateButtonPressed += rotationController.StopContinuousRotation;


        inputHandler.OnSwitchAttachment += attachmentsController.SwitchAttachment;

        //Game Pause Menu
        inputHandler.OnMenuButtonPressed += levelController.OnPauseMenuButtonPressed;

    }


    private void OnDestroy()
    {
        if (inputHandler != null)
        {
            inputHandler.OnRotateButtonPressed -= rotationController.TryRotatePlayer;
            inputHandler.OnStopRotateButtonPressed -= rotationController.StopContinuousRotation;

            inputHandler.OnSwitchAttachment -= attachmentsController.SwitchAttachment;

            inputHandler.OnMenuButtonPressed -= levelController.OnPauseMenuButtonPressed;
        }
    }

    void Start()
    {
        //Send any Player attached Scripts down to Child GameObjects
        tractorBeamController.SetFuelControl(fuelController);



        CheckShipFunctionAvailability();
    }

    private void Update()
    {
        attachmentsController.UseCurrentAttachment(inputHandler.IsUsingAttachment);
    }

    private void FixedUpdate()
    {
        if (inputHandler.MovementInput != Vector2.zero && canMove)
        {
            ApplyMovement(inputHandler.MovementInput);
        }
        else
        {
            GradualStop();
        }


        pulseEngine.SetPulseEngineState(inputHandler.IsFiringPulse);

        //TODO: Outdated event, check that it is correcly handled by attachmentcontroller event for all attachments.
        //tractorBeamController.SetTractorBeamButtonState(inputHandler.IsUsingAttachment);
    }


    /// <summary>
    /// Update once environmental effects get implemented.
    /// </summary>
    void CheckShipFunctionAvailability()
    {
        //Properties for environmental effects
        canMove = true;
        pulseEngine.IsPulseAvailable = true;
        tractorBeamController.IsTractorBeamAvailable = true;
        rotationController.CanRotate = true;
        fuelController.CanRefuel = true;
    }


    void ApplyMovement(Vector2 inputDirection)
    {
        moveAmount = inputDirection * moveSpeed * pulseEngine.PulseMultiplier * Time.deltaTime;

        Vector2 newPosition = playerRb.position + moveAmount;
        playerRb.MovePosition(newPosition);
    }

    void GradualStop()
    {
        playerRb.velocity *= dragFactor;
    }
}