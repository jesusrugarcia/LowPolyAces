using System;
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
        manageStatus();
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

    public void manageStatus(){
        for (int i = 0; i < planeManager.stats.statusEffects.Length; i++){
            planeManager.stats.statusEffects[i] -= Time.deltaTime;
            if(planeManager.stats.statusEffects[i] <= 0){
                planeManager.stats.statusEffects[i] = 0;
            }
        }
    }

    
}
