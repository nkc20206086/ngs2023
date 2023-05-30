using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerEffectData : MonoBehaviour
    {
        [SerializeField]
        public GameObject explosionEffect;
        [SerializeField]
        public GameObject goalCrackerEffect;

        // Start is called before the first frame update
        void Start()
        {
            //accessEffect.gameObject.SetActive(false);
            //Instantiate(explosionEffect);
            explosionEffect.gameObject.transform.position = gameObject.transform.position;
            explosionEffect.gameObject.SetActive(true);
        }
    }
}