using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.UI
{
    public class ResizedGridLayoutGroup : GridLayoutGroup
    {
        RectTransform _transform;
        new RectTransform transform {
            get {
                if (_transform == null) {
                    _transform = base.transform as RectTransform;
                }
                return _transform;
            }
        }

        public override void CalculateLayoutInputHorizontal ()
        {
            Resize ();
            base.CalculateLayoutInputHorizontal ();
        }
        public override void CalculateLayoutInputVertical ()
        {
            Resize ();
            base.CalculateLayoutInputVertical ();
        }
        public override void SetLayoutHorizontal ()
        {
            Resize ();
            base.SetLayoutHorizontal ();
        }
        public override void SetLayoutVertical ()
        {
            Resize ();
            base.SetLayoutVertical ();
        }

        #if UNITY_EDITOR
        protected override void OnValidate ()
        {
            Resize ();
            base.OnValidate ();
        }
        #endif

        public void Resize ()
        {
            switch (this.constraint) {
            case Constraint.FixedColumnCount:
                {
                    float width = this.transform.GetWidth () - (padding.left + padding.right);
                    if (this.constraintCount > 0) {
                        width = (width - (this.constraintCount - 1) * spacing.x) / this.constraintCount;
                    }
                    float height = 0;
                    if (this.cellSize.x != 0) {
                        height = this.cellSize.y / this.cellSize.x * width;
                    }
                    this.cellSize = new Vector2 (width, height);
                    break;
                }
            case Constraint.FixedRowCount:
                {
                    float height = this.transform.GetHeight () - (padding.top + padding.bottom);
                    if (this.constraintCount > 0) {
                        height = (height - (this.constraintCount - 1) * spacing.y) / this.constraintCount;
                    }
                    float width = 0;
                    if (this.cellSize.y != 0) {
                        width = this.cellSize.x / this.cellSize.y * height;
                    }
                    this.cellSize = new Vector2 (width, height);
                    break;
                }
            }
        }
    }
}