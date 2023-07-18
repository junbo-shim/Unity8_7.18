using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMap : TileMapController
{
    private const string OBSTACLE_TILEMAP_OBJ_NAME = "Tilemap_Obstacle";
    // ��ã�� �˰����� �׽�Ʈ �� ������� �������� ĳ���� ������Ʈ �迭
    private GameObject[] castleObj = default;
    


    // ! Awake Ÿ�ӿ� �ʱ�ȭ �� ������ �������Ѵ�.
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
        // { ������� �������� �����ؼ� Ÿ���� ��ġ�Ѵ�.
        castleObj = new GameObject[2];
        TerrainController[] passableTerrains = new TerrainController[2];

        List<TerrainController> searchTerrains = default;
        int searchIdx = 0;
        TerrainController foundTile = default;

        // ������� �������� �������� y ���� ��ġ�Ͽ� �� ������ �޾ƿ´�.
        searchIdx = 0;
        foundTile = default;
        // ������� ã�� ����
        while (foundTile == null || foundTile == default) 
        {
            // ������ �Ʒ��� ��ġ�Ѵ�.
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

        // �������� �������� �������� y ���� ��ġ�ؼ� �� ������ �޾ƿ´�.
        searchIdx = mapController.MapCellSize.x - 1;
        foundTile = default;
        // �������� ã�� ����
        while (foundTile == null || foundTile == default) 
        {
            // �Ʒ����� ���� ��ġ�Ѵ�.
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
        // } ������� �������� �����ؼ� Ÿ���� ��ġ�Ѵ�.



        // { ������� �������� ������ �߰��Ѵ�.
        // } ������� �������� ������ �߰��Ѵ�.
    }

    // ! ������ �߰��Ѵ�.
    public void Add_Obstacle(GameObject obstacle_) 
    {
        allTileObjs.Add(obstacle_);
    }
}
