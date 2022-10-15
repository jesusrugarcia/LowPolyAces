using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffects{
    Invulnerability ,
    Ghost ,
    Stunned ,
    Burning,
    Rusting,
}

public class StatusEffect : MonoBehaviour
{
    public PlaneManager planeManager;

    void FixedUpdate()
    {
        for (int i = 0; i < planeManager.stats.statusEffects.Length; i++){
            planeManager.stats.statusEffects[i] -= Time.deltaTime;
            if(planeManager.stats.statusEffects[i] < 0){
                planeManager.stats.statusEffects[i] = 0;
            }
        }
    }

    public void addStatus(StatusEffects type, float time){
        if (planeManager.stats.statusEffects[(int)type] < time){
            planeManager.stats.statusEffects[(int)type] = time;
        }
        
        //Debug.Log("Inv: " + planeManager.stats.statusEffects[(int)type]);
    }

    public void removeStatus(StatusEffects type){
        planeManager.stats.statusEffects[(int)type] = 0;
        
        //Debug.Log("Inv: " + planeManager.stats.statusEffects[(int)type]);
    }
}
