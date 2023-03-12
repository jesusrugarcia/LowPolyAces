using System;
using UnityEngine;

public enum NodeType{
    Combat,
    Shop,
    Initial,
    Boss
}

[Serializable]
public class DecoIslandNode{
    public int seed = 0;
    public Vector3 screenPos = new Vector3(0,0,0);
}

[Serializable]
public class WorldMapNode{
    public NodeType type;
    public int pos;
    public int layer;
    public bool[] connectedNodes;
    public Vector3 screenPos;
    public int seed;
    public bool visited;
    public bool combatEnded;
    public int[] enemyCount;
    public PowerUpScriptableObject[] powerUps;
    public bool[] purchasedItems = {false,false,false,false};
    public bool shopVisited;

    public WorldMapNode(int posi, int lay, int size, NodeType nodeType = NodeType.Combat){
        type = nodeType;
        pos = posi;
        layer = lay;
        connectedNodes = new bool[size];
        seed = UnityEngine.Random.Range(-9999,9999);
        visited = false;
        combatEnded = false;
        enemyCount = new int[4] {0,0,0,0};
        shopVisited = false;
    }

    public void updateConnections(int size){
        var newConnections = new bool[size];
        for(int i =0; i< connectedNodes.Length; i++){
            newConnections[i] = connectedNodes[i];
        }
        connectedNodes = newConnections;
    }

    public int countConnections(){
        var connections = 0;
        for (int i=0; i < connectedNodes.Length; i++){
            if (connectedNodes[i] == true){
                connections +=1;
            }
        }
        return connections;
    }

    public int[] getConnectedNodes(){
        var connected = new int[countConnections()];
        var current = 0;
        for(int i = 0; i < connectedNodes.Length; i++){
            if(connectedNodes[i] == true){
                connected[current] = i;
                current += 1;
            }
        }

        return connected;
    }

    public int totalEnemies(){
        var enemies = 0;
        for (int i=0; i<enemyCount.Length;i++){
            enemies += enemyCount[i];
        }
        return enemies;
    }
}