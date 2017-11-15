
using UnityEngine;

public static class ControllerFacade
{
    public static ControllerManager ControllerManager { get; private set; }

    public static void SetControllerManager(ControllerManager сontrollerManager)
    {
        ControllerManager = сontrollerManager;
    }

    public static void GameOver()
    {
        ControllerManager.GamveOver();
    }
}

public class ControllerManager : MonoBehaviour {

    public Transform CurrentTransform { get; private set; }
    public Transform CurrentTransformContainer { get; private set; }
    public static bool OnFigureUp { get; private set; }

    public const float SCALE_FACTOR = 0.7f;
    public const string CONTAINER_TAG = "FigureContainer";

    private Vector3 containerScale = new Vector3(SCALE_FACTOR, SCALE_FACTOR, 0);

    private bool onGameOver = false;

    private void Awake()
    {
        ControllerFacade.SetControllerManager(this);
    }

    void Start()
    {
        OnFigureUp = false;
    }

    void Update()
    {
        if (onGameOver == false) {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D _rayHit = Physics2D.Raycast(ray.origin, ray.direction);
                if (_rayHit)
                {
                    Transform _transform = _rayHit.transform;
                    SignalContext.OnFigurePressedSignal.Dispatch(_transform);
                    OnFigureUp = true;
                }

            } else if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit2D _rayHit = Physics2D.Raycast(ray.origin, ray.direction);
                if (_rayHit)
                {
                    Transform _transform = _rayHit.transform;
                    SignalContext.OnFigurePressedSignal.Dispatch(_transform);
                    OnFigureUp = true;
                }
            }

            if ((Input.GetMouseButtonUp(0)) && (OnFigureUp == true))
            {
                SignalContext.OnFigureUpSignal.Dispatch();
                OnFigureUp = false;

            } else if ((Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                SignalContext.OnFigureUpSignal.Dispatch();
                OnFigureUp = false;
            }

            if (OnFigureUp == true)
            {
                SignalContext.OnStartDragSignal.Dispatch();
            }
        }
    }

    public void GamveOver()
    {
        onGameOver = true;
    }
}
