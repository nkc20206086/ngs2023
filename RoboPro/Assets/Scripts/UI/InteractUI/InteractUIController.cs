using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InteractUI
{
    /// <summary>
    /// インタラクトするための操作を教えるためのUIを表示する
    /// </summary>
    public class InteractUIController : MonoBehaviour, IInteractUIControllable
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
			currentInteractKind = InteractKinds.None;
			currentKeyBindingSpriteAsset = null;

			((IInteractUIControllable)this).HideUI();
		}

		void IInteractUIControllable.SetPosition(Vector3 pos)
        {
			gameObject.transform.position = pos;
        }
		
		void IInteractUIControllable.ShowUI(ControllerType controllerType, InteractKinds interactKind)
        {
			bool isNotSetinteractKind = interactKind == InteractKinds.None;
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

		void IInteractUIControllable.HideUI()
        {
			interactUICanvas.enabled = false;
		}
	}
}
