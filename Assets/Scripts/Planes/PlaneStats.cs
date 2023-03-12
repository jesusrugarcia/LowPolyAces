using System;
using UnityEngine;

public enum MissileType {
    None,
    Missile,
    Mine,
    Flare,
    SupportClaw,
    ElectricClaw,
    BurningClaw,
    RustingClaw,
    ClusterMissile,
}

public enum GadgetType{
    None,
    Turbo,
    TankShield,
    AreaOfHeal,
    Laser,
    Coin
}

public enum DefenseType{
    None,
    Dash,
    HealShield,
    Ghost,
    Hook,
    InverseHook,
}

public enum BulletType{
    normal,
    drill,
    missile,
}

[Serializable]
public class PlaneStats : MonoBehaviour
{
    public float maxHealth = 0; //max health of the plane
    public float health = 1;

    public float maxSpeed = 0;
    public float speed = 0;
    public float acceleration =0; //how much the planes accelerates and deacceletates

    public float rotationSpeed = 0; //how fast the plane rotates
    public float rotation = 0; //how much is currently rotating
    public float maxRotation = 0; //maximum rotation per update
    public float timeToRotate = 1f; //how often enemy to start rotating
    public float timeRotating = 1f;// once rotating, how much time enemy spends rotating.

    public float bulletDamage = 1; //damage of bullets and normal drones
    public float turretDamage = 0.1f; //damage of turret and turret drones
    public float missileDamage = 10; //damage of missiles
    public float mineDamage = 5; //damage of mines
    public float drillDamage = 5; //damage by melee weapons

    public float shootSpeed = 1; // time in seconds to shoot
    public int magazineSize = 10; //how many bullets before reload
    public float turretShootSpeed = 1; //turret and turret drone shootspedd
    public float normalDroneShootSpeed = 1; //normal drone shootspeed
    public float specialDroneShootSpeed = 10f; //missile mine and the such drone shootspeed
    public float auxDroneSpeed = 10f; //healer and shield drone effect speed in secons, as all of the above

    public BulletType bulletType;
    public MissileType missileType;
    public GadgetType gadgetType;
    public DefenseType defenseType;

    public int specialAmmo = 0; //missile and gadget ammos
    public int maxSpecialAmmo = 1;
    public int defenseAmmo = 0; //used for defense power ups
    public int maxDefenseAmmo = 1;
    public int extraBullets = 0; // how many bullets in different direction (disparo en abanico)
    public int maxMines = 3; // how many mines deplyed at the same time
    public int mines = 0;
    public float meleeTime = 1; //how much time melee bullets stay in the air before dropping
    public bool trackerBullet = false; // do bullets follow enemies?
    public float searchDistance = 1; // used to increase search distance of turret and missiles

    public float scoreValue = 0;
    public int price = 0;

    public bool hasShield = false;
    public float damageReductionTankShield = 0.7f; //multiplied to damage when impacts tank shield
    public float SpecialShieldDuration = 1; //time special shield stay actived
    public float healAreaAmount = 0.01f; // health ammount heal areas heals per fixedupdate
    public float rechargeDefenseTime = 5f; //time before recharging one defense ammo
    public float rechargeSpecialTime = 10f; //time before recharging one special ammo

    public int maxDrones = 1; //max drones a plane can have simultaneously
    public int drones = 0;
    public PowerUps[] dronesList;

    public float[] statusEffects = new float[Enum.GetNames(typeof(StatusEffects)).Length]; // a list containing the time each status effect has before deactivating. if statusEffect[0] = 0.3 the plane has 0.3 seconds of invulneravility left, for example.
    public float invIncrease = 0.1f; //inv gained when dashing
    public float ghostIncrease = 1f; //time when using ghost power up
    public float evasion = 0; //chance up to 1 to evade damage
    public float timeTargetting = 2f; //time enemies follow the plane after hit by aggro.
    public float statusEffectTime = 2; //time of status effect inflicted by claw powerups
    

    public bool laserActivated = false;
    public float laserTime = 1; //how much time the lasers are activated
    public float laserDamage = 0.1f; //laser damages

    public PlaneManager plane;

    private void Start() {
        //health = maxHealth;
        try {
            if(dronesList == null){
                dronesList = new PowerUps[maxDrones];
            }
        } catch( Exception e){
            dronesList = new PowerUps[maxDrones];
            Debug.Log(e);
        }
    }

    public void copyStats(StatsSave stats){
        maxHealth = stats.maxHealth;
        health = stats.health;

        maxSpeed = stats.maxSpeed;
        acceleration = stats.acceleration;

        rotationSpeed = stats.rotationSpeed;
        rotation =stats.rotation;
        maxRotation = stats.maxRotation;
        timeToRotate = stats.timeToRotate;
        timeRotating = stats.timeRotating;

        bulletDamage = stats.bulletDamage;
        turretDamage = stats.turretDamage;
        missileDamage = stats.missileDamage;
        mineDamage = stats.mineDamage;
        drillDamage = stats.drillDamage;
        

        shootSpeed = stats.shootSpeed;
        magazineSize = stats.magazineSize;
        turretShootSpeed = stats.turretShootSpeed;
        normalDroneShootSpeed = stats.normalDroneShootSpeed;
        specialDroneShootSpeed = stats.specialDroneShootSpeed;
        auxDroneSpeed = stats.auxDroneSpeed;

        bulletType = stats.bulletType;
       missileType = stats.missileType;
        gadgetType = stats.gadgetType;
        defenseType = stats.defenseType;
        
        maxSpecialAmmo = stats.maxSpecialAmmo;
        specialAmmo = stats.specialAmmo;
        maxDefenseAmmo = stats.maxDefenseAmmo;
        defenseAmmo = stats.defenseAmmo;
        extraBullets = stats.extraBullets;
        //maxMines = stats.maxMines;
        mines = stats.mines;
        meleeTime = stats.meleeTime;
        trackerBullet = stats.trackerBullet;
       searchDistance = stats.searchDistance;

        scoreValue = stats.scoreValue;
        price = stats.price; 

        hasShield = false;
        damageReductionTankShield = stats.damageReductionTankShield;
        SpecialShieldDuration = stats.SpecialShieldDuration;
        healAreaAmount = stats.healAreaAmount;
        rechargeDefenseTime = stats.rechargeDefenseTime;
        rechargeSpecialTime = stats.rechargeSpecialTime;
        
        maxDrones = stats.maxDrones;
        drones = stats.drones;
        dronesList = stats.dronesList;

        
       invIncrease = stats.invIncrease;
        ghostIncrease = stats.ghostIncrease;
        evasion = stats.evasion;
        timeTargetting = stats.timeTargetting;
        statusEffectTime = stats.statusEffectTime;

        laserActivated = stats.laserActivated;
        laserTime = stats.laserTime;
        laserDamage = stats.laserDamage;

        statusEffects = new float[Enum.GetNames(typeof(StatusEffects)).Length];
    }

    public void addDrone(PowerUps type){
        Debug.Log(type);
        if (drones >= maxDrones){
            Debug.Log("RETURNED");
            return;
        }
       
        if (dronesList.Length <= maxDrones){
            dronesList[drones] = type;
            Debug.Log("PUT NORMALLY: " + type);
            drones ++;
        } 
    }

    public void increaseDroneList(){
        maxDrones++;
        PowerUps[] dronesNew = new PowerUps[maxDrones];
        for(int i=0; i < dronesList.Length;i++){
            dronesNew[i] = dronesList[i];
        } 
        dronesList = dronesNew;
    }
}
