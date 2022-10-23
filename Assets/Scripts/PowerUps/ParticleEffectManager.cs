using System;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    public PlaneManager plane;
    public StatusEffects type;

    void FixedUpdate()
    {
        try{
            transform.position = plane.transform.position;
            if(plane.stats.statusEffects[(int)type] <= 0 ){
                Destroy(gameObject);
            }
        }catch(Exception e){
            Debug.Log(e);
            Destroy(gameObject);
        }
    }
}
