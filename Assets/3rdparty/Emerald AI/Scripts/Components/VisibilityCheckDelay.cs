using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmeraldAI.Utility
{
    public class VisibilityCheckDelay : MonoBehaviour
    {
        public EmeraldAISystem EmeraldComponent;
        float DeactivateSeconds;
        public enum CurrentBehavior { Passive = 1, Cautious = 2, Companion = 3, Aggresive = 4 };

        void Start()
        {
            DeactivateSeconds = EmeraldComponent.DeactivateDelay;
        }

        IEnumerator OnBecameInvisible()
        {
            //2.2 removed alert
            if (EmeraldComponent.CurrentTarget == null && !EmeraldComponent.ReturningToStartInProgress && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion)
            {
                yield return new WaitForSeconds(DeactivateSeconds);
                EmeraldComponent.Deactivate();
            }
        }

        void OnBecameVisible()
        {
            StopCoroutine("OnBecameInvisible");
            EmeraldComponent.Activate();
        }
    }
}
