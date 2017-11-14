using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class Tween : Tweener
{
    private float timeElapsed;
    private float moveDeltaX;
    private float moveDeltaY;
    private Transform currentTransform;
    private Delegate curretDelegate;

    public Tween(Transform transform, float time, float x = 0, float y = 0, bool alphaOnEnd = false, Delegate _delegate = null)
    {
        timeElapsed = time;
        moveDeltaX = x / time;
        moveDeltaY = y / time;
        currentTransform = transform;
        curretDelegate = _delegate;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        for (float i = timeElapsed; i > 0; i-=0.1f) {
            yield return new WaitForSeconds(0.1f);
            currentTransform.position = new Vector3(currentTransform.position.x + moveDeltaX, currentTransform.position.y + moveDeltaY, currentTransform.position.z);
        }
        if (curretDelegate != null)
        {
            curretDelegate.DynamicInvoke();
        }

    }
}