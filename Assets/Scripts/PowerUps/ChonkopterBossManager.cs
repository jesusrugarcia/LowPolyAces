using System;
using UnityEngine;

public class ChonkopterBossManager : BossManager
{
    const int DRONES = 0;
    const int LASERS = 1;
    const int DAMAGE = 2;

    public float timer = 0;
    public float damageTime = 10;

    public float prepTime = 5f;
    public float laserTime = 1f;
    public bool lasering = false;
    public bool changedPosition = false;
    public Material laserColor;
    public Material prepColor;

    // Start is called before the first frame update
    public override void Start()
    {
        phase = DRONES;
        plane.stats.maxHealth = plane.controller.gameOptions.playerNum * 50;
        plane.stats.health = plane.controller.gameOptions.playerNum * 50;
    }

    public override void activatePhase(){
        phaseActivated = true;
        if (phase == DRONES){
            activateShield();
            summonMinions();
        } else if (phase == LASERS){
            activateLasers();
        }
    }

    public override void deactivatePhase(){
        if (phase == DRONES && summonedMinions == 0){
            phase = LASERS;
            phaseActivated = false;
        } else if (phase == LASERS){
            activateLasers();
        } else if(phase == DAMAGE){
            timer += Time.deltaTime;
            if(timer >= damageTime){
                timer = 0;
                phaseActivated = false;
                phase = DRONES;
            }
        }
    }

    public void activateLasers(){
        timer += Time.deltaTime;
        if(!changedPosition){
            laserTarget(Quaternion.Euler(0,45,0) * Vector3.right, 20);
            laserTarget(Quaternion.Euler(0,-45,0) * Vector3.right, 20);
            laserTarget(Quaternion.Euler(0,135,0) * Vector3.right, 20);
            laserTarget(Quaternion.Euler(0,-135,0) * Vector3.right, 20);
        } else {
            laserTarget(Quaternion.Euler(0,0,0) * Vector3.right, 20);
            laserTarget(Quaternion.Euler(0,90,0) * Vector3.right, 20);
            laserTarget(Quaternion.Euler(0,180,0) * Vector3.right, 20);
            laserTarget(Quaternion.Euler(0,-90,0) * Vector3.right, 20);
        }

        if(lasering && timer >= laserTime){
            if(!changedPosition){
                    lasering = false;
                    changedPosition = true;
                    timer = 0; 
                } else {
                    timer = 0;
                    phaseActivated = false;
                    changedPosition = false;
                    lasering = false;
                    phase = DAMAGE;
                    deactivateShield();
                }
        } else if(!lasering && timer >= prepTime) {
            lasering = true;
            timer = 0;
        }
    }

    public void laserTarget(Vector3 direction, float distance, float thickness = 0){
        if(lasering){
            TargetResult target;
            if( thickness == 0){
                target = SearchTarget.searchTarget(gameObject, distance, direction);
            } else {
                target = SearchTarget.searchTarget(gameObject, thickness, distance, direction);
            }
            var found = target.target.GetComponent<PlaneManager>();
            if(found.teamManager.team != plane.teamManager.team && found.stats.statusEffects[(int)StatusEffects.Invulnerability] <= 0){
                found.collissionManager.damagePlane(damage: plane.stats.laserDamage);
            }
        }

        float thicknessLaser = 0.1f;
        if(lasering){
            thicknessLaser += thickness + 0.25f;
        } 

        drawLine(gameObject.transform.position, direction * distance, 0.025f, thicknessLaser);
    }

    public void drawLine(Vector3 start, Vector3 end, float duration, float thickness){
        if (thickness == 0){
            thickness = 0.01f;
        }

        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        if(lasering){
            lineRenderer.startColor = laserColor.color;
            lineRenderer.endColor = laserColor.color;
            lineRenderer.material = laserColor;
        } else {
            lineRenderer.startColor = prepColor.color;
            lineRenderer.endColor = prepColor.color;
            lineRenderer.material = prepColor;
        }
        
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        lineRenderer.SetPosition(0,start);
        lineRenderer.SetPosition(1,end);
        Destroy(line,duration);
    }
}
