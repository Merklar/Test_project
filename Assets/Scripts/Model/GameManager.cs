using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManagerFacade
{
    public static Transform CurrentTransform { get; private set; }

    public static void OnFigurePressed(Transform _transform)
    {
        CurrentTransform = _transform;
        Debug.Log("On Figure Pressed" + CurrentTransform.name);
    }

    public static void OnFigureUp()
    {
        Debug.Log("On Figure Up");
    }
}

public class GameManager : MonoBehaviour {

    public  static int gridWeight = 10;
    public  static int gridHeight = 10;
    public  Transform[,] grid = new Transform[gridWeight, gridHeight];

    public  Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public  bool IsInsideGrid(Vector2 pos)
    {
        return (((int)pos.x >= 0) && ((int)pos.x < gridWeight) && ((int)pos.y >= 0));
    }

    public  void Delete(int y)
    {
        for (int x = 0; x < gridWeight; x++) {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public  bool IsFull(int y)
    {
        for (int x = 0; x < gridWeight; x++)
			if (grid[x, y] == null)
            return false;
        return true;
    }

    public  void DeleteRow()
    {
        for (int y = 0; y < gridHeight; y++) {
            if (IsFull(y))
            {
                Delete(y);
                RowDownAll(y + 1);
                y--;
            }
        }
    }

    public  void RowDown(int y)
    {
        for (int x = 0; x < gridWeight; x++) {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public  void RowDownAll(int y)
    {
        for (int i = y; i < gridHeight; i++)
			RowDown(i);
    }

    private bool IsValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Round(child.position);
            if (!IsInsideGrid(v))
                return false;
            if (grid[(int)v.x, (int)v.y] != null &&
            grid[(int)v.x, (int)v.y].parent != transform)
				return false;
        }
        return true;
    }

    private void GridUpdate()
    {
        for (int y = 0; y < gridHeight; y++)
			for (int x = 0; x < gridWeight; x++)
				if (grid[x, y] != null)
            if (grid[x, y].parent == transform)
                grid[x, y] = null;
        foreach (Transform child in transform)
        {
            Vector2 v = Round(child.position);
            grid[(int)v.x, (int)v.y] = child;
        }
    }
}
