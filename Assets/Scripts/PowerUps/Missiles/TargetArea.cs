using System;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public float time = 0;
    public float timeToDestroy = 1;
    public PlaneManager plane;
    void OnCollisionEnter(Collision other)
    {
        try{
            var enemy = other.gameObject.GetComponent<EnemyControllerTracking>();
            enemy.objective = plane.transform;
            enemy.rotating = true;
            enemy.time = -plane.stats.timeTargetting;
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
