using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GoalView : MonoBehaviour
{
    [SerializeField]
    private Goal goal;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private ParticleSystem interactingEffect;

    [SerializeField]
    private ParticleSystem clearEffect;

    [Inject]
    private IVCameraTargetChanger vCameraChanger;

    private bool isClear = false;

    private void Start()
    {
        goal.OnHitGoal += OnHitGoal;
        goal.OnExitGoal += OnExitGoal;
        goal.OnChangeInteractingTime += OnChangeInteractingTime;
        goal.OnClear += OnClear;
    }

    private void Update()
    {
        // 現オブジェクトからメインカメラ方向のベクトルを取得する
        Vector3 direction = Camera.main.transform.position - slider.transform.position;

        // オブジェクトをベクトル方向に従って回転させる
        slider.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnChangeInteractingTime(float value)
    {
        if (isClear) return;
        slider.SetValueWithoutNotify(value);
        interactingEffect.Play();
        Debug.Log("OK");
    }

    private void OnHitGoal()
    {
        if (isClear) return;
        animator.SetTrigger("Show");
        interactingEffect.gameObject.SetActive(true);
    }

    private void OnExitGoal()
    {
        if (isClear) return;
        animator.SetTrigger("Hide");
        interactingEffect.gameObject.SetActive(false);
    }

    private void OnClear()
    {
        isClear = true;
        animator.SetTrigger("Hide");

        vCameraChanger.ChangeCameraTarget(VCameraType.Goal);
        clearEffect.Play();
    }
}
