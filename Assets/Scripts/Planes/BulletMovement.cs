using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 30;
    public bool isMelee = false;
    public GameController controller;
    public PlaneManager plane;
    public float timer = 0;
    public float meleeOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (isMelee){
            timer += Time.deltaTime;
            if (timer >= plane.stats.meleeTime){
                Destroy(gameObject);
            }
        }
        move();
        checkInScreen();
    }

    public void move(){
        if(!isMelee){
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        } else {
            transform.position = plane.transform.position + plane.transform.right + (meleeOffset + plane.stats.extraBullets * 0.1f) * plane.transform.up;
            transform.rotation = plane.transform.rotation;
        }
    }

    public void checkInScreen(){
        if (transform.position.z < -controller.maz || transform.position.z > controller.maz || transform.position.x < -controller.max || transform.position.x > controller.max){
            Destroy(gameObject);
        }
    }
}
