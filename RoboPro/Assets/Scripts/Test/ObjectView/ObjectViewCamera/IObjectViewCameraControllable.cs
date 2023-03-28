using UnityEngine;

namespace ObjectView
{
    public interface IObjectViewCameraControllable
    {
        // TODO : カメラ担当に任せる。カメラを回転させるプログラムを作成する
        public void SetCameraRotate(Vector3 targetPos, float angle);
    }
}

