using System;
using System.Collections.Generic;
using UnityEngine;



public class PowerUpsManager : MonoBehaviour
{
    public int min = 1;
    public int max = 2;

    public PowerUpsCentralManager manager;

    public PowerUps type;
    // Start is called before the first frame update

    private void FixedUpdate() {
        randomRotation();
    }

    public void randomRotation(){
        transform.Rotate(UnityEngine.Random.Range(min,max),UnityEngine.Random.Range(min,max),UnityEngine.Random.Range(min,max));
    }

    private void OnCollisionEnter(Collision other) {
        manager.managePowerUp(other, type, gameObject);
    }


    
}
