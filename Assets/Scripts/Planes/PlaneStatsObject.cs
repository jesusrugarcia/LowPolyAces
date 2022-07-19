using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/ PlaneStatsObject")]
public class PlaneStatsObject : ScriptableObject
{
    public float maxHealth = 0;
    public float health = 1;

    public float maxSpeed = 0;
    public float speed = 0;
    public float acceleration =0;

    public float rotationSpeed = 0;
    public float rotation = 0;
    public float maxRotation = 0;

    public float bulletDamage = 1;
    public float damageOnButton = 0.1f;

    public float shootSpeed = 0;
    public int magazineSize = 10;

    public int missiles = 0;
    public int maxMissiles = 1;

    public float scoreValue = 0;
    public float timeToRotate = 1f;

    public float price = 0;
}
