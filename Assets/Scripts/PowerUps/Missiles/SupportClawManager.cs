using System;
using UnityEngine;

public class SupportClawManager : MonoBehaviour
{
    public PlaneManager plane;
    void OnTriggerEnter(Collider other)
    {
        try{
            var ally = other.gameObject.GetComponent<PlaneManager>();
            if (ally.teamManager.team == plane.teamManager.team){
                for (int i = (int)StatusEffects.Ghost+1; i < ally.stats.statusEffects.Length; i++){
                    ally.stats.statusEffects[i] = 0;
                }
                if (ally.stats.health < ally.stats.maxHealth){
                    ally.stats.health ++;
                }
                
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
    }
}
