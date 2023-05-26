using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Stage
{
    public class StageUIManager : MonoBehaviour
    {
        [SerializeField]
        private Button undoButton;

        [SerializeField]
        private Button redoButton;

        [SerializeField]
        private Button playButton;

        private IObserver<Unit> undoAction;
        private IObserver<Unit> redoAction;
        private IObserver<Unit> playAction;

        public IObserver<Unit> undo
        {
            get => undoAction;
            set
            {
                undoAction = value;

                undoButton?.
                    OnClickAsObservable().
                    TakeUntilDestroy(this).
                    Subscribe(undoAction);
            }
        }

        public IObserver<Unit> redo
        {
            get => redoAction;
            set
            {
                redoAction = value;

                redoButton?.
                    OnClickAsObservable().
                    TakeUntilDestroy(this).
                    Subscribe(redoAction);
            }
        }

        public IObserver<Unit> play
        {
            get => playAction;
            set
            {
                playAction = value;

                playButton?.
                    OnClickAsObservable().
                    TakeUntilDestroy(this).
                    Subscribe(playAction);
            }
        }
    }
}