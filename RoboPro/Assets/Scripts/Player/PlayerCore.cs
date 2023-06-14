using Gimmick.Interface;
using Robo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerCore : MonoBehaviour, IStateGetter
    {
        [SerializeField]
        private float ladderUpDownSpeed;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private Vector2 jumpPower;
        [SerializeField]
        private float deathHeight;
        [SerializeField]
        private float playerUI_Yvector;

        [Inject]
        private IAudioPlayer audioPlayer;

        [Inject]
        private IGimmickAccess gimmickAccess;

        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public Rigidbody rigidbody;

        private IStateChange[] stateChangeArray = new IStateChange[(int)PlayerStateEnum.Count];
        private PlayerStateEnum state;
        private GroundChecker groundChecker;
        private LadderChecker ladderChecker;

        private bool isDeath;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            stateChangeArray = GetComponents<IStateChange>();
            foreach (IStateChange stateChange in stateChangeArray)
            {
                stateChange.stateChangeEvent += StateChanger;
            }
            groundChecker = GetComponent<GroundChecker>();
            ladderChecker = GetComponent<LadderChecker>();
        }

        /// <param name="newStateEnum"></param>
        public void StateChanger(PlayerStateEnum newStateEnum)
        {
            //Debug.Log(newStateEnum);
            //�X�e�[�g��ύX
            state = newStateEnum;
            //Debug.Log(state + "�X�e�[�g�ɕύX����܂���");

            //���ʃX�e�[�g�������ꍇ�A�t���O��ς���
            if (newStateEnum == PlayerStateEnum.Die) isDeath = true;
            else isDeath = false;
        }

        PlayerStateEnum IStateGetter.StateGetter()
        {
            return state;
        }

        float IStateGetter.SpeedGetter()
        {
            return moveSpeed;
        }

        Animator IStateGetter.PlayerAnimatorGeter()
        {
            return animator;
        }

        Rigidbody IStateGetter.RigidbodyGetter()
        {
            return rigidbody;
        }

        float IStateGetter.LadderUpDownSpeedGetter()
        {
            return ladderUpDownSpeed;
        }

        Vector2 IStateGetter.JumpPowerGetter()
        {
            return jumpPower;
        }

        IGimmickAccess IStateGetter.GimmickAccessGetter()
        {
            return gimmickAccess;
        }

        GroundChecker IStateGetter.GroundCheckGetter()
        {
            return groundChecker;
        }

        LadderChecker IStateGetter.LadderCheckGetter()
        {
            return ladderChecker;
        }

        float IStateGetter.DeathHeightGetter()
        {
            return deathHeight;
        }

        float IStateGetter.PlayerUI_OffsetYGetter()
        {
            return playerUI_Yvector;
        }

        IAudioPlayer IStateGetter.AudioGetter()
        {
            return audioPlayer;
        }

        bool IStateGetter.CheckDeathBoolGetter()
        {
            return isDeath;
        }
    }
}