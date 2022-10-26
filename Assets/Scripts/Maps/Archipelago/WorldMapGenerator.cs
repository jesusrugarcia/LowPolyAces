using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapGenerator : MonoBehaviour
{
    public WorldMapGraph generateWorldMapGraph(int layers, int maxNodesPerLayer, int maxNodes, Vector3 initialPos, Vector3 finalPos, float xIncrease, float zIncrease, float xMax, float zMax, float minDistance, float distanceAdd, int maxFinalNodes){
        var graph = new WorldMapGraph(layers + 1);
        graph.nodes[0].screenPos = finalPos;
        graph.nodes[1].screenPos = initialPos;

        generateNodeChildren(graph, 1, maxNodesPerLayer, maxNodes, 1, layers, initialPos, xIncrease, zIncrease, xMax, zMax, minDistance, distanceAdd);

        checkLastNodes(graph,maxFinalNodes);
        
        return graph;
    }

    public void generateNodeChildren(WorldMapGraph graph, int nodePos, int maxNodesPerLayer, int maxNodes, int currentLayer, int maxLayers, Vector3 parentPos, float xIncrease, float zIncrease, float xMax, float zMax, float minDistance, float distanceAdd){
        var nodesToGenerate = UnityEngine.Random.Range(1,maxNodesPerLayer + 1);
        if((nodePos == 1 && nodesToGenerate < 2) || nodesToGenerate == 0){
            nodesToGenerate += 1;
        }
        var nodePositions = new int[nodesToGenerate];

        for (int i=0; i < nodesToGenerate; i++){
            nodePositions[i] = graph.generateChildrenNode(nodePos, currentLayer);

            var newPos = calculateNewPos(parentPos, xIncrease, zIncrease, xMax, zMax, i , nodesToGenerate);
            graph.nodes[nodePositions[i]].screenPos = newPos;

            calculateEnoughDistance(graph, graph.nodes[nodePositions[i]], minDistance, distanceAdd);

            ManageConnections(graph,i , nodePositions);
            ManageEnding(graph, currentLayer, maxLayers, maxNodesPerLayer, maxNodes, i,nodePositions, newPos, xIncrease, zIncrease, xMax, zMax, minDistance, distanceAdd);

            
        }

    }

    public Vector3 calculateNewPos(Vector3 pos, float xIncrease, float zIncrease, float xMax, float zMax, int i, int max){ //Hay que ajustalo
        Vector3 newPos = pos;
        newPos.x += UnityEngine.Random.Range(xIncrease, xIncrease + (xIncrease / 4));

        newPos.z +=  UnityEngine.Random.Range(zIncrease/ 2 , zIncrease + zIncrease/ 2);
        var negative =  UnityEngine.Random.Range(0,2);
        if(negative == 0){
            newPos.z *= -1;
        }

        if(newPos.x > xMax){
            newPos.x = xMax;
        } else if(newPos.x < - xMax){
            newPos.x = -xMax;
        } 
        if(newPos.z > zMax){
            newPos.z = zMax;
        } else if(newPos.z < - zMax){
            newPos.z = -zMax;
        }

        //if (i!= 0){//i%2 == 0 && i != 0){
            //newPos.z +=  UnityEngine.Random.Range(zIncrease , zIncrease + (zIncrease/4));
        //} //else if (i > max/2){
            //newPos.z +=  -UnityEngine.Random.Range(zIncrease , zIncrease + (zIncrease/4));
       // }

        return newPos;
    }

    public void ManageEnding(WorldMapGraph graph, int currentLayer, int maxLayers, int maxNodesPerLayer, int maxNodes, int i, int[] nodePositions, Vector3 parentPos, float xIncrease, float zIncrease, float xMax, float zMax, float minDistance, float distanceAdd){
        if (currentLayer < maxLayers && graph.size < maxNodes){ //generate once again
                var nodesIncrease = UnityEngine.Random.Range(0,10);
                int nextNodes = maxNodesPerLayer;
                if(nodesIncrease == 0){
                    nextNodes += 1;
                } else if (nodesIncrease > 1){
                    nextNodes += -1;
                }
                generateNodeChildren(graph, nodePositions[i], nextNodes, maxNodes, currentLayer + 1, maxLayers, parentPos, xIncrease, zIncrease, xMax, zMax, minDistance, distanceAdd);
            } else {
                graph.connectNodes(0,nodePositions[i]); //connect to final node
            }
    }

    public void ManageConnections(WorldMapGraph graph, int i, int[] nodePositions){
        if (i != 0){
            var connect = UnityEngine.Random.Range(0,2);
            if(connect == 0){
                graph.connectNodes(nodePositions[i-1],nodePositions[i]);
            }
        }
        for(int j= 2; j< graph.size;j++) {
            if(graph.nodes[j].layer == graph.nodes[nodePositions[i]].layer){
                var connect = UnityEngine.Random.Range(0,9);
                if(connect == 0){
                    graph.connectNodes(j,nodePositions[i]);
                }
            }
        }
    }

    public void checkLastNodes(WorldMapGraph graph, int maxFinalNodes){
        var lastNodes = graph.nodes[0].getConnectedNodes();
        if(lastNodes.Length >= maxFinalNodes){
            var nodesToUncconect = lastNodes.Length - maxFinalNodes;
            for(int i=0; i< nodesToUncconect; i++){
                var nodeToDesconnect = lastNodes[UnityEngine.Random.Range(0, lastNodes.Length)];
                graph.unconnectNodes(nodeToDesconnect, 0);
                if (graph.nodes[nodeToDesconnect].countConnections() < 2){
                    if(graph.nodes[nodeToDesconnect].connectedNodes[nodeToDesconnect - 1] == false){
                        graph.connectNodes(nodeToDesconnect -1, nodeToDesconnect);
                    } else if(nodeToDesconnect < graph.size-1 && graph.nodes[nodeToDesconnect].connectedNodes[nodeToDesconnect + 1] == false){
                        graph.connectNodes(nodeToDesconnect + 1, nodeToDesconnect);
                    } else {
                        graph.connectNodes(UnityEngine.Random.Range(1, graph.size), nodeToDesconnect);
                    }
                    
                }
                lastNodes = graph.nodes[0].getConnectedNodes();
            }
        }
    }

    public void calculateEnoughDistance(WorldMapGraph graph, WorldMapNode node, float minDistance, float distanceAdd){
        for (int i=0; i < graph.size; i++){
            var currentNode = graph.nodes[i];
            if(i != node.pos ){//&& currentNode.layer == node.layer){
                var distance = Vector3.Distance(node.screenPos, currentNode.screenPos);
                if(distance < minDistance){
                    if (currentNode.screenPos.x < node.screenPos.x){
                        node.screenPos.x += distanceAdd;
                    } else {
                        currentNode.screenPos.x += distanceAdd;
                    }

                    if (currentNode.screenPos.z < node.screenPos.z){
                        node.screenPos.z += distanceAdd;
                    } else {
                        currentNode.screenPos.z += distanceAdd;
                    }

                    calculateEnoughDistance(graph, node, minDistance, distanceAdd);
                }
            }
        }
    }
}
