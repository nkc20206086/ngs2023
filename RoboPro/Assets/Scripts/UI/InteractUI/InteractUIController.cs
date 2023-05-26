using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
		private TextMeshProUGUI interactUIText;

		[SerializeField]
		private Image holdImage;

		private string beforeDisplayText;

		private Sprite beforeSprite;

		private void Start()
		{
			((IInteractUIControllable)this).SetFillAmount(0f);
			((IInteractUIControllable)this).HideUI();
		}

		void IInteractUIControllable.SetPosition(Vector3 pos)
        {
			gameObject.transform.position = pos;
        }
		
		void IInteractUIControllable.ShowUI(ControllerType controllerType, DisplayInteractCanvasAsset displayAsset)
        {
			string displayText = displayAsset.displayTextProp;
			bool changedDisplayText = displayText != beforeDisplayText;
			if(changedDisplayText)
            {
				interactUIText.text = displayText;
				beforeDisplayText = displayText;
			}

			Sprite keyBindingSprite = displayAsset.GetKeyBindingSprite(controllerType);
			bool changedSprite = keyBindingSprite != beforeSprite;
			if(changedSprite)
            {
				interactUIImage.sprite = displayAsset.GetKeyBindingSprite(controllerType);
				beforeSprite = keyBindingSprite;
			}

			interactUICanvas.enabled = true;
		}

		void IInteractUIControllable.HideUI()
        {
			interactUICanvas.enabled = false;
		}

		void IInteractUIControllable.SetFillAmount(float value)
        {
			value = Mathf.Clamp01(value);
			holdImage.fillAmount = value;

		}
	}
}
