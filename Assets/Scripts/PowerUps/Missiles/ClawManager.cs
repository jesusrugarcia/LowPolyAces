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
                addParticleEffect(enemy);
            }
        } catch(Exception e){
            Debug.Log(e);
        }
    }

    public void addParticleEffect(PlaneManager enemy){
        if(type == StatusEffects.Stunned){
            var particles = Instantiate(plane.controller.centralManager.StunnedParticleEffect, transform.position, transform.rotation);
            particles.GetComponent<ParticleEffectManager>().plane = enemy;
        } else if(type == StatusEffects.Burning){
            var particles = Instantiate(plane.controller.centralManager.BurningParticleEffect, transform.position, transform.rotation);
            particles.GetComponent<ParticleEffectManager>().plane = enemy;
        } else if(type == StatusEffects.Rusting){
            var particles = Instantiate(plane.controller.centralManager.RustingParticleEffect, transform.position, transform.rotation);
            particles.GetComponent<ParticleEffectManager>().plane = enemy;
        }
    }
}
