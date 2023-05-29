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
        public const float RADIUS = 1.5f;   // ½è»èÌLøÍÍÌ¼a

        [Tooltip("APÆM~bNðq®CÌF")]
        public Color color;

        private List<GimmickController> gimmickControllers;

        public MainCommand[] controlCommands = new MainCommand[CommandUtility.commandCount];
        private List<MainCommand[]> archives = new List<MainCommand[]>();

        public List<GimmickController> controlGimmicks
        {
            get => gimmickControllers;
        }

        public void StartUp(AccessPointData data)
        {
            for (int i = 0; i < controlCommands.Length; i++)
            {
                controlCommands[i] = CommandCreater.CreateCommand(data.Commands[i]);
            }
        }

        public void GimmickSubscrive(List<GameObject> objList)
        {
            gimmickControllers ??= new List<GimmickController>();

            foreach (GameObject obj in objList)
            {
                gimmickControllers.Add(obj.GetComponent<GimmickController>());
            }

            ArchiveAdd(0);
        }

        public void ControlGimmicksUpdate()
        {
            foreach (GimmickController gimmick in gimmickControllers)
            {
                gimmick.CommandUpdate();
            }
        }

        public void ArchiveAdd(int index)
        {
            for (int i = archives.Count - 1;i >= index;i--)
            {
                archives.RemoveAt(i);
            }

            MainCommand[] mainCommands = new MainCommand[controlCommands.Length];

            for (int i = 0;i < controlCommands.Length;i++)
            {
                mainCommands[i] = controlCommands[i] != null ? controlCommands[i].MainCommandClone() : null;
            }

            archives.Add(mainCommands);

            foreach (GimmickController controller in gimmickControllers)
            {
                controller.CommandSet(controlCommands);
            }
        }

        public void ArchiveSet(int index)
        {
            Array.Copy(archives[index],controlCommands,controlCommands.Length);

            foreach (GimmickController controller in gimmickControllers)
            {
                controller.CommandSet(controlCommands);
            }
        }
    }
}