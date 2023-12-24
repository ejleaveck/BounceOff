using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeamController : MonoBehaviour
{
    [SerializeField] private float gravityStrength = 10f;
    private Transform goalCenter;
    private Rigidbody2D goalObjectRb;

    private bool isTractorBeamOn;

    public bool IsTractorBeamOn
    {
        get { return isTractorBeamOn; }
        set { isTractorBeamOn = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        goalCenter = transform;
    }


    private void OnTriggerStay(Collider other)
    {
        if (isTractorBeamOn)
        {
            if (other.gameObject.CompareTag("GoalObject"))
            {
                if (goalObjectRb == null)
                {
                    goalObjectRb = other.GetComponent<Rigidbody2D>();
                }

                Vector2 directionToCenter = (Vector2)goalCenter.position - goalObjectRb.position;
                float distance = directionToCenter.magnitude;

                Vector2 forceDirection = directionToCenter.normalized;
                float gravityForce = gravityStrength / distance;
                goalObjectRb.AddForce(forceDirection * gravityForce, ForceMode2D.Force);
            }
        }
    }
}
