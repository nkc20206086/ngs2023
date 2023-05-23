using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "PostEffectMaterialDatabase", menuName = "Utility/PostEffectMaterialDatabase")]
    public class PostEffectMaterialDatabase : ScriptableObject, IPostEffectMaterialDatabase
    {
        [SerializeField] private PostEffectMaterial[] materials = null;

		public Shader GetMaterial(PostEffectMaterialKey key)
		{
			foreach(PostEffectMaterial material in materials)
			{
				if(material.Key == key)
				{
					return material.Shader;
				}
			}
			Debug.LogError($"{key}��Material�͌�����܂���ł���");
			return null;
		}
	}
}