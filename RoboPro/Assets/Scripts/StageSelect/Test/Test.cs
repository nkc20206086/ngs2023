using Robo;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] 
    private IStageSelectModel model;

    private void Start()
    {
        model.Initalize(new StageSelectModelArgs(5));
    }
}
