using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public enum MyAnimationCurveType { Bounce, Notification }

public class MyAnimationCurves : MySettingsObject<MyAnimationCurves>, IInitializable
{
    [System.Serializable]
    class Element
    {
        public MyAnimationCurveType Type;
        public AnimationCurve Curve;
    }

    [SerializeField, OneLine.OneLine()]
    Element[] elements = null;

    Dictionary<MyAnimationCurveType, AnimationCurve> curveDic = new Dictionary<MyAnimationCurveType, AnimationCurve>();

    public void Init()
    {
        foreach(var e in elements) curveDic.Add(e.Type, e.Curve);
    }

    public AnimationCurve GetAnimationCurve(MyAnimationCurveType type)
    {
        return curveDic.SafeGetValue(type);
    }
}
