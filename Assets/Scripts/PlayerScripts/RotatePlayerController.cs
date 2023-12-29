using UnityEngine;

public class RotatePlayerController : MonoBehaviour
{
    [SerializeField] private float continousRotationSpeed = 400f;


    private float currentRotationDirection;

    private Rigidbody2D playerRb;

    /// <summary>
    /// Used for environmental effects control
    /// </summary>
    public bool CanRotate { get; set; }

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    public void TryRotatePlayer(float direction)
    {
        currentRotationDirection = direction;
    }

    private void FixedUpdate()
    {
        if (currentRotationDirection != 0 && CanRotate)
        {
            playerRb.constraints = RigidbodyConstraints2D.None;

            ContinousRotation();
        }
    }


    public void StopContinuousRotation()
    {
            currentRotationDirection = 0;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void ContinousRotation()
    {
        float rotationThisFrame = continousRotationSpeed * currentRotationDirection * Time.deltaTime;

        playerRb.MoveRotation(playerRb.rotation + rotationThisFrame);
    }

}
