using UnityEngine;
using Zenject;
using Robo;
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

        [Inject] private IAudioPlayer audioPlayer;
        public void SelectButton()
        {
            audioPlayer.PlaySE(CueSheetType.Command, "SE_Command_Select");
            inventryCommandView.ButtonIndexget(mainIndex, subIndex);
        }
    }
}
