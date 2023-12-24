using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region
    //Status and Flags


    // Move
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float dragFactor = .85f;
    private Vector2 inputDirection;
    private Vector2 moveAmount;

    //Move Flags
    bool canMove = false;

    //Special Movements
    private PlayerRotationController rotationController;
    private TractorBeamController tractorBeamController;

    //Power Ups
    //***
    //Fuel Control
    private FuelControl fuelControl;

    //***
    //Pulse
    bool isPulseFiring = false;
    bool isPulseAvailable = true;

    [SerializeField] private float pulseMultiplier = 5f;
    [SerializeField] private float pulseBurnRate = 2f;

    //Game Objects
    Rigidbody2D playerRb;
    SpriteRenderer spriteRenderer;

    #endregion



    void Start()
    {
        //Initialize Game Components
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fuelControl = GetComponent<FuelControl>();
        rotationController = GetComponent<PlayerRotationController>();
        tractorBeamController = GetComponent<TractorBeamController>();

        //Initialize Player status
        fuelControl.StartRefuel();


        //Initialzie Environment
    }



    void Update()
    {
        //movement input
        CheckMoveAvailability();
        if (canMove)
        {
            inputDirection.x = Input.GetAxis("Horizontal");
            inputDirection.y = Input.GetAxis("Vertical");
            inputDirection.Normalize();
        }

        //pulse engine input ---- special skill input
        //Fire3 = X button
        isPulseFiring = Input.GetButton("Fire3");


        //Rotation input
        //Fire1 = A button  -- Fire2 = B button
        if (Input.GetButtonDown("Fire1"))
        {
            rotationController.TryRotatePlayer(1);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            rotationController.TryRotatePlayer(-1);
        }

        //Input button testing
        // Jump = Y button
        if (Input.GetButton("Jump"))
        {
            tractorBeamController.IsTractorBeamOn = true;
        }

    }


    private void FixedUpdate()
    {

        //incorpate a check in fuelcontrol script.
        fuelControl.StartRefuel();


        if (inputDirection != Vector2.zero)
        {
            ApplyMovement();
        }
        else
        {
            GradualStop();
        }
    }


    void CheckMoveAvailability()
    {
        canMove = true;
    }

    void ApplyMovement()
    {
        moveAmount = inputDirection * moveSpeed * Time.deltaTime;

        FirePulseEngine();

        Vector2 newPosition = playerRb.position + moveAmount;
        playerRb.MovePosition(newPosition);
    }

    void GradualStop()
    {
        playerRb.velocity *= dragFactor;
    }


    void FirePulseEngine()
    {
        if (isPulseFiring && isPulseAvailable && fuelControl.CurrentFuelLevel > 0)
        {
            fuelControl.ConsumeFuel(pulseBurnRate * Time.deltaTime);

            moveAmount *= pulseMultiplier;
        }

    }

}
