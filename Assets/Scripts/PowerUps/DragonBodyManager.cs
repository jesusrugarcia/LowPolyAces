using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DragonBodyManager : MonoBehaviour
{
    public DragonBossManager manager;
    public GameObject target;

    public float timer = 0;
    const float rotateTime = 0.025f;
    

    Vector3 objective = new Vector3(0,0,0);
    // Start is called before the first frame update
    
    void FixedUpdate()
    {
        followTarget();
        transform.position = target.transform.position - (transform.right * manager.partDistance);
    }
    
    public void followTarget(){
        try{
            objective = (target.transform.position - (transform.right * manager.partDistance)) - transform.position;
            var rotation = Vector3.Angle(objective, transform.right);
            var rotationR = Vector3.Angle(objective, transform.up);
            var rotationL = Vector3.Angle(objective, -transform.up);

            if(rotation > manager.partRotation){
             rotation = manager.partRotation;
            }

            if (rotationL < rotationR){
                rotation = -rotation;
            }

            transform.Rotate(0,0,rotation);
        } catch(System.Exception e){
            target = gameObject;
            Debug.Log(e);
        }
        
        
    }
}
