using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Utility
{
    public class PostEffector : IPostEffector
    {
        private IPostEffectMaterialDatabase database;
        private PostEffectRenderPassFeature feature;

        [Inject]
        public PostEffector(UniversalRendererData data, IPostEffectMaterialDatabase database)
        {
            this.database = database;

            feature = (PostEffectRenderPassFeature)data.rendererFeatures.Find(x => x is PostEffectRenderPassFeature);
        }

        public void SetMaterial(PostEffectMaterialKey key)
        {
            feature.SetShader(database.GetMaterial(key));
        }

        public Tween Fade(FadeType fade, float duration, Ease ease)
        {
            float endValue;
            float startValue;
            if (fade == FadeType.In)
            {
                startValue = 1;
                endValue = 0;
            }
            else
            {
                startValue = 0;
                endValue = 1;
            }
            return DOVirtual.Float(startValue, endValue, duration, value =>
            {
                feature.SetFloat("_Fade", value);
            }).SetEase(ease);
        }

        public void SetColor(string nameID, Color color)
        {
            feature.SetColor(nameID, color);
        }

        public void SetFloat(string nameID, float value)
        {
            feature.SetFloat(nameID, value);
        }

        public void SetTexture(string nameID, Texture texture)
        {
            feature.SetTexture(nameID, texture);
        }

        public void SetTexture(string nameID, RenderTexture rTexture, UnityEngine.Rendering.RenderTextureSubElement element)
        {
            feature.SetTexture(nameID, rTexture, element);
        }
    }
}