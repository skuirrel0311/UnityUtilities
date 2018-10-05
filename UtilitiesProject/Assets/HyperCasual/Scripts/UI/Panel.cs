using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.UI
{
    /// <summary>
    /// 透明の uGUI Graphic.
    /// uGUI の当たり判定を、描画コストなく作る。
    /// reference: http://answers.unity.com/answers/1157876/view.html
    /// </summary>
    public class Panel : Graphic
    {
        public override void SetMaterialDirty ()
        {
            return;
        }
        public override void SetVerticesDirty ()
        {
            return;
        }

        /// Probably not necessary since the chain of calls `Rebuild()`->`UpdateGeometry()`->`DoMeshGeneration()`->`OnPopulateMesh()` won't happen; so here really just as a fail-safe.
        protected override void OnPopulateMesh (VertexHelper vh)
        {
            vh.Clear ();
            return;
        }
    }
}