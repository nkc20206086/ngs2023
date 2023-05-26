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

        //�A�N�Z�X�I��������
        public void AccessEnd()
        {
            Debug.Log("�I��");
            accessEndEvent();
        }
    }

}
