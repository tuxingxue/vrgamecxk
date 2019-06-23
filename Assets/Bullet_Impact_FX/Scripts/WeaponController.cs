using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WeaponController : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			DoTrace(Camera.main.transform);
		}
	}

	void DoTrace(Transform fireFrom)
	{
		Vector3 direction = fireFrom.forward;

		Ray ray = new Ray(fireFrom.position, direction);

		RaycastHit hit;

		if (!Physics.Raycast (ray, out hit,1000f,~(1<<LayerMask.NameToLayer("Char"))))
			return;

        if (WPN_Decal_Manager.Instance != null )
        {
            WPN_Decal_Manager.Instance.SpawnBulletHitEffects(hit.point, hit.normal, hit.collider.material, hit.collider.gameObject);
        }
	}
}
