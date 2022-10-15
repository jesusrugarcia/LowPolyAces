using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlaneShooter : PlaneShooter
{
    public PlayerInput input;
    public InputAction missileAction;
    public InputAction shootAction;
    public InputAction gadgetAction;
    public InputAction defenseAction;


    static bool isDown(InputAction action) => action.phase == InputActionPhase.Performed;
    static bool isUp(InputAction action) => action.phase == InputActionPhase.Canceled;

    private void Start() {
        var map = input.currentActionMap;
        missileAction = map.FindAction("Missile",true);
        shootAction = map.FindAction("Shoot",true);
        gadgetAction = map.FindAction("Gadget",true);
        defenseAction = map.FindAction("Defense",true);
        magazine = plane.stats.magazineSize;
        shootTimer = plane.stats.shootSpeed;
    }
    public void OnMissile(){
        if(isUp(missileAction)){
            if(plane.stats.specialAmmo > 0){
            plane.planeShooter.launchMissile();
            }
            if(plane.stats.specialAmmo <= 0){
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

    public override void onButtonShoot(){
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
        shootTimer += Time.deltaTime;
        if(isDown(shootAction) && magazineFull == true && magazine > 0  && shootTimer > shootSpeedOnButton){
            shootTimer = 0;
            shoot();
            magazine += -1;
            if (magazine <= 0){
                magazineFull = false;
            } 
        } else if(isUp(shootAction)){
            magazineFull = false;
        }
    }

    public void onDefense(){
        Debug.Log("OnDefense");
        if(isUp(defenseAction)){
            if(plane.stats.defenseAmmo > 0){
            plane.planeShooter.Defense();
            }
        }
    }

    public void onGadget(){
        Debug.Log("OnGadget");
        if(isUp(gadgetAction)){
            if(plane.stats.specialAmmo > 0){
            plane.planeShooter.launchGadget();
            }
            if(plane.stats.specialAmmo <= 0){
                plane.healthBar.MissileIcon.SetActive(false);
            }
        }
    }
}
