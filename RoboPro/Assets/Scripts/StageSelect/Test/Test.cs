using Robo;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

//�X�e�[�W�Z���N�g�ɕK�v�ȃf�[�^��Inject���ăX�e�[�W�Z���N�g������������e�X�g�N���X
//StageSelect�V�[���Ŏg�p��
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
