using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class Tweener : MonoBehaviour
{
    private List<Tween> AllTween = new List<Tween>();

    public void MoveTo(Transform transform, float time, float x = 0, float y = 0, bool alphaOnEnd = false, Delegate _delegate = null)
    {
        AllTween.Add(new Tween(transform, time, x, y, alphaOnEnd, _delegate));
    }

    
}