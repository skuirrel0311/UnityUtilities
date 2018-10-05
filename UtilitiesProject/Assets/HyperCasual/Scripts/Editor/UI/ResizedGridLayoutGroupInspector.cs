using UnityEngine;
using UnityEditor;

namespace HyperCasual.UI
{
    [CustomEditor (typeof(ResizedGridLayoutGroup))]
    public sealed class ResizedGridLayoutGroupInspector : Editor
    {
        public override void OnInspectorGUI ()
        {
            base.DrawDefaultInspector ();
        }
    }
}