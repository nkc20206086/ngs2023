using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmick
{
    public class FutureGimmickMoveGetter : MonoBehaviour
    {
        public static GameObject pointObject { get; private set; }
        public static GameObject roadObject { get; private set; }

        [SerializeField]
        private GameObject pointPrefab;

        [SerializeField]
        private GameObject roadPrefab;

        private void Awake()
        {
            pointObject = pointPrefab;
            roadObject = roadPrefab;

            Debug.Log($"{pointObject.name},{roadObject.name}");

            gameObject.SetActive(false);
        }
    }
}