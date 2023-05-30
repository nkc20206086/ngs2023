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
        /// �v���C���[�̃|�W�V�����ƌ�����ۑ����鏈��
        /// </summary>
        public void SaveList()
        {
            //�J�E���g�������������炻�̂܂ܓ����
            if (callCount == saveVecList.Count)
            {
                saveVecList.Add(gameObject.transform.position);
                saveQuaternionsList.Add(gameObject.transform.rotation);
            }
            else
            {
                //�߂���������l������
                saveVecList.Insert(callCount, gameObject.transform.position);
                saveQuaternionsList.Insert(callCount, gameObject.transform.rotation);

                //�J�E���g�̍���2�ȏ�ŏ����Ȃ���΂����Ȃ��v�f�����݂���
                if (1 < saveVecList.Count - callCount)
                {
                    saveVecList.RemoveRange(callCount + 1, saveVecList.Count - 1);
                    saveQuaternionsList.RemoveRange(callCount + 1, saveVecList.Count - 1);
                }
            }
            callCount++;
            //List��ۑ�����
            savePosData.SaveData(saveVecList, saveQuaternionsList, callCount);
        }
    }
}
