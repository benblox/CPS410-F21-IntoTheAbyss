using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareDistract : MonoBehaviour
{
	public static List<FlareDistract> flares = new List<FlareDistract>();
	float destructDelay = 5f;
	public void Start()
	{
		flares.Add(this);
		Destroy(this.gameObject, destructDelay);
	}
	public void Update()
	{
	}
	public void OnDestroy()
	{
		flares.Remove(this);
	}
	public static FlareDistract getNearestFlare(Transform caller)
	{
		FlareDistract objectToReturn = null;
		float distance = Mathf.Infinity;
		for (int i = 0; i < flares.Count; i++)
		{
			float newDistance = Vector3.Distance(flares[i].transform.position, caller.position);
			if (newDistance < distance)
			{
				objectToReturn = flares[i];
				distance = newDistance;
			}

		}
		return objectToReturn;
	}
	public static FlareDistract getNearestFlare(Transform caller, float maxDistance)
	{
		FlareDistract objectToReturn = null;
		float distance = maxDistance;
		for (int i = 0; i < flares.Count; i++)
		{
			float newDistance = Vector3.Distance(flares[i].transform.position, caller.position);
			if (newDistance < distance)
			{
				objectToReturn = flares[i];
				distance = newDistance;
			}

		}
		return objectToReturn;
	}
}

