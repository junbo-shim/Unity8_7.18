using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindButton : MonoBehaviour
{
    // ! Find A* 버튼을 누른 경우
    public void OnClickAstarFindBtn() 
    {
        PathFinder.Instance.FindPath_Astar();
    }
}