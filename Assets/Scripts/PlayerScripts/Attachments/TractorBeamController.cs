using UnityEngine;

public class TractorBeamController : Attachment
{

    public override string AttachmentName => "Tractor Beam";
    

    //gravity well
    [SerializeField] private float gravityStrength = 1000f;
    [SerializeField] private float maxGravityForce = 1000f;
    [SerializeField] private float dampingDistance = 1f; // Distance within which damping starts
    [SerializeField] private float dampingFactor = 10f; // Damping factor for reducing velocity

    //game objects
    private Transform tractorBeamCenter;
    Rigidbody2D goalObjectRb;
    private SpriteRenderer tractorBeamRenderer;

    //Fuel control 
    private FuelController fuelControl;
    [SerializeField] private float beamBurnRate = 1f;
    public override float BurnRate => beamBurnRate;

    /// <summary>
    /// Set based on environmentals or status, not fuel levels.
    /// </summary>
    public bool IsTractorBeamAvailable { get; set; }

    private bool isTractorBeamOn;
    private bool isTractorBeamButtonPressed;


    // Start is called before the first frame update
    void Start()
    {
        tractorBeamCenter = transform;
        tractorBeamRenderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {

        if (isTractorBeamOn)
        {
            fuelControl.ConsumeFuel(beamBurnRate);
        }
        else
        {
            fuelControl.StopConsumingFuel();
        }
    }

    public override void Activate()
    {
        isTractorBeamOn = true;
            tractorBeamRenderer.enabled = true;
    }

    public override void Deactivate()
    {
        isTractorBeamOn = false;
            tractorBeamRenderer.enabled = false;

    }


    public void SetTractorBeamButtonState(bool isPressed)
    {
        isTractorBeamButtonPressed = isPressed;
        if(isTractorBeamButtonPressed && IsTractorBeamAvailable && !fuelControl.IsFuelTankEmpty)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }


    /// <summary>
    /// Receives Fuel Control from Player Object.
    /// Since this is a child object
    /// </summary>
    /// <param name="control">Get's the Fuel Controller from the Game Object it is attached to.</param>
    public void SetFuelControl(FuelController control)
    {
        fuelControl = control;
    }

    //TODO: separate phyiscs, use empty tank threshold.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (isTractorBeamOn && other.gameObject.layer == LayerMask.NameToLayer("TractorBeamable") && fuelControl.CurrentFuelLevel > 0)
        {
            goalObjectRb = other.GetComponent<Rigidbody2D>();

            Vector2 directionToCenter = (Vector2)tractorBeamCenter.position - goalObjectRb.position;
            float distance = directionToCenter.magnitude;

            // Apply gravitational force if the goal object is outside the damping distance
            if (distance > dampingDistance)
            {
                Vector2 forceDirection = directionToCenter.normalized;
                float gravityForce = Mathf.Min(gravityStrength / Mathf.Max(distance, 1f), maxGravityForce);
                goalObjectRb.AddForce(forceDirection * gravityForce, ForceMode2D.Force);
            }

            // Damping the velocity as the object gets closer to the center
            if (distance < dampingDistance)
            {
                goalObjectRb.velocity = Vector2.Lerp(goalObjectRb.velocity, Vector2.zero, dampingFactor * Time.deltaTime);
            }
        }
    }

}


