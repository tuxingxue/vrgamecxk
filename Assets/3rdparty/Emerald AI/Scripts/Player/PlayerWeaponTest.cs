using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmeraldAI.CharacterController
{
    public class PlayerWeaponTest : MonoBehaviour
    {

        private Ray ray;
        private RaycastHit hit;
        Camera cam;
        public GameObject SpawnObject;

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                if (Physics.Raycast(ray, out hit, 8))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.GetComponent<EmeraldAISystem>() != null)
                        {
                            hit.collider.GetComponent<EmeraldAISystem>().Damage(5, EmeraldAISystem.TargetType.Player, transform.root, 200);
                        }
                        else if (hit.collider.tag == "Untagged")
                        {
                            //Instantiate(SpawnObject, transform.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}