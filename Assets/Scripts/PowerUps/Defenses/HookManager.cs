using System;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    public bool inverse = false;
    public bool isHooked = false;
    public bool onEdge = false;
    public float speed = 10;
    public float moveSpeed = 10;
    public PlaneManager plane;
    public GameObject hook;
    public bool move = false;
    public Transform target;
    public GameObject targetObject;

    void FixedUpdate()
    {
        try{
            drawLine(hook.transform.position, plane.transform.position, 0.1f, 0.1f);
            advance();
            if(move){
                if(!inverse){
                    movePlane();
                } else {
                    moveObject();
                }
                
            }
            var targete = targetObject.gameObject;
            if((inverse && isHooked && targete == null) || (!inverse && isHooked && targete == null && !onEdge)){
                isHooked = false;
                onEdge = false;
                move = false;
                gameObject.SetActive(false);
            }
        } catch (Exception e){
            Debug.Log(e);
            isHooked = false;
            onEdge = false;
            move = false;
            gameObject.SetActive(false);
        }
        
    }

    public void advance(){
        if(!isHooked){
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            checkInScreen();
        } else {
            hook.transform.position = target.position;
        }
        

    }

    public void checkInScreen(){
        if (transform.position.z < -plane.controller.maz || transform.position.z > plane.controller.maz || transform.position.x < -plane.controller.max || transform.position.x > plane.controller.max){
            if(!inverse){
                onEdge = true;
                isHooked = true;
                target = hook.transform;
            } else {
                gameObject.SetActive(false);
            }
            

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject != plane.gameObject){
            var shield = other.gameObject.GetComponent<ShieldManager>();
            if(shield != null){
                target = shield.target.transform;
            } else {
                target = other.gameObject.transform;
            }
            isHooked = true;
            
            targetObject = other.gameObject;
        }
        
    }

    public void movePlane(){
        plane.transform.position = Vector3.MoveTowards(plane.transform.position, target.position, Time.deltaTime * moveSpeed);
        checkHookPos();
    } 
    public void moveObject(){
        targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, plane.transform.position, Time.deltaTime * moveSpeed);
        checkHookPos();
    }

    public void checkHookPos(){
        if (Vector3.Distance(plane.transform.position, hook.transform.position) < 0.2f){
            plane.stats.statusEffects[(int)StatusEffects.Invulnerability] = 0;
            gameObject.SetActive(false);
            isHooked = false;
            move = false;
            onEdge = false;
        }
    }

    public void drawLine(Vector3 start, Vector3 end, float duration, float thickness){
        if (thickness == 0){
            thickness = 0.01f;
        }

        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();  
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.SetColor("_BaseColor",Color.gray);
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.gray;
        lineRenderer.material = mat ;
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        lineRenderer.SetPosition(0,start);
        lineRenderer.SetPosition(1,end);
        Destroy(line,Time.deltaTime);
    }
}
