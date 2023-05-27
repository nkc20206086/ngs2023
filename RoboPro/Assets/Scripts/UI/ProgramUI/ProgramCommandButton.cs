using UnityEngine;

namespace CommandUI
{
    public class ProgramCommandButton : MonoBehaviour
    {
        [SerializeField]
        private int mainIndex;
        [SerializeField]
        private int subIndex;

        [SerializeField]
        private ProgramCommandView programCommandView;

        public void SelectButton()
        {
            programCommandView.ButtonIndexget(mainIndex, subIndex);
        }
    }
}

