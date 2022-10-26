using System;
using UnityEngine;

[Serializable]
public class WorldMapNode{
    public int pos;
    public int layer;
    public bool[] connectedNodes;
    public Vector3 screenPos;
    public int seed;

    public WorldMapNode(int posi, int lay, int size){
        pos = posi;
        layer = lay;
        connectedNodes = new bool[size];
        seed = UnityEngine.Random.Range(-9999,9999);
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
}