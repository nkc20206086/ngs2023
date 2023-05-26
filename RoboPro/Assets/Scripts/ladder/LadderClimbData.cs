using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ladder
{
    public struct LadderClimbData
    {
        public readonly bool isClimableLadder;
        public readonly Vector3 climbPos;
        public readonly ClimbType climbType;
        public readonly int ladderIndex;
        public readonly float ladderToPlayerLength;
        public readonly static LadderClimbData errorData = new LadderClimbData(false,Vector3.one,ClimbType.Error,-1,-1); 

        public LadderClimbData(bool climableLadder, Vector3 climbPos, ClimbType climbType, int ladderIndex, float ladderToPlayerLength)
        {
            this.isClimableLadder = climableLadder;
            this.climbPos = climbPos;
            this.climbType = climbType;
            this.ladderIndex = ladderIndex;
            this.ladderToPlayerLength = ladderToPlayerLength;
        }
    }
}
