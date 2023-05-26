using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading;
using System.Threading;
using UniRx;

namespace HowToOperateUI
{
    public class HowToOperateUIController : MonoBehaviour
    {
        //[SerializeField]
        //private GameObject uiObj;

        //private float timer = 0f;
        //private float interval = 2f;

        //private void Update()
        //{
        //    if(Input.anyKey)
        //    {
        //        timer = 0f;
        //        uiObj.SetActive(false);
        //    }
        //    else
        //    {
        //        if(timer < interval)
        //        {
        //            timer += Time.deltaTime;
        //            if(timer >= interval)
        //            {
        //                uiObj.SetActive(true);
        //            }
        //        }
        //    }
        //}

        [SerializeField]
        private GameObject uiObj;

        private bool isWaiting = false;

        private CancellationTokenSource cts;

        private async UniTaskVoid Start()
        {
            cts = new CancellationTokenSource();

            while (true)
            {
                if(Input.anyKey)
                {
                    isWaiting = false;
                    uiObj.SetActive(false);
                    cts.Cancel();
                }
                else if(!isWaiting)
                {
                    isWaiting = true;
                    cts = new CancellationTokenSource();
                    await UniTask.Delay(System.TimeSpan.FromSeconds(2), cancellationToken : cts.Token);
                    if(!cts.Token.IsCancellationRequested && !Input.anyKey)
                    {
                        uiObj.SetActive(true);
                    }
                }
                await UniTask.Yield();
            }
        }
        
    }
}
