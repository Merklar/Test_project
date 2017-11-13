
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    public Transform CurrentTransform { get; private set; }
    public Transform CurrentTransformContainer { get; private set; }
    public static bool OnFigureUp { get; private set; }

    public const float SCALE_FACTOR = 0.7f;
    public const string CONTAINER_TAG = "FigureContainer";

    private Vector3 containerScale = new Vector3(SCALE_FACTOR, SCALE_FACTOR, 0);

    void Start()
    {
        OnFigureUp = false;
    }

    void Update()
    {
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

        }

        if (OnFigureUp == true)
        {
            SignalContext.OnStartDragSignal.Dispatch();
        }
    }

}
