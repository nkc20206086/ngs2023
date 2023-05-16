using UnityEngine;

public class PressCheckTest : MonoBehaviour
{
    [SerializeField] private PressColliderChecker checker;
    [SerializeField] private GameObject cube_1;
    [SerializeField] private GameObject cube_2;
    [SerializeField] private float speed = 1;

    private void Start()
    {
        checker.OnPress += (col_1, col_2) => Debug.Log("�ׂ��ꂽ");
        checker.OnPressX += (col_1, col_2) => Debug.Log("X���Œׂ��ꂽ");
        checker.OnPressY += (col_1, col_2) => Debug.Log("Y���Œׂ��ꂽ");
        checker.OnPressZ += (col_1, col_2) => Debug.Log("Z���Œׂ��ꂽ");
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            cube_1.transform.position += Vector3.up * Time.deltaTime * speed;
            cube_2.transform.position += Vector3.down * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cube_1.transform.position += Vector3.down * Time.deltaTime * speed;
            cube_2.transform.position += Vector3.up * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cube_1.transform.position += Vector3.left * Time.deltaTime * speed;
            cube_2.transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cube_1.transform.position += Vector3.right * Time.deltaTime * speed;
            cube_2.transform.position += Vector3.left * Time.deltaTime * speed;
        }
    }
}
