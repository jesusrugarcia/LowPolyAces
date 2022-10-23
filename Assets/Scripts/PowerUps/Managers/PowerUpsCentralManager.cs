using System;
using UnityEngine;

//this class updates the planes to set the power ups and their prefabs.
public class PowerUpsCentralManager : MonoBehaviour
{
    public GameObject shield;
    //Missiles
    public GameObject Missile;
    public GameObject Mine;
    public GameObject Flare;
    public GameObject SupportClaw;
    public GameObject ElectricClaw;
    public GameObject BurningClaw;
    public GameObject RustingClaw;
    public GameObject ClusterMissile;
    //Gadgets
    public GameObject HealArea;
    public GameObject TankShield;
    public GameObject Coin;
    //Defenses
    public GameObject HealShield;
    public GameObject Hook;
    public GameObject InverseHook;
    //Drones
    public GameObject TurretDrone;
    public GameObject MineDrone;
    public GameObject GunnerDrone;
    public GameObject MissileDrone;
    public GameObject ShieldDrone;
    public GameObject RepairDrone;
    public GameObject MissileBullet;
    public GameObject DrillBullet;
    //ParticleEffects
    public GameObject DashParticleEffect;
    public GameObject TurboParticleEffect;
    public GameObject InvulnerabilityParticleEffect;
    public GameObject GhostParticleEffect;
    public GameObject BurningParticleEffect;
    public GameObject StunnedParticleEffect;
    public GameObject RustingParticleEffect;

//this method calls for the apropiate method when obtaining a power up.
    public void managePowerUp(Collision other, PowerUps type, GameObject origin){
        if(type == PowerUps.Missile || type == PowerUps.Mine || type == PowerUps.Flare || type == PowerUps.SupportClaw || 
        type == PowerUps.ElectricClaw || type == PowerUps.BurningClaw || type == PowerUps.RustingClaw || type == PowerUps.ClusterMissile || type == PowerUps.Turbo || 
        type == PowerUps.AreaOfHeal || type == PowerUps.TankShield || type == PowerUps.Laser || type == PowerUps.Coin){
            addWeaponPowerUp(other.gameObject, type, origin);
        } else if(type == PowerUps.Dash || type == PowerUps.Ghost || type == PowerUps.HealShield || type == PowerUps.Hook || type == PowerUps.InverseHook){
            addDefensePowerUp(other.gameObject, type, origin);
        }else if(type == PowerUps.Shield){
            addShield(other.gameObject, origin);
        } else if(type == PowerUps.TurretDrone || type== PowerUps.MineDrone || type== PowerUps.GunnerDrone || type== PowerUps.RepairDrone || 
        type== PowerUps.ShieldDrone || type== PowerUps.MissileDrone){
            addDrone(other.gameObject, type, origin);
        } else if (type == PowerUps.MissileBullet || type == PowerUps.DrillBullet){
            addBulletType(other.gameObject, type, origin);
        }else {
            addStat(other.gameObject, type, origin);
        }
    }

//this method handles missile and gadget power up set ups.
    public void addWeaponPowerUp(GameObject plane, PowerUps type, GameObject origin){
        try {
            bool destroyable = false;
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager.stats.specialAmmo < planeManager.stats.maxSpecialAmmo){
                    planeManager.stats.specialAmmo ++;
                    planeManager.healthBar.MissileIcon.SetActive(true);
                    destroyable = true;
            }

            //Missiles
            if(type == PowerUps.Missile && !(planeManager.stats.missileType == MissileType.Missile)){
                planeManager.stats.missileType = MissileType.Missile;
                planeManager.planeShooter.missile = Missile;
                destroyable = true;
            } else if (type == PowerUps.Mine && !(planeManager.stats.missileType == MissileType.Mine)){
                planeManager.stats.missileType = MissileType.Mine;
                planeManager.planeShooter.missile = Mine;
                destroyable = true;
            } else if(type == PowerUps.Flare && !(planeManager.stats.missileType == MissileType.Flare)){
                planeManager.stats.missileType = MissileType.Flare;
                planeManager.planeShooter.missile = Flare;
                destroyable = true;
            } else if(type == PowerUps.SupportClaw && !(planeManager.stats.missileType == MissileType.SupportClaw)){
                planeManager.stats.missileType = MissileType.SupportClaw;
                planeManager.planeShooter.missile = SupportClaw;
                destroyable = true;
            }
            else if(type == PowerUps.ElectricClaw && !(planeManager.stats.missileType == MissileType.ElectricClaw)){
                planeManager.stats.missileType = MissileType.ElectricClaw;
                planeManager.planeShooter.missile = ElectricClaw;
                destroyable = true;
            }
            else if(type == PowerUps.BurningClaw && !(planeManager.stats.missileType == MissileType.BurningClaw)){
                planeManager.stats.missileType = MissileType.BurningClaw;
                planeManager.planeShooter.missile = BurningClaw;
                destroyable = true;
            }
            else if(type == PowerUps.RustingClaw && !(planeManager.stats.missileType == MissileType.RustingClaw)){
                planeManager.stats.missileType = MissileType.RustingClaw;
                planeManager.planeShooter.missile = RustingClaw;
                destroyable = true;
            } else if(type == PowerUps.ClusterMissile && !(planeManager.stats.missileType == MissileType.ClusterMissile)){
                planeManager.stats.missileType = MissileType.ClusterMissile;
                planeManager.planeShooter.missile = ClusterMissile;
                destroyable = true;
            }
            //Gadgets
            else if (type == PowerUps.Turbo && !(planeManager.stats.gadgetType == GadgetType.Turbo)){
                planeManager.stats.gadgetType = GadgetType.Turbo;
                planeManager.planeShooter.gadget = TurboParticleEffect;
                destroyable = true;
            } else if (type == PowerUps.AreaOfHeal && !(planeManager.stats.gadgetType == GadgetType.AreaOfHeal)){
                planeManager.stats.gadgetType = GadgetType.AreaOfHeal;
                planeManager.planeShooter.gadget = HealArea;
                destroyable = true;
            }  else if (type == PowerUps.TankShield && !(planeManager.stats.gadgetType == GadgetType.TankShield)){
                planeManager.stats.gadgetType = GadgetType.TankShield;
                planeManager.planeShooter.gadget = TankShield;
                destroyable = true;
            } else if (type == PowerUps.Laser && !(planeManager.stats.gadgetType == GadgetType.Laser)){
                planeManager.stats.gadgetType = GadgetType.Laser;
                destroyable = true;
            } else if (type == PowerUps.Coin && !(planeManager.stats.gadgetType == GadgetType.Coin)){
                planeManager.stats.gadgetType = GadgetType.Coin;
                planeManager.planeShooter.gadget = Coin;
                destroyable = true;
            }

            if (destroyable){
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(origin);
            }

        } catch(Exception e){
            Debug.Log(e);
        }
    }

    public void addDefensePowerUp(GameObject plane, PowerUps type, GameObject origin){
        try{
            bool destroyable = false;
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager.stats.defenseAmmo < planeManager.stats.maxDefenseAmmo){
                    planeManager.stats.defenseAmmo ++;
                    destroyable = true;
            }

            if(type == PowerUps.Dash && planeManager.stats.defenseType != DefenseType.Dash){
                planeManager.stats.defenseType = DefenseType.Dash;
                planeManager.planeShooter.defense = DashParticleEffect;
                destroyable = true;
            } else if(type == PowerUps.Ghost && planeManager.stats.defenseType != DefenseType.Ghost){
                planeManager.stats.defenseType = DefenseType.Ghost;
                destroyable = true;
            } else if(type == PowerUps.Hook && planeManager.stats.defenseType != DefenseType.Hook){
                planeManager.stats.defenseType = DefenseType.Hook;
                planeManager.planeShooter.defense = Hook;
                destroyable = true;
            } else if(type == PowerUps.InverseHook && planeManager.stats.defenseType != DefenseType.InverseHook){
                planeManager.stats.defenseType = DefenseType.InverseHook;
                planeManager.planeShooter.defense = InverseHook;
                destroyable = true;
            } else if(type == PowerUps.HealShield && planeManager.stats.defenseType != DefenseType.HealShield){
                planeManager.stats.defenseType = DefenseType.HealShield;
                planeManager.planeShooter.defense = HealShield;
                destroyable = true;
            }


            if (destroyable){
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(origin);
            }

        } catch (Exception e){
            Debug.Log(e);
        }
    }

    public void addShield(GameObject plane, GameObject origin){
        try {
            var planeManager = plane.GetComponent<PlaneManager>();
            if(!planeManager.stats.hasShield){
                planeManager.stats.hasShield = true;
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(origin);
                var shi = Instantiate(shield, plane.transform.position,Quaternion.identity);
                var shieldManager = shi.GetComponent<ShieldManager>();
                shieldManager.teamManager.team = planeManager.teamManager.team;
                shieldManager.target = plane; 
                planeManager.collissionManager.shield = shi;
            } else {
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(origin);
                planeManager.collissionManager.shield.GetComponent<ShieldManager>().addHealth();
            }
        } catch(Exception e){
            Debug.Log(e);
        }
    }

    public void addDrone(GameObject plane, PowerUps type, GameObject origin){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            if (planeManager != null && planeManager.stats.drones < planeManager.stats.maxDrones){
                Destroy(GetComponent<Rigidbody>());
                
                var drone = InstantiateDrone(type, planeManager);
                var droneMovement = drone.GetComponent<DroneMovement>();
                droneMovement.plane = plane;
                droneMovement.ready = true;

                
                planeManager.stats.drones++;
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(origin);

            }
        } catch (Exception e){
            Debug.Log(e);
        }
        
    }

    public GameObject InstantiateDrone(PowerUps type, PlaneManager planeManager){
        GameObject drone = null;
        var x = planeManager.transform.position.x + UnityEngine.Random.Range(-1f,1f);
        var z = planeManager.transform.position.z + UnityEngine.Random.Range(-1f,1f);

        if (type == PowerUps.TurretDrone){
            drone = Instantiate(TurretDrone, new Vector3(x,0,z), Quaternion.identity);
            drone.GetComponentInChildren<Turret>().plane = planeManager;
            drone.GetComponentInChildren<TeamManager>().team = planeManager.teamManager.team;
        } else if(type == PowerUps.MineDrone){
            drone = Instantiate(MineDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
            drone.GetComponent<MineDrone>().plane = planeManager;
        }else if(type == PowerUps.GunnerDrone){
            drone = Instantiate(GunnerDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
            drone.GetComponent<GunnerDrone>().plane = planeManager;
        } else if (type == PowerUps.ShieldDrone){
            drone = Instantiate(ShieldDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
            drone.GetComponent<ShieldDrone>().plane = planeManager;
        } else if (type == PowerUps.MissileDrone){
            drone = Instantiate(MissileDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
            drone.GetComponent<MissileDrone>().plane = planeManager;
        } else if (type == PowerUps.RepairDrone){
            drone = Instantiate(RepairDrone, new Vector3(x,0,z), Quaternion.Euler(-90,0,0));
            drone.GetComponent<RepairDrone>().plane = planeManager;
        }


        return drone;
    }


    public void addBulletType(GameObject plane, PowerUps type, GameObject origin){
        try{
            bool destroyable = false;
            var planeManager = plane.GetComponent<PlaneManager>();
            if (type == PowerUps.MissileBullet && planeManager.planeShooter.bullet != MissileBullet){
                planeManager.planeShooter.bullet = MissileBullet;
                destroyable = true;
            }else if (type == PowerUps.DrillBullet && planeManager.planeShooter.bullet != DrillBullet){
                planeManager.planeShooter.bullet = DrillBullet;
                destroyable = true;
            }

            if (destroyable){
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(origin);
            }
        } catch(Exception e){
            Debug.Log(e);
        }
        
    }

    public void addStat(GameObject plane, PowerUps type, GameObject origin){
        try{
            var planeManager = plane.GetComponent<PlaneManager>();
            bool destroyable = false;

            if(type == PowerUps.ReinforcedFuselage){
                destroyable = true;
                planeManager.stats.maxHealth += 5;
                planeManager.stats.health += 5;
            } else if(type == PowerUps.ExperimentalEngine){
                destroyable = true;
                planeManager.stats.maxSpeed += 1;
                planeManager.stats.acceleration += 0.2f;
            } else if(type == PowerUps.FineTunnedFlaps){
                destroyable = true;
                planeManager.stats.rotationSpeed += 1;
                planeManager.stats.maxRotation += 1f;
                if (planeManager.stats.timeToRotate >= 0.3f){
                    planeManager.stats.timeToRotate -= 0.1f;
                }
                planeManager.stats.timeRotating += 0.1f;
            } else if(type == PowerUps.ExperimentalAmmunition){
                destroyable = true;
                planeManager.stats.bulletDamage += 0.5f;
                planeManager.stats.turretDamage += 0.15f;
                planeManager.stats.missileDamage += 2;
                planeManager.stats.mineDamage += 1;
                planeManager.stats.drillDamage += 0.5f;
                planeManager.stats.laserDamage += 0.1f;
            } else if(type == PowerUps.AutomatizedRechargeSystems){
                destroyable = true;
                if (planeManager.stats.shootSpeed > 0.1f){planeManager.stats.shootSpeed -= 0.1f;}
                if(planeManager.stats.turretShootSpeed > 0.1f){planeManager.stats.turretShootSpeed -= 0.1f;}
                if(planeManager.stats.normalDroneShootSpeed > 0.1f){planeManager.stats.normalDroneShootSpeed -= 0.1f;}
                if(planeManager.stats.specialDroneShootSpeed > 1f){planeManager.stats.specialDroneShootSpeed -= 1f;}
                if(planeManager.stats.auxDroneSpeed > 1){planeManager.stats.auxDroneSpeed-= 1;}
                if(planeManager.stats.rechargeDefenseTime > 0.5f){planeManager.stats.rechargeDefenseTime -= 0.5f;}
                if(planeManager.stats.rechargeSpecialTime > 1){planeManager.stats.rechargeSpecialTime -=1;}
            } else if(type == PowerUps.BarrelMagazines){
                destroyable = true;
                planeManager.stats.magazineSize += 1;
                planeManager.stats.maxSpecialAmmo += 1;
                planeManager.stats.maxMines += 1;
                planeManager.planeShooter.resizeMinesList();
            } else if(type == PowerUps.ExperimentalBarrel){
                destroyable = true;
                planeManager.stats.extraBullets += 1;
            } else if(type == PowerUps.EnhancedControllers){
                destroyable = true;
                planeManager.stats.meleeTime += 1;
                planeManager.stats.laserTime += 1;
            } else if(type == PowerUps.MarauderSystems){
                destroyable = true;
                planeManager.stats.trackerBullet = true;
                planeManager.stats.searchDistance += 0.5f;
            } else if(type == PowerUps.ExperimentalDefenseSystems){
                destroyable = true;
                if(planeManager.stats.damageReductionTankShield < 0.9f){planeManager.stats.damageReductionTankShield += 0.1f;}
                planeManager.stats.SpecialShieldDuration += 1;
                planeManager.stats.healAreaAmount += 0.1f;
                planeManager.stats.maxDefenseAmmo += 1;
            } else if(type == PowerUps.CommanderSystems){
                destroyable = true;
                planeManager.stats.maxDrones += 1;
                planeManager.stats.searchDistance += 0.5f;
                planeManager.stats.timeTargetting += 1f;
            } else if(type == PowerUps.ExperimentalCamouflage){
                destroyable = true;
                planeManager.stats.evasion += 0.1f;
                planeManager.stats.invIncrease += 0.2f;
                planeManager.stats.ghostIncrease += 1;
            } else if(type == PowerUps.HydraulicPincers){
                destroyable = true;
                planeManager.stats.statusEffectTime += 1;
                planeManager.stats.missileDamage += 0.2f;
            } else if(type == PowerUps.Repair && planeManager.stats.health <= planeManager.stats.maxHealth){
                destroyable = true;
                planeManager.stats.health += 3;
                if(planeManager.stats.health > planeManager.stats.maxHealth){
                    planeManager.stats.health = planeManager.stats.maxHealth;
                }
            }

            if (destroyable){
                planeManager.controller.reduceCurrentPowerUps();
                Destroy(origin);
            }

        }catch(Exception e){
            Debug.Log(e);
        }
    }

    public void initialSetUp(PlaneManager plane){
        missileSetUp(plane);
        gadgetSetUp(plane);
        defenseSetUp(plane);
    }

    public void missileSetUp(PlaneManager plane){
        if(plane.stats.missileType == MissileType.Missile){
            plane.planeShooter.missile =  Missile;
        } else if(plane.stats.missileType == MissileType.Mine){
            plane.planeShooter.missile =  Mine;
        } else if(plane.stats.missileType == MissileType.Flare){
            plane.planeShooter.missile = Flare;
        } else if(plane.stats.missileType == MissileType.ElectricClaw){
            plane.planeShooter.missile =  ElectricClaw;
        } else if(plane.stats.missileType == MissileType.BurningClaw){
            plane.planeShooter.missile =  BurningClaw;
        } else if(plane.stats.missileType == MissileType.RustingClaw){
            plane.planeShooter.missile =  RustingClaw;
        } else if(plane.stats.missileType == MissileType.ClusterMissile){
            plane.planeShooter.missile =  ClusterMissile;
        }
    }

    public void gadgetSetUp(PlaneManager plane){
        if(plane.stats.gadgetType == GadgetType.AreaOfHeal){
            plane.planeShooter.gadget = HealArea ;
        } else if(plane.stats.gadgetType == GadgetType.Coin){
            plane.planeShooter.gadget = Coin;
        } else if(plane.stats.gadgetType == GadgetType.TankShield){
            plane.planeShooter.gadget = TankShield ;
        } else if(plane.stats.gadgetType == GadgetType.Turbo){
            plane.planeShooter.gadget = TurboParticleEffect ;
        }
    }

    public void defenseSetUp(PlaneManager plane){
        if(plane.stats.defenseType == DefenseType.HealShield){
            plane.planeShooter.defense =  HealShield;
        } else if(plane.stats.defenseType == DefenseType.Hook){
            plane.planeShooter.defense = Hook ;
        } else if(plane.stats.defenseType == DefenseType.InverseHook){
            plane.planeShooter.defense = InverseHook ;
        }  else if(plane.stats.defenseType == DefenseType.Dash){
            plane.planeShooter.defense = DashParticleEffect ;
        }
    }

    
}
