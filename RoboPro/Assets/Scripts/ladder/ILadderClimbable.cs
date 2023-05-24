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
        public int GetLadderClimableIndex(Transform playerTransform);
    }
}
