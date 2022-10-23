using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject plane;
    public GameObject explosion;
    public int pos;

    public void OnCollisionEnter(Collision collision)
    {
        spawnExplosion();
        //plane.GetComponent<PlaneStats>().mines --;
        Destroy(gameObject);
       
    }

    public void OnTriggerEnter(Collision collision)
    {
        spawnExplosion();
        Destroy(gameObject);
       
    }

    void OnDestroy()
    {
        spawnExplosion();
        var planeManager = plane.GetComponent<PlaneManager>();
        planeManager.planeShooter.mines[pos] = null;
        planeManager.stats.mines --;

    }

    public GameObject spawnExplosion(){
        var area = Instantiate(explosion, transform.position, transform.rotation);
        area.GetComponent<ExplosionManager>().plane = plane.GetComponent<PlaneManager>();
        
        return area;
    }

    public void spawnBiggerExplosion(){
        var area = spawnExplosion();
        area.transform.localScale *= 2;
        area.GetComponent<DamageManager>().damage *= 2;
    }
}
