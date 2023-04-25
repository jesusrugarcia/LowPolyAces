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

    public GameObject spawnPlane(PlaneScriptableObject planeModel, CharacterScriptableObject character, movement movement, int team, GameObject origin = null, StatsSave stats = null){
        Time.timeScale = 0f;
        if (character == null){
            character = new CharacterScriptableObject();
        }
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
            if(movement == movement.Player){
                controller.centralManager.addCharacterPowerUps(plane, character);
            }
            
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
            //add upgrades
            if(controller.gameOptions.mode == gameMode.arcade || controller.gameOptions.mode == gameMode.roguelite && !controller.rogueliteSave.loadStats){
                upgrade(plane, character);
            }
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

        stats.revival = planeModel.stats.revival;

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

    public void upgrade(GameObject plane, CharacterScriptableObject character){
        try{
            var stats = plane.GetComponent<PlaneStats>();

            // 0 health
            if (controller.data.upgrades.increase[0] >0 || character.increase[0] >0){
                stats.maxHealth += controller.data.upgrades.increase[0];
                stats.health += controller.data.upgrades.increase[0];
                stats.maxHealth += character.increase[0];
                stats.health += character.increase[0];
            }
            //1 speed
            if (controller.data.upgrades.increase[1] >0 || character.increase[1] >0){
                stats.maxSpeed += controller.data.upgrades.increase[1]*0.5f;
                stats.maxSpeed += character.increase[1]*0.5f;
                if(stats.maxSpeed > 20){
                    stats.maxSpeed = 20;
                }
            }
            //2 acceleration
            if (controller.data.upgrades.increase[2] >0 || character.increase[2] >0 ){
                stats.acceleration += controller.data.upgrades.increase[2]*0.1f;
                stats.acceleration += character.increase[2]*0.1f;
            }
            //3 rotation speed
            if (controller.data.upgrades.increase[3] >0 || character.increase[3] >0){
                stats.rotationSpeed += controller.data.upgrades.increase[3]*0.5f;
                stats.rotationSpeed += character.increase[3]*0.5f;
                if(stats.rotationSpeed > 10){
                    stats.rotationSpeed = 10;
                }
            }
            //4 shield time
            if (controller.data.upgrades.increase[4] >0 || character.increase[4] >0 ){
                stats.SpecialShieldDuration += controller.data.upgrades.increase[4];
                stats.SpecialShieldDuration += character.increase[4];
            }
            //5 evasion
            if (controller.data.upgrades.increase[5] >0 || character.increase[5] >0){
                stats.evasion += controller.data.upgrades.increase[5]*0.1f;
                stats.evasion += character.increase[5]*0.1f;
                if(stats.evasion > 0.8f){
                    stats.evasion = 0.8f;
                }
            }
            //6 status effect
            if (controller.data.upgrades.increase[6] >0 || character.increase[6] >0){
                stats.statusEffectTime += controller.data.upgrades.increase[6];
                stats.statusEffectTime += character.increase[6];
            }
            //7 bullet damage
            if (controller.data.upgrades.increase[7] >0 || character.increase[7] >0){
                stats.bulletDamage += controller.data.upgrades.increase[7]*0.5f;
                stats.drillDamage += controller.data.upgrades.increase[7]*0.5f;
                stats.bulletDamage += character.increase[7]*0.5f;
                stats.drillDamage += character.increase[7]*0.5f;
            }
            //8 special damage
            if (controller.data.upgrades.increase[8] >0 || character.increase[8] >0){
                stats.turretDamage += controller.data.upgrades.increase[8]*0.15f;
                stats.missileDamage += controller.data.upgrades.increase[8]*1.5f;
                stats.mineDamage += controller.data.upgrades.increase[8]*1.5f;
                stats.laserDamage += controller.data.upgrades.increase[8]*0.1f;

                stats.turretDamage += character.increase[8]*0.15f;
                stats.missileDamage += character.increase[8]*1.5f;
                stats.mineDamage += character.increase[8]*1.5f;
                stats.laserDamage += character.increase[8]*0.1f;
            }
            //9 shootSpeed
            if (controller.data.upgrades.increase[9] >0 || character.increase[9] >0){
                stats.shootSpeed += - controller.data.upgrades.increase[9]*0.1f;
                stats.shootSpeed += - character.increase[9]*0.1f;
                if(stats.shootSpeed < 0.1f){
                    stats.shootSpeed = 0.1f;
                }
            }
            //10 DroneShootSpeed
            if (controller.data.upgrades.increase[10] >0 || character.increase[10] >0){
                stats.normalDroneShootSpeed += - controller.data.upgrades.increase[10]*0.1f;
                stats.normalDroneShootSpeed += - character.increase[10]*0.1f;
                if(stats.normalDroneShootSpeed < 0.1f){
                    stats.normalDroneShootSpeed = 0.1f;
                }
                stats.specialDroneShootSpeed += - controller.data.upgrades.increase[10];
                stats.specialDroneShootSpeed += - character.increase[10];
                if(stats.specialDroneShootSpeed < 1){
                    stats.specialDroneShootSpeed = 1;
                }
                stats.auxDroneSpeed += - controller.data.upgrades.increase[10];
                stats.auxDroneSpeed += - character.increase[10];
                if(stats.auxDroneSpeed < 1){
                    stats.auxDroneSpeed = 1;
                }
            }
            //11 TurretshootSpeed
            if (controller.data.upgrades.increase[11] >0 || character.increase[11] >0){
                stats.turretShootSpeed += - controller.data.upgrades.increase[11]*0.1f;
                stats.turretShootSpeed += - character.increase[11]*0.1f;
                if(stats.turretShootSpeed < 0.1f){
                    stats.turretShootSpeed = 0.1f;
                }
            }
            //12 maxdrones
            if (controller.data.upgrades.increase[12] >0 || character.increase[12] >0){
                stats.maxDrones += controller.data.upgrades.increase[12];
                stats.maxDrones += character.increase[12];
            }
            //13 specialAMmo
            if (controller.data.upgrades.increase[13] >0 || character.increase[13] >0){
                stats.maxSpecialAmmo += controller.data.upgrades.increase[13];
                stats.specialAmmo += controller.data.upgrades.increase[13];
                stats.rechargeSpecialTime += - controller.data.upgrades.increase[13];
                stats.maxSpecialAmmo += character.increase[13];
                stats.specialAmmo += character.increase[13];
                stats.rechargeSpecialTime += - character.increase[13];
                if(stats.rechargeSpecialTime < 1){
                    stats.rechargeSpecialTime = 1;
                }
            }
            //14 defense AMmo
            if (controller.data.upgrades.increase[14] >0 || character.increase[14] >0){
                stats.maxDefenseAmmo += controller.data.upgrades.increase[14];
                stats.defenseAmmo += controller.data.upgrades.increase[14];
                stats.rechargeDefenseTime += - controller.data.upgrades.increase[14]*0.5f;
                stats.maxDefenseAmmo += character.increase[14];
                stats.defenseAmmo += character.increase[14];
                stats.rechargeDefenseTime += - character.increase[14]*0.5f;
                if(stats.rechargeDefenseTime < 0.5f){
                    stats.rechargeDefenseTime = 0.5f;
                }
            }
            //15 extra bullets
            if (controller.data.upgrades.increase[15] >0 || character.increase[15] >0){
                stats.extraBullets += controller.data.upgrades.increase[15];
                stats.extraBullets += character.increase[15];
            }
            //16 magazine size
            if (controller.data.upgrades.increase[16] >0 || character.increase[16] >0){
                stats.magazineSize += controller.data.upgrades.increase[16];
                stats.magazineSize += character.increase[16];
            }
            //17 max mines
            if (controller.data.upgrades.increase[17] >0 || character.increase[17] >0){
                stats.maxMines += controller.data.upgrades.increase[17];
                stats.maxMines += character.increase[17];
            }
            //18 special ammo time
            if (controller.data.upgrades.increase[18] >0 || character.increase[18] >0){
                stats.meleeTime += controller.data.upgrades.increase[18];
                stats.meleeTime += controller.data.upgrades.increase[18];
                stats.meleeTime += character.increase[18];
                stats.meleeTime += character.increase[18];
            }
            //19 revival to be implemented
             if (controller.data.upgrades.increase[19] >0 || character.increase[19] >0){
                stats.revival += controller.data.upgrades.increase[19];
                stats.revival += character.increase[19];
                
             }
        } catch(System.Exception e){
            Debug.Log(e);
        }  
    }
}
