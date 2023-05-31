using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Zenject;

namespace Robo
{
    public class StageSelectView : MonoBehaviour, IStageSelectView
    {
        [SerializeField] 
        private StageSelectElementView elementPrefab;
        
        [SerializeField] 
        private Transform elementsParent;
        
        [SerializeField] 
        private float moveDuration = 0.25f;
        
        [SerializeField] 
        private float putDistance = 5;

        [SerializeField]
        private float goToStageWaitTime = 1;

        [SerializeField]
        private float goToStageFadeTime = 1;

        [SerializeField] 
        private Ease moveEase = Ease.OutCirc;

        [SerializeField]
        private StagePreview preview;

        [Inject]
        private DiContainer container;

        [Inject]
        private IAudioPlayer audioPlayer;

        [Inject]
        private IMultiSceneLoader sceneLoader;

        [Inject]
        private IPostEffector postEffector;

        public event Action<int> OnSelect;
        public event Action<int> OnDeselect;
        public event Action OnSelectNextKey;
        public event Action OnSelectPreviousKey;
        public event Action OnPlay;
        public event Action<string> OnClear;
        public event Action OnSave;

        public IReadOnlyList<StageSelectElementInfo> Infos { get; private set; }
        public IReadOnlyList<StageSelectElementView> Elements => elements;
        public StageSelectSaveData SaveData => saveData;

        private List<StageSelectElementView> elements = new List<StageSelectElementView>();
        private StageSelectSaveData saveData;
        private int nowSelectedIndex = 0;

        void IStageSelectView.Initalize(StageSelectModelArgs args, StageSelectSaveData saveData)
        {
            Infos = args.Infos;
            this.saveData = saveData;
            for (int i = 0; i < args.Infos.Count; i++)
            {
                StageSelectElementView element = container.InstantiatePrefab(elementPrefab).GetComponent<StageSelectElementView>();
                elements.Add(element);

                element.Initalize(args.Infos[i], i);
                element.transform.SetParent(elementsParent);
                element.transform.localPosition = new Vector3(putDistance * i, 0, 0);
            }
            OnSelect += StageSelectElementOnSelect;
            OnDeselect += StageSelectElementOnDeselect;

            if(GoToStageArgmentsSingleton.Get() != null && GoToStageArgmentsSingleton.IsClear())
            {
                OnClear?.Invoke(GoToStageArgmentsSingleton.Get().StageNumber);
            }
        }

        private void StageSelectElementOnSelect(int idx)
        {
            elements[idx].OnSelect(idx);
            preview.transform.position = elements[idx].transform.position;
            preview.CreatePreview(Infos[idx].StageData);
        }
        
        private void StageSelectElementOnDeselect(int idx)
        {
            elements[idx].OnDeselect(idx);
        }

        //ステージ選択
        void IStageSelectView.Select(int idx)
        {
            if (idx == nowSelectedIndex) return;
            Transform element = elements[idx].transform;
            elementsParent.DOLocalMoveX(-element.localPosition.x, moveDuration).SetEase(moveEase);

            OnDeselect?.Invoke(nowSelectedIndex);
            OnSelect?.Invoke(idx);
            nowSelectedIndex = idx;
            audioPlayer.PlaySE(CueSheetType.System, "SE_System_Decision");
        }

        //選択不可能範囲に移動すると警告が出る
        void IStageSelectView.SelectError(int idx)
        {
            Debug.Log("これ以上進めない");
        }

        public async void Play()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(goToStageWaitTime));
            postEffector.SetMaterial(PostEffectMaterialKey.ImageFade);
            postEffector.SetColor("_Color", Color.black);
            Tween fade = postEffector.Fade(FadeType.Out, goToStageFadeTime, Ease.Linear);
            fade.onComplete += async () =>
            {
                await sceneLoader.AddScene(SceneID.Stage, true);
                await sceneLoader.UnloadScene(SceneID.StageSelect);
                Tween fade = postEffector.Fade(FadeType.In, goToStageFadeTime, Ease.Linear);
            };
            //シングルトンへ登録
            GoToStageArgmentsSingleton.SetStage(Infos[nowSelectedIndex]);
            audioPlayer.PlaySE(CueSheetType.System, "SE_System_ScanStaret_2");
        }

        private void Update()
        {
            //Dキーで右に移動
            if(Input.GetKeyDown(KeyCode.D))
            {
                OnSelectNextKey?.Invoke();
            }
            else
            //Aキーで左に移動
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnSelectPreviousKey?.Invoke();
            }
            //Spaceでステージをプレイ
            if(Input.GetKeyDown(KeyCode.E))
            {
                OnPlay?.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnSave?.Invoke();
        }
    }
}