﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robo
{
    public class StageSelectView : MonoBehaviour, IStageSelectView
    {
        [SerializeField] private StageSelectElementView elementPrefab;
        [SerializeField] private Transform elementsParent;
        [SerializeField] private float moveDuration = 0.25f;

        private event Action OnSelectNextKey;
        private event Action OnSelectPreviousKey;
        private event Action OnPlay;

        private List<StageSelectElementView> elements = new List<StageSelectElementView>();

        event Action IStageSelectView.OnSelectNextKey
        {
            add => OnSelectNextKey += value;
            remove => OnSelectNextKey -= value;
        }

        event Action IStageSelectView.OnSelectPreviousKey
        {
            add => OnSelectPreviousKey += value;
            remove => OnSelectPreviousKey -= value;
        }

        event Action IStageSelectView.OnPlay
        {
            add => OnPlay += value;
            remove => OnPlay -= value;
        }

        void IStageSelectView.Initalize(StageSelectModelArgs args)
        {
            for(int i = 0; i < args.StageLength;i++)
            {
                StageSelectElementView element = Instantiate(elementPrefab);
                element.transform.SetParent(elementsParent);
                elements.Add(element);
            }
        }

        //ステージ選択
        void IStageSelectView.Select(int idx)
        {
            Vector3 position = elements[idx].transform.localPosition;
            elementsParent.DOLocalMoveX(-position.x, moveDuration);
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