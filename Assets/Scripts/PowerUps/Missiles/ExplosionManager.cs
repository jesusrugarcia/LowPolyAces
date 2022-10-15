using System;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public float time = 0;
    public float timeToDestroy = 1;
    public DamageManager damage;
    public PlaneManager plane;
    void OnCollisionEnter(Collision other)
    {
        try{
            var enemy = other.gameObject.GetComponent<PlaneManager>();
            if(enemy.teamManager.team != plane.teamManager.team && enemy.stats.statusEffects[(int)StatusEffects.Invulnerability] <= 0){
                enemy.collissionManager.damagePlane(damage: damage.damage);
            }
            
        } catch (Exception e){
            Debug.Log(e);
        }
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        transform.localScale *= 0.9f;
        if (time >= timeToDestroy){
            Destroy(gameObject);
        }
    }
}
