using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadInfo : MonoBehaviour
{
    private PlayerSavePos playerSavePos;
    // Start is called before the first frame update
    void Start()
    {
        playerSavePos = GetComponent<PlayerSavePos>();
    }

    /// <summary>
    /// 1つ手前に戻す処理
    /// </summary>
    public void Undo_PlayerPos()
    {
        playerSavePos.callCount--;
        gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
        gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];
    }

    /// <summary>
    /// 1つやり直す処理
    /// </summary>
    public void Redo_PlayerPos()
    {
        gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
        gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];
        playerSavePos.callCount++;
    }
}
