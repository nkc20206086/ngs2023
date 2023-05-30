using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AccessManager : MonoBehaviour
    {
        public event Action accessEndEvent;

        //アクセス終了時処理
        public void AccessEnd()
        {
            accessEndEvent();
        }
    }

}
