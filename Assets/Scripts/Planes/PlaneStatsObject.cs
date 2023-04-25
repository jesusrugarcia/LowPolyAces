using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/ PlaneStatsObject")]
public class PlaneStatsObject : ScriptableObject
//TURN INTO AN ARRAY?
{
    public float maxHealth = 0;
    public float health = 1;

    public float maxSpeed = 0;
    public float speed = 0;
    public float acceleration =0;

    public float rotationSpeed = 0;
    public float rotation = 0;
    public float maxRotation = 0;
    public float timeToRotate = 1f;
    public float timeRotating = 1f;

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

    public BulletType bulletType;
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
    public int price = 0;

    public bool hasShield = false;
    public float damageReductionTankShield = 0.7f;
    public float SpecialShieldDuration = 1;
    public float healAreaAmount = 0.01f;
    public float rechargeDefenseTime = 5f;
    public float rechargeSpecialTime = 10f;
    
    public int maxDrones = 1;
    public int drones = 0;
    public PowerUps[] dronesList;

    public float[] statusEffects = new float[Enum.GetNames(typeof(StatusEffects)).Length];
    public float invIncrease = 0.1f;
    public float ghostIncrease = 1f;
    public float evasion = 0;
    public float timeTargetting = 2f;
    public float statusEffectTime = 1;
    

    public bool laserActivated = false;
    public float laserTime = 1;
    public float laserDamage = 0.1f;
    public int revival = 0;
}
