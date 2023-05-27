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
        public const float RADIUS = 1.5f;   // 当たり判定の有効範囲の半径

        [Tooltip("APとギミックを繋ぐラインの色")]
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
        /// ギミックを有効化する関数
        /// </summary>
        public void GimmickActivate()
        {
            // ギミック管理クラスの開始関数を実行
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