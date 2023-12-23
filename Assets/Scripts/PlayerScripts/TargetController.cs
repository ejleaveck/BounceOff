using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public Rigidbody2D targetRb;
    [SerializeField] private Vector2 initialforce;

    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private float minSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody2D>();

        ApplyInitialForce();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        ControlSpeed();
    }

    void ApplyInitialForce()
    {
        targetRb.AddForce(initialforce, ForceMode2D.Impulse);
    }

    void ControlSpeed()
    {
        float currentSpeed = targetRb.velocity.magnitude;

        if(currentSpeed > maxSpeed)
        {
            targetRb.velocity = targetRb.velocity.normalized * maxSpeed;
        }
        else if (currentSpeed < minSpeed && currentSpeed > 0)
        {
            targetRb.velocity = targetRb.velocity.normalized * minSpeed;
        }
    }

}
