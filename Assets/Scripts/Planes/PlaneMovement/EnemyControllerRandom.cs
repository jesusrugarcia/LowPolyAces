using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerRandom : PlaneMovement
{
    void Update()
    {
        plane.stats.rotation += Random.Range(-0.5f,0.5f);
        checkRotation();
    }


    public void checkRotation(){
        if( plane.stats.rotation < -plane.stats.maxRotation || plane.stats.rotation > plane.stats.maxRotation){
            plane.stats.rotation = 0;
        }
    }
}
