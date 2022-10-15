using System;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public PlaneManager plane;
    public float time = 0;

    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (plane.stats.laserActivated){
            time += Time.deltaTime;
            laserAllies();
            laserMines();
            if (time >= plane.stats.laserTime){
                plane.stats.laserActivated = false;
                time = 0;
            }
        }
    }

    public void laserAllies(){
        for (int i = 0; i < plane.controller.players.Length; i++){
            try{
                laserTarget(plane.controller.players[i].transform);
            }catch (Exception e){
                Debug.Log(e);
            }
                
            }
    }

    public void laserMines(){
         for (int i = 0; i < plane.controller.players.Length; i++){
            try{
                laserPlayerMines(plane.controller.players[i].GetComponent<PlaneManager>());
            }catch (Exception e){
                Debug.Log(e);
            }
                
            }
    }

    public void laserPlayerMines(PlaneManager plane){
        
        for (int i = 0; i < plane.planeShooter.mines.Length; i++){
            try{
                laserTarget(plane.planeShooter.mines[i].transform);
            }catch (Exception e){
                Debug.Log(e);
            }
            
        }
    
    }

    public void laserTarget(Transform objective, float thickness = 0){
        var distance = Vector3.Distance(gameObject.transform.position,objective.position);
        var direction = objective.position - gameObject.transform.position;
        TargetResult target;
        if( thickness == 0){
            target = SearchTarget.searchTarget(gameObject, distance, direction);
        } else {
            target = SearchTarget.searchTarget(gameObject, thickness, distance, direction);
        }
        var found = target.target.GetComponent<PlaneManager>();
        if(found.teamManager.team != plane.teamManager.team && found.stats.statusEffects[(int)StatusEffects.Invulnerability] <= 0){
            found.collissionManager.damagePlane(damage: plane.stats.laserDamage);
        }
        drawLine(gameObject.transform.position, objective.position, 0.25f, thickness);
    }

    public void drawLine(Vector3 start, Vector3 end, float duration, float thickness){
        if (thickness == 0){
            thickness = 0.01f;
        }

        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();  
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.SetColor("_BaseColor",Color.red);
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.material = mat ;
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        lineRenderer.SetPosition(0,start);
        lineRenderer.SetPosition(1,end);
        Destroy(line,Time.deltaTime);
    }
}
