using UnityEngine;
using System;
using Zenject;
using UniRx;

namespace Command
{
    public class AccessPoint : MonoBehaviour
    {
        [Header("�f�o�b�O�p")]
        [SerializeField]
        private KeyCode openKeyCode = KeyCode.Return;
        [SerializeField]
        private KeyCode closeKeyCode = KeyCode.Escape;

        [Header("�l�m�F�p�@���l�ύX�񐄏�")]
        public int index;
        public bool updatePlay = true;

        public IObserver<int> openAct;      // �R�}���h����ւ����s�p�A�N�V����
        public IObserver<Unit> closeAct;    // �R�}���h����ւ��I���p�A�N�V����

        // Update is called once per frame
        void Update()
        {
            if (!updatePlay) return;

            // �����̏����̓f�o�b�O�p�Ȃ̂ŁA���ۂɗp����ꍇ�͕ύX���邱��

            if (Input.GetKeyDown(openKeyCode))
            {
                openAct.OnNext(index);
            }

            if (Input.GetKeyDown(closeKeyCode))
            {
                closeAct.OnNext(default);
            }
        }
    }
}