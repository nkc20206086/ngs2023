using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ladder
{
    struct LadderClimbData
    {
        public bool climableLadder;
        public Vector3 climbPos;
        public ClimbType climbType;

        public LadderClimbData(bool climableLadder, Vector3 climbPos, ClimbType climbType)
        {
            this.climableLadder = climableLadder;
            this.climbPos = climbPos;
            this.climbType = climbType;
        }

        public override bool Equals(object obj)
        {
            return obj is LadderClimbData data &&
                   climableLadder == data.climableLadder &&
                   climbPos.Equals(data.climbPos) &&
                   climbType == data.climbType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(climableLadder, climbPos, climbType);
        }
    }
}
