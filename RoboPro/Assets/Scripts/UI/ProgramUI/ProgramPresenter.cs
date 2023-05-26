using UnityEngine;
using CommandUI;
using Command;

public class ProgramPresenter : MonoBehaviour
{
    [SerializeField] ProgramCommandView programCommandView;
    [SerializeField] InventryCommandView inventryCommandView;
    [SerializeField] CommandDirector commandDirector;
    //[SerializeField] InventryModel inventryModel;
    //[SerializeField] ProgramPanelModel programPanelModel;

    private void Awake()
    {
        commandDirector.UIEvent_MainCommands += programCommandView.ProgramCommandTextChange;//MainCommand[]を送るイベント+=inventryCommandView.InventryTextChange;
        commandDirector.UIEvent_StorageCommands += inventryCommandView.InventryTextChange;//CommandBase[]を送るイベント+=inventryCommandView.InventryTextChange;
    }
}
