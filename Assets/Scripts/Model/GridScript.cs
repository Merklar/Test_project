using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public static int gridWidth = 10;
    public static int gridHeight = 10;

    public Transform[,] grid = new Transform[gridWidth, gridHeight];

    public Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public bool IsInsideGrid(Vector2 pos)
    {
        return (((int)pos.x >= 0) && ((int)pos.x < gridWidth) && ((int)pos.y >= 0) && ((int)pos.y < gridHeight));
    }

    public void DeleteY(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public void DeleteX(int x)
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public bool IsFullY(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
            if (grid[x, y] == null)
            {
                return false;
            }
        return true;
    }

    public bool IsFullX(int x)
    {
        for (int y = 0; y < gridHeight; ++y)
            if (grid[x, y] == null)
            {
                return false;
            }
        return true;
    }

    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (IsFullY(y))
            {
                DeleteY(y);
                --y;
            }
        }
    }

    public void DeleteColone()
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (IsFullX(x))
            {
                DeleteX(x);
                --x;
            }
        }
    }

    public bool OnCheckValidFigurePos(Transform _transform)
    {
        foreach (Transform subFigure in _transform)
        {
            Vector2 pos = Round(subFigure.position);
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
            for (int x = 0; x < gridWidth; ++x)
                if (grid[x, y] != null)
                    if (grid[x, y].parent == _figure)
                        grid[x, y] = null;

        foreach (Transform child in _figure)
        {
            Vector2 v = Round(child.position);
            child.position = v;
            grid[(int)v.x, (int)v.y] = child;
        }

        DeleteRow();
        DeleteColone();
    }

    public Transform GetTransformOnPosition(Vector2 _position)
    {
        if (_position.y > gridWidth - 1)
        {
            return null;
        } else
        {
            return grid[(int)_position.x, (int)_position.y];
        }
    }

    public bool OnGameOverCheck(List<Transform> _figures)
    {
       foreach (Transform figure in _figures)
        {
            Vector2 oldPosition = figure.position;
            Vector3 oldScale = figure.localScale;
            figure.localScale = Vector3.one;
            for (int y = 0; y < gridHeight; ++y)
            {
                for (int x = 0; x < gridWidth; ++x)
                {

                    figure.position = new Vector2(x, y);
                    if (OnCheckValidFigurePos(figure) == true)
                    {
                        figure.position = oldPosition;
                        figure.localScale = oldScale;
                        return true;
                    }
                }
            }
            figure.position = oldPosition;
            figure.localScale = oldScale;
        }
        return false;
    }
}
