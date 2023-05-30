using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerSavePos : MonoBehaviour
    {

        private PlayerSavePosData savePosData;
        public List<Vector3> saveVecList = new List<Vector3>();
        public List<Quaternion> saveQuaternionsList = new List<Quaternion>();
        public int callCount;

        // Start is called before the first frame update
        void Start()
        {
            savePosData = new PlayerSavePosData(saveVecList, saveQuaternionsList, callCount);
            SaveList();
        }

        /// <summary>
        /// プレイヤーのポジションと向きを保存する処理
        /// </summary>
        public void SaveList()
        {
            //カウントが同じだったらそのまま入れる
            if (callCount == saveVecList.Count)
            {
                saveVecList.Add(gameObject.transform.position);
                saveQuaternionsList.Add(gameObject.transform.rotation);
            }
            else
            {
                //戻った所から値を入れる
                saveVecList.Insert(callCount, gameObject.transform.position);
                saveQuaternionsList.Insert(callCount, gameObject.transform.rotation);

                //カウントの差が2以上で消さなければいけない要素が存在する
                if (1 < saveVecList.Count - callCount)
                {
                    saveVecList.RemoveRange(callCount + 1, saveVecList.Count - 1);
                    saveQuaternionsList.RemoveRange(callCount + 1, saveVecList.Count - 1);
                }
            }
            callCount++;
            //Listを保存する
            savePosData.SaveData(saveVecList, saveQuaternionsList, callCount);
        }
    }
}
