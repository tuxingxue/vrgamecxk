using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

namespace EmeraldAI
{
    public class EmeraldAINonAIDamage : MonoBehaviour
    {
        public int Health = 50;

        /// <summary>
        /// Manages Non-AI damage with an external script that can be customized as needed.
        /// </summary>
        public void SendNonAIDamage(int DamageAmount, Transform Target)
        {
            DefaultDamage(DamageAmount, Target);
        }

        void DefaultDamage(int DamageAmount, Transform Target)
        {
            Health -= DamageAmount;

            if (Health <= 0)
            {
                Debug.Log("The Non-AI Target has died.");
                gameObject.layer = 0;
                gameObject.tag = "Untagged";
                Target.GetComponent<EmeraldAISystem>().EmeraldDetectionComponent.SearchForTarget();
            }
        }
    }
}
