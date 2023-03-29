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
		private InteractKinds interactKind;
		public InteractKinds interactKindsProp => interactKind;

		[SerializeField]
		private Sprite keyboardInteractSprite;
		public Sprite keyboardInteractSpriteProp => keyboardInteractSprite;

		[SerializeField]
		private Sprite controllerInteractSprite;
		public Sprite controllerInteractSpriteProp => controllerInteractSprite;

		/// <summary>
		/// �R���g���[���[���Ƃ̉摜��Ԃ�
		/// </summary>
		/// <param name="controllerType">�R���g���[���[�̎��</param>
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
