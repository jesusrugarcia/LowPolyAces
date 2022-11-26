using System;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public GameObject plane;
    public float maxDistance = 2f;
    public float speed = 6f;

    public float tpDistance = 1f;

    public bool ready = false;
    

    void FixedUpdate()
    {
        checkPlaneAlive();
        chechFarAway();
        rotate();
        move();
        //orbit();
        
    }

    public void checkPlaneAlive(){
        if(ready){
            try{
                if(plane == null || plane.gameObject.activeSelf == false){
                    Destroy(gameObject);
                }
            } catch (Exception e){
                Destroy(gameObject);
                Debug.Log(e);
            }
            
        }
    }

    public void move(){
        speed = plane.GetComponent<PlaneStats>().speed;
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void rotate(){
        transform.rotation = plane.transform.rotation;
    }

    


    public void chechFarAway(){
     if(Vector3.Distance(transform.position, plane.transform.position) > maxDistance){
            var x = plane.transform.position.x + UnityEngine.Random.Range(-tpDistance, tpDistance);
            var z = plane.transform.position.z + UnityEngine.Random.Range(-tpDistance, tpDistance);
            transform.position = new Vector3(x,0,z);
        }
     
    }

    void OnCollisionEnter(Collision other)
    {
        
    }
}
