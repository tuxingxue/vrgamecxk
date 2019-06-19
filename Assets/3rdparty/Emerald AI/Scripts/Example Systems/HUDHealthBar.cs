using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EmeraldAI.Example
{
    public class HUDHealthBar : MonoBehaviour
    {
        public string AITag = "Respawn";
        public int DetectDistance = 40;

        private GameObject HUDObject;
        private Text AINameText;
        private Image AIHealthBar;
        private RaycastHit hit;

        private void Start()
        {
            HUDObject = Instantiate(Resources.Load("HUD Canvas") as GameObject, Vector3.zero, Quaternion.identity);
            AINameText = GameObject.Find("HUD - AI Name").GetComponent<Text>();
            AIHealthBar = GameObject.Find("HUD - AI Health Bar").GetComponent<Image>();
        }

        private void FixedUpdate()
        {
            //Draw a ray foward from our player at a distance according to the Detect Distance
            if (Physics.Raycast(transform.position, transform.forward, out hit, DetectDistance))
            {
                if (hit.collider.CompareTag(AITag))
                {
                    //Check to see if the object we have hit contains an Emerald AI component
                    if (hit.collider.gameObject.GetComponent<EmeraldAISystem>() != null)
                    {
                        //Get a reference to the Emerald AI object that was hit
                        EmeraldAISystem EmeraldComponent = hit.collider.gameObject.GetComponent<EmeraldAISystem>();
                        HUDObject.SetActive(true);
                        AINameText.text = EmeraldComponent.AIName;
                        AIHealthBar.fillAmount = (float)EmeraldComponent.CurrentHealth / EmeraldComponent.StartingHealth;
                    }
                }
                else
                {
                    HUDObject.SetActive(false);
                }
            }
            else
            {
                HUDObject.SetActive(false);
            }
        }
    }
}