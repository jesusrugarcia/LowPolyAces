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
    public GameObject MaxHealth;
    public GameObject MaxSpeed;

    public GameObject Acceleration;
    public GameObject RotationSpeed;

    public GameObject Damage;
    public GameObject TurretDamage;
    public GameObject MissileDamage;
    public GameObject MineDamage;
    public GameObject DrillDamage;

    public GameObject ShootSpeed;
    public GameObject MagazineSize;
    public GameObject TurretShootSpeed;
    public GameObject NormalDroneShootSpeed;
    public GameObject SpecialDroneShootSpeed;
    public GameObject AuxDroneShootSpeed;

    public GameObject MaxSpecialAmmo;
    public GameObject MaxMines;
    public GameObject MeleeTime;
    public GameObject MaxDrones;

    public void spawnPowerUp(PowerUps type){ //to be updated with propper elements...
        if(controller.currentPowerUps >= controller.gameOptions.maxPowerUps){
            return;
        }

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
        }
        
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
        } 
        
        else if(type == PowerUps.Shield){
            var pow =Instantiate(shield,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if(type == PowerUps.Repair){
            var pow =Instantiate(repair,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        
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
        }else if (type == PowerUps.MissileBullet){
            var pow =Instantiate(MissileBullet,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        else if (type == PowerUps.DrillBullet){
            var pow =Instantiate(DrillBullet,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.ExtraBullet){
            var pow =Instantiate(ExtraBullet,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.TrackerBullet){
            var pow =Instantiate(TrackerBullet,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }
        
        
        else if (type == PowerUps.MaxHealth){
            var pow =Instantiate(MaxHealth,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.MaxSpeed){
            var pow =Instantiate(MaxSpeed,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.Acceleration){
            var pow =Instantiate(Acceleration,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.RotationSpeed){
            var pow =Instantiate(RotationSpeed,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.Damage){
            var pow =Instantiate(Damage,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.TurretDamage){
            var pow =Instantiate(TurretDamage,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.MissileDamage){
            var pow =Instantiate(MissileDamage,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.MineDamage){
            var pow =Instantiate(MineDamage,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.DrillDamage){
            var pow =Instantiate(DrillDamage,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.ShootSpeed){
            var pow =Instantiate(ShootSpeed,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.MagazineSize){
            var pow =Instantiate(MagazineSize,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.TurretShootSpeed){
            var pow =Instantiate(TurretShootSpeed,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.NormalDroneSpeed){
            var pow =Instantiate(NormalDroneShootSpeed,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.SpecialDroneSpeed){
            var pow =Instantiate(SpecialDroneShootSpeed,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.AuxDroneSpeed){
            var pow =Instantiate(AuxDroneShootSpeed,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        }else if (type == PowerUps.MaxSpecialAmmo){
            var pow =Instantiate(MaxSpecialAmmo,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.MaxMines){
            var pow =Instantiate(MaxMines,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.MeleeTime){
            var pow =Instantiate(MeleeTime,getPosition(),Quaternion.identity);
            pow.GetComponent<PowerUpsManager>().manager = controller.centralManager;
        } else if (type == PowerUps.MaxDrones){
            var pow =Instantiate(MaxDrones,getPosition(),Quaternion.identity);
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
