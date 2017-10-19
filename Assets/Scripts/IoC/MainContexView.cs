using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using strange.extensions.injector.api;

public class MainContexView : ContextView
{
     void Start()
    {
        var context = new MainContext(this);
        context.Start();
    }
}