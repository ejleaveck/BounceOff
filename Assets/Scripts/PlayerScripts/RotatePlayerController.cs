using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerController : MonoBehaviour
{

    [SerializeField] private float rotationAmount = 22.5f;
    private bool canRotate = true;

    
    /// <summary>
    /// Used for environmental effects control
    /// </summary>
    public bool CanRotate
    {
        get { return canRotate; }
        set { canRotate = value; }
    }

  
    public void TryRotatePlayer(float direction)
    {
        if (canRotate)
        {
           RotatePlayer(direction);
        }
    }

    private void RotatePlayer(float direction)
    {
        transform.Rotate(0, 0, rotationAmount * direction);
    }

}
