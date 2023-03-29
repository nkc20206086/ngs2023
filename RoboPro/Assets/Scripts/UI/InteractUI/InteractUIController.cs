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
		/// UI�̏ꏊ��ݒ肷��
		/// </summary>
		/// <param name="pos">UI�̈ʒu</param>
		public void SetPosition(Vector3 pos)
        {
			gameObject.transform.position = pos;
        }

		/// <summary>
		/// UI��\������
		/// </summary>
		/// <param name="controllerType">�R���g���[���[�̎��</param>
		/// <param name="interactKind">�C���^���N�g�̎��</param>
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
		/// UI���\���ɂ���
		/// </summary>
		public void HideUI()
        {
			interactUICanvas.enabled = false;
		}
	}
}
