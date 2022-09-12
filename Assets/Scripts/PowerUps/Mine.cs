using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject plane;

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        plane.GetComponent<PlaneStats>().mines --;
    }

    public void OnTriggerEnter(Collision collision)
    {
        Destroy(gameObject);
        plane.GetComponent<PlaneStats>().mines --;
    }
}
