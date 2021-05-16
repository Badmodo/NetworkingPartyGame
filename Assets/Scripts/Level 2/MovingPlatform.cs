using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public Transform TargetPoint;
	public Transform origPoint;
	float distance;
	bool reached = false;
	public float Speed = 0.01f;

	public void Start()
	{
		
	}

	public void FixedUpdate()
	{
		if (!reached)
		{
			distance = Vector3.Distance(transform.position, TargetPoint.transform.position);
			if (distance > .1)
			{
				move(transform.position, TargetPoint.transform.position);
			}
			else
			{
				reached = true;
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void move(Vector3 pos, Vector3 towards)
	{
		transform.position = Vector3.MoveTowards(pos, towards, Speed * Time.deltaTime * 150f);
	}
}
