using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI {
	public class UICutoutMask : Image {
		public override Material materialForRendering {
			get {
				Material forRendering = new Material(base.materialForRendering);
				forRendering.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
				return forRendering;
			}
		}
	}
}
