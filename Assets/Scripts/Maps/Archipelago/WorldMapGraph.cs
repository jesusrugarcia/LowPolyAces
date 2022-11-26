using System;
using UnityEngine;

[Serializable]
public class WorldMapGraph 
{
    public int stage;
    public int size;
    public WorldMapNode[] nodes;
    public int currentMapNode;
    public DecoIslandNode[] decoIslands;
    public int layers;

    public WorldMapGraph(int layers, int decoIslandsNum, int stageNum = 0 ){
        size = 2;
        nodes = new WorldMapNode[size];
        nodes[0] = new WorldMapNode(0,layers,size);  //lastNode 
        nodes[1] = new WorldMapNode(1,0,size);   
        currentMapNode = 1;
        nodes[1].visited = true;
        nodes[0].type = NodeType.Boss;
        nodes[1].type = NodeType.Initial;
        stage = stageNum;
        this.layers = layers;

        decoIslands = new DecoIslandNode[decoIslandsNum];
    }

    public int generateChildrenNode(int parentPos, int layer){
        size += 1;
        var newNodes = new WorldMapNode[size];
        for (int i = 0; i < nodes.Length; i++){
            newNodes[i] = nodes[i];
            newNodes[i].updateConnections(size);
        }
        var newNode = new WorldMapNode(size - 1, layer, size);
        newNode.connectedNodes[parentPos] = true;
        newNodes[size - 1] = newNode;
        nodes = newNodes;
        nodes[parentPos].connectedNodes[size - 1] = true;
        return size - 1;
    }

    public void connectNodes(int node1, int node2){
        nodes[node1].connectedNodes[node2] = true;
        nodes[node2].connectedNodes[node1] = true;
    }

    public void unconnectNodes(int node1, int node2){
        nodes[node1].connectedNodes[node2] = false;
        nodes[node2].connectedNodes[node1] = false;
    }
}
