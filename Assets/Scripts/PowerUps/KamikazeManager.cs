using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeManager : MonoBehaviour
{
    public float damage = 10;
    public PlaneManager plane;
    public GameObject explosion;
    void OnTriggerEnter(Collider other)
    {
        try {
            if(other.gameObject.GetComponent<TeamManager>().team != plane.teamManager.team){
            other.GetComponent<CollisionManager>().damagePlane(damage: damage);
            spawnExplosion();
            plane.collissionManager.damagePlane(damage: 100);
            }
        } catch (System.Exception e){
            Debug.Log(e);
        }
        
    }

    public void spawnExplosion(){
        var area = Instantiate(explosion, transform.position, Quaternion.identity);
        area.GetComponent<ExplosionManager>().plane = plane;
    }
}
