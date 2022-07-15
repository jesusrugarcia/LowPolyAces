using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 30;
    public GameController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        move();
        checkInScreen();
    }

    public void move(){
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void checkInScreen(){
        if (transform.position.z < -controller.maz || transform.position.z > controller.maz || transform.position.x < -controller.max || transform.position.x > controller.max){
            Destroy(gameObject);
        }
    }
}
