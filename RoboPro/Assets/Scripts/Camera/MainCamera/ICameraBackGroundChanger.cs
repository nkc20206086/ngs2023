using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainCamera
{
    public interface ICameraBackGroundChanger
    {
        /// <summary>
        /// ���񂾂Ƃ��̔w�i�̐F�ɕύX
        /// </summary>
        public void Death_BackGroundChange();

        /// <summary>
        /// �ʏ�̔w�i�̐F�ɕύX
        /// </summary>
        public void Default_BackGroundChange();
    }
}

