using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ladder
{
    class LadderDirector : MonoBehaviour, ILadderClimbable
    {
        private List<LadderController> ladderObjcts = new List<LadderController>();

        private void Start()
        {
            var objs = GetComponents<LadderController>();
            foreach (var item in objs)
            {
                ladderObjcts.Add(item);
            }
        }
        int ILadderClimbable.GetLadderClimableIndex(Transform playerTransform)
        {
            throw new NotImplementedException();
        }

    }
}
