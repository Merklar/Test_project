using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public static int gridWeight = 10;
    public static int gridHeight = 10;

    public Transform[,] grid = new Transform[gridWeight, gridHeight];

    public Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public bool IsInsideGrid(Vector2 pos)
    {
        return (((int)pos.x >= 0) && ((int)pos.x < gridWeight) && ((int)pos.y >= 0));
    }

    public void Delete(int y)
    {
        for (int x = 0; x < gridWeight; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public bool IsFull(int y)
    {
        for (int x = 0; x < gridWeight; x++)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if (IsFull(y))
            {
                Delete(y);
                RowDownAll(y + 1);
                y--;
            }
        }
    }

    public void RowDown(int y)
    {
        for (int x = 0; x < gridWeight; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void RowDownAll(int y)
    {
        for (int i = y; i < gridHeight; i++)
            RowDown(i);
    }

    public bool OnCheckValidFigurePos(Transform _transform)
    {
        foreach (Transform subFigure in _transform)
        {
            Debug.Log("check Child");
            Vector2 pos = Round(subFigure.position);
            Debug.Log(pos);
            if (IsInsideGrid(pos) == false)
            {
                return false;
            }
            if (GetTransformOnPosition(pos) != null && GetTransformOnPosition(pos).parent != _transform)
            {
                return false;
            }
        }

        return true;
    }

    public void GridUpdate(Transform _figure)
    {
        for (int y = 0; y < gridHeight; ++y)
            for (int x = 0; x < gridWeight; ++x)
                if (grid[x, y] != null)
                    if (grid[x, y].parent == _figure)
                        grid[x, y] = null;

        foreach (Transform child in _figure)
        {
            Vector2 v = Round(child.position);
            child.position = v;
            grid[(int)v.x, (int)v.y] = child;
        }
    }

    public Transform GetTransformOnPosition(Vector2 _position)
    {
        if (_position.y > gridWeight - 1)
        {
            return null;
        } else
        {
            return grid[(int)_position.x, (int)_position.y];
        }
    }
}
