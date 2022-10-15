using System;
using UnityEngine;

public class ClawManager : MonoBehaviour
{
    public PlaneManager plane;
    public StatusEffects type;

    void OnTriggerEnter(Collider other)
    {
        try{
            var enemy = other.gameObject.GetComponent<PlaneManager>();
            if(enemy.teamManager.team != plane.teamManager.team){
                enemy.statusManager.addStatus(type, plane.stats.statusEffectTime);
            }
        } catch(Exception e){
            Debug.Log(e);
        }
    }
}
