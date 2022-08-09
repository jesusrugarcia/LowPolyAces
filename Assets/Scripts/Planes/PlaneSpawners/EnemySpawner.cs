using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour //This class SUCKS, needs refactor mate!
{
    public EnemyList enemyList;
    public GameController controller;
    public PlaneSpawner spawner;
    
    public float scoreForMedium = 20;
    public float scoreForHard = 50;
    public float scoreForImposible = 100;
    public float delayTimer = 0;
    public float timeToDelay = 1;
    public float timeToDelayMultiplier = 4;
    public float lesserDifProbability =  0.5f;


    public void spawnEnemy(){
        timeToDelay = controller.currentEnemies/timeToDelayMultiplier;
        delayTimer += Time.deltaTime;
        if(delayTimer < timeToDelay){
            return;
        }
        
        int prefab = 0;
        if (controller.totalScore < scoreForMedium){
            prefab = (int)Math.Round((float)UnityEngine.Random.Range(0,enemyList.prefabsEasy.planes.Length));
            spawnEasy(prefab);
        } else if (controller.totalScore < scoreForHard){
            prefab = (int)Math.Round((float)UnityEngine.Random.Range(0,enemyList.prefabsMedium.planes.Length));
            spawnMedium(prefab);
        } else if (controller.totalScore < scoreForImposible){
            prefab = (int)Math.Round((float)UnityEngine.Random.Range(0,enemyList.prefabsHard.planes.Length));
            spawnHard(prefab);
        } else {
            prefab = (int)Math.Round((float)UnityEngine.Random.Range(0,enemyList.prefabsImposible.planes.Length));
            spawnImposible(prefab); 
        }
        delayTimer = 0;
        
    }

    public void spawnEasy(int prefab){
        spawner.spawnPlane(enemyList.prefabsEasy.planes[prefab],movement.Easy,0);
    }

    public void spawnMedium(int prefab){
        if(UnityEngine.Random.Range(0f,1f) < lesserDifProbability){
                spawnEasy(prefab);
                return;
            }
        spawner.spawnPlane(enemyList.prefabsMedium.planes[prefab],movement.Medium,0);
    }

    public void spawnHard(int prefab){
        if(UnityEngine.Random.Range(0f,1f) < lesserDifProbability){
                spawnMedium(prefab);
                return;
            }
        spawner.spawnPlane(enemyList.prefabsHard.planes[prefab],movement.Hard,0);
    }

    public void spawnImposible(int prefab){
        if(UnityEngine.Random.Range(0f,1f) < lesserDifProbability){
                spawnHard(prefab);
                return;
            }
        spawner.spawnPlane(enemyList.prefabsImposible.planes[prefab],movement.Impossible,0);
    }
}
