using UnityEngine;
using System;
using Zenject;
using UniRx;

namespace Command
{
    public class AccessPoint : MonoBehaviour
    {
        public const float MAX_RADIUS = 1.5f;

        [Header("値確認用　数値変更非推奨")]
        public int index;
    }
}