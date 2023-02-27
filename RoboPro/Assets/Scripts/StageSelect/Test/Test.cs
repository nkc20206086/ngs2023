using Robo;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] 
    private IStageSelectModel model;

    [SerializeField]
    private List<StageSelectElementInfo> infos;

    private void Start()
    {
        model.Initalize(new StageSelectModelArgs(infos));
    }
}
