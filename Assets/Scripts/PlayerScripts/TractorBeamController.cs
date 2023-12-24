using Unity.VisualScripting;
using UnityEngine;

public class TractorBeamController : MonoBehaviour
{
    /*
    private bool isTractorBeamOn;
    private GameObject capturedObject = null;

    private Collider2D tractorBeamCollider;
    private float beamRadius;

    public bool IsTractorBeamOn
    {
        get { return isTractorBeamOn; }
        set
        {
            isTractorBeamOn = value;
            if (isTractorBeamOn)
            {
                TryCaptureObjectsAlreadyInBeam();
            }
            else
            {
                ReleaseObject();
            }
        }
    }


    Transform tractorBeamCenter;
    SpriteRenderer tractorBeamRenderer;
    private void Start()
    {
        
            tractorBeamCenter = transform;
            tractorBeamRenderer = GetComponent<SpriteRenderer>();
            Vector2 boundsSize = CalculateColliderBounds();
            beamRadius = Mathf.Max(boundsSize.x, boundsSize.y) / 2f; // Use the larger dimension as diameter
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTractorBeamOn && other.gameObject.layer == LayerMask.NameToLayer("TractorBeamable") && capturedObject == null)
        {
            CaptureObject(other.gameObject);
        }
    }

    private void CaptureObject(GameObject obj)
    {
        capturedObject = obj;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        obj.transform.SetParent(transform);
    }

    private void ReleaseObject()
    {
        if (capturedObject != null)
        {
            Rigidbody2D rb = capturedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            capturedObject.transform.SetParent(null);
            capturedObject = null;
        }
    }



    private void TryCaptureObjectsAlreadyInBeam()
    {
        Collider2D[] objectsInBeam = Physics2D.OverlapCircleAll(transform.position, beamRadius, LayerMask.GetMask("TractorBeamable"));
        foreach (var obj in objectsInBeam)
        {
            if (capturedObject == null) // Only capture if no object is already captured
            {
                CaptureObject(obj.gameObject);
                break; // Assuming you want to capture only one object at a time
            }
        }
    }





    private Vector2 CalculateColliderBounds()
    {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider != null)
        {
            return polygonCollider.bounds.size;
        }
        return Vector2.zero;
    }

    */




































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

    //Fuel control 
    private FuelControl fuelControl;
    [SerializeField] private float beamBurnRate = .5f;

    

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
        if (isTractorBeamOn && fuelControl.CurrentFuelLevel > 0)
        {
            fuelControl.ConsumeFuel(beamBurnRate * Time.deltaTime); 
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

    //so i can send the control over from player object.
    public void SetFuelControl(FuelControl control)
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


