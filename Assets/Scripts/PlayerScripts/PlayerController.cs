using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float dragFactor = .95f;
    [SerializeField] private float moveMultiplier = 2f;


    private Rigidbody2D playerRb;
    private Vector2 moveDirection;
    private Vector2 initialTouchPosition;
    private bool isTouching = false;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTouchInput();
    }

    private void FixedUpdate()
    {
        if (isTouching)
        {
            ApplyMovement();
        }
        else
        {
            GradualStop();
        }

    }

    void HandleTouchInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    initialTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    Vector2 currentTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    moveDirection = (currentTouchPosition - initialTouchPosition).normalized;
                    initialTouchPosition = currentTouchPosition;
                    isTouching = true;
                    break;

                case TouchPhase.Ended:
                    isTouching=false;
                    break;
            }
        }
        else
        {
            isTouching = false;
        }
    }

    void ApplyMovement()
    {
        Vector2 force = moveDirection * moveForce * moveMultiplier;
        playerRb.AddForce(force);
    }

    void GradualStop()
    {
        playerRb.velocity *= dragFactor;
    }

}
