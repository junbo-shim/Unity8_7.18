using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarNode
{
    public TerrainController Terrain { get; private set; }
    public GameObject DestinationObj { get; private set; }

    // A* algorithm
    public float AstarF { get; private set; } = float.MaxValue;
    public float AstarG { get; private set; } = float.MaxValue;
    public float AstarH { get; private set; } = float.MaxValue;

    public AstarNode AstarPrevNode { get; private set; } = default;

    // AStarNode 생성자
    public AstarNode(TerrainController terrain_, GameObject destinationObj_) 
    {
        Terrain = terrain_;
        DestinationObj = destinationObj_;
    }





    // ! AStar 알고리즘에 사용할 비용을 설정한다.
    public void UpdateCost_Astar(float gCost, float heuristic, 
        AstarNode prevNode) 
    {
        float aStarF = gCost + heuristic;

        // 새로 계산한 비용이 더 작은 경우에만 업데이트 한다.
        if(aStarF < AstarF) 
        {
            AstarG = gCost;
            AstarH = heuristic;
            AstarF = aStarF;

            AstarPrevNode = prevNode;
        }
        else { /*Do Nothing*/ }
    }





    // ! 설정한 비용을 출력한다.
    public void ShowCost_Astar() 
    {
        GFunc.Log($"TileIdx1D: {Terrain.tileIdx1D}, " +
            $"F: {AstarF}, G: {AstarG}, H: {AstarH}");
    }
}
