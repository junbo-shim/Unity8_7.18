using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMap : TileMapController
{
    private const string OBSTACLE_TILEMAP_OBJ_NAME = "Tilemap_Obstacle";
    // 길찾기 알고리즘을 테스트 할 출발지와 목적지를 캐싱한 오브젝트 배열
    private GameObject[] castleObj = default;
    


    // ! Awake 타임에 초기화 할 내용을 재정의한다.
    public override void InitAwake(MapBoard mapController_)
    {
        this.tileMapObjName = OBSTACLE_TILEMAP_OBJ_NAME;
        base.InitAwake(mapController_);
    }

    private void Start()
    {
        StartCoroutine(DelayStart(0f));
    }

    private IEnumerator DelayStart(float delay) 
    {
        yield return new WaitForSeconds(delay);
        DoStart();
    }

    private void DoStart() 
    {
        // { 출발지와 목적지를 설정해서 타일을 배치한다.
        castleObj = new GameObject[2];
        TerrainController[] passableTerrains = new TerrainController[2];

        List<TerrainController> searchTerrains = default;
        int searchIdx = 0;
        TerrainController foundTile = default;

        // 출발지는 좌측에서 우측으로 y 축을 서치하여 빈 지형을 받아온다.
        searchIdx = 0;
        foundTile = default;
        // 출발지를 찾는 루프
        while (foundTile == null || foundTile == default) 
        {
            // 위에서 아래로 서치한다.
            searchTerrains = mapController.GetTerrains_Column(searchIdx, true);

            foreach(var searchTerrain in searchTerrains) 
            {
                if (searchTerrain.IsPassible) 
                {
                    foundTile = searchTerrain;
                    break;
                }
                else { /* Do Nothing */ }
            }

            if (foundTile != null || foundTile != default) { break; }
            if (mapController.MapCellSize.x - 1 <= searchIdx) { break; }
            searchIdx++;
        }
        passableTerrains[0] = foundTile;

        // 목적지는 우측에서 좌측으로 y 축을 서치해서 빈 지형을 받아온다.
        searchIdx = mapController.MapCellSize.x - 1;
        foundTile = default;
        // 목적지를 찾는 루프
        while (foundTile == null || foundTile == default) 
        {
            // 아래에서 위로 서치한다.
            searchTerrains = mapController.GetTerrains_Column(searchIdx);

            foreach(var searchTerrain in searchTerrains) 
            {
                if (searchTerrain.IsPassible) 
                {
                    foundTile = searchTerrain;
                    break;
                }
                else { /* Do Nothing */ }
            }
            if(foundTile != null || foundTile != default) { break; }
            if(searchIdx <= 0) { break; }
            searchIdx--;
        }
        passableTerrains[1] = foundTile;
        // } 출발지와 목적지를 설정해서 타일을 배치한다.



        // { 출발지와 목적지에 지물을 추가한다.
        GameObject changeTilePrefab = ResManager.Instance.
            obstaclePrefabs[RDefine.OBSTACLE_PREF_PLAIN_CASTLE];
        GameObject tempChangeTile = default;

        // 출발지와 목적지를 인스턴스화해서 캐싱하는 루프
        for(int i = 0; i < 2; i++) 
        {
            tempChangeTile = Instantiate(changeTilePrefab, tileMap.transform);
            tempChangeTile.name = string.Format("{0}_{1}",
                changeTilePrefab.name, passableTerrains[i].tileIdx1D);

            tempChangeTile.SetLocalScale(passableTerrains[i].transform.localScale);
            tempChangeTile.SetLocalPos(passableTerrains[i].transform.localPosition);

            // 출발지와 목적지를 캐싱한다.
            castleObj[i] = tempChangeTile;
            Add_Obstacle(tempChangeTile);

            tempChangeTile = default;
        }
        // } 출발지와 목적지에 지물을 추가한다.

        Update_SourDestToPathFinder();
    }

    // ! 지물을 추가한다.
    public void Add_Obstacle(GameObject obstacle_) 
    {
        allTileObjs.Add(obstacle_);
    }

    // ! PathFinder에 출발지와 목적지를 설정한다.
    public void Update_SourDestToPathFinder() 
    {
        PathFinder.Instance.sourceObj = castleObj[0];
        PathFinder.Instance.destinationObj = castleObj[1];
    }
}
