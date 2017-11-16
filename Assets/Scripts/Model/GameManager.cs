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

    public SpawnBoxScript SpawnBoxScript { get; private set; }
    public GridScript GridScript { get; private set; }

    public List<Transform> EnableFigureCollection = new List<Transform>();
    public List<Transform> AllFigureCollectionOnField = new List<Transform>();

    public const float SCALE_FACTOR = 0.7f;
    public const string CONTAINER_TAG = "FigureContainer";
    public const string CLICK_SOUND = "ClickSound";
    public const string DROP_SOUND = "DropSound";

    private Vector3 containerScale = new Vector3(SCALE_FACTOR, SCALE_FACTOR, 0);


    private void Awake()
    {
        GameManagerFacade.SetGameManager(this);
        GridTransform = GameObject.Find("Grid").transform;
        SpawnBoxScript = GetComponent<SpawnBoxScript>();
        GridScript = GameObject.Find("Grid").GetComponent<GridScript>();
    }
   
    public void OnCMDPressed(Transform _transform)
    {
        if ((_transform.gameObject.tag == CONTAINER_TAG) & (_transform.childCount > 0))
        {
            CurrentTransformContainer = _transform;
            CurrentTransform = _transform.GetChild(0).transform;
            OnExitFromContainer();
        }
    }

    public void OnCMDUp()
    {
        if (CurrentTransform != null & CurrentTransformContainer != null) {
            if (GridScript.OnCheckValidFigurePos(CurrentTransform) == true)
            {
                CurrentTransform.parent = null;
                AllFigureCollectionOnField.Add(CurrentTransform);
                GridScript.GridUpdate(CurrentTransform);
                SpawnBoxScript.CheakAndSpawn();
                EnableFigureCollection.Remove(CurrentTransform);
                SignalContext.OnPlaySoundSignal.Dispatch(CLICK_SOUND);
                //ClearEmptyFigure();
                if (GridScript.OnGameOverCheck(EnableFigureCollection) == false)
                 {
                    OnGameOver();
                 }
            } else
            {
                OnEnterInContainer(CurrentTransformContainer);
            }
            CurrentTransform = null;
            CurrentTransformContainer = null;
        }
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

    private void OnGameOver()
    {
        Debug.Log("GAME_OVER");
        SignalContext.GameOverSignal.Dispatch();
    }

    private void ClearEmptyFigure()
    {
        foreach (Transform figure in AllFigureCollectionOnField)
        {
            if (figure == null)
            {
                AllFigureCollectionOnField.Remove(figure);
                return;
            } else if (figure.childCount == 0)
            {
                Destroy(figure.gameObject);
                return;
            }
        }
    }
}
