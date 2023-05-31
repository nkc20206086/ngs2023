using UnityEngine;
using Zenject;
using Robo;

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

        [Inject] private IAudioPlayer audioPlayer;

        public void SelectButton()
        {
            audioPlayer.PlaySE(CueSheetType.Command, "SE_Command_Select");
            programCommandView.ButtonIndexget(mainIndex, subIndex);
        }
    }
}

