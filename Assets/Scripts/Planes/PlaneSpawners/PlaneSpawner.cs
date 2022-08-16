using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movement {
    Player = 0,
    Easy = 1,
    Medium = 2,
    Hard = 3,
    Impossible = 4
}

public class PlaneSpawner : MonoBehaviour
{
    public GameController controller;
    public GameObject healthBar;
    public GameObject healthBarEnemy;

    public GameObject spawnPlane(PlaneScriptableObject planeModel, movement movement, int team){
        Time.timeScale = 0f;
        var plane = Instantiate(planeModel.plane, getRandomPosition(), Quaternion.Euler(-90,0,0)); //rotation might need to change when i have good models.
        calculateRotation(plane);
        //plane.AddComponent<PlaneManager>();
        var planeManager = plane.GetComponent<PlaneManager>();
        planeManager.controller = controller;
        planeManager.teamManager.team = team;

        GameObject health;
        if (movement != movement.Player){
            health = Instantiate(healthBarEnemy,plane.transform.position, Quaternion.Euler(270,0,0));
            controller.currentEnemies ++;

            
        } else {
            health = Instantiate(healthBar,plane.transform.position, Quaternion.Euler(270,0,0));
        }
        planeManager.planeShooter.bullet.GetComponent<TeamManager>().team = planeManager.teamManager.team;
        var healthComponent = health.GetComponent<HealthBar>();
        healthComponent.plane = plane.GetComponent<PlaneManager>();
        plane.GetComponent<PlaneManager>().healthBar = healthComponent;

        copyStats(plane, planeModel, movement);
        addMovement(plane, movement);
        Time.timeScale = 1f;
        return plane;
    }


    public void copyStats(GameObject plane, PlaneScriptableObject planeModel, movement movement){
        var stats = plane.GetComponent<PlaneStats>();
        
        stats.maxHealth = planeModel.stats.maxHealth;
        stats.health = planeModel.stats.health;

        stats.maxSpeed = planeModel.stats.maxSpeed;
        stats.speed = planeModel.stats.speed;
        stats.acceleration = planeModel.stats.acceleration;

        stats.rotationSpeed = planeModel.stats.rotationSpeed;
        stats.rotation = planeModel.stats.rotation;
        stats.maxRotation = planeModel.stats.maxRotation;

        stats.bulletDamage = planeModel.stats.bulletDamage;
        stats.damageTurret = planeModel.stats.damageTurret;

        stats.shootSpeed = planeModel.stats.shootSpeed;
        stats.magazineSize = planeModel.stats.magazineSize;
        stats.turretShootSpeed = planeModel.stats.turretShootSpeed;

        stats.maxMissiles = planeModel.stats.maxMissiles;
        stats.missiles = planeModel.stats.missiles;

        stats.timeToRotate = planeModel.stats.timeToRotate;
        stats.timeRotating = planeModel.stats.timeRotating;

        stats.scoreValue = planeModel.stats.scoreValue;
        stats.price = planeModel.stats.price; 

        if(movement == movement.Player){ //to be ajusted for balance
            stats.maxHealth += 5;
            stats.health += 5;
        } else if(movement == movement.Medium){
            stats.maxHealth += 1;
            stats.health += 1;
        } else if(movement == movement.Hard){
            stats.maxHealth += 1;
            stats.health += 1;
        } else if(movement == movement.Impossible){
            stats.maxHealth += 1;
            stats.health += 1;
        }
    }

    public void addMovement(GameObject plane, movement movement){
       if (movement == movement.Easy){
            plane.AddComponent<EnemyControllerRandom>();
        } else if (movement == movement.Medium){
            plane.AddComponent<EnemyControllerTracking>();
            //plane.GetComponent<EnemyControlMedium>().objective = controller.player.transform;
        } else if (movement == movement.Hard){
            plane.AddComponent<EnemyControllerTracking>();
            //plane.GetComponent<EnemyControlHard>().objective = controller.player.transform;
        } else if (movement == movement.Impossible){
            plane.AddComponent<EnemyControllerTracking>();
           // plane.GetComponent<EnemyControllerImposible>().objective = controller.player.transform;
        }
    }

    public Vector3 getRandomPosition(){
        float x = UnityEngine.Random.Range(-1,1);
        float z = UnityEngine.Random.Range(-1,1);

        if (x < 0) {x = -controller.max + 0.5f;} else {x = controller.max - 0.5f;}
        if (z < 0) {z = -controller.maz + 0.5f;} else {z = controller.maz - 0.5f;}

        return new Vector3(x,0,z);
    }

    public void calculateRotation(GameObject Plane){
        Vector3 dir;
        var target = new Vector3(0,0,0) - Plane.transform.position;
        
        if(Plane.transform.position.z > 0){
            dir = new Vector3(-90,(Vector3.Angle(target, Plane.transform.right)),0);
        } else {
            dir = new Vector3(-90,-(Vector3.Angle(target, Plane.transform.right)),0);
        }
        
        //Debug.Log(dir);
        Plane.transform.rotation = Quaternion.Euler(dir);
    }
}
