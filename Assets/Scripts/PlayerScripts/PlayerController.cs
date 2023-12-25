using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region
    // Move
    private PlayerControls inputActions;
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float dragFactor = .85f;
    private Vector2 inputDirection;
    private Vector2 moveAmount;

    //Move Flags
    bool canMove = false;



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


    //For game testing
    [SerializeField] private float maxFuelLevel = 5f;

    #endregion

    #region //New Input System Methods for adding listeners and Enabling / diabling

    private void OnEnable()
    {
        //Player Controls Events

        inputActions.Player.MovePlayer.performed += context => MovePlayer(context.ReadValue<Vector2>());
        inputActions.Player.MovePlayer.canceled += context => MovePlayer(Vector2.zero);

        inputActions.Player.ControllerXButton.performed += context => TryFiringPulseEngine(true);
        inputActions.Player.ControllerXButton.canceled += context => TryFiringPulseEngine(false);

        inputActions.Player.ControllerYButton.performed += context => TryUsingTractorBeam(true);
        inputActions.Player.ControllerYButton.canceled += context => TryUsingTractorBeam(false);

        inputActions.Player.ControllerAButton.performed += context => RotatePlayer(1);
        inputActions.Player.ControllerAButton.canceled += context => RotatePlayer(0);

        inputActions.Player.ControllerBButton.performed += context => RotatePlayer(-1);
        inputActions.Player.ControllerBButton.canceled += context => RotatePlayer(0);


        inputActions.Enable();

        //Other Actions

    }

    private void OnDisable()
    {
        //Disable Player Controls Events
        inputActions.Player.MovePlayer.performed -= context => MovePlayer(context.ReadValue<Vector2>());
        inputActions.Player.MovePlayer.canceled -= context => MovePlayer(Vector2.zero);

        inputActions.Player.ControllerXButton.performed -= context => TryFiringPulseEngine(true);
        inputActions.Player.ControllerXButton.canceled -= context => TryFiringPulseEngine(false);

        inputActions.Player.ControllerYButton.performed -= context => TryUsingTractorBeam(true);
        inputActions.Player.ControllerYButton.canceled -= context => TryUsingTractorBeam(false);

        inputActions.Player.ControllerAButton.performed -= context => RotatePlayer(1);
        inputActions.Player.ControllerAButton.canceled -= context => RotatePlayer(0);

        inputActions.Player.ControllerBButton.performed -= context => RotatePlayer(-1);
        inputActions.Player.ControllerBButton.canceled -= context => RotatePlayer(0);

        inputActions.Disable();

        //Disable Other Actions
    }

    #endregion


    private void Awake()
    {
        inputActions = new PlayerControls();
        //Initialize Game Components
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fuelController = GetComponent<FuelController>();
        rotationController = GetComponent<RotatePlayerController>();
        tractorBeamController = GetComponentInChildren<TractorBeamController>();
        pulseEngine = GetComponent<PulseEngineController>();
    }

    void Start()
    {
        //Send any Player attached Scripts down to Child GameObjects
        tractorBeamController.SetFuelControl(fuelController);


        //Initialize Player status
        fuelController.StartRefuel();


    }

    private void OnDestroy()
    {
        //Future use for any in Game instantiated objects
    }


    void Update()
    {
        CheckShipFunctionAvailability();
    }


    private void FixedUpdate()
    {
        //incorpate a check in fuelcontrol script.
        fuelController.StartRefuel();

        if (inputDirection != Vector2.zero && canMove)
        {
            ApplyMovement();
        }
        else
        {
            GradualStop();
        }
    }


    void CheckShipFunctionAvailability()
    {
        //Save for future environmental effects

        //canMove is specific for environmental effects, not control
        canMove = true;

        //Is Pulse Available is for environmental effects, not fuel level.
        pulseEngine.IsPulseAvailable = true;
        tractorBeamController.IsTractorBeamAvailable = true;

        //Currently being set variable used to set in inspector
        //update as necessary as power ups are integrated.
        fuelController.MaxFuelLevel = maxFuelLevel;

    }

    private void MovePlayer(Vector2 direction)
    {
        inputDirection = direction;
        inputDirection.Normalize();
    }

    private void RotatePlayer(float direction)
    {
        rotationController.TryRotatePlayer(direction);
    }

    /// <summary>
    /// Flag to designate if player is pressing pulse button.
    /// Updates PusleMultiplier property according to Pulse On / Off value.
    /// </summary>
    /// <param name="isPlayerTrying">Set in Input Actions Event</param>
    private void TryFiringPulseEngine(bool isPlayerTryingToFirePulse)
    {
       pulseEngine.SetPulseEngineState(isPlayerTryingToFirePulse);
    }

    private void TryUsingTractorBeam(bool isPlayerTryingToTurnOn)
    {
        tractorBeamController.SetTractorBeamState(isPlayerTryingToTurnOn);
    }


    void ApplyMovement()
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
