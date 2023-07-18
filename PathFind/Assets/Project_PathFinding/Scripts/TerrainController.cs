using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    private const string TILE_FRONT_RENDERER_OBJ_NAME = "Renderer_Fore";

    private TerrainType terrainType = TerrainType.NONE;
    private MapBoard mapController = default;

    public bool IsPassible { get; private set; } = false;
    public int tileIdx1D { get; private set; } = -1;
    public Vector2Int tileIdx2D { get; private set; } = default;

    #region 길찾기 알고리즘을 위한 변수
    private SpriteRenderer frontRenderer = default;
    private Color defaultColor = default;
    private Color selectColor = default;
    private Color searchColor = default;
    private Color inactiveColor = default;
    #endregion

    private void Awake()
    {
        frontRenderer = gameObject.FindChildComponent<SpriteRenderer>(
            TILE_FRONT_RENDERER_OBJ_NAME);

        defaultColor = new Color(1f, 1f, 1f, 1f);
        selectColor = new Color(236f / 255f, 130f / 255f, 20f / 255f, 1f);
        searchColor = new Color(0f, 192f / 255f, 0f, 1f);
        inactiveColor = new Color(128f / 255f, 126f / 255f, 128f / 255f, 1f);
    }

    // ! 지형정보를 초기 설정한다
    public void SetupTerrain(MapBoard mapController_, 
        TerrainType type_, int tileIdx1D_) 
    {
        terrainType = type_;
        mapController = mapController_;
        tileIdx1D = tileIdx1D_;
        tileIdx2D = mapController.GetTileIdx2D(tileIdx1D);

        string prefabName = string.Empty;
        // 타일의 타입 별로 다른 설정을 한다.
        switch (type_) 
        {
            case TerrainType.PLAIN_PASS:
                prefabName = RDefine.TERRAIN_PREF_PLAIN;
                IsPassible = true;
                break;
            case TerrainType.OCEAN_N_PASS:
                prefabName = RDefine.TERRAIN_PREF_OCEAN;
                IsPassible = false;
                break;
            default:
                prefabName = "Tile_Default";
                IsPassible = false; 
                break;
        }

        this.name = string.Format("{0}_{1}", prefabName, tileIdx1D);
    }

    // ! 지형의 Front 색상을 변경한다.
    public void SetTileActiveColor(RDefine.TileStatusColor tileStatus) 
    {
        switch (tileStatus)
        {
            case RDefine.TileStatusColor.SELECTED:
                frontRenderer.color = selectColor;
                break;
            case RDefine.TileStatusColor.SEARCH:
                frontRenderer.color = searchColor; 
                break;
            case RDefine.TileStatusColor.INACTIVE:
                frontRenderer.color = inactiveColor;
                break;
            default:
                frontRenderer.color = defaultColor;
                break;
        }
    }
}
