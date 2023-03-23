using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectViewCamera : MonoBehaviour
{
    [SerializeField] 
    private ObjectView.ObjectViewCameraController controller;

    private ObjectView.ObjectViewObjectCopy objectCopy;

    private Vector3 myPos;

    void Start()
    {
        myPos = this.gameObject.transform.position;
        objectCopy = new ObjectView.ObjectViewObjectCopy();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            controller.SetCameraPos(this.gameObject.transform);
        }

        if(Input.GetKey(KeyCode.W))
        {
            float angle = 3f;
            controller.SetCameraRotate(myPos, angle);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            MeshRenderer meshRenderer= GetComponent<MeshRenderer>();
            objectCopy.ObjectCopy(meshFilter, meshRenderer, this.gameObject.name);
        }
    }
}
