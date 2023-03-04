using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int widht;
    private int height;
    private float sellSize;
    private GridObject [,] gridObjectArray;
    public GridSystem(int widht, int height, float sellSize)
    {
        this.widht = widht;
        this.height = height;
        this.sellSize = sellSize;

        gridObjectArray = new GridObject[widht, height];

        for (int x = 0; x < widht; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridObjectArray[x,z] =  new GridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * sellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition
        (
            Mathf.RoundToInt(worldPosition.x / sellSize),
            Mathf.RoundToInt(worldPosition.z / sellSize)
        );
    }
    
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < widht; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x,gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return  gridPosition.x >= 0 &&
                gridPosition.z >= 0 &&
                gridPosition.x < widht &&
                gridPosition.z < height;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetWidht()
    {
        return widht;
    }
}
