using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ILadderTouchable
{
    /// <summary>
    /// 使用可能かどうかを判定する
    /// </summary>
    /// <returns></returns>
    public bool IsUsable();
}

