using UnityEngine;
using CommandUI;

public class ProgramPresenter : MonoBehaviour
{
    [SerializeField] ProgramPanelView programPanelView;
    [SerializeField] ProgramCommandView programCommandView;
    [SerializeField] InventryCommandView inventryCommandView;

    //テスト用
    [SerializeField] InventryModel inventryModel;
    [SerializeField] ProgramPanelModel programPanelModel;

    private void Awake()
    {
        programPanelModel.UIEvent += programCommandView.ProgramCommandTextChange;
        inventryModel.UIEvent += inventryCommandView.InventryTextChange;
        programCommandView.ProgramCommandIndexes += programPanelModel.Test;
        inventryCommandView.InventryCommandIndexes += inventryModel.Test;
    }
}
