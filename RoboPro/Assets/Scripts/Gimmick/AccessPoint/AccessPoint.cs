using UnityEngine;
using System;
using Zenject;
using UniRx;

namespace Command
{
    public class AccessPoint : MonoBehaviour,IGimmickAccess
    {
        [Header("�l�m�F�p�@���l�ύX�񐄏�")]
        public int index;
        public bool updatePlay = true;

        public IObserver<int> openAct;      // �R�}���h����ւ����s�p�A�N�V����
        public IObserver<Unit> closeAct;    // �R�}���h����ւ��I���p�A�N�V����

        void IGimmickAccess.GimmickAccess()
        {
            openAct.OnNext(index);
        }

        void IGimmickAccess.RemoveAccess()
        {
            closeAct.OnNext(default);
        }
    }
}