using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManagerFacade
{
    public static Transform CurrentTransform { get; private set; }
    public static GameManager GameManager { get; private set; }

     public static void SetGameManager(GameManager gameManager)
    {
        GameManager = gameManager;
    }

    public static void OnFigurePressed(Transform _transform)
    {
        CurrentTransform = _transform;
        GameManager.OnCMDPressed(_transform);
    }

    public static void OnFigureUp()
    {
        GameManager.OnCMDUp();
    }

    public static void OnStartDrag()
    {
        GameManager.StartDrag();
    }
}

public class GameManager : MonoBehaviour {

    public Transform CurrentTransform { get; private set; }
    public Transform CurrentTransformContainer { get; private set; }
    public Transform GridTransform { get; private set; }
    public GridScript GridScript { get; private set; }

    public const float SCALE_FACTOR = 0.7f;
    public const string CONTAINER_TAG = "FigureContainer";

    private Vector3 containerScale = new Vector3(SCALE_FACTOR, SCALE_FACTOR, 0);


    private void Awake()
    {
        GameManagerFacade.SetGameManager(this);
        GridTransform = GameObject.Find("Grid").transform;
        GridScript = GameObject.Find("Grid").GetComponent<GridScript>();
    }
    /*
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (IsValidPosition())
                // It's valid. Update grid.
                GridUpdate();
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (IsValidPosition())
                // It's valid. Update grid.
                GridUpdate();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // See if valid
            if (IsValidPosition())
                // It's valid. Update grid.
                GridUpdate();
            else
                // It's not valid. revert.
                transform.Rotate(0, 0, 90);
        }

        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (IsValidPosition())
            {
                // It's valid. Update grid.
                GridUpdate();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                DeleteRow();

                // Spawn next Group
                //FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

           // lastFall = Time.time;
        }
    } */
    public void OnCMDPressed(Transform _transform)
    {
        if ((_transform.gameObject.tag == CONTAINER_TAG) & (_transform.childCount > 0))
        {
            Debug.Log(_transform.GetChild(0));
            CurrentTransformContainer = _transform;
            CurrentTransform = _transform.GetChild(0).transform;
            OnExitFromContainer();
        }
    }

    public void OnCMDUp()
    {
        if (GridScript.OnCheckValidFigurePos(CurrentTransform) == true)
        {
            /* CurrentTransform.parent = GridTransform;
             foreach (Transform subFigure in CurrentTransform)
             {
                 Vector2 pos = GridScript.Round(subFigure.position);
                 subFigure.position = pos;
             } */
            GridScript.GridUpdate(CurrentTransform);
        } else
        {
            OnEnterInContainer(CurrentTransformContainer);
        }
        CurrentTransformContainer = null;
    }

    protected void OnExitFromContainer()
    {
        CurrentTransform.localScale = Vector3.one;
        CurrentTransform.parent = null;
    }

    protected void OnEnterInContainer(Transform _transform)
    {
        CurrentTransform.localScale = containerScale;
        CurrentTransform.parent = _transform;
        CurrentTransform.localPosition = new Vector3(0, 0, 0);
        CurrentTransform = null;
    }

    public void StartDrag()
    {
        if (CurrentTransform != null)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            CurrentTransform.position = objPosition;
        }
    }

}
