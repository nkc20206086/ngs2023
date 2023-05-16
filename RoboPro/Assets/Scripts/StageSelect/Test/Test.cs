using Robo;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

//ステージセレクトに必要なデータをInjectしてステージセレクトを初期化するテストクラス
//StageSelectシーンで使用中
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
