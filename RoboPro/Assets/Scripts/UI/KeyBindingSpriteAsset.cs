using UnityEngine;

namespace InteractUI
{
	/// <summary>
	/// キーバインド画像を登録するためのスクリプト
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
