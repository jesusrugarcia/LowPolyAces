using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject previous;
    public GameObject next;

    public PlaneManager plane;
    public TeamManager teamManager;

    public float rotationSpeed = 1;
    public float targetDistance = 10;

    public void rotateTowards(){
        if(next != null){
            transform.LookAt(next.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<TeamManager>().team == plane.teamManager.team){
            rotateTowards();
            var mm = other.gameObject.GetComponent<MissileManager>();
            var bm = other.gameObject.GetComponent<BulletMovement>();
            if(mm != null){
               mm.target = next;
               mm.speed *= 1.1f;
               mm.maxRotation *= 1.5f;
               mm.GetComponent<DamageManager>().damage *= 1.5f;
            } else if (bm != null){
                bm.rotateTowards(next);
                bm.speed *= 1.2f;
                bm.GetComponent<DamageManager>().damage *= 1.5f;
            }
            updatePrevious();
            
            Destroy(gameObject);
            
        }
    }

    public void updatePrevious(){
        try{
            if(previous == null){
            plane.planeShooter.firstCoin = next;
        } else {
            next.GetComponent<CoinManager>().previous = previous;
            previous.GetComponent<CoinManager>().next = next;
        }
        next.GetComponent<CoinManager>().updatePrevious();
        } catch (Exception e){
            Debug.Log(e);
        }
    }

    void FixedUpdate()
    {
        try{
            var con = next.GetComponent<CoinManager>();
            if(next == null){
            searchTarget();
            rotate(2);
            } else if(con != null) {
                rotate();
            } else {
                rotateTowards();
            }
        
        } catch(Exception e){
            Debug.Log(e);
            searchTarget();
            rotate(2);
        }
        
    }

    public void searchTarget(){
        try{
            Debug.DrawRay(transform.position,transform.forward * (targetDistance + plane.stats.searchDistance),Color.red);
        var detected = SearchTarget.searchTarget(gameObject, targetDistance + plane.stats.searchDistance, transform.forward).target;
        if (detected != gameObject){
            next = detected;
        } else if (Vector3.Distance(transform.position, next.transform.position) > (targetDistance + plane.stats.searchDistance)){
            next = null;
        }
        } catch(Exception e){
            Debug.Log(e);
        }
    }

    public void rotate(float speed = 0){
        if(speed != 0){
            transform.Rotate(0,rotationSpeed * speed,0);
        } else {
            transform.Rotate(0,rotationSpeed,0);
        }
    }
}
