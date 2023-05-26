using UnityEngine;

namespace Ladder
{
    interface ILadderTouchable
    {
        /// <summary>
        /// 使用可能かどうかを判定する
        /// </summary>
        /// <returns></returns>
        public LadderClimbData IsUsable(Transform playerTrans);
    }


}
