using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.UI
{
    [ExecuteInEditMode]
    public class Gauge : MonoBehaviour
    {
        [SerializeField, Range (0, 1)] float ratio = 1.0f;
        [SerializeField] RectTransform bar;
        [SerializeField] Text label;

        RectTransform _parent;
        RectTransform parent {
            get {
                if (_parent == null && bar != null && bar.parent != null) {
                    _parent = bar.parent as RectTransform;
                }
                return _parent;
            }
        }
        
        #if UNITY_EDITOR
        void Update ()
        {
            if (parent) {
                SetRatio (this.ratio);
            }
        }
        #endif

        public void UpdateParent ()
        {
            _parent = null;
        }

        public float GetRatio ()
        {
            return this.ratio;
        }
        public void SetRatio (float ratio)
        {
            this.ratio = Mathf.Clamp01 (ratio);

            if (bar == null) {
                return;
            }

            var rect = this.parent.GetSize ();

            var horizontal = bar.GetHorizontalPivot ();
            if (horizontal == UIExtensions.HorizontalEdge.Left ||
                horizontal == UIExtensions.HorizontalEdge.Right) {
                bar.SetDeltaWidth (rect.x * this.ratio);
                return;
            }

            var vertical = bar.GetVerticalPivot ();
            if (vertical == UIExtensions.VerticalEdge.Top ||
                vertical == UIExtensions.VerticalEdge.Bottom) {
                bar.SetDeltaHeight (rect.y * this.ratio);
                return;
            }
        }

        public void SetLabel (string text)
        {
            if (this.label == null) {
                return;
            }

            this.label.text = text;
        }
    }
}