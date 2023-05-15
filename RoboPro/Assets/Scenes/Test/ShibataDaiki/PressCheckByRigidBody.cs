using System;
using System.Collections.Generic;
using UnityEngine;

public class PressCheckByRigidBody : MonoBehaviour
{
	private Dictionary<Collider, Vector3> touchingObjects = new Dictionary<Collider, Vector3>();
	private BoxCollider myCollider;

	private void Start()
	{
		myCollider = GetComponent<BoxCollider>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		touchingObjects.Add(collision.collider, Array.Find(collision.contacts, x => x.thisCollider.gameObject == gameObject).point);
	}

	private void OnCollisionExit(Collision collision)
	{
		touchingObjects.Remove(collision.collider);
	}

	private void Update()
	{
		if (touchingObjects.Count >= 2)
		{
			//自分との接触に関するContactPointを全てリストへ格納
			List<Vector3> contactPoints = new List<Vector3>();
			foreach (KeyValuePair<Collider, Vector3> col in touchingObjects)
			{
				contactPoints.Add(col.Value);
			}

			Vector3 myPos = transform.position + myCollider.center;
			//ContactPoint間の関係を全て調べ、押しつぶされているかを確認する
			for (int i = 0; i < contactPoints.Count;i++)
			{
				for (int j = i + 1; j < contactPoints.Count; j++)
				{
					//X軸で押しつぶされているか
					if (contactPoints[i].x > myPos.x + myCollider.size.x / 2 && contactPoints[j].x <= myPos.x - myCollider.size.x / 2)
					{
						Debug.Log("X");
					}
					else
					if (contactPoints[j].x > myPos.x + myCollider.size.x / 2 && contactPoints[i].x <= myPos.x - myCollider.size.x / 2)
					{
						Debug.Log("X");
					}

					//Y軸で押しつぶされているか
					if (contactPoints[i].y > myPos.y + myCollider.size.y / 2 && contactPoints[j].y <= myPos.y - myCollider.size.y / 2)
					{
						Debug.Log("Y");
					}
					else
					if (contactPoints[j].y > myPos.y + myCollider.size.y / 2 && contactPoints[i].y <= myPos.y - myCollider.size.y / 2)
					{
						Debug.Log("Y");
					}

					//Z軸で押しつぶされているか
					if (contactPoints[i].z > myPos.z + myCollider.size.z / 2 && contactPoints[j].z <= myPos.z - myCollider.size.z / 2)
					{
						Debug.Log("Z");
					}
					else
					if (contactPoints[j].z > myPos.z + myCollider.size.z / 2 && contactPoints[i].z <= myPos.z - myCollider.size.z / 2)
					{
						Debug.Log("Z");
					}
				}
			}
		}
	}
}
