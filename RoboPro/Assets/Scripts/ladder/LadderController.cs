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
        [SerializeField, Header("��q�̂�����")]
        private float ladderHigh;
        [SerializeField, Range(1, 20), Header("�͂����̐�")]
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
                //�͂����𐶐����q�I�u�W�F�N�g�Ƃ��ēo�^
                ladderObjects[i] = Instantiate(instanceLadder, this.gameObject.transform);
                //�ŏ��̐����ʒu���v�Z
                float firstYPos = transform.position.y;
                //�����ʒu��ݒ�
                ladderObjects[i].transform.position = new Vector3(transform.position.x, firstYPos + (i /*+ 1*/) * ladderHigh, transform.position.z);
            }
            //�Ō�̂͂����t���O������
            ladderObjects[ladderCount - 1].GetComponent<Ladder>().isTopLadder = true;
        }
        private LadderClimbData IsUsable(Transform playerTrans)
        {
            foreach (var item in ladders)
            {
                //��ł��o��Ȃ����Ƃ����������ꍇ�A�o��Ȃ������Ԃ�
                if (item.UpGroundCheck() == false)
                {
                    LadderClimbData ladderClimbData = LadderClimbData.errorData;
                    return ladderClimbData;
                }
            }
            //�ǂ̂͂����ɋ߂��������߂�
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
            //�o��锻��f�[�^��Ԃ�
            LadderClimbData climbableData =
                new LadderClimbData(true, ladderObjects[minLadderIndex].transform.position,
                minLadderIndex == 0 ? ClimbType.Up : ClimbType.Down, minLadderIndex, minHighDistance);
            return climbableData;
        }
        private bool DirectionCheck(Transform playerTrans, Vector3 calcPos, int minladderIndex)
        {
            //���������Ă���Ƃ��͓o��Ȃ��̂Ŋp�x���璲��
            Vector2 playerPos = new Vector2(playerTrans.position.x, playerTrans.position.z);
            Vector2 ladderPos = new Vector2(calcPos.x, calcPos.z);

            if (GetAngle(playerPos, ladderPos) <= 180)
            {
                //�v���C���[�Ƃ͂����̊Ԃɏ�Q�����Ȃ����m�F
                if (ladders[minladderIndex].PlayerOnLadderCheck(playerPos, ladderPos))
                {
                    return true;
                }
            }
            return false;
        }
        //2�_����p�x�����߂�
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
