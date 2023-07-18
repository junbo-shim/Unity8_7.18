using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    protected string tileMapObjName = default;

    protected MapBoard mapController = default;
    protected Tilemap tileMap = default;
    protected List<GameObject> allTileObjs = default;

    // ! Awake Ÿ�ӿ� �ʱ�ȭ�� ������ ��ӹ��� Ŭ���� ���� �������Ѵ�.
    public virtual void InitAwake(MapBoard mapController_) 
    {
        mapController = mapController_;
        tileMap = gameObject.FindChildComponent<Tilemap>("Tilemap_Terrain");

        // ���簢�� ���·� �ʱ�ȭ �� Ÿ���� ĳ���ؼ� ������ �ִ´�.
        //allTileObjs = tileMap.gameObject.GetChildrenObjs();
        allTileObjs = tileMap.gameObject.GetChildrenObjs();

        //Debug.LogFormat("Count: {0}", allTileObjs.Count);

        if (allTileObjs.IsValid()) 
        {
            //allTileObjs.Sort(GFunc.CompareTileObjToLocalPos2D);
            allTileObjs.Sort(GFunc.CompareTileObjToLocalPos2D);
        }
        else { allTileObjs = new List<GameObject>(); }
    }
}