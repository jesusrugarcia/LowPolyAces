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
    RustingClaw
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
    Hook
}

public class PlaneStats : MonoBehaviour
{
    public float maxHealth = 0;
    public float health = 1;

    public float maxSpeed = 0;
    public float speed = 0;
    public float acceleration =0;

    public float rotationSpeed = 0; //how fast the plane rotates
    public float rotation = 0; //how much is currently rotating
    public float maxRotation = 0;
    public float timeToRotate = 1f; //how often enemy to start rotating
    public float timeRotating = 1f;// once rotating, how much time enemy spends rotating.

    public float bulletDamage = 1;
    public float turretDamage = 0.1f;
    public float missileDamage = 10;
    public float mineDamage = 5;
    public float drillDamage = 5;

    public float shootSpeed = 1;
    public int magazineSize = 10;
    public float turretShootSpeed = 1;
    public float normalDroneShootSpeed = 1;
    public float specialDroneShootSpeed = 10f;
    public float auxDroneSpeed = 10f;

    public MissileType missileType;
    public GadgetType gadgetType;
    public DefenseType defenseType;

    public int specialAmmo = 0;
    public int maxSpecialAmmo = 1;
    public int defenseAmmo = 0;
    public int maxDefenseAmmo = 1;
    public int extraBullets = 0;
    public int maxMines = 3;
    public int mines = 0;
    public float meleeTime = 1;
    public bool trackerBullet = false;
    public float searchDistance = 1;

    public float scoreValue = 0;
    public float price = 0;

    public bool hasShield = false;
    public float damageReductionTankShield = 0.7f;
    public float SpecialShieldDuration = 1;
    public float healAreaAmount = 0.01f;
    public float rechargeDefenseTime = 5f;
    public float rechargeSpecialTime = 10f;

    public int maxDrones = 1;
    public int drones = 0;

    public float[] statusEffects = new float[Enum.GetNames(typeof(StatusEffects)).Length];
    public float invIncrease = 0.1f;
    public float ghostIncrease = 1f;
    public float timeTargetting = 2f; //time enemies follow the plane after hit by aggro.
    public float statusEffectTime = 2;
    

    public bool laserActivated = false;
    public float laserTime = 1;
    public float laserDamage = 0.1f;

    public PlaneManager plane;

    private void Start() {
        health = maxHealth;
    }
}
