using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public TeamManager teamManager;
    public float health = 1;
    public GameObject target;
    public Material[] materials;
    public float maxHealth;

    private void Start() {
        maxHealth = materials.Length;
    }

    private void FixedUpdate() {
        transform.position = target.transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("This: " + gameObject.name + " Enemy: " + other.gameObject.name);
        if (other.GetComponent<TeamManager>().team != teamManager.team){
            Destroy(other.gameObject);
            damagePlane(other);
        }
        
    }

    public void damagePlane(Collider other){
            health += - other.GetComponent<DamageManager>().damage;
            if (health < 1){
                target.GetComponent<PlaneStats>().hasShield = false;
                Destroy(other.gameObject);
                destroyShield();
            } else {
                setMaterial();
            }
    }

    public void destroyShield(){
        Destroy(gameObject);
    }

    public void setMaterial(){
        GetComponent<Renderer>().material = materials[(int)health - 1];
    }

    public void addHealth(){
        if (health < maxHealth){
            health ++;
            setMaterial();
        }
    }
}
