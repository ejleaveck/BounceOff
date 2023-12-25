using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObjectController : MonoBehaviour
{

    Rigidbody2D goalObjectRb;

    [SerializeField] private Vector2 initialForce;

    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private float minSpeed = 10f;


    private void Awake()
    {
        goalObjectRb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ApplyInitialForce();
    }


    private void FixedUpdate()
    {
        ControlSpeed();

    }

    void ApplyInitialForce()
    {
        goalObjectRb.AddForce(initialForce, ForceMode2D.Impulse);
    }

    void ControlSpeed()
    {
        float currentSpeed = goalObjectRb.velocity.magnitude;

        if(currentSpeed > maxSpeed)
        {
            goalObjectRb.velocity = goalObjectRb.velocity.normalized * maxSpeed;
        }
        else if (currentSpeed < minSpeed)
        {
            goalObjectRb.velocity = goalObjectRb.velocity.normalized * minSpeed;
        }
    }

}
