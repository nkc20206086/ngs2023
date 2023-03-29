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
		private InteractKinds interactKind;
		public InteractKinds interactKindsProp => interactKind;

		[SerializeField]
		private Sprite keyboardInteractSprite;
		public Sprite keyboardInteractSpriteProp => keyboardInteractSprite;

		[SerializeField]
		private Sprite controllerInteractSprite;
		public Sprite controllerInteractSpriteProp => controllerInteractSprite;

		/// <summary>
		/// コントローラーごとの画像を返す
		/// </summary>
		/// <param name="controllerType">コントローラーの種類</param>
		public Sprite GetKeyBindingSprite(ControllerType controllerType)
        {
            switch (controllerType)
            {
                case ControllerType.keyboard:
					return keyboardInteractSpriteProp;
                case ControllerType.controller:
					return controllerInteractSpriteProp;
                default:
                    return null;
            }
        }
	}
}
