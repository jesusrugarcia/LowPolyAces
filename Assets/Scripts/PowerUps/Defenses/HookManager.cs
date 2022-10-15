using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    public bool isHooked = false;
    public float speed = 10;
    public float moveSpeed = 10;
    public PlaneManager plane;
    public GameObject hook;
    public bool move = false;
    public Transform target;

    void FixedUpdate()
    {
        drawLine(hook.transform.position, plane.transform.position, 0.1f, 0.1f);
        advance();
        if(move){
            movePlane();
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
            isHooked = true;
            target = hook.transform;

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject != plane.gameObject){
            isHooked = true;
            target = other.gameObject.transform;
        }
        
    }

    void movePlane(){
        plane.transform.position = Vector3.MoveTowards(plane.transform.position, target.position, Time.deltaTime * moveSpeed);
        checkHookPos();
    }

    public void checkHookPos(){
        if (Vector3.Distance(plane.transform.position, hook.transform.position) < 0.1f){
            plane.stats.statusEffects[(int)StatusEffects.Invulnerability] = 0;
            gameObject.SetActive(false);
            isHooked = false;
            move = false;
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
