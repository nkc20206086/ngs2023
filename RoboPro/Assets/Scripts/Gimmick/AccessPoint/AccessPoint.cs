using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;
using UniRx;
using Command;
using Command.Entity;

namespace Gimmick
{
    public class AccessPoint : MonoBehaviour
    {
        public const float RADIUS = 1.5f;   // �����蔻��̗L���͈͂̔��a

        [Tooltip("AP�ƃM�~�b�N���q�����C���̐F")]
        public Color color;

        private MainCommand[] mainCommands = new MainCommand[4];
        private List<GimmickController> gimmickControllers;
        private List<CommandContainer> usableCommands = new List<CommandContainer>();

        public List<GimmickController> controlGimmicks
        {
            get => gimmickControllers;
        }

        public void StartUp(AccessPointData data)
        {
            usableCommands = data.Commands;
        }

        public void GimmickSubscrive(List<GameObject> objList)
        {
            gimmickControllers ??= new List<GimmickController>();

            foreach (GameObject obj in objList)
            {
                gimmickControllers.Add(obj.GetComponent<GimmickController>());
            }
        }

        /// <summary>
        /// �M�~�b�N��L��������֐�
        /// </summary>
        public void GimmickActivate()
        {
            // �M�~�b�N�Ǘ��N���X�̊J�n�֐������s
            foreach (GimmickController gimmick in gimmickControllers)
            {
                gimmick.StartUp(usableCommands.ToArray());
            }
        }

        public void ControlGimmicksUpdate()
        {
            foreach (GimmickController gimmick in gimmickControllers)
            {
                gimmick.CommandUpdate();
            }
        }
    }
}