using DG.Tweening;
using UnityEngine;

namespace Utility
{
	public interface IPostEffector
	{
		void SetMaterial(PostEffectMaterialKey key);
		void SetColor(string nameID, Color color);
		void SetFloat(string nameID, float value);
		void SetTexture(string nameID, Texture texture);
		void SetTexture(string nameID, RenderTexture rTexture, UnityEngine.Rendering.RenderTextureSubElement element);
		Tween Fade(FadeType fade, float duration, Ease ease);
	}
}