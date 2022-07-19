using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlaneShooter : PlaneShooter
{
    public PlayerInput input;
    public InputAction missileAction;
    public InputAction shootAction;

    public bool magazineFull = true;
    public int magazine;
    public bool stoppedShooting = true;

    static bool isDown(InputAction action) => action.phase == InputActionPhase.Performed;
    static bool isUp(InputAction action) => action.phase == InputActionPhase.Canceled;

    private void Start() {
        var map = input.currentActionMap;
        missileAction = map.FindAction("Missile",true);
        shootAction = map.FindAction("Shoot",true);
        magazine = plane.stats.magazineSize;
        shootTimer = plane.stats.shootSpeed;
    }
    public void OnMissile(){
        if(isUp(missileAction)){
            if(plane.stats.missiles > 0){
            plane.planeShooter.launchMissile();
            }
            if(plane.stats.missiles <= 0){
                plane.healthBar.MissileIcon.SetActive(false);
            }
        }
        
    }

    public override void checkShoot(){
        if(type == shootingType.onButton){
            onButtonShoot();
        } else if(type == shootingType.normal){
            shootTimer += Time.deltaTime;
        if (shootTimer >= plane.stats.shootSpeed){
            shootTimer = 0;
            shoot();
        }
        }
        
    }

    public void onButtonShoot(){
        onShoot();
        if(magazineFull == false){
            shootTimer += Time.deltaTime;
            if (shootTimer >= plane.stats.shootSpeed){
                magazineFull = true;
                magazine = plane.stats.magazineSize;
            }
        }
    }

    public void onShoot(){
        if(isDown(shootAction) && magazineFull == true && magazine > 0 && stoppedShooting){
            shoot();
            magazine += -1;
            if (magazine <= 0){
                magazineFull = false;
                stoppedShooting = false;
                shootTimer = 0;
            } else {
                shootTimer = plane.stats.shootSpeed * (magazine/plane.stats.magazineSize);
            } 
        } else if(isUp(shootAction)){
            magazineFull = false;
            stoppedShooting = true;
        }
    }
}
