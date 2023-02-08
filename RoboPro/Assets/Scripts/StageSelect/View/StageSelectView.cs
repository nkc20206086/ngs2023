using DG.Tweening;
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

        public event Action OnSelectNextKey;
        public event Action OnSelectPreviousKey;
        public event Action OnPlay;

        private List<StageSelectElementView> elements = new List<StageSelectElementView>();

        public void Initalize(StageSelectModelArgs args)
        {
            for(int i = 0; i < args.StageLength;i++)
            {
                StageSelectElementView element = Instantiate(elementPrefab);
                element.transform.SetParent(elementsParent);
                elements.Add(element);
            }
        }

        public void Select(int idx)
        {
            Vector3 position = elements[idx].transform.localPosition;
            elementsParent.DOLocalMoveX(-position.x, moveDuration);
        }

        public void SelectError(int idx)
        {
            Debug.Log("これ以上進めない");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                OnSelectNextKey?.Invoke();
            }
            else
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnSelectPreviousKey?.Invoke();
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                OnPlay?.Invoke();
            }
        }
    }
}