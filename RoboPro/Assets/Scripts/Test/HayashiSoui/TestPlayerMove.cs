using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    Animator playerAnimator;

    [SerializeField] float speed = 5;

    bool moveFlg = false;

    void Start()
    {
        playerAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moveFlg = true;
        }
        else
        {
            moveFlg = false;
        }

        PlayerController();

        AnimationTest();
    }

    private void PlayerController()
    {
        // �J�����̌�������ɂ������ʕ����̃x�N�g��
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �O�㍶�E�̓��́iWASD�L�[�j����A�ړ��̂��߂̃x�N�g�����v�Z
        // Input.GetAxis("Vertical") �͑O��iWS�L�[�j�̓��͒l
        // Input.GetAxis("Horizontal") �͍��E�iAD�L�[�j�̓��͒l
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //�@�O��i�J�����)
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // ���E�i�J������j

        // �v���C���[�̌�������͂̌����ɕύX
        transform.LookAt(transform.position + moveZ + moveX);

        // ���ʂɐi��
        if (moveFlg == true)
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        }
    }

    private void AnimationTest()
    {
        // �ړ�
        if (moveFlg == true)
        {
            playerAnimator.SetBool("Flg_Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_Walk", false);
        }
        // �A�N�Z�X
        if (Input.GetKey(KeyCode.E))
        {
            playerAnimator.SetBool("Flg_Access", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_Access", false);
        }
        // �W�����v(��э~��)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetBool("Flg_StepOff", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_StepOff", false);
        }
        // �����@���{���͒n�ɑ������Ă��邩���m
        if (Input.GetKey(KeyCode.Space))
        {
            playerAnimator.SetBool("Flg_Fall", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_Fall", false);
        }
        // ���n
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimator.SetBool("Flg_Landing", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_Landing", false);
        }

    }
}
