using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaded : MonoBehaviour
{
    public float timer = 0;
    public float timeToDeactivate = 1;

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= timeToDeactivate){
            gameObject.SetActive(false);
        }
    }
}
