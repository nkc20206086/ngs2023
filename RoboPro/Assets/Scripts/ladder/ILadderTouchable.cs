using UnityEngine;

namespace Ladder
{
    interface ILadderTouchable
    {
        /// <summary>
        /// �g�p�\���ǂ����𔻒肷��
        /// </summary>
        /// <returns></returns>
        public LadderClimbData IsUsable(Transform playerTrans);
    }


}
