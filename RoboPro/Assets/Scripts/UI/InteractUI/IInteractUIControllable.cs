using UnityEngine;

namespace InteractUI
{
    public interface IInteractUIControllable
    {
        /// <summary>
		/// UIの場所を設定する
		/// </summary>
		/// <param name="pos">UIの位置</param>
        public void SetPosition(Vector3 pos);

        /// <summary>
		/// UIを表示する
		/// </summary>
		/// <param name="controllerType">コントローラーの種類</param>
		/// <param name="interactKind">インタラクトの種類</param>
        public void ShowUI(ControllerType controllerType, InteractKinds interactKind);

        /// <summary>
		/// UIを非表示にする
		/// </summary>
        public void HideUI();
    }
}