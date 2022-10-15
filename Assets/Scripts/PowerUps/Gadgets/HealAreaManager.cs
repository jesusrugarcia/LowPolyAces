using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAreaManager : MonoBehaviour
{
    public PlaneManager plane;
    public float time = 0;

    public float timeHealing = 3f;
    // Start is called before the first frame update
   

    private void FixedUpdate() {
        transform.position = plane.transform.position;
        time += Time.deltaTime;
        if ( time >= plane.stats.SpecialShieldDuration){
            destroyShield();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        var ally = other.gameObject.GetComponent<PlaneManager>();
        if (ally.teamManager.team == plane.teamManager.team){
            ally.collissionManager.healing = timeHealing;
            ally.collissionManager.healAmount = plane.stats.healAreaAmount;
        }
    }

    void OnCollisionExit(Collision other)
    {
        //var ally = other.gameObject.GetComponent<PlaneManager>();
        //if (ally.teamManager.team == plane.teamManager.team){
        //    ally.collissionManager.healing = false;
        //}
    }

    public void destroyShield(){
        Destroy(gameObject);
    }
}
