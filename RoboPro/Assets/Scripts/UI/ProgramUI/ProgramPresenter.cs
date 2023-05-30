using UnityEngine;
using CommandUI;
using Command;

public class ProgramPresenter : MonoBehaviour
{
    [SerializeField] ProgramPanelView programPanelView;
    [SerializeField] ProgramCommandView programCommandView;
    [SerializeField] InventryCommandView inventryCommandView;

    //テスト用
    [SerializeField] InventryModel inventryModel;
    [SerializeField] ProgramPanelModel programPanelModel;

    [SerializeField]
    private CommandDirector commandDirector;

    private void Awake()
    {
        commandDirector.swapUI_MainCommand += programCommandView.ProgramCommandTextChange;
        commandDirector.swapUI_Storage += inventryCommandView.InventryTextChange;
        programCommandView.ProgramCommandIndexes += commandDirector.GetMainCommandIndexSet();
        inventryCommandView.InventryCommandIndexes += commandDirector.GetStorageIndexSet();
        commandDirector.showUI += programPanelView.CanvasShow;
        commandDirector.hideUI += programPanelView.CanvasHide;
    }
}
