using UnityEngine;
using CommandUI;

public class ProgramPresenter : MonoBehaviour
{
    [SerializeField] ProgramCommandView programCommandView;
    [SerializeField] InventryCommandView inventryCommandView;
    [SerializeField] InventryModel inventryModel;
    [SerializeField] ProgramPanelModel programPanelModel;

    private void Awake()
    {
        programPanelModel.UIEvent += programCommandView.ProgramCommandTextChange;//MainCommand[]を送るイベント+=inventryCommandView.InventryTextChange;
        inventryModel.UIEvent += inventryCommandView.InventryTextChange;//CommandBase[]を送るイベント+=inventryCommandView.InventryTextChange;
    }
}
