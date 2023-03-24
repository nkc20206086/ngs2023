using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectView;

/// <summary>
/// 作ったプログラムを動作させるための仮プログラム
/// </summary>
public class TestObjectViewCamera : MonoBehaviour
{
    [SerializeField] 
    private ObjectViewCameraController controller;
    private IObjectViewCameraControllable controllable;

    [SerializeField]
    private GameObject axisObj;

    private ObjectViewObjectCopy objectCopy;
    private IObjectViewObjectCopyable objectCopyable;

    private Vector3 myPos;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    private GameObject copyObj;

    void Start()
    {
        myPos = this.gameObject.transform.position;

        objectCopy = new ObjectViewObjectCopy();
        objectCopyable = objectCopy;

        controllable = controller.GetComponent<IObjectViewCameraControllable>();

        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            copyObj = objectCopyable.MakeObjectCopy(meshFilter, meshRenderer, this.gameObject.name, transform);
            axisObj.transform.position = copyObj.transform.position;
            controllable.SetCameraPos(copyObj.transform);
        }

        if(Input.GetKey(KeyCode.W))
        {
            float angle = 3f;
            controllable.SetCameraRotate(copyObj.transform.position, angle);
        }
    }
}
