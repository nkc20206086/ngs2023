using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSavePosData
{
    public List<Vector3> saveVecList = new List<Vector3>();
    public List<Quaternion> saveQuaternionsList = new List<Quaternion>();
    public int callCount;

    public PlayerSavePosData(List<Vector3> saveVecList, List<Quaternion> saveQuaternionsList,int callCount)
    {
        this.saveVecList = saveVecList;
        this.saveQuaternionsList = saveQuaternionsList;
        this.callCount = callCount;
    }

    public void SaveData(List<Vector3> vecList, List<Quaternion> quaternionsList,int count)
    {
        saveVecList = vecList;
        saveQuaternionsList = quaternionsList;
        callCount = count;
    }





    public override bool Equals(object obj)
    {
        return obj is PlayerSavePosData data &&
               EqualityComparer<List<Vector3>>.Default.Equals(saveVecList, data.saveVecList) &&
               EqualityComparer<List<Quaternion>>.Default.Equals(saveQuaternionsList, data.saveQuaternionsList);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(saveVecList, saveQuaternionsList);
    }
}
