using UnityEngine;

namespace HowToOperateUI
{
    public class HowToOperateUIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject uiObj;

        private float timer = 0f;
        private float interval = 2f;

        private void Update()
        {
            if(Input.anyKey)
            {
                timer = 0f;
                uiObj.SetActive(false);
            }
            else
            {
                if(timer < interval)
                {
                    timer += Time.deltaTime;
                    if(timer >= interval)
                    {
                        uiObj.SetActive(true);
                    }
                }
            }
        }
    }
}
