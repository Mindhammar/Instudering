using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tileData;

    [SerializeField] TurnBasedController turnBasedController;
    [SerializeField] UnitManager unitManager;

    public Dictionary<TileBase, TileData> DataFromTiles { get; private set; }
    public HashSet<Vector3Int> WalkableTilePositions { get; private set; }
    public TileBase CurrentTile { get; private set; }
    public Vector3Int CurrentTilePosition { get; private set; }


    private void Awake()
    {
        DataFromTiles = new Dictionary<TileBase, TileData>();
        WalkableTilePositions = new HashSet<Vector3Int>();

        foreach (var tileDataInfo in tileData)
        {
            foreach (var tile in tileDataInfo.tiles)
            {
                // Add the tile to the DataFromTiles dictionary
                if (!DataFromTiles.TryAdd(tile, tileDataInfo))
                {
                    Debug.LogWarning($"Duplicate: {tile} found. Skipping.");
                    continue;
                }

                // Check for canWalk in tileData and add to List
                if (tileDataInfo.canWalk)
                {
                    foreach (var pos in map.cellBounds.allPositionsWithin)
                    {
                        if (map.GetTile(pos) == tile)
                        {
                            WalkableTilePositions.Add(pos);
                        }
                    }
                }
            }
        }
    }


    public void GetTile()
    {

        if (Camera.main != null)
        {
            Vector2 mouseCameraPosition = turnBasedController.PointerPosition;

            Vector3 mouseWorldPosition =
                Camera.main.ScreenToWorldPoint(new Vector3(mouseCameraPosition.x, mouseCameraPosition.y, 0));

            Vector3Int gridPosition = map.WorldToCell(mouseWorldPosition);

            CurrentTilePosition = gridPosition;

            TileBase targetTile = map.GetTile(gridPosition);
            if (targetTile is null)
            {
                Debug.Log("No tile found");

                return;
            }

            float modifierMovement = DataFromTiles[targetTile].modifierMovement;
            bool canWalk = DataFromTiles[targetTile].canWalk;
            CurrentTile = targetTile;
            Debug.Log("Rutan är " + targetTile + " på " + gridPosition + "      [canWalk: " + canWalk +
                      "] [modifierMovement : " + modifierMovement + "]");

        }

    }


    public void SelectionInput()
    {
        GetTile();

        if (unitManager.UnitPositions.ContainsKey(CurrentTilePosition))
        {
            unitManager.SelectUnit(CurrentTilePosition);

        }
        else
        {
            Debug.Log("Tile has no unit.");
            unitManager.hasSelectedUnit = false;
        }
    }

    public void MoveInput()
    {
        GetTile();
        if (unitManager.hasSelectedUnit)
        {
            unitManager.OnTryMoveUnit();
        }
        else
        {
            Debug.Log("No unit selected.");

        }
    }
}