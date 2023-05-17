using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer_Command : MonoBehaviour
{
    private bool access = false;
    private IGimmickAccess gimmickAccess;

    // Update is called once per frame
    void Update()
    {
        if (!access)
        {
            Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 5 * Time.deltaTime;

            transform.position += vec;
        }


        if (gimmickAccess != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gimmickAccess.GimmickAccess();
                access = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gimmickAccess.RemoveAccess();
                access = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && gimmickAccess == null)
        {
            gimmickAccess = other.GetComponent<IGimmickAccess>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            gimmickAccess = null;
        }
    }
}
