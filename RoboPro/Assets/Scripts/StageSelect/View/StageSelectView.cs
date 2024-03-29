﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
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
        private Ease moveEase = Ease.OutCirc;

        [Inject]
        private DiContainer container;

        public event Action<int> OnSelect;
        public event Action<int> OnDeselect;
        public event Action OnSelectNextKey;
        public event Action OnSelectPreviousKey;
        public event Action OnPlay;

        public IReadOnlyList<StageSelectElementInfo> Infos { get; private set; }
        public IReadOnlyList<StageSelectElementView> Elements => elements;

        private List<StageSelectElementView> elements = new List<StageSelectElementView>();
        private int nowSelectedIndex = 0;

        void IStageSelectView.Initalize(StageSelectModelArgs args)
        {
            Infos = args.Infos;
            for (int i = 0; i < args.Infos.Count; i++)
            {
                StageSelectElementView element = container.InstantiatePrefab(elementPrefab).GetComponent<StageSelectElementView>();
                elements.Add(element);

                element.Initalize(args.Infos[i], i);
                element.transform.SetParent(elementsParent);
                element.transform.localPosition = new Vector3(putDistance * i, 0, 0);

                OnSelect += (idx) => element.OnSelect(idx);
                OnDeselect += (idx) => element.OnDeselect(idx);
            }
        }

        //ステージ選択
        void IStageSelectView.Select(int idx)
        {
            Transform element = elements[idx].transform;
            elementsParent.DOLocalMoveX(-element.localPosition.x, moveDuration).SetEase(moveEase);

            OnDeselect?.Invoke(nowSelectedIndex);
            OnSelect?.Invoke(idx);
            nowSelectedIndex = idx;
        }

        //選択不可能範囲に移動すると警告が出る
        void IStageSelectView.SelectError(int idx)
        {
            Debug.Log("これ以上進めない");
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
            if(Input.GetKeyDown(KeyCode.Space))
            {
                OnPlay?.Invoke();
            }
        }
    }
}