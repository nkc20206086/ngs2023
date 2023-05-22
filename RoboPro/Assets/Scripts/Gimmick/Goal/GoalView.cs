using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalView : MonoBehaviour
{
    [SerializeField]
    private Goal goal;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Slider slider;

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
        // ���I�u�W�F�N�g���烁�C���J���������̃x�N�g�����擾����
        Vector3 direction = Camera.main.transform.position - slider.transform.position;

        // �I�u�W�F�N�g���x�N�g�������ɏ]���ĉ�]������
        slider.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnChangeInteractingTime(float value)
    {
        if (isClear) return;
        slider.SetValueWithoutNotify(value);
    }

    private void OnHitGoal()
    {
        if (isClear) return;
        animator.SetTrigger("Show");
    }

    private void OnExitGoal()
    {
        if (isClear) return;
        animator.SetTrigger("Hide");
    }

    private void OnClear()
    {
        isClear = true;
        animator.SetTrigger("Hide");
    }
}
