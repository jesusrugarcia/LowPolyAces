using System;
using UnityEngine;

public enum movement {
    Player = 0,
    NoTracking = 1,
    Tracking = 2,
}

public class PlaneSpawner : MonoBehaviour
{
    public GameController controller;
    public GameObject healthBar;
    public GameObject healthBarEnemy;

    public GameObject spawnPlane(PlaneScriptableObject planeModel, movement movement, int team, GameObject origin = null, StatsSave stats = null){
        Time.timeScale = 0f;
        GameObject plane;
        if (origin != null){
            var x = origin.transform.position.x + UnityEngine.Random.Range(-1f,1f);
            var z = origin.transform.position.z + UnityEngine.Random.Range(-1f,1f);
            plane = Instantiate(planeModel.plane, new Vector3(x,0,z) , origin.transform.rotation); //rotation might need to change when i have good models.
        } else {
            plane = Instantiate(planeModel.plane, getRandomPosition(), Quaternion.Euler(-90,0,0)); //rotation might need to change when i have good models.
        }
        
        calculateRotation(plane);
        //plane.AddComponent<PlaneManager>();
        var planeManager = plane.GetComponent<PlaneManager>();
        planeManager.controller = controller;
        planeManager.teamManager.team = team;

        if(stats == null){
            copyStats(plane, planeModel, movement);
            planeManager.stats.hasShield = planeModel.stats.hasShield;
        } else {
            planeManager.stats.copyStats(stats);
            initialInv(plane, movement);
        }
        
        addMovement(plane, movement);
        controller.centralManager.initialSetUp(plane.GetComponent<PlaneManager>());

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

        
        Time.timeScale = 1f;
        return plane;
    }


    public void copyStats(GameObject plane, PlaneScriptableObject planeModel, movement movement){
        var stats = plane.GetComponent<PlaneStats>();
        
        stats.maxHealth = planeModel.stats.maxHealth;
        stats.health = planeModel.stats.health;
        if(stats.health < 1){
            stats.health = 1;
        }

        stats.maxSpeed = planeModel.stats.maxSpeed;
        stats.speed = planeModel.stats.speed;
        stats.acceleration = planeModel.stats.acceleration;

        stats.rotationSpeed = planeModel.stats.rotationSpeed;
        stats.rotation = planeModel.stats.rotation;
        stats.maxRotation = planeModel.stats.maxRotation;
        stats.timeToRotate = planeModel.stats.timeToRotate;
        stats.timeRotating = planeModel.stats.timeRotating;

        stats.bulletDamage = planeModel.stats.bulletDamage;
        stats.turretDamage = planeModel.stats.turretDamage;
        stats.missileDamage = planeModel.stats.missileDamage;
        stats.mineDamage = planeModel.stats.mineDamage;
        stats.drillDamage = planeModel.stats.drillDamage;
        

        stats.shootSpeed = planeModel.stats.shootSpeed;
        stats.magazineSize = planeModel.stats.magazineSize;
        stats.turretShootSpeed = planeModel.stats.turretShootSpeed;
        stats.normalDroneShootSpeed = planeModel.stats.normalDroneShootSpeed;
        stats.specialDroneShootSpeed = planeModel.stats.specialDroneShootSpeed;
        stats.auxDroneSpeed = planeModel.stats.auxDroneSpeed;

        stats.bulletType = planeModel.stats.bulletType;
        stats.missileType = planeModel.stats.missileType;
        stats.gadgetType = planeModel.stats.gadgetType;
        stats.defenseType = planeModel.stats.defenseType;
        
        stats.maxSpecialAmmo = planeModel.stats.maxSpecialAmmo;
        stats.specialAmmo = planeModel.stats.specialAmmo;
        stats.maxDefenseAmmo = planeModel.stats.maxDefenseAmmo;
        stats.defenseAmmo = planeModel.stats.defenseAmmo;
        stats.extraBullets = planeModel.stats.extraBullets;
        stats.maxMines = planeModel.stats.maxMines;
        stats.mines = planeModel.stats.mines;
        stats.meleeTime = planeModel.stats.meleeTime;
        stats.trackerBullet = planeModel.stats.trackerBullet;
        stats.searchDistance = planeModel.stats.searchDistance;

        stats.scoreValue = planeModel.stats.scoreValue;
        stats.price = planeModel.stats.price; 

        stats.hasShield = planeModel.stats.hasShield;
        stats.damageReductionTankShield = planeModel.stats.damageReductionTankShield;
        stats.SpecialShieldDuration = planeModel.stats.SpecialShieldDuration;
        stats.healAreaAmount = planeModel.stats.healAreaAmount;
        stats.rechargeDefenseTime = planeModel.stats.rechargeDefenseTime;
        stats.rechargeSpecialTime = planeModel.stats.rechargeSpecialTime;
        
        stats.maxDrones = planeModel.stats.maxDrones;
        stats.drones = planeModel.stats.drones;
        stats.dronesList = planeModel.stats.dronesList;
        try {
            if (stats.dronesList.Length == 0){
                stats.dronesList = new PowerUps[stats.maxDrones];
            }
        } catch(Exception e){
            Debug.Log(e);
            stats.dronesList = new PowerUps[stats.maxDrones];
        }

        stats.statusEffects = new float[Enum.GetNames(typeof(StatusEffects)).Length];//planeModel.stats.statusEffects;
        stats.invIncrease = planeModel.stats.invIncrease;
        stats.ghostIncrease = planeModel.stats.ghostIncrease;
        stats.evasion = planeModel.stats.evasion;
        stats.timeTargetting = planeModel.stats.timeTargetting;
        stats.statusEffectTime = planeModel.stats.statusEffectTime;

        stats.laserActivated = planeModel.stats.laserActivated;
        stats.laserTime = planeModel.stats.laserTime;
        stats.laserDamage = planeModel.stats.laserDamage;

        initialInv(plane, movement);
    }

    public void initialInv(GameObject plane, movement movement){
        var stats = plane.GetComponent<PlaneStats>();
        if(movement == movement.Player){ //to be ajusted for balance
           stats.statusEffects[(int)StatusEffects.Invulnerability] = 1;
           var particles = Instantiate(controller.centralManager.InvulnerabilityParticleEffect, transform.position, transform.rotation);
          particles.GetComponent<ParticleEffectManager>().plane = plane.GetComponent<PlaneManager>();
        }
    }

    public void addMovement(GameObject plane, movement movement){
       if (movement == movement.NoTracking){
            plane.AddComponent<EnemyControllerRandom>();
        } else if (movement == movement.Tracking){
            plane.AddComponent<EnemyControllerTracking>();
            //plane.GetComponent<EnemyControlMedium>().objective = controller.player.transform;
        }
    }

    public Vector3 getRandomPosition(){
        var election = UnityEngine.Random.Range(0,2);
        float x;
        float z;
        if(election == 0){ // x = max or -max
            election = UnityEngine.Random.Range(0,2);
            if(election == 0){
                x = controller.max;
            } else {
                x = -controller.max;
            }
            z = UnityEngine.Random.Range(-controller.maz + 0.5f,controller.maz - 0.5f);

        } else {
            election = UnityEngine.Random.Range(0,2);
            if(election == 0){
                z = controller.maz;
            } else {
                z = -controller.maz;
            }
            x = UnityEngine.Random.Range(-controller.max + 0.5f,controller.max - 0.5f);
        }
        
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
