using System.Collections;
using System.Collections.Generic;
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

    public specialAmmo specialAmmoType;
    public int specialAmmo = 0;
    public int maxSpecialAmmo = 1;
    public int extraBullets = 0;
    public int maxMines = 3;
    public int mines = 0;
    public float meleeTime = 1;
    public bool trackerBullet = false;
    public float searchDistance = 1;

    public float scoreValue = 0;
    public float price = 0;

    public bool hasShield = false;
    public int maxDrones = 1;
    public int drones = 0;
}
