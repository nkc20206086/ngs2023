using UnityEngine;

namespace Ladder
{
    interface ILadderTouchable
    {
        /// <summary>
        /// Žg—p‰Â”\‚©‚Ç‚¤‚©‚ð”»’è‚·‚é
        /// </summary>
        /// <returns></returns>
        public LadderClimbData IsUsable(Transform playerTrans);
    }


}
