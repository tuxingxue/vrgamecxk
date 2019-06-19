using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmeraldAI
{
    /// <summary>
    /// This script handles all of Emerald AI's behaviors and states.
    /// </summary>
    public class EmeraldAIBehaviors : MonoBehaviour
    {
        [HideInInspector]
        public EmeraldAISystem EmeraldComponent;

        void Start()
        {
            EmeraldComponent = GetComponent<EmeraldAISystem>();
        }

        /// <summary>
        /// Handles the Aggressive Behavior Type
        /// </summary>
        public void AggressiveBehavior()
        {
            if (EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
            {
                EmeraldComponent.ObstructionDetectionUpdateTimer += Time.deltaTime;
                if (!EmeraldComponent.IsTurning && EmeraldComponent.ObstructionDetectionUpdateTimer >= EmeraldComponent.ObstructionDetectionUpdateSeconds)
                {
                    EmeraldComponent.EmeraldDetectionComponent.CheckForObstructions();
                    EmeraldComponent.ObstructionDetectionUpdateTimer = 0;
                }
            }

            if (!EmeraldComponent.BackingUp && EmeraldComponent.AIAgentActive && !EmeraldComponent.Attacking && EmeraldComponent.CurrentTarget)
            {
                EmeraldComponent.DistanceFromTarget = Vector3.Distance(EmeraldComponent.CurrentTarget.position, transform.position);
                AttackState();

                //If our target exceeds the max chase distance, clear the target and resume wander type by returning to the default state.
                if (EmeraldComponent.DistanceFromTarget > EmeraldComponent.MaxChaseDistance)
                {
                    DefaultState();
                }

                //If using blocking, attempt to trigger the blocking state
                if (EmeraldComponent.UseBlockingRef == EmeraldAISystem.YesOrNo.Yes)
                {
                    BlockState();
                }

                //Monitor the distance away from the current target, 
                //if the backup range is met, trigger the backup state.
                if (EmeraldComponent.BackupTypeRef != EmeraldAISystem.BackupType.Off)
                {
                    CalculateBackupState();
                }
            }

            //Backs AI up when true
            if (EmeraldComponent.BackingUp)
            {
                BackupState();
            }

            //If our AI target dies, search for another target
            if (EmeraldComponent.TargetTypeRef == EmeraldAISystem.TargetType.AI)
            {
                if (EmeraldComponent.TargetEmerald != null)
                {
                    if (EmeraldComponent.TargetEmerald.CurrentHealth <= 0 && !EmeraldComponent.DeathDelayActive)
                    {
                        EmeraldComponent.EmeraldDetectionComponent.PreviousTarget = EmeraldComponent.CurrentTarget;
                        EmeraldComponent.EmeraldDetectionComponent.SearchForTarget();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Companion Behavior Type
        /// </summary>
        public void CompanionBehavior()
        {
            if (EmeraldComponent.CompanionTargetRef != null && EmeraldComponent.CurrentTarget == null)
            {
                if (EmeraldComponent.TargetTypeRef == EmeraldAISystem.TargetType.AI)
                {
                    if (EmeraldComponent.TargetEmerald != null && EmeraldComponent.TargetEmerald.BehaviorRef == EmeraldAISystem.CurrentBehavior.Aggressive)
                    {
                        if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Defensive)
                        {
                            if (EmeraldComponent.TargetEmerald.CombatStateRef == EmeraldAISystem.CombatState.Active)
                            {
                                EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.AttackDistance;
                                EmeraldComponent.EmeraldDetectionComponent.PreviousTarget = EmeraldComponent.CompanionTargetRef;
                                EmeraldComponent.CurrentTarget = EmeraldComponent.CompanionTargetRef;
                                EmeraldComponent.EmeraldBehaviorsComponent.ActivateCombatState();
                            }
                        }
                        else if (EmeraldComponent.CombatTypeRef == EmeraldAISystem.CombatType.Offensive)
                        {
                            EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.AttackDistance;
                            EmeraldComponent.EmeraldDetectionComponent.PreviousTarget = EmeraldComponent.CompanionTargetRef;
                            EmeraldComponent.CurrentTarget = EmeraldComponent.CompanionTargetRef;
                            EmeraldComponent.EmeraldBehaviorsComponent.ActivateCombatState();
                        }
                    }
                }
                else
                {
                    EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.AttackDistance;
                    EmeraldComponent.EmeraldDetectionComponent.PreviousTarget = EmeraldComponent.CompanionTargetRef;
                    EmeraldComponent.CurrentTarget = EmeraldComponent.CompanionTargetRef;
                    EmeraldComponent.EmeraldBehaviorsComponent.ActivateCombatState();
                }
            }
            else if (EmeraldComponent.CurrentTarget != null)
            {
                if (EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                {
                    EmeraldComponent.ObstructionDetectionUpdateTimer += Time.deltaTime;
                    if (!EmeraldComponent.IsTurning && EmeraldComponent.ObstructionDetectionUpdateTimer >= EmeraldComponent.ObstructionDetectionUpdateSeconds)
                    {
                        EmeraldComponent.EmeraldDetectionComponent.CheckForObstructions();
                        EmeraldComponent.ObstructionDetectionUpdateTimer = 0;
                    }
                }

                if (!EmeraldComponent.BackingUp && EmeraldComponent.AIAgentActive && !EmeraldComponent.Attacking && EmeraldComponent.CurrentTarget)
                {
                    EmeraldComponent.DistanceFromTarget = Vector3.Distance(EmeraldComponent.CurrentTarget.position, transform.position);
                    AttackState();

                    //If our target exceeds the max chase distance, clear the target and resume wander type by returning to the default state.
                    if (EmeraldComponent.DistanceFromTarget > EmeraldComponent.MaxChaseDistance)
                    {
                        DefaultState();
                    }

                    //If using blocking, attempt to trigger the blocking state
                    if (EmeraldComponent.UseBlockingRef == EmeraldAISystem.YesOrNo.Yes)
                    {
                        BlockState();
                    }

                    //Monitor the distance away from the current target, 
                    //if the backup range is met, trigger the backup state.
                    if (EmeraldComponent.BackupTypeRef != EmeraldAISystem.BackupType.Off)
                    {
                        CalculateBackupState();
                    }
                }

                //Backs AI up when true
                if (EmeraldComponent.BackingUp)
                {
                    BackupState();
                }

                //If our AI target dies, search for another target
                if (EmeraldComponent.TargetTypeRef == EmeraldAISystem.TargetType.AI)
                {
                    if (EmeraldComponent.TargetEmerald != null)
                    {
                        if (EmeraldComponent.TargetEmerald.CurrentHealth <= 0 && !EmeraldComponent.DeathDelayActive)
                        {
                            EmeraldComponent.EmeraldDetectionComponent.PreviousTarget = EmeraldComponent.CurrentTarget;
                            EmeraldComponent.EmeraldDetectionComponent.SearchForTarget();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Coward Behavior Type
        /// </summary>
        public void CowardBehavior()
        {
            if (EmeraldComponent.CurrentTarget)
            {
                EmeraldComponent.DistanceFromTarget = Vector3.Distance(EmeraldComponent.CurrentTarget.position, transform.position);
                FleeState();

                //If our target exceeds the max chase distance, clear the target and resume wander type by returning to the default state.
                if (EmeraldComponent.DistanceFromTarget > EmeraldComponent.MaxChaseDistance)
                {
                    DefaultState();
                }

                //If our AI target dies, search for another target
                if (EmeraldComponent.TargetTypeRef == EmeraldAISystem.TargetType.AI)
                {
                    if (EmeraldComponent.TargetEmerald != null)
                    {
                        if (EmeraldComponent.TargetEmerald.CurrentHealth <= 0)
                        {
                            EmeraldComponent.EmeraldDetectionComponent.PreviousTarget = EmeraldComponent.CurrentTarget;
                            EmeraldComponent.EmeraldDetectionComponent.SearchForTarget();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Coward Behavior Type
        /// </summary>
        public void CautiousBehavior()
        {
            EmeraldComponent.CautiousTimer += Time.deltaTime;

            if (EmeraldComponent.CautiousTimer >= EmeraldComponent.CautiousSeconds)
            {
                EmeraldComponent.EmeraldEventsManagerComponent.ChangeBehavior(EmeraldAISystem.CurrentBehavior.Aggressive);
                EmeraldComponent.CautiousTimer = 0;
            }

            if (EmeraldComponent.UseWarningAnimationRef == EmeraldAISystem.YesOrNo.Yes && !EmeraldComponent.WarningAnimationTriggered && EmeraldComponent.CautiousTimer > 2)
            {
                EmeraldComponent.AIAnimator.SetTrigger("Warning");
                EmeraldComponent.WarningAnimationTriggered = true;
            }
        }


        /// <summary>
        /// Controls what happens when the AI dies
        /// </summary>
        public void DeadState()
        {
            EmeraldComponent.IsDead = true;
            EmeraldComponent.DeathEvent.Invoke();

            if (EmeraldComponent.m_NavMeshAgent.enabled)
            {
                EmeraldComponent.m_NavMeshAgent.ResetPath();
                EmeraldComponent.m_NavMeshAgent.isStopped = true;
                EmeraldComponent.m_NavMeshAgent.enabled = false;
            }

            EmeraldComponent.EmeraldInitializerComponent.InitializeAIDeath();

            if (EmeraldComponent.DeathTypeRef == EmeraldAISystem.DeathType.Ragdoll)
            {
                EmeraldComponent.EmeraldInitializerComponent.EnableRagdoll();
            }
            else if (EmeraldComponent.DeathTypeRef == EmeraldAISystem.DeathType.Animation)
            {
                EmeraldComponent.EmeraldDetectionComponent.enabled = false;
                EmeraldComponent.EmeraldEventsManagerComponent.enabled = false;
                EmeraldComponent.AIBoxCollider.enabled = false;
                EmeraldComponent.AICollider.enabled = false;
                EmeraldComponent.AIAnimator.ResetTrigger("Hit");
                EmeraldComponent.AIAnimator.ResetTrigger("Attack");
                EmeraldComponent.AIAnimator.SetTrigger("Dead");
                EmeraldComponent.AIAnimator.SetInteger("Death Index", Random.Range(1, EmeraldComponent.TotalDeathAnimations + 1));
            }

            if (EmeraldComponent.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.Yes || EmeraldComponent.DisplayAINameRef == EmeraldAISystem.DisplayAIName.Yes)
            {
                if (EmeraldComponent.HealthBarCanvas != null)
                {
                    EmeraldComponent.HealthBar.GetComponent<EmeraldAI.Utility.EmeraldAIHealthBar>().FadeOut();
                }
            }
        }

        /// <summary>
        /// Controls our AI's fleeing logic and functionality
        /// </summary>
        public void FleeState()
        {
            if (EmeraldComponent.m_NavMeshAgent.remainingDistance <= EmeraldComponent.StoppingDistance)
            {
                Vector3 direction = (EmeraldComponent.CurrentTarget.position - transform.position).normalized;
                Vector3 GeneratedDestination = transform.position + -direction * 30 + Random.insideUnitSphere * 10;
                GeneratedDestination.y = transform.position.y;
                EmeraldComponent.m_NavMeshAgent.destination = GeneratedDestination;
            }
        }

        /// <summary>
        /// Keeps track of whether or not certain animations are currently playing
        /// </summary>
        public void CheckAnimationStates()
        {
            if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active)
            {
                if (EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Attack1Animation || 
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Attack2Animation || 
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Attack3Animation)
                {
                    EmeraldComponent.Attacking = true;

                    if (EmeraldComponent.CurrentBlockingState == EmeraldAISystem.BlockingState.Blocking)
                    {
                        EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
                        EmeraldComponent.AIAnimator.SetBool("Blocking", false);
                        EmeraldComponent.AIAnimator.ResetTrigger("Hit");
                    }
                }
                else
                {
                    EmeraldComponent.Attacking = false;
                }
            }

            if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
            {
                if (EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Hit1Animation || 
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Hit2Animation || 
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Hit3Animation)
                {
                    EmeraldComponent.GettingHit = true;
                }
                else
                {
                    EmeraldComponent.GettingHit = false;
                }

                if (EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Emote1Animation ||
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Emote2Animation ||
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.Emote3Animation)
                {
                    EmeraldComponent.EmoteAnimationActive = true;
                }
                else
                {
                    EmeraldComponent.EmoteAnimationActive = false;
                }
            }
            else if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active)
            {
                if (EmeraldComponent.CurrentAnimationClip == EmeraldComponent.CombatHit1Animation || 
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.CombatHit2Animation || 
                    EmeraldComponent.CurrentAnimationClip == EmeraldComponent.CombatHit3Animation)
                {
                    EmeraldComponent.GettingHit = true;
                    EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
                    EmeraldComponent.AIAnimator.SetBool("Blocking", false);
                }
                else
                {
                    EmeraldComponent.GettingHit = false;
                }
            }
        }

        /// <summary>
        /// Sets the AI back to its default state
        /// </summary>
        public void DefaultState()
        {
            EmeraldComponent.BehaviorRef = (EmeraldAISystem.CurrentBehavior)EmeraldComponent.StartingBehaviorRef;
            EmeraldComponent.ConfidenceRef = (EmeraldAISystem.ConfidenceType)EmeraldComponent.StartingConfidenceRef;
            EmeraldComponent.CombatStateRef = EmeraldAISystem.CombatState.NotActive;
            EmeraldComponent.CurrentDetectionRef = EmeraldAISystem.CurrentDetection.Unaware;
            EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
            EmeraldComponent.AttackTimer = 0;
            EmeraldComponent.RunAttackTimer = 0;
            EmeraldComponent.Attacking = false;
            EmeraldComponent.WarningAnimationTriggered = false;
            EmeraldComponent.AIAnimator.SetBool("Turn Right", false);
            EmeraldComponent.AIAnimator.SetBool("Turn Left", false);
            EmeraldComponent.AIAnimator.SetBool("Blocking", false);
            EmeraldComponent.AIAnimator.ResetTrigger("Hit");
            EmeraldComponent.AIAnimator.ResetTrigger("Attack");
            EmeraldComponent.AIAnimator.SetBool("Combat State Active", false);
            EmeraldComponent.ClearTarget();
            EmeraldComponent.CurrentMovementState = EmeraldComponent.StartingMovementState;
            
            if (EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion)
            {
                EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.StoppingDistance;             
            }
            else
            {
                EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.FollowingStoppingDistance;
            }

            EmeraldComponent.m_NavMeshAgent.updateRotation = true;
            EmeraldComponent.AIAnimator.SetBool("Walk Backwards", false);
            EmeraldComponent.BackingUp = false;
            EmeraldComponent.BackingUpTimer = 0;
            EmeraldComponent.CautiousTimer = 0;
            EmeraldComponent.DeathDelayTimer = 0;
            EmeraldComponent.DeathDelayActive = false;
            EmeraldComponent.AICollider.enabled = false;
            EmeraldComponent.AICollider.enabled = true;

            if (EmeraldComponent.UseEquipAnimation == EmeraldAISystem.YesOrNo.Yes)
            {
                StartCoroutine(DelayReturnToDestination(1));
            }
            else
            {
                StartCoroutine(DelayReturnToDestination(0));
            }

            if (EmeraldComponent.RefillHealthType == EmeraldAISystem.RefillHealth.Instantly)
            {
                EmeraldComponent.CurrentHealth = EmeraldComponent.StartingHealth;
            }
            else if (EmeraldComponent.RefillHealthType == EmeraldAISystem.RefillHealth.OverTime)
            {
                StartCoroutine(RefillHeathOverTime());
            }
        }

        IEnumerator RefillHeathOverTime ()
        {
            while (EmeraldComponent.CurrentHealth < EmeraldComponent.StartingHealth)
            {
                EmeraldComponent.RegenerateTimer += Time.deltaTime;
                if (EmeraldComponent.RegenerateTimer >= EmeraldComponent.HealthRegRate)
                {
                    EmeraldComponent.CurrentHealth += EmeraldComponent.RegenerateAmount;
                    EmeraldComponent.RegenerateTimer = 0;
                }

                if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active)
                {
                    break;
                }

                yield return null;
            }

            if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
            {
                EmeraldComponent.CurrentHealth = EmeraldComponent.StartingHealth;
            }
        }

        IEnumerator DelayReturnToDestination (float DelaySeconds)
        {
            yield return new WaitForSeconds(DelaySeconds);
            if (EmeraldComponent.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion)
            {
                if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Dynamic)
                {
                    EmeraldComponent.GenerateWaypoint();
                }
                else if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Stationary && EmeraldComponent.m_NavMeshAgent.enabled)
                {
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.StartingDestination;
                    EmeraldComponent.ReturnToStationaryPosition = true;
                }
                else if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Waypoints && EmeraldComponent.m_NavMeshAgent.enabled)
                {
                    EmeraldComponent.m_NavMeshAgent.ResetPath();
                    if (EmeraldComponent.WaypointTypeRef != EmeraldAISystem.WaypointType.Random)
                    {
                        EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.WaypointsList[EmeraldComponent.WaypointIndex];
                    }
                    else
                    {
                        EmeraldComponent.WaypointTimer = 1;
                    }
                }
                else if (EmeraldComponent.WanderTypeRef == EmeraldAISystem.WanderType.Destination && EmeraldComponent.m_NavMeshAgent.enabled)
                {
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.SingleDestination;
                    EmeraldComponent.ReturnToStationaryPosition = true;
                }
            }
        }

        /// <summary>
        /// Activates our AI's Combat State
        /// </summary>
        public void ActivateCombatState()
        {
            EmeraldComponent.AIAnimator.ResetTrigger("Hit");
            EmeraldComponent.CombatStateRef = EmeraldAISystem.CombatState.Active;
            EmeraldComponent.AIAnimator.SetBool("Idle Active", false);
            EmeraldComponent.AIAnimator.SetBool("Combat State Active", true);
            EmeraldComponent.CurrentMovementState = EmeraldAISystem.MovementState.Run;

            if (EmeraldComponent.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward)
            {
                EmeraldComponent.OnFleeEvent.Invoke();
            }
        }

        /// <summary>
        /// Calculates our AI's attacks
        /// </summary>
        public void AttackState()
        {
            if (EmeraldComponent.DistanceFromTarget > EmeraldComponent.StoppingDistance && !EmeraldComponent.DeathDelayActive)
            {
                if (EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                {
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.CurrentTarget.position;
                }
                else if (EmeraldComponent.TargetObstructed && EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged && 
                    EmeraldComponent.TargetObstructedActionRef != EmeraldAISystem.TargetObstructedAction.StayStationary)
                {
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.CurrentTarget.position;
                }
                else if (!EmeraldComponent.TargetObstructed && EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                {
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.CurrentTarget.position;
                }
            }

            if (EmeraldComponent.TargetObstructed && EmeraldComponent.TargetObstructedActionRef == EmeraldAISystem.TargetObstructedAction.MoveCloserAfterSetSeconds)
            {
                EmeraldComponent.ObstructionTimer += Time.deltaTime;

                if (EmeraldComponent.ObstructionTimer >= EmeraldComponent.ObstructionSeconds && EmeraldComponent.m_NavMeshAgent.stoppingDistance > 10)
                {
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.CurrentTarget.position;
                    EmeraldComponent.m_NavMeshAgent.stoppingDistance -= 5;
                }
            }

            if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active && !EmeraldComponent.DeathDelayActive)
            {
                if (EmeraldComponent.m_NavMeshAgent.remainingDistance < EmeraldComponent.m_NavMeshAgent.stoppingDistance && !EmeraldComponent.IsMoving && !EmeraldComponent.Attacking)
                {
                    EmeraldComponent.AttackTimer += Time.deltaTime;

                    if (EmeraldComponent.AttackTimer >= EmeraldComponent.AttackSpeed && EmeraldComponent.DestinationAdjustedAngle <= EmeraldComponent.MaxDamageAngle)
                    {
                        if (!EmeraldComponent.TargetObstructed && EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged || 
                            EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee && !EmeraldComponent.m_NavMeshAgent.pathPending)
                        {
                            EmeraldComponent.GeneratedBlockOdds = Random.Range(1, 101);
                            EmeraldComponent.GeneratedBackupOdds = Random.Range(1, 101);
                            EmeraldComponent.AIAnimator.ResetTrigger("Hit");
                            EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
                            EmeraldComponent.AIAnimator.SetBool("Blocking", false);
                            EmeraldComponent.GetDamageAmount();
                            EmeraldComponent.AttackAnimationNumber = Random.Range(1, EmeraldComponent.TotalAttackAnimations + 1);
                            EmeraldComponent.AIAnimator.SetInteger("Attack Index", EmeraldComponent.AttackAnimationNumber);
                            EmeraldComponent.AIAnimator.SetTrigger("Attack");
                            EmeraldComponent.AttackSpeed = Random.Range(EmeraldComponent.MinimumAttackSpeed, EmeraldComponent.MaximumAttackSpeed + 1);
                            EmeraldComponent.AttackTimer = 0;                          
                        }
                    }
                }
                else if (EmeraldComponent.m_NavMeshAgent.remainingDistance > EmeraldComponent.m_NavMeshAgent.stoppingDistance && EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                {
                    EmeraldComponent.AttackTimer = EmeraldComponent.AttackSpeed;
                }

                if (EmeraldComponent.UseRunAttacksRef == EmeraldAISystem.UseRunAttacks.Yes)
                {
                    if (Vector3.Distance(EmeraldComponent.CurrentTarget.position, transform.position) <= (EmeraldComponent.m_NavMeshAgent.stoppingDistance + 
                        EmeraldComponent.RunAttackDistance) && EmeraldComponent.IsMoving)
                    {
                        EmeraldComponent.RunAttackTimer += Time.deltaTime;

                        if (EmeraldComponent.RunAttackTimer >= EmeraldComponent.RunAttackSpeed)
                        {
                            EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
                            EmeraldComponent.AIAnimator.SetBool("Blocking", false);
                            EmeraldComponent.AIAnimator.ResetTrigger("Hit");
                            EmeraldComponent.RunAttackAnimationNumber = Random.Range(1, EmeraldComponent.TotalRunAttackAnimations + 1);
                            EmeraldComponent.AIAnimator.SetInteger("Run Attack Index", EmeraldComponent.RunAttackAnimationNumber);
                            EmeraldComponent.AIAnimator.SetTrigger("Run Attack");
                            EmeraldComponent.RunAttackSpeed = Random.Range(EmeraldComponent.MinimumRunAttackSpeed, EmeraldComponent.MaximumRunAttackSpeed);
                            EmeraldComponent.RunAttackTimer = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies our AI's Block State
        /// </summary>
        public void BlockState()
        {
            //Activates blocking, when the appropriate conditions are met.
            if (!EmeraldComponent.BackingUp && !EmeraldComponent.IsMoving && EmeraldComponent.CurrentTarget && !EmeraldComponent.DeathDelayActive)
            {
                if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.Active && !EmeraldComponent.Attacking && !EmeraldComponent.GettingHit && EmeraldComponent.AIAgentActive && 
                    EmeraldComponent.m_NavMeshAgent.remainingDistance <= EmeraldComponent.m_NavMeshAgent.stoppingDistance && !EmeraldComponent.m_NavMeshAgent.pathPending)
                {
                    if (EmeraldComponent.AttackTimer < EmeraldComponent.AttackSpeed - 0.25f && EmeraldComponent.GeneratedBlockOdds <= EmeraldComponent.BlockOdds && 
                        EmeraldComponent.CurrentAnimationClip == EmeraldComponent.CombatIdleAnimation && EmeraldComponent.CurrentBlockingState == EmeraldAISystem.BlockingState.NotBlocking)
                    {
                        EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.Blocking;
                        EmeraldComponent.AIAnimator.SetBool("Blocking", true);
                    }
                }
            }

            //Disables blocking, when the appropriate conditions are met.
            if (EmeraldComponent.IsMoving || EmeraldComponent.BackingUp || EmeraldComponent.AIAnimator.GetBool("Turn Right") || 
                EmeraldComponent.AIAnimator.GetBool("Turn Left") || EmeraldComponent.AIAnimator.GetBool("Attack"))
            {
                EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
                EmeraldComponent.AIAnimator.SetBool("Blocking", false);
            }
        }

        /// <summary>
        /// Applies our AI's Backup State
        /// </summary>
        public void BackupState ()
        {
            EmeraldComponent.BackingUpTimer += Time.deltaTime;

            float angle = Vector3.Angle(new Vector3(EmeraldComponent.CurrentTarget.position.x, 0, EmeraldComponent.CurrentTarget.position.z) - new Vector3(transform.position.x, 0, transform.position.z), transform.forward);
            float AdjustedAngle = Mathf.Abs(angle);

            if (EmeraldComponent.BackingUpTimer >= EmeraldComponent.BackingUpSeconds || 
                EmeraldComponent.m_NavMeshAgent.remainingDistance <= EmeraldComponent.m_NavMeshAgent.stoppingDistance && EmeraldComponent.BackupDestination.x == EmeraldComponent.m_NavMeshAgent.destination.x)
            {
                EmeraldComponent.AIAnimator.ResetTrigger("Hit");
                EmeraldComponent.AIAnimator.SetBool("Walk Backwards", false);
                EmeraldComponent.AttackTimer = 0;
            }

            if (EmeraldComponent.BackingUpTimer >= EmeraldComponent.BackingUpSeconds + 0.5f)
            {
                float CurrentDistance = Vector3.Distance(EmeraldComponent.CurrentTarget.position, transform.position);
                
                if (CurrentDistance > 3 && CurrentDistance < 8 && EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                {
                    EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
                    EmeraldComponent.AIAnimator.SetBool("Blocking", false);
                    EmeraldComponent.AIAnimator.ResetTrigger("Hit");

                    if (EmeraldComponent.UseRunAttacksRef == EmeraldAISystem.UseRunAttacks.Yes)
                    {
                        EmeraldComponent.AIAnimator.SetInteger("Run Attack Index", Random.Range(1, EmeraldComponent.TotalRunAttackAnimations + 1));
                        EmeraldComponent.AIAnimator.SetTrigger("Run Attack");
                    }
                }
                else
                {
                    EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.CurrentTarget.position;
                }

                if (EmeraldComponent.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                {
                    EmeraldComponent.AttackTimer = EmeraldComponent.AttackSpeed;
                }

                EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.AttackDistance;
                EmeraldComponent.m_NavMeshAgent.updateRotation = true;
                EmeraldComponent.BackingUp = false;
                EmeraldComponent.BackingUpTimer = 0;
                EmeraldComponent.BackingUpSeconds = Random.Range(2, 4);
                EmeraldComponent.GeneratedBackupOdds = Random.Range(1, 101);
            }

            if (AdjustedAngle > 60)
            {
                EmeraldComponent.AIAnimator.SetBool("Walk Backwards", false);
                EmeraldComponent.m_NavMeshAgent.updateRotation = true;
                EmeraldComponent.BackingUp = false;
                EmeraldComponent.BackingUpTimer = 0;
                EmeraldComponent.IsMoving = false;
            }
        }

        /// <summary>
        /// Calculates backing our AI up, when the appropriate conditions are met
        /// </summary>
        public void CalculateBackupState()
        {
            if (EmeraldComponent.CurrentTarget != null)
            {
                if (EmeraldComponent.BackupTypeRef == EmeraldAISystem.BackupType.Instant || 
                    EmeraldComponent.BackupTypeRef == EmeraldAISystem.BackupType.Odds && EmeraldComponent.GeneratedBackupOdds <= EmeraldComponent.BackupOdds && EmeraldComponent.AttackTimer > 0.5f)
                {
                    if (EmeraldComponent.DistanceFromTarget < EmeraldComponent.TooCloseDistance && !EmeraldComponent.Attacking)
                    {
                        float AdjustedAngle = EmeraldComponent.TargetAngle();

                        if (AdjustedAngle <= 60 && !EmeraldComponent.AIAnimator.GetBool("Turn Left") && !EmeraldComponent.AIAnimator.GetBool("Turn Right"))
                        {
                            //Do a quick raycast to see if behind the AI is clear before calling the backup state.
                            RaycastHit HitBehind;
                            if (!Physics.Raycast(EmeraldComponent.HeadTransform.position, -transform.forward, out HitBehind, 5))
                            {
                                if (EmeraldComponent.AnimatorType == EmeraldAISystem.AnimatorTypeState.NavMeshDriven)
                                {
                                    EmeraldComponent.m_NavMeshAgent.speed = EmeraldComponent.WalkSpeed;
                                }
                                EmeraldComponent.CurrentBlockingState = EmeraldAISystem.BlockingState.NotBlocking;
                                EmeraldComponent.AIAnimator.SetBool("Blocking", false);
                                EmeraldComponent.m_NavMeshAgent.updateRotation = false;
                                Vector3 diff = transform.position - EmeraldComponent.CurrentTarget.position;
                                diff.y = 0.0f;
                                EmeraldComponent.BackupDestination = EmeraldComponent.CurrentTarget.position + diff.normalized * EmeraldComponent.BackupDistance;
                                EmeraldComponent.m_NavMeshAgent.destination = EmeraldComponent.BackupDestination;
                                EmeraldComponent.AIAnimator.ResetTrigger("Hit");
                                EmeraldComponent.AIAnimator.ResetTrigger("Attack");
                                EmeraldComponent.AIAnimator.SetBool("Turn Left", false);
                                EmeraldComponent.AIAnimator.SetBool("Turn Right", false);
                                EmeraldComponent.AIAnimator.SetBool("Walk Backwards", true);
                                EmeraldComponent.BackingUp = true;
                            }
                        }
                    }
                }
            }
            else
            {
                //If our target dies or is lost, search for another target.
                EmeraldComponent.EmeraldDetectionComponent.SearchForTarget();
            }
        }
    }
}
