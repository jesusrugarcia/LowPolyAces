using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameController controller;

    //WeaponPowerUps
    public GameObject missile;
    public GameObject mine;
    public GameObject Flare;
    public GameObject SupportClaw;
    public GameObject ElectricClaw;
    public GameObject BurningClaw;
    public GameObject RustingClaw;
    public GameObject ClusterMissile;
    
    //Gadgets
    public GameObject Turbo;
    public GameObject TankShield;
    public GameObject AreaOfHeal;
    public GameObject Laser;
    public GameObject Coin;
    
    //Defense
    public GameObject Dash;
    public GameObject HealShield;
    public GameObject Ghost;
    public GameObject Hook;
    public GameObject InverseHook;


    public GameObject shield;
    public GameObject repair;
    //Drones
    public GameObject turretDrone;
    public GameObject MineDrone;
    public GameObject gunnerDrone;
    public GameObject MissileDrone;
    public GameObject RepairDrone;
    public GameObject ShieldDrone;
    //BulletTypes
    public GameObject MissileBullet;
    public GameObject DrillBullet;
    public GameObject ExtraBullet;
    public GameObject TrackerBullet;
    //Stats
    public GameObject ReinforcedFuselage;
    public GameObject ExperimentalEngine;
    public GameObject FineTunnesFlaps;
    public GameObject ExperimentalAmmunition;
    public GameObject AutomatizedRechargeSystems;
    public GameObject BarrelMagazines;
    public GameObject ExperimentalBarrel;
    public GameObject EnhancedControllers;
    public GameObject MarauderSystems;
    public GameObject ExperimentalDefenseSystems;
    public GameObject CommanderSystems;
    public GameObject ExperimentalCamouflage;
    public GameObject HydraulicPincers;

    public void spawnPowerUp(PowerUps type){ //to be updated with propper elements...
        if(controller.currentPowerUps >= controller.gameOptions.maxPowerUps){
            return;
        }

        //misiles
        if(type == PowerUps.Missile){
            var pow = Instantiate(missile,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }  else if (type == PowerUps.Mine){
            var pow =Instantiate(mine,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.Flare){
            var pow =Instantiate(Flare,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.SupportClaw){
            var pow =Instantiate(SupportClaw,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.ElectricClaw){
            var pow =Instantiate(ElectricClaw,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.BurningClaw){
            var pow =Instantiate(BurningClaw,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.RustingClaw){
            var pow =Instantiate(RustingClaw,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.ClusterMissile){
            var pow =Instantiate(ClusterMissile,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        //gadgets
        else if(type == PowerUps.Turbo){
            var pow =Instantiate(Turbo,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.TankShield){
            var pow =Instantiate(TankShield,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.AreaOfHeal){
            var pow =Instantiate(AreaOfHeal,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.Laser){
            var pow =Instantiate(Laser,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.Coin){
            var pow =Instantiate(Coin,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        //defenses
        
        else if(type == PowerUps.Dash){
            var pow =Instantiate(Dash,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.HealShield){
            var pow =Instantiate(HealShield,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.Ghost){
            var pow =Instantiate(Ghost,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.Hook){
            var pow =Instantiate(Hook,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.InverseHook){
            var pow =Instantiate(InverseHook,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        //misc
        else if(type == PowerUps.Shield){
            var pow =Instantiate(shield,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.Repair){
            var pow =Instantiate(repair,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        //drones
        else if (type == PowerUps.TurretDrone){
            var pow =Instantiate(turretDrone,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.MineDrone){
            var pow =Instantiate(MineDrone,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.GunnerDrone){
            var pow =Instantiate(gunnerDrone,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.MissileDrone){
            var pow =Instantiate(MissileDrone,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.RepairDrone){
            var pow =Instantiate(RepairDrone,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.ShieldDrone){
            var pow =Instantiate(ShieldDrone,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        //bullets
        else if (type == PowerUps.MissileBullet){
            var pow =Instantiate(MissileBullet,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.DrillBullet){
            var pow =Instantiate(DrillBullet,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        //stats
        else if (type == PowerUps.ReinforcedFuselage){
            var pow =Instantiate(ReinforcedFuselage,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.ExperimentalEngine){
            var pow =Instantiate(ExperimentalEngine,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.FineTunnedFlaps){
            var pow =Instantiate(FineTunnesFlaps,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.ExperimentalAmmunition){
            var pow =Instantiate(ExperimentalAmmunition,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.AutomatizedRechargeSystems){
            var pow =Instantiate(AutomatizedRechargeSystems,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.BarrelMagazines){
            var pow =Instantiate(BarrelMagazines,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.ExperimentalBarrel){
            var pow =Instantiate(ExperimentalBarrel,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.EnhancedControllers){
            var pow =Instantiate(EnhancedControllers,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.MarauderSystems){
            var pow =Instantiate(MarauderSystems,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.ExperimentalDefenseSystems){
            var pow =Instantiate(ExperimentalDefenseSystems,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.CommanderSystems){
            var pow =Instantiate(CommanderSystems,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.ExperimentalCamouflage){
            var pow =Instantiate(ExperimentalCamouflage,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.HydraulicPincers){
            var pow =Instantiate(HydraulicPincers,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        controller.currentPowerUps ++;
    }

    public Vector3 getPosition(){
        var x = UnityEngine.Random.Range(-controller.max, controller.max);
        var z = UnityEngine.Random.Range(-controller.maz, controller.maz);

        return new Vector3(x,0,z);
    }
}
