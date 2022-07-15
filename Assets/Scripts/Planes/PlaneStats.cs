using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStats : MonoBehaviour
{
    public float maxHealth = 0;
    public float health = 1;

    public float maxSpeed = 0;
    public float speed = 0;

    public float rotationSpeed = 0;
    public float rotation = 0;
    public float maxRotation = 0;

    public float acceleration =0;
    public float shootSpeed = 0;

    public float scoreValue = 0;
    public float timeToRotate = 1f;

    public float price = 0;

    public int missiles = 0;
    public int maxMissiles = 1;

    public bool hasShield = false;

    public PlaneManager plane;

    private void Start() {
        health = maxHealth;
    }
}
