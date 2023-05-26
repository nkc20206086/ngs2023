using UnityEditor;
using UnityEngine;

namespace Ladder
{
    public class LadderController : MonoBehaviour, ILadderTouchable
    {
        [SerializeField]
        private GameObject[] ladderObjects;
        [SerializeField]
        private GameObject instanceLadder;
        [SerializeField, Header("梯子のたかさ")]
        private float ladderHigh;
        [SerializeField, Range(1, 20), Header("はしごの数")]
        private int ladderCount;

        private Ladder[] ladders;
        public void OnButtonClickInInspector()
        {
            for (int i = 0; i < ladderObjects.Length; i++)
            {
                DestroyImmediate(ladderObjects[i]);
            }
            InstanceLadder();
        }

        private void Start()
        {
            ladders = new Ladder[ladderCount];
            for (int i = 0; i < ladderObjects.Length; i++)
            {
                ladders[i] = ladderObjects[i].GetComponent<Ladder>();
            }
        }
        private void Update()
        {

        }
        private void InstanceLadder()
        {
            ladderObjects = new GameObject[ladderCount];
            for (int i = 0; i < ladderCount; i++)
            {
                //はしごを生成し子オブジェクトとして登録
                ladderObjects[i] = Instantiate(instanceLadder, this.gameObject.transform);
                //最初の生成位置を計算
                float firstYPos = transform.position.y;
                //生成位置を設定
                ladderObjects[i].transform.position = new Vector3(transform.position.x, firstYPos + (i /*+ 1*/) * ladderHigh, transform.position.z);
            }
            //最後のはしごフラグを入れる
            ladderObjects[ladderCount - 1].GetComponent<Ladder>().isTopLadder = true;
        }
        private LadderClimbData IsUsable(Transform playerTrans)
        {
            foreach (var item in ladders)
            {
                //一つでも登れないことが判明した場合、登れない判定を返す
                if (item.UpGroundCheck() == false)
                {
                    LadderClimbData ladderClimbData = LadderClimbData.errorData;
                    return ladderClimbData;
                }
            }
            //どのはしごに近いかを決める
            float minHighDistance = float.MaxValue;
            int minLadderIndex = -1;

            float upDistance = Vector3.Distance(playerTrans.position, ladderObjects[0].transform.position);
            if (minHighDistance > upDistance)
            {
                minHighDistance = upDistance;
                minLadderIndex = 0;
            }
            float downDistance = Vector3.Distance(playerTrans.position, ladderObjects[ladderCount - 1].transform.position);
            if (minHighDistance > downDistance)
            {
                minHighDistance = downDistance;
                minLadderIndex = ladderCount - 1;
            }

            bool directionCheck = DirectionCheck(playerTrans, ladderObjects[minLadderIndex].transform.position, minLadderIndex);
            if (directionCheck == false)
            {
                return LadderClimbData.errorData;
            }
            //登れる判定データを返す
            LadderClimbData climbableData =
                new LadderClimbData(true, ladderObjects[minLadderIndex].transform.position,
                minLadderIndex == 0 ? ClimbType.Up : ClimbType.Down, minLadderIndex, minHighDistance);
            return climbableData;
        }
        private bool DirectionCheck(Transform playerTrans, Vector3 calcPos, int minladderIndex)
        {
            //裏を向いているときは登れないので角度から調査
            Vector2 playerPos = new Vector2(playerTrans.position.x, playerTrans.position.z);
            Vector2 ladderPos = new Vector2(calcPos.x, calcPos.z);

            if (GetAngle(playerPos, ladderPos) <= 180)
            {
                //プレイヤーとはしごの間に障害物がないか確認
                if (ladders[minladderIndex].PlayerOnLadderCheck(playerPos, ladderPos))
                {
                    return true;
                }
            }
            return false;
        }
        //2点から角度を求める
        private float GetAngle(Vector2 start, Vector2 target)
        {
            Vector2 dt = target - start;
            float rad = Mathf.Atan2(dt.y, dt.x);
            float degree = rad * Mathf.Rad2Deg;
            return Mathf.Repeat(degree, 360);
        }


        LadderClimbData ILadderTouchable.IsUsable(Transform playerTrans)
        {
            return IsUsable(playerTrans);
        }
    }

}
