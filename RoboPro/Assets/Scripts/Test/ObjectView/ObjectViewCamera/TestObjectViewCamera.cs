using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectViewCamera : MonoBehaviour
{
    [SerializeField] 
    private ObjectView.ObjectViewCameraController controller;

    private ObjectView.ObjectViewObjectCopy objectCopy;

    private Vector3 myPos;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    private GameObject copyObj;

    void Start()
    {
        myPos = this.gameObject.transform.position;
        objectCopy = new ObjectView.ObjectViewObjectCopy();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            copyObj = objectCopy.MakeObjectCopy(meshFilter, meshRenderer, this.gameObject.name, transform);
            controller.SetCameraPos(copyObj.transform);
        }

        if(Input.GetKey(KeyCode.W))
        {
            float angle = 3f;
            controller.SetCameraRotate(copyObj.transform.position, angle);
        }
    }
}
