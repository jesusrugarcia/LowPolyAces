using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    public float rotationSpeedX = 50;
    public float rotationSpeedY = 0;
    public float rotationSpeedZ = 0;
    public PlaneManager parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<PlaneManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(parent == null){
            transform.Rotate(rotationSpeedX,rotationSpeedY,rotationSpeedZ);
        } else {
            transform.Rotate(rotationSpeedX * (parent.stats.speed/parent.stats.maxSpeed),rotationSpeedY * (parent.stats.speed/parent.stats.maxSpeed),rotationSpeedZ * (parent.stats.speed/parent.stats.maxSpeed));
        }
        
    }
}
