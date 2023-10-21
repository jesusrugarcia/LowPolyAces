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
        if(plane.stats.statusEffects[(int)StatusEffects.Stunned] <= 0){
            transform.Translate(Vector3.right * Time.deltaTime * plane.stats.speed);
            transform.Rotate(0,0,plane.stats.rotation);
        }
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
        if (transform.position.x < -plane.controller.max - 0.1f){
            transform.position= new Vector3(plane.controller.max,0,transform.position.z);
        } else if (transform.position.x > plane.controller.max + 0.1f){
            transform.position= new Vector3(-plane.controller.max,0,transform.position.z);
        }

        if (transform.position.z < -plane.controller.maz - 0.1f){
            transform.position= new Vector3(transform.position.x,0,plane.controller.maz);
        } else if (transform.position.z > plane.controller.maz + 0.1f){
            transform.position= new Vector3(transform.position.x,0,-plane.controller.maz);
        }
    }

    

    public virtual void accelerate(){
        if (plane.stats.speed < plane.stats.maxSpeed){
            plane.stats.speed += plane.stats.maxSpeed * plane.stats.acceleration * 0.005f;
        } else if(plane.stats.speed > plane.stats.maxSpeed){
           plane.stats.speed += -plane.stats.maxSpeed * plane.stats.acceleration ;
        }
    }
}
