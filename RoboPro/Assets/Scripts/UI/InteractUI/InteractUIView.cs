using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MVPModule;

namespace InteractUI
{
	/// <summary>
	/// �C���^���N�g���邽�߂̑���������邽�߂�UI��\������
	/// </summary>
	public class InteractUIView : MonoBehaviour
	{
		[SerializeField]
		private Canvas interactUICanvas;

		[SerializeField]
		private Image interactUIImage;

		[SerializeField]
		private KeyBindingSpriteAsset keyBindingSprite;

		private void Start()
		{
			HideUI();
		}

		/// <summary>
		/// UI��\������
		/// </summary>
		/// <param name="controllerType">�R���g���[���[�̎��</param>
		public void ShowUI(ControllerType controllerType)
        {
			interactUICanvas.enabled = true;
            switch (controllerType)
            {
                case ControllerType.keyboard:
					interactUIImage.sprite = keyBindingSprite.keyboardInteractSprite;
                    break;
                case ControllerType.controller:
					interactUIImage.sprite = keyBindingSprite.controllerInteractSprite;
                    break;
                default:
                    break;
            }
        }

		/// <summary>
		/// UI���\���ɂ���
		/// </summary>
		public void HideUI()
        {
			interactUICanvas.enabled = false;
		}
	}
}
