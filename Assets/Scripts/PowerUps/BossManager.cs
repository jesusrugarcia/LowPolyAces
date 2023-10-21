using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public PlaneManager plane;
    public int phase;
    public bool phaseActivated = false;

    public int summonedMinions = 0;
    public PlanesListScriptableObject minionsList;

    public GameObject bossShield;

    public float behindTimer = 0f;
    public float maxBehindTime = 5f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void FixedUpdate()
    {
        if(!phaseActivated){
            activatePhase();
        } else {
            deactivatePhase();
        }
    }

    public virtual void activatePhase(){
        phaseActivated = true;
    }

    public virtual void deactivatePhase(){

    }

    public virtual void activateShield(){
        bossShield.SetActive(true);
        bossShield.GetComponent<TeamManager>().team = plane.teamManager.team;
        bossShield.GetComponent<BossShieldManager>().plane = plane;
        plane.statusManager.addStatus(StatusEffects.Invulnerability, 10000);
    }

    public virtual void deactivateShield(){
        bossShield.SetActive(false);
        plane.statusManager.removeStatus(StatusEffects.Invulnerability);
    }

    public void summonMinions(int waves = 1){
        for (int i = 0; i < plane.controller.players.Length * waves; i++){
            var player = plane.controller.players[i];
            if(player.activeSelf){
                var minion = summonMinion(player);
                minion.GetComponent<EnemyControllerTracking>().objective = player.transform;
                minion.GetComponent<PlaneStats>().speed =  minion.GetComponent<PlaneStats>().maxSpeed;
            }
        }
    }

    public GameObject summonMinion(GameObject player){
        var dronePrefab = getRandomMinion();
        var drone = plane.controller.enemySpawner.spawner.spawnPlane(dronePrefab,null,movement.Tracking,plane.teamManager.team, gameObject);
        var minion = drone.GetComponent<BossMinionManager>();
        minion.bossManager = this;
        summonedMinions ++;
        return drone;
    }

    public PlaneScriptableObject getRandomMinion(){
        var droneToGet = UnityEngine.Random.Range(0, minionsList.planes.Length);
        return minionsList.planes[droneToGet];
    }

    public void checkBehind(int angle){
        for (int i = 0; i < plane.controller.gameOptions.playerNum ; i++){
            try {
                Vector3 objective = plane.controller.players[i].transform.position - transform.position;
                var rotation = Vector3.Angle(objective, transform.right);
                var debAngle = Quaternion.Euler(0, angle, 0) * transform.right * 10;
                var negDebAngle = Quaternion.Euler(0, -angle, 0) * transform.right * 10;
                Debug.DrawRay(transform.position, debAngle, Color.green);
                Debug.DrawRay(transform.position, negDebAngle, Color.magenta);
                Debug.DrawRay(transform.position, objective, Color.yellow);
                if (rotation >= angle){
                    Debug.Log(rotation);
                    behindTimer += Time.deltaTime;
                    break;
                }
                behindTimer = 0;
            } catch (System.Exception e){
                Debug.Log(e);
            }
        }
    }



}
