using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    public Transform CurrentTransform { get; private set; }

    private Vector3 containerScale = new Vector3(0.7f, 0.7f, 0);

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnExitFromContainer(Transform _transform)
    {
        CurrentTransform.localScale = Vector3.one;
        CurrentTransform.parent = null;
    }

    private void OnEnterInContainer(Transform _transform)
    {
        CurrentTransform.localScale = containerScale;
        CurrentTransform.parent = _transform;
    }
}
