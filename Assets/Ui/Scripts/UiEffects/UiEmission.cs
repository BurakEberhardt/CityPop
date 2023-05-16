using UnityEngine;
using UnityEngine.UI;

namespace Zen.Ui.UiEffects
{
    public class UiEmission : BaseMeshEffect
    {
        [SerializeField] float _emissionStrength;
        public float EmissionStrength
        {
            get => _emissionStrength;
            set
            {
                _emissionStrength = value;
                graphic.SetVerticesDirty();
            }
        }
        
        [SerializeField, ColorUsage(false, false)] Color _emissionColor;
        public Color EmissionColor
        {
            get => _emissionColor;
            set
            {
                _emissionColor = value;
                graphic.SetVerticesDirty();
            }
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            if(!IsActive()) 
                return;

            var vertCount = vh.currentVertCount;
            var vert = new UIVertex();
            
            for (var i = 0; i < vertCount; ++i)
            {
                vh.PopulateUIVertex(ref vert,i);
                vert.uv1 = new Vector4(_emissionColor.r, _emissionColor.g, _emissionColor.b, _emissionStrength);
                vh.SetUIVertex(vert,i);
            }
        }
    }
}