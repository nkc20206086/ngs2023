using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class LadderChecker : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private float ladderRayLength;

        private CapsuleCollider capsuleCollider;

        // Start is called before the first frame update
        void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        /// <summary>
        /// “o‚é‚Æ‚«‚Ì’òŽq‚ð”»’è‚·‚é
        /// </summary>
        /// <returns></returns>
        public bool LadderClimbCheck()
        {
            RaycastHit ladderRay = new RaycastHit();
            Physics.Raycast(transform.position, transform.forward, out ladderRay, ladderRayLength, layerMask);
            //Debug.DrawRay(transform.position, transform.forward * ladderRayLength);
            if (ladderRay.collider == null || ladderRay.collider.gameObject.layer != 10) return false;
            return true;
        }

        /// <summary>
        /// ‰º‚é‚Æ‚«‚Ì’òŽq‚ð”»’è‚·‚é
        /// </summary>
        /// <returns></returns>
        public bool LadderDownCheck()
        {
            RaycastHit ladderRay = new RaycastHit();
            Vector3 ladderRayVec = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            Physics.Raycast(ladderRayVec, transform.forward, out ladderRay, ladderRayLength, layerMask);
            //Debug.DrawRay(ladderRayVec, transform.forward * ladderRayLength);
            if (ladderRay.collider == null || ladderRay.collider.gameObject.layer != 10) return false;
            return true;
        }

        public bool Complete_LadderClimbCheck()
        {
            RaycastHit ladderRay = new RaycastHit();
            Vector3 completeLadderRayVec = new Vector3(transform.position.x, transform.position.y + capsuleCollider.height -0.5f, transform.position.z);
            Physics.Raycast(completeLadderRayVec, transform.forward, out ladderRay, ladderRayLength, layerMask);
            Debug.DrawRay(completeLadderRayVec, transform.forward * ladderRayLength);
            if(ladderRay.collider == null)
            {
                return false;
            }
            else if (ladderRay.collider.gameObject.layer == 10) 
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool Complete_LadderDownCheck()
        {
            return true;
        }
    }
}