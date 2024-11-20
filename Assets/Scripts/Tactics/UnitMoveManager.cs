using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitMoveManager : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] GameObject unitPrefab;

   
    public Dictionary<Vector3Int, GameObject> UnitPositions { get; } = new Dictionary<Vector3Int, GameObject>();
    public bool hasSelectedUnit;
    private Vector3Int _selectedUnitTilePosition;
    private string _selectedUnitName;

    
    //Example that populates the whole map
    public void PlaceUnitsOnWalkableTiles()
    {
        foreach (var tilePosition in mapManager.WalkableTilePositions)
        {
            PlaceUnitOnTile(tilePosition, unitPrefab);
        }
    }

    public void SpawnUnitHere()
    {
        PlaceUnitOnTile(mapManager.CurrentTilePosition, unitPrefab);
    }
    
    //Main Unit logic, add variables for different kinds of unitPrefabs!!!
    void PlaceUnitOnTile(Vector3Int tilePosition, GameObject unitPrefab)
    {
        if (mapManager.WalkableTilePositions.Contains(tilePosition))
        {
            if (unitPrefab == null)
            {
                Debug.LogError("Unit prefab is not assigned.");
                return;
            }

            if (UnitPositions.ContainsKey(tilePosition))
            {
                Debug.Log("Tile is already occupied by another unit.");
                return;
            }

            Vector3 worldPosition = TileUtil.TileCenter(tilemap, tilePosition);
            GameObject unit = Instantiate(unitPrefab, worldPosition, Quaternion.identity);
            UnitPositions[tilePosition] = unit;
        }
    }


    public void SelectUnit(Vector3Int tilePosition)
    {
        if (UnitPositions.TryGetValue(tilePosition, out var selectedUnit))
        {
            hasSelectedUnit = true;
            Debug.Log("Selected unit " + selectedUnit + "at " + tilePosition);
            _selectedUnitTilePosition = tilePosition;
            _selectedUnitName = selectedUnit.name;
            
        }
    }
    
   
    
    
    public void RemoveUnitFromTile()
    {

        if (hasSelectedUnit)
        {
            if (UnitPositions.ContainsKey(_selectedUnitTilePosition))
            {
                Debug.Log("Removed unit " + _selectedUnitName + "at " + _selectedUnitTilePosition);
                Destroy(UnitPositions[_selectedUnitTilePosition]);
                UnitPositions.Remove(_selectedUnitTilePosition);
                hasSelectedUnit = false;
            }
          
        }
        else
        {
            Debug.Log("No unit selected.");
        }
    }


    public void OnTryMoveUnit()
    {
        TryMoveUnit(mapManager.CurrentTilePosition);
    }


    void TryMoveUnit(Vector3Int targetMovePosition)
    {
        
        if (!hasSelectedUnit)
        {
            Debug.Log ("No unit selected to move!");
            return;
        }
        
        var targetTile = tilemap.GetTile(targetMovePosition);
        if (targetTile == null)
        {
            Debug.Log("Tile is outside of the map.");
            return;
        }

        if (!mapManager.WalkableTilePositions.Contains(targetMovePosition))
        {
            Debug.Log("Can't move " + _selectedUnitName + " at " + _selectedUnitTilePosition + mapManager.CurrentTile);
            return;
        }

        if (UnitPositions.ContainsKey(targetMovePosition))
        {
            Debug.Log("Target tile is already occupied by another unit.");
            return;
        }
        
        //check currently selected unit AP (movement points/action points)
        
        MoveUnit(targetMovePosition);

    }
    void MoveUnit(Vector3Int targetMovePosition)
    {
       GameObject selectedUnit = UnitPositions[_selectedUnitTilePosition];
       
       UnitPositions.Remove(_selectedUnitTilePosition);
       UnitPositions[targetMovePosition] = selectedUnit;
       
       Vector3 centeredTargetMovePosition = TileUtil.TileCenter(tilemap, targetMovePosition);
       
       selectedUnit.transform.position = centeredTargetMovePosition;
       
       hasSelectedUnit = false;
        
        
    }
    
    
    
}
