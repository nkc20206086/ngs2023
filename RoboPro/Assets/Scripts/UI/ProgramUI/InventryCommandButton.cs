using UnityEngine;

namespace CommandUI
{
    public class InventryCommandButton : MonoBehaviour
    {
        [SerializeField]
        private int mainIndex;
        [SerializeField]
        private int subIndex;

        [SerializeField]
        private InventryCommandView inventryCommandView;

        public void SelectButton()
        {
            inventryCommandView.ButtonIndexget(mainIndex, subIndex);
        }
    }
}
