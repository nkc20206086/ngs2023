using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ladder
{
    interface ILadderClimbable
    {
        public LadderClimbData GetLadderClimableData(Transform playerTransform);
        //public Vector3 ClimbLadder(LadderClimbData ladderClimbData);
    }
}
