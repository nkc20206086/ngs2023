using UnityEngine;

namespace InteractUI
{
	/// <summary>
	/// �L�[�o�C���h�摜��o�^���邽�߂̃X�N���v�g
	/// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/KeyBindingSpriteAsset")]
	public class KeyBindingSpriteAsset : ScriptableObject
	{
		[SerializeField]
		public Sprite keyboardInteractSprite;

		[SerializeField]
		public Sprite controllerInteractSprite;
	}
}
