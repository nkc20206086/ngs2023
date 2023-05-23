using UnityEngine;

namespace Utility
{
	[System.Serializable]
	public class PostEffectMaterial
	{
		[SerializeField] private PostEffectMaterialKey key;
		[SerializeField] private Shader shader;

		public PostEffectMaterialKey Key => key;
		public Shader Shader => shader;
	}
}