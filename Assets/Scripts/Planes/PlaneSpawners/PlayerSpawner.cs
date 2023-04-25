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
        GameObject plane;
        if (controller.data.selectedPlayer[player] < playerList.planes.Length){
            prefab = controller.data.selectedPlayer[player];
        }
        
        if(player == 0){
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite && controller.rogueliteSave.loadStats){ //loadstats means stats need to be loaded from mision modes previous state.
                save = controller.rogueliteSave.stats[player];
            }
            plane = spawner.spawnPlane(playerList.planes[prefab],controller.characters.characters[controller.data.selectedChar[player]],movement.Player,team,stats:save);
        } else if(player ==1){
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite &&controller.rogueliteSave.loadStats){
                save = controller.rogueliteSave.stats[player];
            }
            plane =  spawner.spawnPlane(playerList2.planes[prefab],controller.characters.characters[controller.data.selectedChar[player]],movement.Player,team,stats:save);
        } else if(player ==2){
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite &&controller.rogueliteSave.loadStats){
                save = controller.rogueliteSave.stats[player];
            }
            plane =  spawner.spawnPlane(playerList3.planes[prefab],controller.characters.characters[controller.data.selectedChar[player]],movement.Player,team,stats:save);
        } else {
            StatsSave save = null;
            if(controller.gameOptions.mode == gameMode.roguelite &&controller.rogueliteSave.loadStats){
                save = controller.rogueliteSave.stats[player];
            }
            plane =  spawner.spawnPlane(playerList4.planes[prefab],controller.characters.characters[controller.data.selectedChar[player]],movement.Player,team,stats:save);
        }
         

         return plane;
    }
}
