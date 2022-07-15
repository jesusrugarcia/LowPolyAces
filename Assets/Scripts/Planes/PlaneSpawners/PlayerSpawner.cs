using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    public PlaneModel[] playerList;
    public PlaneModel[] playerList2;
    public PlaneModel[] playerList3;
    public PlaneModel[] playerList4;
    public GameController controller;
    public PlaneSpawner spawner;

    public GameObject spawnPlayer(int player, int team){
        var prefab = 0;
        if (controller.data.selectedPlayer[player] < playerList.Length){
            prefab = controller.data.selectedPlayer[player];
        }
        
        if(player == 0){
            return spawner.spawnPlane(playerList[prefab],movement.Player,team);
        } else if(player ==1){
            return spawner.spawnPlane(playerList2[prefab],movement.Player,team);
        } else if(player ==2){
            return spawner.spawnPlane(playerList3[prefab],movement.Player,team);
        } else {
            return spawner.spawnPlane(playerList4[prefab],movement.Player,team);
        }
         
    }
}
