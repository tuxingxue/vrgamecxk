using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmeraldAI.Utility
{
    public class VisibilityCheck : MonoBehaviour
    {
        public EmeraldAISystem EmeraldComponent;

        void OnBecameInvisible()
        {
            EmeraldComponent.Deactivate();
        }

        void OnBecameVisible()
        {
            EmeraldComponent.Activate();
        }
    }
}
