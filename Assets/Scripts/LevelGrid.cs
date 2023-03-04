using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid instance {get; private set;}
    [SerializeField] private Transform GridDebugObjectPrafabs;
    private GridSystem gridSystem;

    private void Awake() 
    {
        if(instance != null)
        {
            Debug.LogError("There's more than one LevelGrid!" + transform + "-" + instance);
            Destroy(gameObject);
            return;
        }
        instance = this;

        gridSystem = new GridSystem(10,10,2f);
        gridSystem.CreateDebubObjects(GridDebugObjectPrafabs);
    }

    public void AddUnitGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMoveGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);

        AddUnitGridPosition(toGridPosition, unit);
    }

    public GridPosition GetGridPosition (Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
}
