using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private float gravityStrength = 10f;
    [SerializeField] private float maxGravityForce = 10f;
    [SerializeField] private float dampingDistance = 1f; // Distance within which damping starts
    [SerializeField] private float dampingFactor = 0.5f; // Damping factor for reducing velocity

    private Transform goalCenter;

    void Start()
    {
        goalCenter = transform; // Assuming the goal's center is at its own transform position
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoalObject"))
        {
            Rigidbody2D goalObjectRb = other.GetComponent<Rigidbody2D>();

            Vector2 directionToCenter = (Vector2)goalCenter.position - goalObjectRb.position;
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
