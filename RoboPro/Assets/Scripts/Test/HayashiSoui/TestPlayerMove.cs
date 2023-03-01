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
        // カメラの向きを基準にした正面方向のベクトル
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 前後左右の入力（WASDキー）から、移動のためのベクトルを計算
        // Input.GetAxis("Vertical") は前後（WSキー）の入力値
        // Input.GetAxis("Horizontal") は左右（ADキー）の入力値
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //　前後（カメラ基準)
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // 左右（カメラ基準）

        // プレイヤーの向きを入力の向きに変更
        transform.LookAt(transform.position + moveZ + moveX);

        // 正面に進む
        if (moveFlg == true)
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        }
    }

    private void AnimationTest()
    {
        // 移動
        if (moveFlg == true)
        {
            playerAnimator.SetBool("Flg_Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_Walk", false);
        }
        // アクセス
        if (Input.GetKey(KeyCode.E))
        {
            playerAnimator.SetBool("Flg_Access", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_Access", false);
        }
        // ジャンプ(飛び降り)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetBool("Flg_StepOff", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_StepOff", false);
        }
        // 落下　※本来は地に足をついているか検知
        if (Input.GetKey(KeyCode.Space))
        {
            playerAnimator.SetBool("Flg_Fall", true);
        }
        else
        {
            playerAnimator.SetBool("Flg_Fall", false);
        }
        // 着地
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
