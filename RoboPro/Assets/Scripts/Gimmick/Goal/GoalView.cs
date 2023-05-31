using Cysharp.Threading.Tasks;
using InteractUI;
using Robo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

public class GoalView : MonoBehaviour
{
    [SerializeField]
    private Goal goal;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ParticleSystem interactingEffect;

    [SerializeField]
    private ParticleSystem clearEffect;

    [SerializeField]
    private GameObject[] clearOnHideObjects;

    [SerializeField]
    private Vector3 uiOffset;

    [SerializeField]
    private DisplayInteractCanvasAsset interactAsset;

    [SerializeField]
    private float goalAnimationTime = 2f;

    [Inject]
    private IVCameraTargetChanger vCameraChanger;

    [Inject]
    private IInteractUIControllable interactUIControllable;

    [Inject]
    private IMultiSceneLoader multiSceneLoader;

    [Inject]
    private IPostEffector postEffector;

    private bool isClear = false;

    private void Start()
    {
        goal.OnHitGoal += OnHitGoal;
        goal.OnExitGoal += OnExitGoal;
        goal.OnChangeInteractingTime += OnChangeInteractingTime;
        goal.OnClear += OnClear;
        interactUIControllable.SetPosition(transform.position + uiOffset);
    }

    //private void Update()
    //{
    //    // 現オブジェクトからメインカメラ方向のベクトルを取得する
    //    Vector3 direction = Camera.main.transform.position - slider.transform.position;

    //    // オブジェクトをベクトル方向に従って回転させる
    //    slider.transform.rotation = Quaternion.LookRotation(direction);
    //}

    private void OnChangeInteractingTime(float value)
    {
        if (isClear) return;
        //slider.SetValueWithoutNotify(value);
        interactingEffect.Play();
        interactUIControllable.SetFillAmount(value);
    }

    private void OnHitGoal()
    {
        if (isClear) return;
        animator.SetTrigger("Show");
        interactingEffect.gameObject.SetActive(true);

        var controllerNames = Input.GetJoystickNames();
        ControllerType type = ControllerType.Controller;
        if (controllerNames.Length == 0) type = ControllerType.Keyboard;
        interactUIControllable.ShowUI(type, interactAsset);
    }

    private void OnExitGoal()
    {
        if (isClear) return;
        animator.SetTrigger("Hide");
        interactingEffect.gameObject.SetActive(false);
        interactUIControllable.HideUI();
    }

    private async void OnClear()
    {
        isClear = true;
        animator.SetTrigger("Hide");

        vCameraChanger.ChangeCameraTarget(VCameraType.Goal);
        clearEffect.Play();
        interactUIControllable.HideUI();

        for(int i = 0; i < clearOnHideObjects.Length; i++)
        {
            clearOnHideObjects[i].gameObject.SetActive(false);
        }
        GoToStageArgmentsSingleton.Clear();

        await UniTask.Delay(TimeSpan.FromSeconds(goalAnimationTime));
        BackToStageSelect();
    }

    public void BackToStageSelect()
    {
        postEffector.SetMaterial(PostEffectMaterialKey.ImageFade);
        postEffector.Fade(FadeType.Out, 1, DG.Tweening.Ease.Linear).onComplete += async () =>
        {
            await multiSceneLoader.AddScene(Robo.SceneID.StageSelect, true);
            await multiSceneLoader.UnloadScene(Robo.SceneID.Stage);
            postEffector.Fade(FadeType.In, 1, DG.Tweening.Ease.Linear);
        };
    }
}
