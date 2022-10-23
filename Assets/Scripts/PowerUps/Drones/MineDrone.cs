using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDrone : MonoBehaviour
{
    public PlaneManager plane;
    public GameObject Mine;
    public float timer = 0f;

    private void Start(){
        
    }

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if (timer >= plane.stats.specialDroneShootSpeed  ){//&& plane.stats.mines < plane.stats.maxMines){
            var mine = Instantiate(Mine, transform.position, Quaternion.Euler(-90,0,0));
            mine.GetComponent<TeamManager>().team = plane.teamManager.team;
            mine.GetComponent<DamageManager>().damage = plane.stats.mineDamage;
            timer = 0;
            mine.GetComponent<Mine>().plane = plane.gameObject;
            plane.stats.mines ++;
        }
    }
}
