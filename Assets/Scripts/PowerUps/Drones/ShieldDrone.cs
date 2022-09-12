using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDrone : MonoBehaviour
{
    public PlaneManager plane;
    public float timer = 0;
    public GameObject shield;

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer >= plane.stats.auxDroneSpeed && !plane.stats.hasShield){
            timer = 0;
            plane.stats.hasShield = true;
            var shi = Instantiate(shield, plane.transform.position,Quaternion.identity);
            var shieldManager = shi.GetComponent<ShieldManager>();
            shieldManager.teamManager.team = plane.teamManager.team;
            shieldManager.target = plane.gameObject;
            plane.collissionManager.shield = shi; 
        } else if(timer >= plane.stats.auxDroneSpeed && plane.stats.hasShield){
                plane.collissionManager.shield.GetComponent<ShieldManager>().addHealth();
                timer = 0;
            }
    }
}
