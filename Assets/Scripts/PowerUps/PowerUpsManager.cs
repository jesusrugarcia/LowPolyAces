using System;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUps{
    Missile,
    Mine,
    Repair,
    Shield,
    TurretDrone,
    MineDrone,
    GunnerDrone,
    ShieldDrone,
    RepairDrone,
    MissileDrone,
    MissileBullet,
    DrillBullet,
    ExtraBullet,
    TrackerBullet
}

public class PowerUpsManager : MonoBehaviour
{
    public int min = 1;
    public int max = 2;

    public GameObject shield;
    public GameObject TurretDrone;
    public GameObject MineDrone;
    public GameObject GunnerDrone;
    public GameObject MissileDrone;
    public GameObject ShieldDrone;
    public GameObject RepairDrone;
    public GameObject MissileBullet;
    public GameObject DrillBullet;

    public PowerUps type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        randomRotation();
    }

    public void randomRotation(){
        transform.Rotate(UnityEngine.Random.Range(min,max),UnityEngine.Random.Range(min,max),UnityEngine.Random.Range(min,max));
    }

    private void OnCollisionEnter(Collision other) {
        if(type == PowerUps.Missile){
            addMissile(other.gameObject);
        } else if(type == PowerUps.Shield){
            addShield(other.gameObject);
        } else if(type == PowerUps.Repair){
            addHealth(other.gameObject);
        } else if(type == PowerUps.TurretDrone){
            addTurretDrone(other.gameObject);
        } else if(type == PowerUps.Mine){
            addMine(other.gameObject);
        } else if (type== PowerUps.MineDrone){
            addMineDrone(other.gameObject);
        } else if (type== PowerUps.GunnerDrone){
            addGunnerDrone(other.gameObject);
        }else if (type== PowerUps.RepairDrone){
            addRepairDrone(other.gameObject);
        }else if (type== PowerUps.ShieldDrone){
            addShieldDrone(other.gameObject);
        }else if (type== PowerUps.MissileDrone){
            addMissileDrone(other.gameObject);
        }else if (type == PowerUps.MissileBullet){
            addMissileBullet(other.gameObject);
        }else if (type == PowerUps.DrillBullet){
            addDrillBullet(other.gameObject);
        }else if (type == PowerUps.ExtraBullet){
            addExtraBullet(other.gameObject);
        }else if (type == PowerUps.TrackerBullet){
            addTrackerBullet(other.gameObject);
        }
    }

    public void addMissile(GameObject plane){
        try {
            var stats = plane.GetComponent<PlaneStats>();
            if(stats.specialAmmo < stats.maxSpecialAmmo){
                stats.specialAmmoType = specialAmmo.Missile;
                stats.specialAmmo ++;
                stats.plane.healthBar.MissileIcon.SetActive(true);
                stats.plane.controller.reduceCurrentPowerUps();
                Destroy(gameObject);
            } else if(stats.specialAmmoType != specialAmmo.Missile){
                stats.specialAmmoType = specialAmmo.Missile;
                stats.plane.controller.reduceCurrentPowerUps();
                Destroy(gameObject);
            }
            stats.specialAmmoType = specialAmmo.Missile;
            
        } catch(Exception e) {
            Debug.Log(e);
        }
    }

    public void addMine(GameObject plane){
        try {
            var stats = plane.GetComponent<PlaneStats>();
            if(stats.specialAmmo < stats.maxSpecialAmmo){
                stats.specialAmmo ++;
                stats.plane.healthBar.MissileIcon.SetActive(true);
                stats.specialAmmoType = specialAmmo.Mine;
                stats.plane.controller.reduceCurrentPowerUps();
                Destroy(gameObject);
                
            } else if(stats.specialAmmoType != specialAmmo.Mine){
                stats.specialAmmoType = specialAmmo.Mine;
                stats.plane.controller.reduceCurrentPowerUps();
                Destroy(gameObject);
            }
        } catch(Exception e) {
            Debug.Log(e);
        }
    }

    public void addShield(GameObject plane){
        try {
            var planeManager = plane.GetComponent<PlaneManager>();
            if(!planeManager.stats.hasShield){
                planeManager.stats.hasShield = true;
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(gameObject);
                var shi = Instantiate(shield, plane.transform.position,Quaternion.identity);
                var shieldManager = shi.GetComponent<ShieldManager>();
                shieldManager.teamManager.team = planeManager.teamManager.team;
                shieldManager.target = plane; 
                planeManager.collissionManager.shield = shi;
            } else {
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(gameObject);
                planeManager.collissionManager.shield.GetComponent<ShieldManager>().addHealth();
            }
        } catch(Exception e){
            Debug.Log(e);
        }
    }

    public void addHealth(GameObject plane){
        try {
            var stats = plane.GetComponent<PlaneStats>();
            if(stats.health < stats.maxHealth){
                stats.health ++;
                stats.plane.controller.reduceCurrentPowerUps();
                Destroy(gameObject);
            }
        } catch(Exception e) {
            Debug.Log(e);
        }
    }

    public void addTurretDrone(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager != null && planeManager.stats.drones < planeManager.stats.maxDrones){
                Destroy(GetComponent<Rigidbody>());
                var x = plane.transform.position.x + UnityEngine.Random.Range(-1f,1f);
                var z = plane.transform.position.z + UnityEngine.Random.Range(-1f,1f);
                var drone = Instantiate(TurretDrone, new Vector3(x,0,z), Quaternion.identity);
                var droneMovement = drone.GetComponent<DroneMovement>();
                planeManager.controller.reduceCurrentPowerUps();

                drone.GetComponentInChildren<TeamManager>().team = planeManager.teamManager.team;
                drone.GetComponentInChildren<Turret>().plane = planeManager;
                
                droneMovement.plane = plane;
                droneMovement.ready = true;

                Destroy(gameObject);
                planeManager.stats.drones++;
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
        
    }

    public void addMineDrone(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager != null && planeManager.stats.drones < planeManager.stats.maxDrones){
                Destroy(GetComponent<Rigidbody>());
                var x = plane.transform.position.x + UnityEngine.Random.Range(-1f,1f);
                var z = plane.transform.position.z + UnityEngine.Random.Range(-1f,1f);
                var drone = Instantiate(MineDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
                var droneMovement = drone.GetComponent<DroneMovement>();
                droneMovement.plane = plane;
                planeManager.controller.reduceCurrentPowerUps();

                drone.GetComponent<MineDrone>().plane = planeManager;

                
                droneMovement.ready = true;

                

                Destroy(gameObject);
                planeManager.stats.drones++;
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
        
    }

    public void addGunnerDrone(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager != null && planeManager.stats.drones < planeManager.stats.maxDrones){
                Destroy(GetComponent<Rigidbody>());
                var x = plane.transform.position.x + UnityEngine.Random.Range(-1f,1f);
                var z = plane.transform.position.z + UnityEngine.Random.Range(-1f,1f);
                var drone = Instantiate(GunnerDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
                var droneMovement = drone.GetComponent<DroneMovement>();
                droneMovement.plane = plane;
                planeManager.controller.reduceCurrentPowerUps();

                drone.GetComponent<GunnerDrone>().plane = planeManager;

                
                droneMovement.ready = true;

                planeManager.stats.drones++;

                Destroy(gameObject);
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
        
    }

    public void addShieldDrone(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager != null && planeManager.stats.drones < planeManager.stats.maxDrones){
                Destroy(GetComponent<Rigidbody>());
                var x = plane.transform.position.x + UnityEngine.Random.Range(-1f,1f);
                var z = plane.transform.position.z + UnityEngine.Random.Range(-1f,1f);
                var drone = Instantiate(ShieldDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
                var droneMovement = drone.GetComponent<DroneMovement>();
                droneMovement.plane = plane;
                planeManager.controller.reduceCurrentPowerUps();

                drone.GetComponent<ShieldDrone>().plane = planeManager;

                
                droneMovement.ready = true;

                planeManager.stats.drones++;

                Destroy(gameObject);
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
        
    }

    public void addMissileDrone(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager != null && planeManager.stats.drones < planeManager.stats.maxDrones){
                Destroy(GetComponent<Rigidbody>());
                var x = plane.transform.position.x + UnityEngine.Random.Range(-1f,1f);
                var z = plane.transform.position.z + UnityEngine.Random.Range(-1f,1f);
                var drone = Instantiate(MissileDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
                var droneMovement = drone.GetComponent<DroneMovement>();
                droneMovement.plane = plane;
                planeManager.controller.reduceCurrentPowerUps();

                drone.GetComponent<MissileDrone>().plane = planeManager;

                
                droneMovement.ready = true;

                planeManager.stats.drones++;

                Destroy(gameObject);
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
        
    }

    public void addRepairDrone(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager != null && planeManager.stats.drones < planeManager.stats.maxDrones){
                Destroy(GetComponent<Rigidbody>());
                var x = plane.transform.position.x + UnityEngine.Random.Range(-1f,1f);
                var z = plane.transform.position.z + UnityEngine.Random.Range(-1f,1f);
                var drone = Instantiate(RepairDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
                var droneMovement = drone.GetComponent<DroneMovement>();
                droneMovement.plane = plane;
                planeManager.controller.reduceCurrentPowerUps();

                drone.GetComponent<RepairDrone>().plane = planeManager;

                
                droneMovement.ready = true;

                planeManager.stats.drones++;

                Destroy(gameObject);
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
        
    }

    public void addMissileBullet(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
        if (planeManager.planeShooter.bullet != MissileBullet){
            planeManager.planeShooter.bullet = MissileBullet;
            
            planeManager.controller.reduceCurrentPowerUps();
            Destroy(gameObject);
        }
        } catch(Exception e){
            Debug.Log(e);
        }
        
    }

    public void addDrillBullet(GameObject plane){
        try {
            var planeManager = plane.GetComponent<PlaneManager>();
        if (planeManager.planeShooter.bullet != DrillBullet){
            planeManager.planeShooter.bullet = DrillBullet;
            
            planeManager.controller.reduceCurrentPowerUps();
            Destroy(gameObject);
        }
        } catch(Exception e){
            Debug.Log(e);
        }
        
    }

    public void addExtraBullet(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            planeManager.stats.extraBullets ++;
            planeManager.controller.reduceCurrentPowerUps();
            Destroy(gameObject);
        } catch( Exception e){
            Debug.Log(e);
        }
    }

    public void addTrackerBullet(GameObject plane){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (!planeManager.stats.trackerBullet){
                planeManager.stats.trackerBullet = true;
            } else {
                planeManager.stats.searchDistance ++;
            }
            planeManager.controller.reduceCurrentPowerUps();
            Destroy(gameObject);
        } catch( Exception e){
            Debug.Log(e);
        }
    }
}
