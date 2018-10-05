using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual
{
    public static class CameraExtensions
    {
        /// <summary>
        /// ワールド座標におけるスクリーンのサイズを取得します。
        /// </summary>
        /// <returns>The screen world size.</returns>
        /// <param name="camera">Camera.</param>
        /// <param name="distance">Perspective camera の場合の、想定するスクリーンのカメラからの距離。Orthograpic camera の場合は使用しません。</param>
        public static Vector3 GetScreenWorldSize(this Camera camera, float distance = 0)
        {
            if (camera.orthographic) {
                var height = camera.orthographicSize * 2;
                var width = height * Screen.width / Screen.height;
                return new Vector3(width, height);
            } else {
                return camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, distance)) * 2;
            }
        }


        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、指定した幅に収まる倍率を取得します。
        /// </summary>
        /// <returns>The scale to adjust width.</returns>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        /// <param name="realWidth">Real width.</param>
        /// <param name="realHeight">Real height.</param>
        public static float GetScaleToAdjustWidth(float idealWidth, float idealHeight, float realWidth, float realHeight)
        {
            var ideal = idealWidth / idealHeight;
            var current = realWidth / realHeight;
            var scale = current / ideal;
            return scale;
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、幅が Screen 以内に収まる倍率まで、カメラをズームイン・ズームアウトします。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        public static void ZoomToAdjustWidth(this Camera camera, float idealWidth, float idealHeight)
        {
            camera.ZoomToAdjustWidth(idealWidth, idealHeight, Screen.width, Screen.height);
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、指定した幅に収まる倍率まで、カメラをズームイン・ズームアウトします。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        /// <param name="realWidth">Real width.</param>
        /// <param name="realHeight">Real height.</param>
        public static void ZoomToAdjustWidth(this Camera camera, float idealWidth, float idealHeight, float realWidth, float realHeight)
        {
            var scale = GetScaleToAdjustWidth(idealWidth, idealHeight, realWidth, realHeight);
            camera.Zoom(scale);
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、幅が Screen 以内に収まる倍率まで、カメラをズームアウトします（ズームインはしません）。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <returns><c>true</c> ならば、ズームアウト。</returns>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        public static bool ZoomOutToAdjustWidth(this Camera camera, float idealWidth, float idealHeight)
        {
            return camera.ZoomOutToAdjustWidth(idealWidth, idealHeight, Screen.width, Screen.height);
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、指定した幅に収まる倍率まで、カメラをズームアウトします（ズームインはしません）。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <returns><c>true</c> ならば、ズームアウト。</returns>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        /// <param name="realWidth">Real width.</param>
        /// <param name="realHeight">Real height.</param>
        public static bool ZoomOutToAdjustWidth(this Camera camera, float idealWidth, float idealHeight, float realWidth, float realHeight)
        {
            var scale = GetScaleToAdjustWidth(idealWidth, idealHeight, realWidth, realHeight);
            if (scale < 1) {
                camera.Zoom(scale);
                return true;
            } else {
                return false;
            }
        }


        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、指定した高さに収まる倍率を取得します。
        /// </summary>
        /// <returns>The scale to adjust width.</returns>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        /// <param name="realWidth">Real width.</param>
        /// <param name="realHeight">Real height.</param>
        public static float GetScaleToAdjustHeight(float idealWidth, float idealHeight, float realWidth, float realHeight)
        {
            var scale = GetScaleToAdjustHeight(idealWidth, idealHeight, realWidth, realHeight);
            return 1 / scale;
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、高さが Screen 以内に収まる倍率まで、カメラをズームイン・ズームアウトします。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        public static void ZoomToAdjustHeight(this Camera camera, float idealWidth, float idealHeight)
        {
            camera.ZoomToAdjustHeight(idealWidth, idealHeight, Screen.width, Screen.height);
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、指定した高さに収まる倍率まで、カメラをズームイン・ズームアウトします。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        public static void ZoomToAdjustHeight(this Camera camera, float idealWidth, float idealHeight, float realWidth, float realHeight)
        {
            var scale = GetScaleToAdjustHeight(idealWidth, idealHeight, realWidth, realHeight);
            camera.Zoom(scale);
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、高さが Screen 以内に収まる倍率まで、カメラをズームアウトします（ズームインはしません）。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <returns><c>true</c> ならば、ズームアウト。</returns>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        public static bool ZoomOutToAdjustHeight(this Camera camera, float idealWidth, float idealHeight)
        {
            return camera.ZoomOutToAdjustHeight(idealWidth, idealHeight, Screen.width, Screen.height);
        }
        /// <summary>
        /// 指定した幅・高さを理想とするアスペクト比で、指定した高さに収まる倍率まで、カメラをズームアウトします（ズームインはしません）。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <returns><c>true</c> ならば、ズームアウト。</returns>
        /// <param name="camera">Camera.</param>
        /// <param name="idealWidth">Ideal width.</param>
        /// <param name="idealHeight">Ideal height.</param>
        /// <param name="realWidth">Real width.</param>
        /// <param name="realHeight">Real height.</param>
        public static bool ZoomOutToAdjustHeight(this Camera camera, float idealWidth, float idealHeight, float realWidth, float realHeight)
        {
            var scale = GetScaleToAdjustHeight(idealWidth, idealHeight, realWidth, realHeight);
            if (scale < 1) {
                camera.Zoom(scale);
                return true;
            } else {
                return false;
            }
        }


        /// <summary>
        /// 指定した倍率で、カメラをズームします（0~1の場合はズームアウト、1~の場合はズームインします）。
        /// Orthographic camera では OrthographicSize を、 Perspective camera では FieldOfView を調整します。
        /// </summary>
        /// <param name="camera">Camera.</param>
        /// <param name="scale">Scale.</param>
        public static void Zoom(this Camera camera, float scale)
        {
            if (scale < 0) {
                return;
            }
            if (camera.orthographic) {
                camera.orthographicSize = camera.orthographicSize / scale;
            } else {
                camera.fieldOfView = Mathf.Rad2Deg * Mathf.Atan(1 / scale * Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView / 2)) * 2;
            }
        }
    }
}