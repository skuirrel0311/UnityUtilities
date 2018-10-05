using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HyperCasual.UI
{
    public static class UIExtensions
    {
        public static Rect GetWorldRect (this RectTransform transform)
        {
            var corners = new Vector3[4];
            transform.GetWorldCorners (corners);
            return new Rect (
                corners [0].x,
                corners [0].y,
                corners [2].x - corners [0].x,
                corners [2].y - corners [0].y
            );
        }

        public static float GetWidth (this Graphic graphic)
        {
            return graphic.rectTransform.GetWidth ();
        }
        public static float GetWidth (this RectTransform rect)
        {
            return rect.rect.width;
        }
        public static float GetHeight (this Graphic graphic)
        {
            return graphic.rectTransform.GetHeight ();
        }
        public static float GetHeight (this RectTransform rect)
        {
            return rect.rect.height;
        }
        public static Vector2 GetSize (this Graphic graphic)
        {
            return graphic.rectTransform.GetSize ();
        }
        public static Vector2 GetSize (this RectTransform rect)
        {
            return rect.rect.size;
        }

        public static float GetDeltaWidth (this Graphic graphic)
        {
            return graphic.rectTransform.GetDeltaWidth ();
        }
        public static float GetDeltaWidth (this RectTransform rect)
        {
            return rect.sizeDelta.x;
        }
        public static float GetDeltaHeight (this Graphic graphic)
        {
            return graphic.rectTransform.GetDeltaHeight ();
        }
        public static float GetDeltaHeight (this RectTransform rect)
        {
            return rect.sizeDelta.y;
        }
        public static Vector2 GetDeltaSize (this Graphic graphic)
        {
            return graphic.rectTransform.GetDeltaSize ();
        }
        public static Vector2 GetDeltaSize (this RectTransform rect)
        {
            return rect.sizeDelta;
        }

        public static RectTransform SetDeltaWidth (this Graphic graphic, float width)
        {
            return graphic.rectTransform.SetDeltaWidth (width);
        }
        public static RectTransform SetDeltaWidth (this RectTransform rect, float width)
        {
            rect.sizeDelta = new Vector2 (width, rect.sizeDelta.y);
            return rect;
        }
        public static RectTransform SetDeltaHeight (this Graphic graphic, float height)
        {
            return graphic.rectTransform.SetDeltaHeight (height);
        }
        public static RectTransform SetDeltaHeight (this RectTransform rect, float height)
        {
            rect.sizeDelta = new Vector2 (rect.sizeDelta.x, height);
            return rect;
        }
        public static RectTransform SetDeltaWidth (this Graphic graphic, float width, float height)
        {
            return graphic.rectTransform.SetDeltaSize (width, height);
        }
        public static RectTransform SetDeltaWidth (this Graphic graphic, Vector2 size)
        {
            return graphic.rectTransform.SetDeltaSize (size);
        }
        public static RectTransform SetDeltaSize (this RectTransform rect, float width, float height)
        {
            rect.sizeDelta = new Vector2 (width, height);
            return rect;
        }
        public static RectTransform SetDeltaSize (this RectTransform rect, Vector2 size)
        {
            rect.sizeDelta = size;
            return rect;
        }


        const int FillOriginRadial360Top = 2;

        public static void CutOffRadial360Top (this Image image, float ratio)
        {
            ratio = Mathf.Clamp01 (ratio);
        
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Radial360;
            image.fillOrigin = FillOriginRadial360Top;
            image.fillAmount = ratio;
            image.fillClockwise = true;
            image.rectTransform.localEulerAngles = Vector3.zero;
        }
        public static void CutOffRadial360Centerized (this Image image, float ratio)
        {
            ratio = Mathf.Clamp01 (ratio);
        
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Radial360;
            image.fillOrigin = FillOriginRadial360Top;
            image.fillAmount = ratio;
            image.fillClockwise = true;
            image.rectTransform.localEulerAngles = new Vector3 (0, 0, ratio * 180);
        }
        public enum HorizontalEdge
        {
            Custom,
            Left,
            Center,
            Right,
            Stretch
        }

        public enum VerticalEdge
        {
            Custom,
            Top,
            Middle,
            Bottom,
            Stretch
        }

        public static HorizontalEdge GetHorizontalAnchor (this RectTransform transform)
        {
            var min = transform.anchorMin.x;
            var max = transform.anchorMax.x;

            if (min == 0.0f && max == 0.0f) {
                return HorizontalEdge.Left;
            }
            if (min == 0.5f && max == 0.5f) {
                return HorizontalEdge.Center;
            }
            if (min == 1.0f && max == 1.0f) {
                return HorizontalEdge.Right;
            }
            if (min == 0.0f && max == 1.0f) {
                return HorizontalEdge.Stretch;
            }
            return HorizontalEdge.Custom;
        }
        public static VerticalEdge GetVerticalAnchor (this RectTransform transform)
        {
            var min = transform.anchorMin.y;
            var max = transform.anchorMax.y;

            if (min == 0.0f && max == 0.0f) {
                return VerticalEdge.Top;
            }
            if (min == 0.5f && max == 0.5f) {
                return VerticalEdge.Middle;
            }
            if (min == 1.0f && max == 1.0f) {
                return VerticalEdge.Bottom;
            }
            if (min == 0.0f && max == 1.0f) {
                return VerticalEdge.Stretch;
            }
            return VerticalEdge.Custom;
        }

        public static HorizontalEdge GetHorizontalPivot (this RectTransform transform)
        {
            if (transform.pivot.x == 0.0f) {
                return HorizontalEdge.Left;
            }
            if (transform.pivot.x == 0.5f) {
                return HorizontalEdge.Center;
            }
            if (transform.pivot.x == 1.0f) {
                return HorizontalEdge.Right;
            }
            return HorizontalEdge.Custom;
        }
        public static VerticalEdge GetVerticalPivot (this RectTransform transform)
        {
            if (transform.pivot.y == 0.0f) {
                return VerticalEdge.Top;
            }
            if (transform.pivot.y == 0.5f) {
                return VerticalEdge.Middle;
            }
            if (transform.pivot.y == 1.0f) {
                return VerticalEdge.Bottom;
            }
            return VerticalEdge.Custom;
        }

    }
}