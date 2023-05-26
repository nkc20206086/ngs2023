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
        [SerializeField, Header("はしごを登れる距離")]
        private float canClimbLength;
        private List<ILadderTouchable> ladderControllers = new List<ILadderTouchable>();

        private void Start()
        {
            foreach (ILadderTouchable item in FindObjectsOfType<LadderController>())
            {
                ladderControllers.Add(item);
            }
        }
        LadderClimbData ILadderClimbable.GetLadderClimableData(Transform playerTransform)
        {
            //全はしごの中から一番近いはしごを判別
            int minIndex = -1;
            float minLength = float.MaxValue;
            Vector3 climbPos = new Vector3();
            ClimbType climbType = ClimbType.Error;
            LadderClimbData ladderClimbData;
            Debug.Log("count=" + ladderControllers.Count);
            for (int i = 0; i < ladderControllers.Count; i++)
            {
                //はしごデータを取得
                ladderClimbData = ladderControllers[i].IsUsable(playerTransform);
                if (ladderClimbData.isClimableLadder)
                {
                    if (ladderClimbData.ladderToPlayerLength < minIndex)
                    {
                        minIndex = i;
                        minLength = ladderControllers[i].IsUsable(playerTransform).ladderToPlayerLength;
                        climbPos = ladderControllers[i].IsUsable(playerTransform).climbPos;
                        climbType = ladderControllers[i].IsUsable(playerTransform).climbType;
                    }
                }

            }
            Debug.Log($"一番近いはしご" + minLength);
            //一番近いはしごとの距離が触れる距離かどうか
            if (canClimbLength >= minLength)
            {
                LadderClimbData resultData = new LadderClimbData(true, climbPos, climbType, minIndex, minLength);
                return resultData;
            }
            else
            {
                return LadderClimbData.errorData;
            }
            return LadderClimbData.errorData;
        }
    }
}
