using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MVPModule;

namespace InteractUI
{
	/// <summary>
	/// インタラクトするための操作を教えるためのUIを表示する
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
		/// UIを表示する
		/// </summary>
		/// <param name="controllerType">コントローラーの種類</param>
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
		/// UIを非表示にする
		/// </summary>
		public void HideUI()
        {
			interactUICanvas.enabled = false;
		}
	}
}
