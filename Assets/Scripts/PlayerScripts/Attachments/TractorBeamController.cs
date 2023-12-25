using UnityEngine;

public class TractorBeamController : MonoBehaviour
{
    //gravity well
    [SerializeField] private float gravityStrength = 1000f;
    [SerializeField] private float maxGravityForce = 1000f;
    [SerializeField] private float dampingDistance = 1f; // Distance within which damping starts
    [SerializeField] private float dampingFactor = 10f; // Damping factor for reducing velocity

    //game objects
    private Transform tractorBeamCenter;
    Rigidbody2D goalObjectRb;
    private SpriteRenderer tractorBeamRenderer;

    //Beam variables
    private bool isTractorBeamOn;
    private bool isTractorBeamAvailable;

    //Fuel control 
    private FuelController fuelControl;
    [SerializeField] private float beamBurnRate = .5f;

    /// <summary>
    /// Set based on environmentals or status, not fuel levels.
    /// </summary>
    public bool IsTractorBeamAvailable
    {
        get { return isTractorBeamAvailable; }
        set { isTractorBeamAvailable = value; }
    }

    public bool IsTractorBeamOn
    {
        get { return isTractorBeamOn; }
        set { isTractorBeamOn = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        tractorBeamCenter = transform;
        tractorBeamRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (isTractorBeamOn && isTractorBeamAvailable && fuelControl.CurrentFuelLevel > 0)
        {
            fuelControl.ConsumeFuel(beamBurnRate);
        }
        else
        {
            SetTractorBeamState(false);
        }
    }

    public void SetTractorBeamState(bool isActive)
    {
        IsTractorBeamOn = isActive;
        tractorBeamRenderer.enabled = isActive;
    }

    /// <summary>
    /// Receives Fuel Control from Player Object.
    /// Since this is a child object
    /// </summary>
    /// <param name="control">the Controller that has the Fuel Controller attached.</param>
    public void SetFuelControl(FuelController control)
    {
        fuelControl = control;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (isTractorBeamOn && other.gameObject.layer == LayerMask.NameToLayer("TractorBeamable"))
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


