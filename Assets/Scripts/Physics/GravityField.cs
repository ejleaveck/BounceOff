using UnityEngine;

public class GravityField : MonoBehaviour
{

    public enum GravityType { Attract, Repel, AttractThenRepel }
    public GravityType gravityType = GravityType.Attract;
    public enum GravityFieldPurpose { Default, Goal, Special }
    [SerializeField] private GravityFieldPurpose gravityFieldPurpose = GravityFieldPurpose.Default;

    public float strength = 10f;
    public float maxForce = 100f;
    public float dampingDistance = 1f;
    public float repelForceMultiplier = 2f;

    public string targetTag = "";

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!string.IsNullOrEmpty(targetTag) && !other.CompareTag(targetTag))
        {
            return;
        }

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        AffectedByGravity affectedByGravity = other.GetComponent<AffectedByGravity>();
        GoalObjectController goalObjectController = other.GetComponent<GoalObjectController>();

        if (rb != null && affectedByGravity != null)
        {
            ApplyGravityEffect(rb, affectedByGravity);

            if(goalObjectController != null && IsWithinDampingDistance(rb.position))
            {
                if (gravityFieldPurpose == GravityFieldPurpose.Goal)
                {
                    goalObjectController.OnEnterDampingDistance();
                }
                else
                {
                    // Optionally do some stuff to things for other gravity field purposes like "eat" debris
                    
                }
            }
        }
    }

    private bool IsWithinDampingDistance(Vector2 position)
    {
        float distance = ((Vector2)transform.position - position).magnitude;
        return distance < dampingDistance;
    }

    private void ApplyGravityEffect(Rigidbody2D rb, AffectedByGravity affectedByGravity)
    {
        Vector2 directionToCenter = (Vector2)transform.position - rb.position;
        float distance = directionToCenter.magnitude;

        Vector2 forceDirection = directionToCenter.normalized;
        float forceMagnitude = Mathf.Min(strength / Mathf.Max(distance, 1f), maxForce) * affectedByGravity.effectMultiplier;

        switch (gravityType)
        {
            case GravityType.Attract:
                rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Force);
                break;
            case GravityType.Repel:
                rb.AddForce(-forceDirection * forceMagnitude, ForceMode2D.Force);
                break;
            case GravityType.AttractThenRepel:
                if (distance > dampingDistance)
                {
                    rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Force);

                }
                else
                {
                    rb.AddForce(-forceDirection * forceMagnitude * repelForceMultiplier, ForceMode2D.Force);
                }
                break;
        }
    }

}
