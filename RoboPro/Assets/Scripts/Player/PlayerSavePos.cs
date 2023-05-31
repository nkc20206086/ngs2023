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
            saveVecList.Add(gameObject.transform.position);
            saveQuaternionsList.Add(gameObject.transform.rotation);
        }

        /// <summary>
        /// プレイヤーのポジションと向きを保存する処理
        /// </summary>
        public void SaveList()
        {
            //カウントが同じだったらそのまま入れる
            if (callCount == saveVecList.Count - 1)
            {
                saveVecList.Add(gameObject.transform.position);
                saveQuaternionsList.Add(gameObject.transform.rotation);
                //Debug.Log("保存" + callCount);
            }
            else
            {
                for (int i = saveVecList.Count - 1; i > callCount; i--)
                {
                    saveVecList.RemoveAt(i);
                    saveQuaternionsList.RemoveAt(i);
                }
                //戻った所から値を入れる
                saveVecList.Add(gameObject.transform.position);
                saveQuaternionsList.Add(gameObject.transform.rotation);
            }
            callCount++;
            Debug.Log("callCount " + callCount);
            Debug.Log("List.Count " + saveVecList.Count);
            //Listを保存する
            savePosData.SaveData(saveVecList, saveQuaternionsList, callCount);
        }
    }
}
