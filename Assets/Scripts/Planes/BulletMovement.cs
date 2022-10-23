using System;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 30;
    public bool isMelee = false;
    public GameController controller;
    public PlaneManager plane;
    public float timer = 0;
    public float meleeOffset;
    public bool meleeFall = false;
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (isMelee){
            timer += Time.deltaTime;
            if (timer >= plane.stats.meleeTime){
                if (!meleeFall){
                    meleeFall = true;
                    timer = 0;
                }else {
                    Destroy(gameObject);
                }
                
            }
        }
        move();
        checkInScreen();
    }

    public void move(){
        if(!isMelee){
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        } else if(!meleeFall){
            transform.position = plane.transform.position + plane.transform.right + (meleeOffset + plane.stats.extraBullets * 0.1f) * plane.transform.up;
            transform.rotation = plane.transform.rotation;
        } else {
            transform.position = transform.position + (transform.right * 0.1f) + new Vector3(0,-0.2f,0);
        }
    }

    public void checkInScreen(){
        if (transform.position.z < -controller.maz || transform.position.z > controller.maz || transform.position.x < -controller.max || transform.position.x > controller.max){
            Destroy(gameObject);
        }
    }

    public void rotateTowards(GameObject target){
        try{
            transform.LookAt(target.transform.position);
            transform.LookAt(transform.position + (-transform.right));
        } catch(Exception e){
            target = gameObject;
            Debug.Log(e);
        }
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        try{
            var plan = other.gameObject.GetComponent<PlaneManager>();
            if(plan.teamManager.team != GetComponent<TeamManager>().team && plan.stats.health > GetComponent<DamageManager>().damage && plan.stats.statusEffects[(int)StatusEffects.Invulnerability] <= 0){
                Instantiate(hitEffect,transform.position,transform.rotation);
            }
        } catch (Exception e){
            Debug.Log(e);
        }
        
    }
}
