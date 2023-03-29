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
	public class InteractUIController : MonoBehaviour
	{
		[SerializeField]
		private Canvas interactUICanvas;

		[SerializeField]
		private Image interactUIImage;

		[SerializeField]
		private List<KeyBindingSpriteAsset> keyBindingSpriteAssets = new List<KeyBindingSpriteAsset>();

		private InteractKinds currentInteractKind;
		private KeyBindingSpriteAsset currentKeyBindingSpriteAsset;

		private void Start()
		{
			currentInteractKind = InteractKinds.none;
			currentKeyBindingSpriteAsset = null;

			HideUI();
		}

		/// <summary>
		/// UIの場所を設定する
		/// </summary>
		/// <param name="pos">UIの位置</param>
		public void SetPosition(Vector3 pos)
        {
			gameObject.transform.position = pos;
        }

		/// <summary>
		/// UIを表示する
		/// </summary>
		/// <param name="controllerType">コントローラーの種類</param>
		/// <param name="interactKind">インタラクトの種類</param>
		public void ShowUI(ControllerType controllerType, InteractKinds interactKind)
        {
			bool isNotSetinteractKind = interactKind == InteractKinds.none;
			if (isNotSetinteractKind)
            {
				return;
            }

			bool changedInteractKind = interactKind != currentInteractKind;
			if (changedInteractKind)
            {
				for (int i = 0; i < keyBindingSpriteAssets.Count; i++)
				{
					if (keyBindingSpriteAssets[i].interactKindsProp == interactKind)
					{
						currentKeyBindingSpriteAsset = keyBindingSpriteAssets[i];
						currentInteractKind = interactKind;
						break;
					}
					currentKeyBindingSpriteAsset = null;
				}
				
				interactUIImage.sprite = currentKeyBindingSpriteAsset?.GetKeyBindingSprite(controllerType);
			}

			interactUICanvas.enabled = true;
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
