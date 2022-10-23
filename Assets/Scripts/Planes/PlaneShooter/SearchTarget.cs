using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct TargetResult
{
    public GameObject target;
    public float distance;
}

public static class SearchTarget 
{
    public static TargetResult searchTarget(GameObject origin, float targetDistance, Vector3 direction, bool checkForMines = false, bool targetAllies = false){
        try{
            TargetResult result = new TargetResult();
            RaycastHit hit;
            Ray ray = new Ray(origin.transform.position, direction);
            Debug.DrawRay(origin.transform.position, direction * targetDistance, Color.blue, .5f);
            if(Physics.Raycast(ray, out hit, targetDistance)){
                var detected = hit.collider.gameObject;
                var dist = Vector3.Distance(detected.transform.position, origin.transform.position);
                var mine = detected.GetComponent<Mine>();
                if((!targetAllies && !hit.collider.isTrigger && detected.GetComponent<TeamManager>().team != origin.GetComponent<TeamManager>().team && detected.GetComponent<PlaneStats>().statusEffects[(int)StatusEffects.Ghost] <= 0) || // enemies
                (checkForMines && mine != null && hit.collider.isTrigger && detected.GetComponent<TeamManager>().team == origin.GetComponent<TeamManager>().team) || //mines
                (targetAllies && !hit.collider.isTrigger && detected.GetComponent<TeamManager>().team == origin.GetComponent<TeamManager>().team)){ //allues
                    result.target = detected;
                    result.distance = dist;
                }
            }

            if (result.distance > 0){
                return result;
            } else {
                result.target = origin;
                result.distance = float.MaxValue;
                return result;
            }
        } catch(Exception e){
            Debug.Log(e);
            TargetResult result = new TargetResult();
            result.target = origin;
            result.distance = float.MaxValue;
            return result;
        }
    }

    public static TargetResult searchTarget(GameObject origin, float thickness, float targetDistance, Vector3 direction, bool checkForMines = false, bool targetAllies = false){
        try{
            TargetResult result = new TargetResult();
            RaycastHit hit;
            //Debug.DrawRay(origin.transform.position, direction * targetDistance, Color.green, .5f);
            if(Physics.SphereCast(origin.transform.position, thickness, direction, out hit, targetDistance)){
                var detected = hit.collider.gameObject;
                var dist = hit.distance; //Vector3.Distance(detected.transform.position, origin.transform.position);
                var mine = detected.GetComponent<Mine>();
                if((!targetAllies && !hit.collider.isTrigger && detected.GetComponent<TeamManager>().team != origin.GetComponent<TeamManager>().team && detected.GetComponent<PlaneStats>().statusEffects[(int)StatusEffects.Ghost] <= 0) || // enemies
                (checkForMines && mine != null && hit.collider.isTrigger && detected.GetComponent<TeamManager>().team == origin.GetComponent<TeamManager>().team) || //mines
                (targetAllies && !hit.collider.isTrigger && detected.GetComponent<TeamManager>().team == origin.GetComponent<TeamManager>().team)){ //allues
                    result.target = detected;
                    result.distance = dist;
                }
            }

            if (result.distance > 0){
                return result;
            } else {
                result.target = origin;
                result.distance = float.MaxValue;
                return result;
            }
        } catch(Exception e){
            Debug.Log(e);
            TargetResult result = new TargetResult();
            result.target = origin;
            result.distance = float.MaxValue;
            return result;
        }
    }

    public static int CompareTargetResults(TargetResult[] results){
        var best = 0;
        for(int i = 1; i < results.Length; i++){
            if(results[i].distance < results[best].distance){
                best = i;
            }
        }

        return best;
    }
}

