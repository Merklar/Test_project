using strange.extensions.signal.impl;
using UnityEngine;

public class AppStartSignal: Signal { }

public class OnFigurePressedSignal: Signal<Transform> { }
public class OnFigureUpSignal : Signal { }
public class OnStartDragSignal : Signal { }
