using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    public PlanesListScriptableObject playerList;
    public PlanesListScriptableObject playerList2;
    public PlanesListScriptableObject playerList3;
    public PlanesListScriptableObject playerList4;
    public GameController controller;
    public PlaneSpawner spawner;

    public GameObject spawnPlayer(int player, int team){
        var prefab = 0;
        if (controller.data.selectedPlayer[player] < playerList.planes.Length){
            prefab = controller.data.selectedPlayer[player];
        }
        
        if(player == 0){
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite && controller.rogueliteSave.loadStats){
                save = controller.rogueliteSave.stats[player];
            }
            return spawner.spawnPlane(playerList.planes[prefab],movement.Player,team,stats:save);
        } else if(player ==1){
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite &&controller.rogueliteSave.loadStats){
                save = controller.rogueliteSave.stats[player];
            }
            return spawner.spawnPlane(playerList2.planes[prefab],movement.Player,team,stats:save);
        } else if(player ==2){
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite &&controller.rogueliteSave.loadStats){
                save = controller.rogueliteSave.stats[player];
            }
            return spawner.spawnPlane(playerList3.planes[prefab],movement.Player,team,stats:save);
        } else {
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite &&controller.rogueliteSave.loadStats){
                save = controller.rogueliteSave.stats[player];
            }
            return spawner.spawnPlane(playerList4.planes[prefab],movement.Player,team,stats:save);
        }
         
    }
}
