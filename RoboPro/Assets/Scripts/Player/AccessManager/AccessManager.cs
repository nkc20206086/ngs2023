using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AccessManager : MonoBehaviour
    {
        public event Action accessEndEvent;

        private void Awake()
        {
            Locator<AccessManager>.Bind(this);
        }

        //アクセス終了時処理
        public void AccessEnd()
        {
            Debug.Log("終了");
            accessEndEvent();
        }
    }

}
