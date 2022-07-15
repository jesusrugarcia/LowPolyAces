using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaneMovement : MonoBehaviour
{   
    
    public PlaneManager plane;
    

    public Vector2 movement = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        plane = GetComponent<PlaneManager>();
        plane.planeMovement = this;

       
        
    }

    
    private void FixedUpdate() {
        accelerate();
        move();
        checkInScreen();
    }

    public void move(){
        transform.Translate(Vector3.right * Time.deltaTime * plane.stats.speed);
        transform.Rotate(0,0,plane.stats.rotation);
    }

    /* public void checkInScreen(){
        if (transform.position.z < -stats.controller.maz || transform.position.z > stats.controller.maz || transform.position.x < -stats.controller.max || transform.position.x > stats.controller.max){
            transform.Translate(Vector3.left * Time.deltaTime * speed);

            var target = new Vector3(0,0,0) - transform.position;
            var rot = Vector3.Angle(target, transform.right);
            var rotationR = Vector3.Angle(target, transform.up);
            var rotationL = Vector3.Angle(target, -transform.up);
            if (rotationL < rotationR){
                rot = -rot;
            }
            
            transform.Rotate(0,0,rot);
            speed = 0;
            }
    } */

    public void checkInScreen(){
        if (transform.position.x < -plane.controller.max * 1.1f || transform.position.x > plane.controller.max * 1.1f){
            transform.position= new Vector3(-transform.position.x,0,transform.position.z);
        }

        if (transform.position.z < -plane.controller.maz * 1.1f|| transform.position.z > plane.controller.maz * 1.1f){
            transform.position= new Vector3(transform.position.x,0,-transform.position.z);
        }
    }

    

    public virtual void accelerate(){
        if (plane.stats.speed < plane.stats.maxSpeed){
            plane.stats.speed += plane.stats.maxSpeed * plane.stats.acceleration * 0.1f;
        }
    }
}
