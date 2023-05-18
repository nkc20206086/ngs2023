using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Gimmick;
using Gimmick.Interface;

public class TestPlayer_Command : MonoBehaviour
{
    [Inject]
    private IGimmickAccess gimmickAccess;
    private bool access = false;
    private GimmickDirector director;

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 5 * Time.deltaTime;

        transform.position += vec;

        int index = gimmickAccess.AcquisitionOfHitAccessPoint(transform.position);

        if (index >= 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gimmickAccess.Access(index);
            }
        }
    }
}
