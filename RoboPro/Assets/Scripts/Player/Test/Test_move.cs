using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_move : MonoBehaviour
{
    Vector3 defaultScale;
    Quaternion rotation;
    Vector3 angle;
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = gameObject.transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Vector3 pos = gameObject.transform.position;
            var translate = new Vector3(0, 0.01f + transform.position.y, 0);

            angle.y += 1f;

            rotation = Quaternion.Euler(0, angle.y, 0);
            Quaternion rotationNum = Quaternion.Euler(0, angle.y, 0);
            Debug.Log(angle.y);

            //transform.rotation = rotationNum;

            var matrix_ex = Matrix4x4.TRS(translate, transform.rotation * Quaternion.AngleAxis(30, Vector3.up), Vector3.one * 5.0f);


            var matrix_rotate = Matrix4x4.TRS(Vector3.one, transform.rotation, Vector3.one);
            var resultMatrix = matrix_ex * matrix_rotate;
            transform.position = new Vector3(matrix_ex.m03, matrix_ex.m13, matrix_ex.m23);
            gameObject.transform.rotation = matrix_ex.rotation;
            gameObject.transform.localScale = matrix_ex.lossyScale;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.parent = null;
        }

        if (transform.parent == null)
        {
            transform.localScale = new Vector3(defaultScale.x * defaultScale.x, defaultScale.y * defaultScale.y, defaultScale.z * defaultScale.z);
        }
        else
        {
            transform.localScale = new Vector3(defaultScale.x / transform.parent.lossyScale.x,
            defaultScale.y / transform.parent.lossyScale.y,
            defaultScale.z / transform.parent.lossyScale.z
            );
        }
    }
}
