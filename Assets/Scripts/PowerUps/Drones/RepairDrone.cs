using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairDrone : MonoBehaviour
{
    public PlaneManager plane;
    public bool statusRepair = true;
    public float timer = 0;

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer >= plane.stats.auxDroneSpeed){
            if( plane.stats.health < plane.stats.maxHealth){
                plane.stats.health ++;
                timer = 0;
            }
            if (statusRepair){
                for(int i = 0; i < plane.stats.statusEffects.Length; i++){
                if(plane.stats.statusEffects[i] > 0){
                    timer = 0;
                    plane.stats.statusEffects[i] = 0;
                }
            }
            }
        }
    }
}
