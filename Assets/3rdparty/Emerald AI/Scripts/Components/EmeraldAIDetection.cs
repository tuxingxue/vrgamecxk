using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmeraldAI.Utility
{
    public class EmeraldAIDetection : MonoBehaviour
    {
        [HideInInspector]
        public Transform PreviousTarget;
        [HideInInspector]
        public bool SearchForRandomTarget = false;
        EmeraldAISystem EmeraldComponent;
        bool AvoidanceTrigger;
        float AvoidanceTimer;
        float AvoidanceSeconds = 1.25f;
        Vector3 TargetDirection;
        GameObject CurrentObstruction;
        Color C = Color.white;

        void Start()
        {
            EmeraldComponent = GetComponent<EmeraldAISystem>();
        }

        void FixedUpdate()
        {
            if (EmeraldComponent.UseAIAvoidance == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.m_NavMeshAgent.enabled && EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
            {
                AIAvoidance();
            }

            if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight && EmeraldComponent.OptimizedStateRef == EmeraldAISystem.OptimizedState.Inactive && EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
            {
                LineOfSightDetection();
            }

            if (EmeraldComponent.UseHeadLookRef == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.CurrentTarget != null)
            {
                if (EmeraldComponent.AIAnimator.layerCount == 2)
                {
                    EmeraldComponent.m_LayerCurrentState = EmeraldComponent.AIAnimator.GetCurrentAnimatorStateInfo(1);
                }

                float CurrentDistance = Vector3.Distance(EmeraldComponent.CurrentTarget.position, transform.position);
                float angle = Vector3.Angle(new Vector3(EmeraldComponent.CurrentTarget.position.x, 0, EmeraldComponent.CurrentTarget.position.z) - new Vector3(transform.position.x, 0, transform.position.z), transform.forward);
                float AdjustedAngle = Mathf.Abs(angle);

                if (CurrentDistance <= EmeraldComponent.MaxLookAtDistance && AdjustedAngle <= EmeraldComponent.LookAtLimit)
                {
                    EmeraldComponent.lookWeight = Mathf.Lerp(EmeraldComponent.lookWeight, 1f, Time.deltaTime * EmeraldComponent.LookSmoother);
                }
                else
                {
                    EmeraldComponent.lookWeight = Mathf.Lerp(EmeraldComponent.lookWeight, 0, Time.deltaTime * EmeraldComponent.LookSmoother);
                }
            }
            else if (EmeraldComponent.UseHeadLookRef == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.HeadLookRef != null)
            {
                if (EmeraldComponent.AIAnimator.layerCount == 2)
                {
                    EmeraldComponent.m_LayerCurrentState = EmeraldComponent.AIAnimator.GetCurrentAnimatorStateInfo(1);
                }

                float CurrentDistance = Vector3.Distance(EmeraldComponent.HeadLookRef.position, transform.position);
                float angle = Vector3.Angle(new Vector3(EmeraldComponent.HeadLookRef.position.x, 0, EmeraldComponent.HeadLookRef.position.z) - new Vector3(transform.position.x, 0, transform.position.z), transform.forward);
                float AdjustedAngle = Mathf.Abs(angle);

                if (CurrentDistance <= EmeraldComponent.MaxLookAtDistance && AdjustedAngle <= EmeraldComponent.LookAtLimit)
                {
                    EmeraldComponent.lookWeight = Mathf.Lerp(EmeraldComponent.lookWeight, 1f, Time.deltaTime * EmeraldComponent.LookSmoother);
                }
                else
                {
                    EmeraldComponent.lookWeight = Mathf.Lerp(EmeraldComponent.lookWeight, 0, Time.deltaTime * EmeraldComponent.LookSmoother);
                }
            }
            else
            {
                if (EmeraldComponent.AIAnimator.layerCount == 2)
                {
                    EmeraldComponent.m_LayerCurrentState = EmeraldComponent.AIAnimator.GetCurrentAnimatorStateInfo(1);
                }

                EmeraldComponent.lookWeight = Mathf.Lerp(EmeraldComponent.lookWeight, 0, Time.deltaTime * EmeraldComponent.LookSmoother);
            }
        }

        private void OnAnimatorIK()
        {
            if (EmeraldComponent.UseHeadLookRef == EmeraldAISystem.YesOrNo.Yes)
            {
                if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                {
                    EmeraldComponent.AIAnimator.SetLookAtWeight(EmeraldComponent.lookWeight * EmeraldComponent.HeadLookWeightNonCombat, EmeraldComponent.lookWeight * EmeraldComponent.BodyLookWeightNonCombat);
                }
                else if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active)
                {
                    EmeraldComponent.AIAnimator.SetLookAtWeight(EmeraldComponent.lookWeight * EmeraldComponent.HeadLookWeightCombat, EmeraldComponent.lookWeight * EmeraldComponent.BodyLookWeightCombat);
                }

                //AI
                if (EmeraldComponent.TargetTypeRef == EmeraldAISystem.TargetType.AI)
                {
                    if (EmeraldComponent.CurrentTarget != null && PreviousTarget != null && EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active)
                    {
                        Vector3 CurrentTargetPos = new Vector3(EmeraldComponent.CurrentTarget.position.x, EmeraldComponent.CurrentTarget.position.y + EmeraldComponent.TargetEmerald.ProjectileCollisionPointY, EmeraldComponent.CurrentTarget.position.z);
                        Vector3 PreviousTargetPos = new Vector3(PreviousTarget.position.x, PreviousTarget.position.y + PreviousTarget.localScale.y * 2f, PreviousTarget.position.z);
                        EmeraldComponent.AIAnimator.SetLookAtPosition(Vector3.Lerp(PreviousTargetPos, CurrentTargetPos, EmeraldComponent.lookWeight));

                        if (EmeraldComponent.EnableDebugging == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.DrawRaycastsEnabled == EmeraldAISystem.YesOrNo.Yes)
                        {
                            Debug.DrawRay(new Vector3(EmeraldComponent.HeadTransform.position.x, EmeraldComponent.HeadTransform.position.y, EmeraldComponent.HeadTransform.position.z), CurrentTargetPos - EmeraldComponent.HeadTransform.position, Color.yellow);
                        }
                    }
                    else if (EmeraldComponent.HeadLookRef != null)
                    {
                        EmeraldComponent.AIAnimator.SetLookAtPosition(new Vector3(EmeraldComponent.HeadLookRef.position.x, EmeraldComponent.HeadLookRef.position.y +
                            EmeraldComponent.HeadLookRef.localScale.y / 2 + (EmeraldComponent.HeadLookYOffset), EmeraldComponent.HeadLookRef.position.z));
                    }
                }
                //Player
                else
                {
                    if (EmeraldComponent.CurrentTarget != null && PreviousTarget != null && EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active)
                    {
                        Vector3 CurrentTargetPos = new Vector3(EmeraldComponent.CurrentTarget.position.x, EmeraldComponent.CurrentTarget.position.y +
                            EmeraldComponent.CurrentTarget.localScale.y / 2 + (EmeraldComponent.HeadLookYOffset), EmeraldComponent.CurrentTarget.position.z);
                        Vector3 PreviousTargetPos = new Vector3(PreviousTarget.position.x, PreviousTarget.position.y +
                            PreviousTarget.localScale.y / 2 + (EmeraldComponent.HeadLookYOffset), PreviousTarget.position.z);
                        EmeraldComponent.AIAnimator.SetLookAtPosition(Vector3.Lerp(PreviousTargetPos, CurrentTargetPos, EmeraldComponent.lookWeight));

                        if (EmeraldComponent.EnableDebugging == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.DrawRaycastsEnabled == EmeraldAISystem.YesOrNo.Yes)
                        {
                            Debug.DrawRay(new Vector3(EmeraldComponent.HeadTransform.position.x, EmeraldComponent.HeadTransform.position.y, EmeraldComponent.HeadTransform.position.z), CurrentTargetPos - EmeraldComponent.HeadTransform.position, Color.yellow);
                        }
                    }
                    else if (EmeraldComponent.HeadLookRef != null)
                    {
                        EmeraldComponent.AIAnimator.SetLookAtPosition(new Vector3(EmeraldComponent.HeadLookRef.position.x, EmeraldComponent.HeadLookRef.position.y +
                            EmeraldComponent.HeadLookRef.localScale.y / 2 + (EmeraldComponent.HeadLookYOffset), EmeraldComponent.HeadLookRef.position.z));
                    }
                }
            }
        }

        public void DetectTarget(GameObject C)
        {
            if (EmeraldComponent.UseHeadLookRef == EmeraldAISystem.YesOrNo.Yes && C.CompareTag(EmeraldComponent.PlayerTag) && EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
            {
                EmeraldComponent.HeadLookRef = C.transform;
            }

            if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.Trigger)
            {
                if (C.CompareTag(EmeraldComponent.PlayerTag))
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.Player;
                    EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Alert;

                    if (EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion
                        && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Passive && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        EmeraldComponent.EmeraldBehaviorsComponent.ActivateCombatState();

                        //Pick our target depending on the AI's options
                        if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.FirstDetected)
                        {
                            if (PreviousTarget == null)
                            {
                                PreviousTarget = C.transform;
                            }

                            EmeraldComponent.CurrentTarget = C.transform;
                        }
                        else if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.Closest)
                        {
                            SearchForTarget();
                        }
                    }
                    else if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion && !C.CompareTag(EmeraldComponent.PlayerTag)) //2.2 May need some adjusting
                    {
                        //2.2 should make this less repetitive
                        if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Defensive)
                        {
                            EmeraldComponent.CompanionTargetRef = C.transform;
                        }
                        else if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Offensive)
                        {
                            EmeraldComponent.CompanionTargetRef = C.transform;
                        }
                    }
                    else if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Passive)
                    {
                        EmeraldComponent.PassiveTargetRef = C.transform;
                    }
                }
                else if (C.CompareTag(EmeraldComponent.EmeraldTag))
                {
                    if (C.GetComponent<EmeraldAISystem>() != null)
                    {
                        EmeraldComponent.ReceivedFaction = C.GetComponent<EmeraldAISystem>().CurrentFaction;
                    }
                    if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction) && EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldComponent.ReceivedFaction)] == 0)
                    {
                        if (C.GetComponent<EmeraldAISystem>() != null)
                        {
                            EmeraldComponent.TargetEmerald = C.GetComponent<EmeraldAISystem>();
                            EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.AI;
                        }
                        EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Alert;

                        if (EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Passive && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                        {
                            EmeraldComponent.CombatStateRef = EmeraldAISystem.CombatState.Active;
                            EmeraldComponent.AIAnimator.SetBool("Idle Active", false);
                            EmeraldComponent.AIAnimator.SetBool("Combat State Active", true);

                            EmeraldComponent.CurrentMovementState = EmeraldAISystem.MovementState.Run;

                            //Pick our target depending on the AI's options
                            if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.FirstDetected)
                            {
                                if (PreviousTarget == null)
                                {
                                    PreviousTarget = C.transform;
                                }

                                EmeraldComponent.CurrentTarget = C.transform;
                            }
                            else if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.Closest)
                            {
                                SearchForTarget();
                            }
                        }
                        else if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                        {
                            //2.2 should make this less repetitive
                            if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Defensive)
                            {
                                EmeraldComponent.CompanionTargetRef = C.transform;
                            }
                            else if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Offensive)
                            {
                                EmeraldComponent.CompanionTargetRef = C.transform;
                            }
                        }
                        else if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Passive)
                        {
                            EmeraldComponent.PassiveTargetRef = C.transform;
                        }
                    }
                }
                else if (C.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                {
                    if (C.GetComponent<EmeraldAISystem>() == null)
                    {
                        EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.NonAITarget;
                    }
                    EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Alert;

                    if (EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Passive && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        EmeraldComponent.CombatStateRef = EmeraldAISystem.CombatState.Active;
                        EmeraldComponent.AIAnimator.SetBool("Idle Active", false);
                        EmeraldComponent.AIAnimator.SetBool("Combat State Active", true);

                        //Pick our target depending on the AI's options
                        if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.FirstDetected)
                        {
                            if (PreviousTarget == null)
                            {
                                PreviousTarget = C.transform;
                            }

                            EmeraldComponent.CurrentTarget = C.transform;
                        }
                        else if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.Closest)
                        {
                            SearchForTarget();
                        }
                    }
                    else if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Defensive)
                        {
                            EmeraldComponent.CompanionTargetRef = C.transform;
                        }
                        else if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Offensive)
                        {
                            EmeraldComponent.CompanionTargetRef = C.transform;
                        }
                    }
                    else if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Passive)
                    {
                        EmeraldComponent.PassiveTargetRef = C.transform;
                    }
                }
            }
            else if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight)
            {
                if (C.CompareTag(EmeraldComponent.PlayerTag) && !EmeraldComponent.LineOfSightTargets.Contains(C.transform) && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always)
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.Player;
                    EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Alert;
                    EmeraldComponent.LineOfSightTargets.Add(C.transform);
                }
                else if (C.CompareTag(EmeraldComponent.EmeraldTag))
                {
                    if (C.GetComponent<EmeraldAISystem>() != null)
                    {
                        EmeraldComponent.ReceivedFaction = C.GetComponent<EmeraldAISystem>().CurrentFaction;
                    }
                    if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction) && EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldComponent.ReceivedFaction)] == 0 && !EmeraldComponent.LineOfSightTargets.Contains(C.transform))
                    {
                        if (C.GetComponent<EmeraldAISystem>() != null)
                        {
                            EmeraldComponent.TargetEmerald = C.GetComponent<EmeraldAISystem>();
                            EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.AI;
                        }
                        EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Alert;
                        EmeraldComponent.LineOfSightTargets.Add(C.transform);
                    }
                }
                else if (C.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes && !EmeraldComponent.LineOfSightTargets.Contains(C.transform))
                {
                    if (C.GetComponent<EmeraldAISystem>() == null)
                    {
                        EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.NonAITarget;
                    }
                    EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Alert;
                    EmeraldComponent.LineOfSightTargets.Add(C.transform);
                }
            }
        }

        void AIAvoidance()
        {
            RaycastHit HitForward;            
            if (EmeraldComponent.IsMoving && Physics.Raycast(EmeraldComponent.HeadTransform.position, 
                transform.forward, out HitForward, (EmeraldComponent.StoppingDistance*2+1), EmeraldComponent.AIAvoidanceLayerMask) && !AvoidanceTrigger)
            {
                if (HitForward.transform != transform)
                {
                    EmeraldComponent.TargetDestination = transform.position + HitForward.transform.right / Random.Range(-5,6) * (EmeraldComponent.StoppingDistance*2+1);
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.TargetDestination;
                    AvoidanceTrigger = true;
                    AvoidanceTimer = 0;
                }
            }
            else if (AvoidanceTrigger)
            {
                if (EmeraldComponent.CurrentMovementState == EmeraldAISystem.MovementState.Walk)
                {
                    AvoidanceSeconds = 1;
                }
                else
                {
                    AvoidanceSeconds = 0.75f;
                }

                AvoidanceTimer += Time.deltaTime;
                if (AvoidanceTimer > AvoidanceSeconds && EmeraldComponent.CurrentTarget == null)
                {
                    if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Dynamic)
                    {
                        EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.NewDestination;
                    }
                    else if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Destination)
                    {
                        EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.SingleDestination;
                    }
                    else if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Stationary)
                    {
                        EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.StartingDestination;
                    }
                    if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Waypoints)
                    {
                        EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.WaypointsList[EmeraldComponent.WaypointIndex];
                    }

                    AvoidanceTimer = 0;
                    AvoidanceTrigger = false;
                }
            }
        }

        //This is responsible for all of our detection mechanics. This OnTriggerEnter function will only set
        //targets whose tag exists in the AI's target tags.
        void OnTriggerEnter(Collider C)
        {
            if (EmeraldComponent.CurrentTarget == null)
            {
                if (C.CompareTag(EmeraldComponent.EmeraldTag) || C.CompareTag(EmeraldComponent.PlayerTag) && EmeraldComponent.AIAttacksPlayerRef != EmeraldAISystem.AIAttacksPlayer.Never || C.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                {
                    if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.Trigger)
                    {
                        DetectTarget(C.gameObject);
                    }
                    else if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight && EmeraldComponent.CurrentTarget == null)
                    {
                        DetectTarget(C.gameObject);
                    }
                }
            }
            else if (C.CompareTag(EmeraldComponent.EmeraldTag) && EmeraldComponent.DeathDelayActive && EmeraldComponent.CurrentTarget != null)
            {
                if (EmeraldComponent.TargetEmerald != null)
                {
                    if (EmeraldComponent.TargetEmerald.CurrentHealth <= 0)
                    {
                        EmeraldComponent.DeathDelayActive = false;
                        SearchForTarget();
                    }
                }
            }

            if (C.CompareTag(EmeraldComponent.FollowTag) && C.tag != "Untagged" && EmeraldComponent.CurrentFollowTarget == null)
            {
                if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion || EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Pet)
                {
                    EmeraldComponent.CurrentFollowTarget = C.transform;
                    EmeraldComponent.CurrentMovementState = EmeraldAISystem.MovementState.Run;
                    EmeraldComponent.StartingMovementState = EmeraldAISystem.MovementState.Run;

                    if (EmeraldComponent.UseCombatTextRef == EmeraldAISystem.UseCombatText.Yes && EmeraldComponent.CombatTextObject != null)
                    {
                        EmeraldComponent.CombatTextParent.SetActive(true);
                    }
                }
            }

            if (C.CompareTag(EmeraldComponent.UITag))
            {
                if (EmeraldComponent.HealthBarCanvas != null && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                {
                    EmeraldComponent.SetUI(true);
                }

                if (EmeraldComponent.UseCombatTextRef == EmeraldAISystem.UseCombatText.Yes && EmeraldComponent.CombatTextObject != null && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                {
                    EmeraldComponent.CombatTextParent.SetActive(true);
                }
            }

            if (EmeraldComponent.UseHeadLookRef == EmeraldAISystem.YesOrNo.Yes && C.CompareTag(EmeraldComponent.PlayerTag) && EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
            {
                EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.Player;
                EmeraldComponent.HeadLookRef = C.transform;
            }
        }

        void OnTriggerExit(Collider C)
        {
            if (EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet && C.GetComponent<EmeraldAISystem>() != null)
            {
                if (C.CompareTag(EmeraldComponent.EmeraldTag))
                {
                    EmeraldComponent.ReceivedFaction = C.GetComponent<EmeraldAISystem>().CurrentFaction;
                }

                if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction) && EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldComponent.ReceivedFaction)] == 0
                    || C.CompareTag(EmeraldComponent.PlayerTag) && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always
                    || C.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                {
                    if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight && EmeraldComponent.CurrentTarget == null && EmeraldComponent.LineOfSightTargets.Contains(C.transform))
                    {
                        if (EmeraldComponent.LineOfSightTargets.Count <= 1)
                        {
                            EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Unaware;
                            EmeraldComponent.CombatStateRef = EmeraldAISystem.CombatState.NotActive;
                            EmeraldComponent.AIAnimator.SetBool("Combat State Active", false);
                            EmeraldComponent.FirstTimeInCombat = true;
                        }

                        EmeraldComponent.LineOfSightTargets.Remove(C.transform);
                        EmeraldComponent.TargetEmerald = null;
                    }
                    EmeraldComponent.fieldOfViewAngle = EmeraldComponent.fieldOfViewAngleRef;
                }

                if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.Trigger && EmeraldComponent.DistanceFromTarget >= EmeraldComponent.DetectionRadius && EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                {
                    EmeraldComponent.WarningAnimationTriggered = false;
                    EmeraldComponent.FirstTimeInCombat = true;
                    EmeraldComponent.ReturningToStartInProgress = true;
                    EmeraldComponent.CautiousTimer = 0;

                    if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                    {
                        EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Unaware;
                        EmeraldComponent.CompanionTargetRef = null;
                        EmeraldComponent.TargetEmerald = null;
                        EmeraldComponent.CurrentTarget = null;
                    }
                }

                if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious && EmeraldComponent.ConfidenceRef != EmeraldAISystem.ConfidenceType.Coward)
                {
                    if (EmeraldComponent.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight && EmeraldComponent.CurrentTarget != null && EmeraldComponent.LineOfSightTargets.Contains(C.transform))
                    {
                        EmeraldComponent.LineOfSightTargets.Remove(C.transform);
                        EmeraldComponent.TargetEmerald = null;
                    }

                    EmeraldComponent.EmeraldBehaviorsComponent.DefaultState();
                }
            }

            if (C.CompareTag(EmeraldComponent.UITag))
            {
                if (EmeraldComponent.HealthBarCanvas != null && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                {
                    EmeraldComponent.SetUI(false);
                }

                if (EmeraldComponent.UseCombatTextRef == EmeraldAISystem.UseCombatText.Yes && EmeraldComponent.CombatTextObject != null && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                {
                    EmeraldComponent.CombatTextParent.SetActive(false);
                }
            }

            if (EmeraldComponent.TargetTypeRef == EmeraldAISystem.TargetType.Player && C.CompareTag(EmeraldComponent.PlayerTag))
            {
                EmeraldComponent.HeadLookRef = null;

                if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious && EmeraldComponent.ConfidenceRef != EmeraldAISystem.ConfidenceType.Coward)
                {
                    EmeraldComponent.EmeraldBehaviorsComponent.DefaultState();
                }
            }
        }

        //Calculates our AI's line of sight mechanics.
        //For each target that is within the AI's trigger radius, and they are within the AI's
        //line of sight, set the first seen target as the CurrentTarget.
        public void LineOfSightDetection()
        {
            if (EmeraldComponent.CurrentDetectionRef == EmeraldAISystem.CurrentDetection.Alert && EmeraldComponent.CurrentTarget == null && EmeraldComponent.CurrentHealth > 0)
            {
                foreach (Transform T in EmeraldComponent.LineOfSightTargets.ToArray())
                {
                    Vector3 direction = (new Vector3(T.position.x, T.position.y + T.localScale.y / 2, T.position.z)) - EmeraldComponent.HeadTransform.position;
                    float angle = Vector3.Angle(new Vector3(direction.x, 0, direction.z), transform.forward);

                    if (EmeraldComponent.EnableDebugging == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.DrawRaycastsEnabled == EmeraldAISystem.YesOrNo.Yes)
                    {
                        Debug.DrawRay(EmeraldComponent.HeadTransform.position, direction, new Color(1, 0.549f, 0));
                    }

                    if (angle < EmeraldComponent.fieldOfViewAngle * 0.5f)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(EmeraldComponent.HeadTransform.position, direction, out hit, EmeraldComponent.AICollider.radius))
                        {
                            if (hit.collider.CompareTag(EmeraldComponent.EmeraldTag) || hit.collider.CompareTag(EmeraldComponent.PlayerTag) && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always || 
                                hit.collider.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                            {
                                if (hit.collider.CompareTag(EmeraldComponent.EmeraldTag))
                                {
                                    EmeraldComponent.ReceivedFaction = hit.collider.GetComponent<EmeraldAISystem>().CurrentFaction;
                                }
                                if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction) && EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldComponent.ReceivedFaction)] == 0 || hit.collider.CompareTag(EmeraldComponent.PlayerTag)
                                    && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always || hit.collider.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                                {
                                    if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction))
                                    {
                                        EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.AI;
                                        EmeraldComponent.TargetEmerald = hit.collider.GetComponent<EmeraldAISystem>();
                                    }
                                    else if (hit.collider.CompareTag(EmeraldComponent.PlayerTag) && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always)
                                    {
                                        EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.Player;
                                    }
                                    else if (hit.collider.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                                    {
                                        EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.NonAITarget;
                                    }

                                    SetLineOfSightTarget(hit.collider.transform);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SetLineOfSightTarget(Transform LineOfSightTarget)
        {
            EmeraldComponent.LineOfSightRef = LineOfSightTarget;
            //Pick our target depending on the AI's options
            if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.FirstDetected || EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious)
            {
                EmeraldComponent.CurrentTarget = EmeraldComponent.LineOfSightRef;
            }
            else if (EmeraldComponent.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.Closest)
            {
                SearchForTarget();
            }

            EmeraldComponent.EmeraldBehaviorsComponent.ActivateCombatState();

            if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
            {
                EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.StoppingDistance;
            }
        }

        public void CheckForObstructions()
        {
            if (EmeraldComponent.CurrentTarget != null && EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
            {
                if (EmeraldComponent.TargetTypeRef == EmeraldAISystem.TargetType.AI)
                {
                    TargetDirection = EmeraldComponent.TargetEmerald.m_ProjectileCollisionPoint - EmeraldComponent.HeadTransform.position;
                }
                else
                {
                    TargetDirection = (new Vector3(EmeraldComponent.CurrentTarget.position.x, EmeraldComponent.CurrentTarget.position.y + EmeraldComponent.CurrentTarget.localScale.y / 2 + (EmeraldComponent.PlayerYOffset), EmeraldComponent.CurrentTarget.position.z)) - EmeraldComponent.HeadTransform.position;
                }

                float angle = Vector3.Angle(new Vector3(TargetDirection.x, 0, TargetDirection.z), new Vector3(transform.forward.x, 0, transform.forward.z));
                RaycastHit hit;

                if (EmeraldComponent.EnableDebugging == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.DrawRaycastsEnabled == EmeraldAISystem.YesOrNo.Yes)
                {
                    Debug.DrawRay(new Vector3(EmeraldComponent.HeadTransform.position.x, EmeraldComponent.HeadTransform.position.y, EmeraldComponent.HeadTransform.position.z), TargetDirection, C);
                }

                //Check for obstructions and incrementally lower our AI's stopping distance until one is found. If none are found when the distance has reached 5 or below, search for a new target to see if there is a better option
                if (Physics.Raycast(EmeraldComponent.HeadTransform.position, (TargetDirection), out hit, EmeraldComponent.DistanceFromTarget + 2, ~EmeraldComponent.ObstructionDetectionLayerMask))
                {
                    if (EmeraldComponent.CurrentTarget != null && angle > 45 && EmeraldComponent.m_NavMeshAgent.stoppingDistance > 5 && hit.collider.gameObject != this.gameObject && hit.collider.gameObject != EmeraldComponent.CurrentTarget.gameObject
                        || EmeraldComponent.CurrentTarget != null && hit.collider.gameObject != EmeraldComponent.CurrentTarget.gameObject && hit.collider.gameObject != this.gameObject && EmeraldComponent.m_NavMeshAgent.stoppingDistance > 5)
                    {
                        C = Color.red;
                        EmeraldComponent.TargetObstructed = true;

                        if (EmeraldComponent.EnableDebugging == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.DebugLogObstructionsEnabled == EmeraldAISystem.YesOrNo.Yes && !EmeraldComponent.DeathDelayActive)
                        {
                            if (hit.collider.gameObject != CurrentObstruction)
                            {
                                Debug.Log("<b>" + "<color=green>" + gameObject.name + "'s Current Obstruction: " + "</color>" + "<color=red>" + hit.collider.gameObject.name + "</color>" + "</b>");
                                CurrentObstruction = hit.collider.gameObject;
                            }
                        }

                        if (EmeraldComponent.m_NavMeshAgent.stoppingDistance > EmeraldComponent.StoppingDistance && !EmeraldComponent.BackingUp && !EmeraldComponent.IsTurning && EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                        {
                            if (!EmeraldComponent.Attacking && hit.collider.tag != EmeraldComponent.EmeraldTag && hit.collider.tag != EmeraldComponent.PlayerTag)
                            {
                                if (EmeraldComponent.TargetObstructedActionRef == EmeraldAISystem.TargetObstructedAction.MoveCloserImmediately)
                                {
                                    EmeraldComponent.m_NavMeshAgent.stoppingDistance -= 5;
                                }
                            }
                        }
                    }

                    if (EmeraldComponent.CurrentTarget != null && hit.collider.gameObject == EmeraldComponent.CurrentTarget.gameObject && !EmeraldComponent.IsTurning)
                    {
                        C = Color.green;
                        EmeraldComponent.TargetObstructed = false;
                        EmeraldComponent.CurrentMovementState = EmeraldAISystem.MovementState.Run;
                        EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.AttackDistance;
                        EmeraldComponent.ObstructionTimer = 0;
                    }
                }
                else
                {
                    C = Color.red;
                    EmeraldComponent.TargetObstructed = true;
                }
            }
        }

        //Find colliders within range using a Physics.OverlapSphere. Mask the Physics.OverlapSphere to the user set layers. 
        //This will allow the Physics.OverlapSphere to only get relevent colliders.
        //Once found, use Emerald's custom tag system to find matches for potential targets. Once found, apply them to a list for potential targets.
        //Finally, search through each target in the list and set the nearest one as our current target.
        public void SearchForTarget()
        {
            Collider[] Col = Physics.OverlapSphere(transform.position, EmeraldComponent.DetectionRadius, EmeraldComponent.DetectionLayerMask);
            EmeraldComponent.CollidersInArea = Col;

            foreach (Collider C in EmeraldComponent.CollidersInArea)
            {
                if (C.gameObject != this.gameObject && !EmeraldComponent.potentialTargets.Contains(C.gameObject))
                {
                    if (C.gameObject.GetComponent<EmeraldAISystem>() != null)
                    {
                        EmeraldAISystem EmeraldRef = C.gameObject.GetComponent<EmeraldAISystem>();
                        if (EmeraldComponent.AIFactionsList.Contains(EmeraldRef.CurrentFaction) && 
                            EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldRef.CurrentFaction)] == 0 && 
                            EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion)
                        {
                            EmeraldComponent.potentialTargets.Add(C.gameObject);
                        }
                        else if (EmeraldComponent.AIFactionsList.Contains(EmeraldRef.CurrentFaction) && 
                            EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldRef.CurrentFaction)] == 0 && 
                            EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion && EmeraldRef.CombatStateRef == EmeraldAISystem.CombatState.Active)
                        {
                            EmeraldComponent.potentialTargets.Add(C.gameObject);
                        }
                        else if (EmeraldComponent.AIFactionsList.Contains(EmeraldRef.CurrentFaction) && 
                            EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldRef.CurrentFaction)] == 0 && 
                            EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion && EmeraldRef.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                        {
                            EmeraldComponent.CompanionTargetRef = C.transform;
                            EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.AI;
                            EmeraldComponent.TargetEmerald = C.gameObject.GetComponent<EmeraldAISystem>();
                        }
                    }
                    else if (C.gameObject.CompareTag(EmeraldComponent.PlayerTag) && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always)
                    {
                        if (EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion || EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion && C.gameObject.transform != EmeraldComponent.CurrentFollowTarget)
                        {
                            EmeraldComponent.potentialTargets.Add(C.gameObject);
                        }
                    }
                    else if (C.gameObject.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                    {
                        EmeraldComponent.potentialTargets.Add(C.gameObject);
                    }
                }
            }

            //Search for a random target (Only usable through the Aggro feature)
            if (SearchForRandomTarget && EmeraldComponent.potentialTargets.Count > 1)
            {
                EmeraldComponent.CurrentTarget = EmeraldComponent.potentialTargets[Random.Range(0, EmeraldComponent.potentialTargets.Count)].transform;
                EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.CurrentTarget.position;
                EmeraldComponent.lookWeight = 0f;

                if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction) && EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldComponent.ReceivedFaction)] == 0)
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.AI;
                    EmeraldComponent.TargetEmerald = EmeraldComponent.CurrentTarget.GetComponent<EmeraldAISystem>();
                }
                else if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.PlayerTag && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always)
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.Player;
                }
                else if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.NonAITarget;
                }
            }

            //No targets were found within the AI's trigger radius. Set AI back to its default state.
            if (EmeraldComponent.potentialTargets.Count == 0)
            {
                if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                {
                    EmeraldComponent.CompanionTargetRef = null;
                }

                EmeraldComponent.DeathDelay = Random.Range(EmeraldComponent.DeathDelayMin, EmeraldComponent.DeathDelayMax + 1);
                EmeraldComponent.m_NavMeshAgent.destination = this.transform.position;
                EmeraldComponent.DeathDelayActive = true;
            }

            float previousDistance = Mathf.Infinity;
            float currentDistance;

            foreach (GameObject target in EmeraldComponent.potentialTargets.ToArray())
            {
                if (!SearchForRandomTarget && target != null)
                {
                    if (EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        EmeraldComponent.distanceBetween = target.transform.position - transform.position;
                    }
                    else if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion && EmeraldComponent.CurrentFollowTarget != null)
                    {
                        EmeraldComponent.distanceBetween = target.transform.position - EmeraldComponent.CurrentFollowTarget.position;
                    }
                    currentDistance = EmeraldComponent.distanceBetween.sqrMagnitude;

                    if (currentDistance < previousDistance)
                    {
                        if (PreviousTarget == null)
                        {
                            PreviousTarget = target.transform;
                        }

                        EmeraldComponent.AIAnimator.SetBool("Turn Left", false);
                        EmeraldComponent.AIAnimator.SetBool("Turn Right", false);
                        EmeraldComponent.lookWeight = 0f;
                        EmeraldComponent.CurrentTarget = target.transform;

                        //If our combat state is not active, activate it.
                        if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                        {
                            EmeraldComponent.EmeraldBehaviorsComponent.ActivateCombatState();
                        }

                        if (EmeraldComponent.ConfidenceRef != EmeraldAISystem.ConfidenceType.Coward)
                        {
                            EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.AttackDistance;
                            EmeraldComponent.OnStartCombatEvent.Invoke();
                        }

                        EmeraldComponent.potentialTargets.Clear();

                        //Once a target has been found, reduce the Detection Radius back to the defaul value.
                        EmeraldComponent.DetectionRadius = EmeraldComponent.StartingDetectionRadius;
                        EmeraldComponent.MaxChaseDistance = EmeraldComponent.StartingChaseDistance;
                        EmeraldComponent.AICollider.radius = EmeraldComponent.StartingDetectionRadius;
                        EmeraldComponent.fieldOfViewAngle = EmeraldComponent.fieldOfViewAngleRef;

                        /*
                        if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.EmeraldTag)
                        {
                            EmeraldComponent.ReceivedFaction = EmeraldComponent.CurrentTarget.GetComponent<EmeraldAISystem>().CurrentFaction;
                        }

                        if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction) && EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldComponent.ReceivedFaction)] == 0)
                        {
                            EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.AI;
                            EmeraldComponent.TargetEmerald = EmeraldComponent.CurrentTarget.GetComponent<EmeraldAISystem>();
                        }
                        if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.PlayerTag && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always)
                        {
                            EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.Player;
                        }
                        if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                        {
                            EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.NonAITarget;
                        }
                        */

                        if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Aggressive)
                        {
                            if (EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                            {
                                EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.CurrentTarget.position;
                            }
                        }

                        previousDistance = currentDistance;
                    }
                }
            }

            SearchForRandomTarget = false;

            if (EmeraldComponent.CurrentTarget != null)
            {
                if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.EmeraldTag)
                {
                    EmeraldComponent.ReceivedFaction = EmeraldComponent.CurrentTarget.GetComponent<EmeraldAISystem>().CurrentFaction;
                }

                if (EmeraldComponent.AIFactionsList.Contains(EmeraldComponent.ReceivedFaction) && EmeraldComponent.FactionRelations[EmeraldComponent.AIFactionsList.IndexOf(EmeraldComponent.ReceivedFaction)] == 0)
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.AI;
                    EmeraldComponent.TargetEmerald = EmeraldComponent.CurrentTarget.GetComponent<EmeraldAISystem>();
                }
                if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.PlayerTag && EmeraldComponent.AIAttacksPlayerRef == EmeraldAISystem.AIAttacksPlayer.Always)
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.Player;
                }
                if (EmeraldComponent.CurrentTarget.tag == EmeraldComponent.NonAITag && EmeraldComponent.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                {
                    EmeraldComponent.TargetTypeRef = EmeraldAISystem.TargetType.NonAITarget;
                }
            }

            if (EmeraldComponent.EnableDebugging == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.DebugLogTargetsEnabled == EmeraldAISystem.YesOrNo.Yes && !EmeraldComponent.DeathDelayActive)
            {
                if (EmeraldComponent.CurrentTarget != null)
                {
                    Debug.Log("<b>" + "<color=green>" + gameObject.name + "'s Current Target: " + "</color>" + "<color=red>" + EmeraldComponent.CurrentTarget.gameObject.name + "</color>" + "</b>");
                }
            }
        }
    }
}
