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
        //plane.GetComponent<PlaneStats>().mines --;
        Destroy(gameObject);
       
    }

    public void OnTriggerEnter(Collision collision)
    {
        var other = collision.gameObject.GetComponent<MissileManager>();
        if(other.explodesMines && other.teamManager.team == plane.GetComponent<TeamManager>().team){
            explosion.transform.localScale *= 2;
            explosion.GetComponent<DamageManager>().damage *= 2;
            Destroy(gameObject);
        }
       // plane.GetComponent<PlaneStats>().mines --;
        Destroy(gameObject);
       
    }

    void OnDestroy()
    {
        var planeManager = plane.GetComponent<PlaneManager>();
        var area = Instantiate(explosion, transform.position + transform.right * 1, transform.rotation);
        area.GetComponent<ExplosionManager>().plane = plane.GetComponent<PlaneManager>();
        planeManager.planeShooter.mines[pos] = null;
        planeManager.stats.mines --;

    }
}
