using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : MonoBehaviour
{

    public GameObject player;

    [SerializeField] private float rotateDegrees = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotatePlayer()
    {
        //TODO: track rotation so it can do infinite rotation

        Vector3 axis = new Vector3(0, 0, 1);

        player.transform.rotation = Quaternion.AngleAxis(rotateDegrees += rotateDegrees, axis);
    }
}
