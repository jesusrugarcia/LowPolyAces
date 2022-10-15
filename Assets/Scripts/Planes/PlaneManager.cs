using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class acts as link for all the components of the planes.

public class PlaneManager : MonoBehaviour
{
    public TeamManager teamManager;
    public PlaneStats stats;
    public HealthBar healthBar;
    public CollisionManager collissionManager;
    public PlaneShooter planeShooter;
    public PlaneMovement planeMovement;
    public GameController controller;
    public StatusEffect statusManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
