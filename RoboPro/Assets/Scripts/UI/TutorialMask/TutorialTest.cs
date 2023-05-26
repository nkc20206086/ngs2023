using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ScreenMask;

public class TutorialTest : MonoBehaviour
{
    [Inject]
    private IScreenMaskable con;

    [SerializeField]
    private GameObject obj;

    void Start()
    {
        con.SetMaskFadeinMax();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = obj.transform.position;

            con.PlayMaskFadeout(pos, new Vector2(100, 100), .3f);
        }

        if (Input.GetMouseButtonDown(1))
        {
            con.PlayMaskFadeinMax(1f);
        }
    }
}
