using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlaneMovement : PlaneMovement
{
    public PlayerInput input;
    public InputAction acceleateAction;
    public InputAction decelerateAction;
    public InputAction missileAction;


    float minSpeed = 1;
    float accelerationValue = 0.25f;
    


    // Start is called before the first frame update
    void Start()
    {
        plane = GetComponent<PlaneManager>();
        plane.planeMovement = this;

        input = GetComponent<PlayerInput>();
        if (plane.controller.gameOptions.playerNum > 1){
            input.neverAutoSwitchControlSchemes = true;
        } else {
            input.neverAutoSwitchControlSchemes = false;
        }

        var map = input.currentActionMap;
        acceleateAction = map.FindAction("Accelerate",true);
        decelerateAction = map.FindAction("Decelerate",true);
        missileAction = map.FindAction("Missile",true);
    }

    public void OnRotate(InputAction.CallbackContext context){
        movement = context.ReadValue<Vector2>();
        //Debug.Log(movement);
    }

    // Update is called once per frame
    void Update()
    {
        plane.stats.rotation = movement.x * plane.stats.rotationSpeed;
        
    }

    static bool isDown(InputAction action) => action.phase == InputActionPhase.Performed;
    static bool isUp(InputAction action) => action.phase == InputActionPhase.Canceled;

    public override void accelerate(){
        if (isDown(acceleateAction)){
            if (plane.stats.speed < plane.stats.maxSpeed){
            plane.stats.speed += plane.stats.maxSpeed * plane.stats.acceleration * accelerationValue;
            }
        } else if(isDown(decelerateAction)){
            if (plane.stats.speed > minSpeed){
            plane.stats.speed += -plane.stats.maxSpeed * plane.stats.acceleration * accelerationValue;
            }
        } else if (plane.stats.speed < plane.stats.maxSpeed/2 ){
             plane.stats.speed += plane.stats.maxSpeed * plane.stats.acceleration * accelerationValue;
        } else if (plane.stats.speed > plane.stats.maxSpeed/2 ){
             plane.stats.speed += -plane.stats.maxSpeed * plane.stats.acceleration * accelerationValue;
         }
    }

    public void OnMissile(){
        if(isUp(missileAction)){
            if(plane.stats.missiles > 0){
            plane.planeShooter.launchMissile();
            }
        }
        
    }
}
