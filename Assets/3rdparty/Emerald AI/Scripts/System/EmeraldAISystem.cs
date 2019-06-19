using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Events;
using EmeraldAI.Utility;

namespace EmeraldAI
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AudioSource))]  
    [RequireComponent(typeof(EmeraldAIDetection))]
    [RequireComponent(typeof(EmeraldAIInitializer))]
    [RequireComponent(typeof(EmeraldAIBehaviors))]
    [RequireComponent(typeof(EmeraldAIEventsManager))]
    [SelectionBase]

    public class EmeraldAISystem : MonoBehaviour {

        public bool m_AttackAnimationClipMissing = false;
        public float ProjectileCollisionPointY = 0.5f;
        public Vector3 m_ProjectileCollisionPoint;
        public float ObstructionTimer;
        public int ObstructionSeconds = 4;

        //Volumes
        public float IdleVolume = 1;
        public float WalkFootstepVolume = 0.1f;
        public float RunFootstepVolume = 0.1f;
        public float BlockVolume = 0.65f;
        public float ImpactVolume = 1;
        public float InjuredVolume = 1;
        public float AttackVolume = 1;
        public float WarningVolume = 1;
        public float DeathVolume = 0.7f;
        public float EquipVolume = 1;
        public float UnequipVolume = 1;

        //Head Look
        public float HeadLookWeightCombat = 0.5f;
        public float BodyLookWeightCombat = 0.35f;
        public float HeadLookWeightNonCombat = 1f;
        public float BodyLookWeightNonCombat = 0.3f;
        public int MaxLookAtDistance = 15;
        public float HeadLookYOffset = 0;
        public YesOrNo UseHeadLookRef = YesOrNo.No;
        public float LookSmoother = 1.25f;
        public int NonCombatLookAtLimit = 90;
        public int CombatLookAtLimit = 45;
        public float lookWeight;
        public int LookAtLimit;
        public AnimatorStateInfo m_LayerCurrentState;
        public Transform HeadTransform;
        public Vector3 StartingLookAtPosition;

        public int CurrentAggroHits = 0;
        public int TotalAggroHits = 5;
        public enum AggroAction {LastAttacker = 0, RandomAttacker, ClosestAttacker};
        public AggroAction AggroActionRef = AggroAction.LastAttacker;
        public bool ReturnToStationaryPosition;

        public EmeraldAIDetection EmeraldDetectionComponent;
        public EmeraldAIInitializer EmeraldInitializerComponent;
        public EmeraldAIEventsManager EmeraldEventsManagerComponent;
        public EmeraldAIBehaviors EmeraldBehaviorsComponent;

        //Blocking
        public YesOrNo UseBlockingRef = YesOrNo.No;
        float BlockTimer;
        float BlockAngle;
        float AdjustedBlockAngle;
        public int MitigationAmount = 50;
        public int MaxBlockAngle = 75;
        public int MaxDamageAngle = 75;
        Vector3 SurfaceNormal;
        Quaternion NormalRotation;
        public float MaxNormalAngle;
        public int MaxNormalAngleEditor = 25;
        float AngleCheckTimer;
        public int BlockOdds = 80;
        public int GeneratedBlockOdds;

        public bool m_AnimationsChanged = false;
        public int m_LastAnimatorType;
        public AnimatorTypeState AnimatorType = AnimatorTypeState.NavMeshDriven;
        public enum AnimatorTypeState {RootMotion, NavMeshDriven}

        public MovementState StartingMovementState = MovementState.Run;
        public MovementState CurrentMovementState = MovementState.Run;
        public enum MovementState {Walk = 0, Run}

        public BlockingState CurrentBlockingState = BlockingState.NotBlocking;
        public enum BlockingState {Blocking, NotBlocking}

        public BackupType BackupTypeRef = BackupType.Instant;
        public enum BackupType {Off = 0, Instant, Odds}
        public int BackupOdds = 50;
        public int GeneratedBackupOdds;
        public int BackupDistance = 12;
        public Vector3 BackupDestination;

        //Combat
        public bool Attacking;
        public float BackingUpTimer;
        public float BackingUpSeconds;
        public float RunAttackTimer;
        public int RunAttackSpeed;
        public int MinimumRunAttackSpeed = 2;
        public int MaximumRunAttackSpeed = 4;
        public float AttackTimer;
        public bool GettingHit;
        public bool HasTarget;

        public int TotalIdleAnimations = 1;
        public int TotalHitAnimations = 1;
        public int TotalCombatHitAnimations = 1;
        public int TotalEmoteAnimations = 1;
        public int TotalAttackAnimations = 1;
        public int TotalRunAttackAnimations = 1;
        public int TotalDeathAnimations = 1;
        public bool m_IdleAnimaionIndexOverride = false;
        public bool MirrorWalkLeft = false;
        public bool MirrorWalkRight = false;
        public bool MirrorRunLeft = false;
        public bool MirrorRunRight = false;
        public bool MirrorCombatWalkLeft = false;
        public bool MirrorCombatWalkRight = false;
        public bool MirrorCombatRunLeft = false;
        public bool MirrorCombatRunRight = false;
        public bool MirrorCombatTurnLeft = false;
        public bool MirrorCombatTurnRight = false;
        public bool MirrorTurnLeft = false;
        public bool MirrorTurnRight = false;
        public bool ReverseWalkAnimation = false;

        int AngleToTurn;
        public int NonCombatAngleToTurn = 25;
        public int CombatAngleToTurn = 10;
        public GameObject WeaponObject;
        public GameObject DroppableWeaponObject;
        public Transform WeaponObjectTransfrom;
        public Vector3 WeaponObjectPosition;
        public Vector3 WeaponObjectRotation;
        public int StationaryTurningSpeedCombat = 120;
        public int StationaryTurningSpeedNonCombat = 60;
        public int MaxSlopeLimit = 30;
        public float Acceleration = 1;
        public float Deceleration = 0.5f;
        public float TurnAngle = 30;
        public int AlignmentSpeed;
        public int NonCombatAlignmentSpeed = 15;
        public int CombatAlignmentSpeed = 25;
        public float DirectionDampTime = 0.25f;
        public NavMeshAgent m_NavMeshAgent;
        public AudioSource m_AudioSource;
        public AudioSource m_SecondaryAudioSource;
        public AudioSource m_EventAudioSource;
        public float WaypointTimer;
        float IdleAnimationTimer;
        public int IdleAnimationSeconds = 3;
        public bool EmoteAnimationActive = false;

        public GameObject SpawnTestObject;
        public Vector3 NewDestination;
        float DistanceFromDestination;
        float Velocity;

        float AdjustedSpeed;

        Quaternion TargetQ;
        Quaternion GroundQ;
        float PreviousAngle;

        float AddForceTimer;
        float DeathTimer;
        public Transform ForceTransform;
        public int ReceivedRagdollForceAmount;
        public int SentRagdollForceAmount = 200;
        public Transform HitTransform;
        public Transform RagdollTransform;
        public Vector3 TargetDestination;

        Quaternion AligmentAngle;

        public bool IsMoving;
        public float DestinationAdjustedAngle;
        float DestinationAngle;
        Vector3 DestinationDirection;

        public GameObject WeaponTrail;

        //Emerald AI Events
        public UnityEvent DeathEvent;
        public UnityEvent DamageEvent;
        public UnityEvent ReachedDestinationEvent;
        public UnityEvent OnStartEvent;
        public UnityEvent OnEnabledEvent;
        public UnityEvent OnAttackEvent;
        public UnityEvent OnFleeEvent;
        public UnityEvent OnStartCombatEvent;

        public int IdleSoundsSeconds;
        public int IdleSoundsSecondsMin = 5;
        public int IdleSoundsSecondsMax = 10;
        public float IdleSoundsTimer;
        public int CurrentAttacks = 0;
        public int ExpandedChaseDistance = 80;
        public int StartingChaseDistance;
        public int ExpandedDetectionRadius = 40;
        public int StartingDetectionRadius;
        public bool IsRunAttack = false;
        public int RandomDirection;
        public bool TargetInView = false;
        public Vector3 ProjectileFirePosition;
        public float Attack1HeatSeekingSeconds = 1;
        public float Attack2HeatSeekingSeconds = 1;
        public float Attack3HeatSeekingSeconds = 1;
        public float RunAttackHeatSeekingSeconds = 1;
        public enum HeatSeeking { No = 0, Yes = 1 };
        public HeatSeeking Attack1HeatSeekingRef = HeatSeeking.No;
        public HeatSeeking Attack2HeatSeekingRef = HeatSeeking.No;
        public HeatSeeking Attack3HeatSeekingRef = HeatSeeking.No;
        public HeatSeeking RunAttackHeatSeekingRef = HeatSeeking.No;

        [SerializeField]
        public int CurrentFaction;
        [SerializeField]
        public static List<string> StringFactionList = new List<string>();
        public enum RelationType { Enemy = 0, Neutral = 1, Friendly = 2 };
        public RelationType RelationTypeRef;
        public List<int> FactionRelations = new List<int>();
        public int FactionRelation1;
        public int FactionRelation2;
        public int FactionRelation3;
        public int FactionRelation4;
        public int FactionRelation5;

        public static GameObject ObjectPool;
        public float ProjectileTimeoutTime = 4.5f;
        public float StartingRunSpeed;
        public float StartingRunAnimationSpeed;
        public float StationaryIdleTimer = 0;
        public int StationaryIdleSeconds;
        public int StationaryIdleSecondsMin = 3;
        public int StationaryIdleSecondsMax = 6;
        public GameObject Attack1CollisionEffect;
        public GameObject Attack2CollisionEffect;
        public GameObject Attack3CollisionEffect;
        public GameObject RunAttackCollisionEffect;
        public AudioClip Attack1ImpactSound;
        public AudioClip Attack2ImpactSound;
        public AudioClip Attack3ImpactSound;
        public AudioClip RunAttackImpactSound;
        public int Attack1ProjectileSpeed = 30;
        public int Attack2ProjectileSpeed = 30;
        public int Attack3ProjectileSpeed = 30;
        public int RunAttackProjectileSpeed = 30;
        public bool Attack1DisableOnCollision = true;
        public bool Attack2DisableOnCollision = true;
        public bool Attack3DisableOnCollision = true;
        public bool RunAttackDisableOnCollision = true;
        public bool Projectile1Updated = false;
        public bool Projectile2Updated = false;
        public bool Projectile3Updated = false;
        public bool RunProjectileUpdated = false;
        public bool BloodEffectUpdated = false;
        public float Attack1CollisionSeconds = 0f;
        public float Attack2CollisionSeconds = 0f;
        public float Attack3CollisionSeconds = 0f;
        public float RunAttackCollisionSeconds = 0f;
        public float Effect1TimeoutSeconds = 3f;
        public float Effect2TimeoutSeconds = 3f;
        public float Effect3TimeoutSeconds = 3f;
        public float RunEffectTimeoutSeconds = 3f;
        public float BloodEffectTimeoutSeconds = 3f;
        public float PlayerYOffset = 0;

        public GameObject Attack1Projectile;
        public GameObject Attack2Projectile;
        public GameObject Attack3Projectile;
        public GameObject RunAttackProjectile;
        public GameObject CurrentProjectile;
        public Vector3 ProjectileDirection;

        public List<GameObject> potentialTargets = new List<GameObject>();
        public List<Transform> LineOfSightTargets = new List<Transform>();

        [System.Serializable]
        public class InteractSoundClass
        {
            public int SoundEffectID = 1;
            public AudioClip SoundEffectClip;
        }
        public List<InteractSoundClass> InteractSoundList = new List<InteractSoundClass>();

        [System.Serializable]
        public class ItemClass
        {
            public int ItemID = 1;
            public GameObject ItemObject;
        }
        public List<ItemClass> ItemList = new List<ItemClass>();

        [System.Serializable]
        public class EmoteAnimationClass
        {
            public int AnimationID = 1;
            public AnimationClip EmoteAnimationClip;
        }
        public List<EmoteAnimationClass> EmoteAnimationList = new List<EmoteAnimationClass>();

        [System.Serializable]
        public class AnimationClass
        {
            public float AnimationSpeed = 1;
            public AnimationClip AnimationClip;
        }
        public List<AnimationClass> IdleAnimationList = new List<AnimationClass>();
        public List<AnimationClass> AttackAnimationList = new List<AnimationClass>();
        public List<AnimationClass> RunAttackAnimationList = new List<AnimationClass>();
        public List<AnimationClass> DeathAnimationList = new List<AnimationClass>();
        public List<AnimationClass> CombatHitAnimationList = new List<AnimationClass>();
        public List<AnimationClass> HitAnimationList = new List<AnimationClass>();

        public AnimationClip CurrentAnimationClip;
        public AnimationClip Attack1Animation, Attack2Animation, Attack3Animation, RunAttack1Animation, RunAttack2Animation, RunAttack3Animation;
        public AnimationClip Idle1Animation, Idle2Animation, Idle3Animation, IdleWarningAnimation;
        public AnimationClip NonCombatTurnLeftAnimation, NonCombatTurnRightAnimation, CombatTurnLeftAnimation, CombatTurnRightAnimation;
        public AnimationClip NonCombatIdleAnimation, WalkLeftAnimation, WalkStraightAnimation, WalkRightAnimation, RunLeftAnimation, RunStraightAnimation, RunRightAnimation, Emote1Animation, Emote2Animation, Emote3Animation;
        public AnimationClip CombatIdleAnimation, CombatWalkLeftAnimation, CombatWalkStraightAnimation, CombatWalkBackAnimation, CombatWalkRightAnimation, CombatRunLeftAnimation, CombatRunStraightAnimation, CombatRunRightAnimation;
        public AnimationClip Hit1Animation, Hit2Animation, Hit3Animation, CombatHit1Animation, CombatHit2Animation, CombatHit3Animation, BlockIdleAnimation, BlockHitAnimation, PutAwayWeaponAnimation, PullOutWeaponAnimation;
        public AnimationClip Death1Animation, Death2Animation, Death3Animation;

        [SerializeField]
        public bool AnimatorControllerGenerated = false;
        public bool AnimationListsChanged = false;
        public string FilePath;
        public string EmeraldTag = "Respawn";
        public string UITag = "Player";
        public string RagdollTag = "Untagged";
        public bool AnimationsUpdated = false;

        public float RayCastUpdateSeconds = 0.1f;
        float RayCastUpdateTimer;
        public float ObstructionDetectionUpdateSeconds = 0.1f;
        public float ObstructionDetectionUpdateTimer;
        public int CurrentHealth;
        public int StartingHealth = 15;
        public int DetectionRadius = 18;
        public int CurrentDamageAmount = 5;

        public int DamageAmount1 = 5;
        public int DamageAmount2 = 5;
        public int DamageAmount3 = 5;
        public int DamageAmountRun = 5;
        public int WanderRadius = 25;
        public int TabNumber = 0;
        public int TemperamentTabNumber = 0;
        public int DetectionTagsTabNumber = 0;
        public int AnimationTabNumber = 0;
        public int SoundTabNumber = 0;
        public int MovementTabNumber = 0;
        public int AttackAnimationNumber = 1;
        public int RunAttackAnimationNumber = 1;
        public int IdleAnimationNumber = 1;
        public int CombatTabNumber = 0;
        public int EventTabNumber = 0;

        public int MinimumWaitTime = 3;
        public int MaximumWaitTime = 6;

        public int MinimumFollowWaitTime = 1;
        public int MaximumFollowWaitTime = 2;

        public int DeathDelay;
        public bool DeathDelayActive;
        public float DeathDelayTimer;
        public int DeathDelayMin = 1;
        public int DeathDelayMax = 3;

        public int MinimumAttackSpeed = 1;
        public int MaximumAttackSpeed = 3;

        public int MinimumDamageAmount1 = 2;
        public int MaximumDamageAmount1 = 5;
        public int MinimumDamageAmount2 = 2;
        public int MaximumDamageAmount2 = 5;
        public int MinimumDamageAmount3 = 2;
        public int MaximumDamageAmount3 = 5;
        public int MinimumDamageAmountRun = 2;
        public int MaximumDamageAmountRun = 5;

        public float WalkSpeed = 2;
        public float RunSpeed = 5;
        public int ExpandedFieldOfViewAngle = 300;
        public int fieldOfViewAngle = 180;
        public int fieldOfViewAngleRef;
        public int MaxChaseDistance = 30;
        public int AngleNeededToTurn = 10;
        public int AngleNeededToTurnRunning = 30;
        public float TooCloseDistance = 2;
        public float AttackDistance = 4;
        public float RunAttackDistance = 3f;
        public int CautiousSeconds = 8;
        public int DeactivateDelay = 5;
        float DeactivateTimer;
        public int SecondsToDisable = 6;
        public float HealthRegRate = 1;
        public int RegenerateAmount = 1;
        public float RegenerateTimer = 0;
        public int AILevel = 1;
        public string AIName = "AI Name";
        public string AITitle = "AI Title";

        public float StoppingDistance = 4;
        public float FollowingStoppingDistance = 5;
        public float DistanceOffset = 1;
        public float RunAttackDelay = 0.4f;
        public float Attack1Delay = 0.4f;
        public float Attack2Delay = 0.4f;
        public float Attack3Delay = 0.4f;
        public float DistanceFromTarget;
        public float AgentRadius = 0.5f;
        public float AgentBaseOffset = 0;
        public float AgentTurnSpeed = 2000;
        public float AgentAcceleration = 75;
        public float MaxXAngle = 15;
        public float MaxZAngle = 5;
        public int HealthPercentageToFlee = 10;

        public float Idle1AnimationSpeed = 1;
        public float Idle2AnimationSpeed = 1;
        public float Idle3AnimationSpeed = 1;
        public float IdleWarningAnimationSpeed = 1;
        public float IdleCombatAnimationSpeed = 1;
        public float IdleNonCombatAnimationSpeed = 1;
        public float Attack1AnimationSpeed = 1;
        public float Attack2AnimationSpeed = 1;
        public float Attack3AnimationSpeed = 1;
        public float RunAttack1AnimationSpeed = 1;
        public float RunAttack2AnimationSpeed = 1;
        public float RunAttack3AnimationSpeed = 1;
        public float TurnLeftAnimationSpeed = 1;
        public float TurnRightAnimationSpeed = 1;
        public float CombatTurnLeftAnimationSpeed = 1;
        public float CombatTurnRightAnimationSpeed = 1;
        public float Death1AnimationSpeed = 1;
        public float Death2AnimationSpeed = 1;
        public float Death3AnimationSpeed = 1;
        public float Emote1AnimationSpeed = 1;
        public float Emote2AnimationSpeed = 1;
        public float Emote3AnimationSpeed = 1;
        public float WalkAnimationSpeed = 1;
        public float RunAnimationSpeed = 1;
        public float NonCombatWalkAnimationSpeed = 1;
        public float NonCombatRunAnimationSpeed = 1;
        public float CombatWalkAnimationSpeed = 1;
        public float CombatRunAnimationSpeed = 1;
        public float Hit1AnimationSpeed = 1;
        public float Hit2AnimationSpeed = 1;
        public float Hit3AnimationSpeed = 1;
        public float CombatHit1AnimationSpeed = 1;
        public float CombatHit2AnimationSpeed = 1;
        public float CombatHit3AnimationSpeed = 1;

        public int StartingBehaviorRef;
        public int StartingConfidenceRef;

        public Vector3 SingleDestination;

        public Renderer Renderer1;
        public Renderer Renderer2;
        public Renderer Renderer3;
        public Renderer Renderer4;

        public enum DeathType {Animation = 0, Ragdoll};
        public DeathType DeathTypeRef = DeathType.Animation;

        public enum CurrentBehavior { Passive = 0, Cautious = 1, Companion = 2, Aggressive = 3, Pet = 4 };
        public CurrentBehavior BehaviorRef = CurrentBehavior.Passive;

        public enum DetectionType { Trigger = 0, LineOfSight = 1 };
        public DetectionType DetectionTypeRef = DetectionType.Trigger;

        public enum MaxChaseDistanceType { TargetDistance = 0, StartingDistance = 1 };
        public MaxChaseDistanceType MaxChaseDistanceTypeRef = MaxChaseDistanceType.TargetDistance;

        public enum ConfidenceType { Coward = 0, Brave = 1, Foolhardy = 2 };
        public ConfidenceType ConfidenceRef = ConfidenceType.Brave;

        public enum OptimizedState { Active = 0, Inactive = 1 };
        public OptimizedState OptimizedStateRef = OptimizedState.Inactive;

        public enum CurrentDetection { Alert = 0, Unaware = 1 };
        public CurrentDetection CurrentDetectionRef = CurrentDetection.Unaware;

        public enum TargetType { Player = 0, AI = 1, NonAITarget = 2 };
        public TargetType TargetTypeRef = TargetType.Player;

        public enum CombatState { NotActive = 0, Active = 1 };
        public CombatState CombatStateRef = CombatState.NotActive;

        public enum CreateHealthBars { No = 0, Yes = 1 };
        public CreateHealthBars CreateHealthBarsRef = CreateHealthBars.No;

        public enum UseCombatText { No = 0, Yes = 1 };
        public UseCombatText UseCombatTextRef = UseCombatText.No;

        public enum CombatType { Offensive = 0, Defensive = 1 };
        public CombatType CombatTypeRef = CombatType.Offensive;

        public enum RandomizeDamage { No = 0, Yes = 1 };
        public RandomizeDamage RandomizeDamageRef = RandomizeDamage.Yes;

        public enum CustomizeHealthBar { No = 0, Yes = 1 };
        public CustomizeHealthBar CustomizeHealthBarRef = CustomizeHealthBar.No;

        public enum UseCustomFont { No = 0, Yes = 1 };
        public UseCustomFont UseCustomFontRef = UseCustomFont.No;

        public enum AnimateCombatText { Stationary = 0, Upwards = 1, Random = 2 };
        public AnimateCombatText AnimateCombatTextRef = AnimateCombatText.Stationary;

        public enum DisplayAIName { No = 0, Yes = 1 };
        public DisplayAIName DisplayAINameRef = DisplayAIName.No;

        public enum DisplayAITitle { No = 0, Yes = 1 };
        public DisplayAITitle DisplayAITitleRef = DisplayAITitle.No;

        public enum DisplayAILevel { No = 0, Yes = 1 };
        public DisplayAILevel DisplayAILevelRef = DisplayAILevel.No;

        public enum RefillHealth {Disable = 0, Instantly = 1, OverTime = 2};
        public RefillHealth RefillHealthType = RefillHealth.OverTime;

        public enum OpposingFactionsEnum { One = 0, Two = 1, Three = 2, Four = 3, Five = 4 };
        public OpposingFactionsEnum OpposingFactionsEnumRef = OpposingFactionsEnum.Two;

        public enum WanderType { Dynamic = 0, Waypoints = 1, Stationary = 2, Destination = 3 };
        public WanderType WanderTypeRef = WanderType.Dynamic;

        public enum WaypointType { Loop = 0, Reverse = 1, Random = 2 };
        public WaypointType WaypointTypeRef = WaypointType.Loop;

        public enum AlignAIWithGround { No = 0, Yes = 1 };
        public AlignAIWithGround AlignAIWithGroundRef = AlignAIWithGround.No;

        public enum UseBloodEffect { Yes = 0, No = 1 };
        public UseBloodEffect UseBloodEffectRef = UseBloodEffect.No;

        public enum AlignmentQuality { Low = 0, Medium = 1, High = 2 };
        public AlignmentQuality AlignmentQualityRef = AlignmentQuality.Medium;

        public enum ObstructionDetectionQuality { Low = 0, Medium = 1, High = 2 };
        public ObstructionDetectionQuality ObstructionDetectionQualityRef = ObstructionDetectionQuality.Medium;

        public enum ActionAnimation { Eat = 0, Sleep = 1, Talk = 2, Work = 3, Interact = 4 };
        public ActionAnimation ActionAnimationRef = ActionAnimation.Talk;

        public enum PickTargetMethod { Closest = 0, FirstDetected = 1 };
        public PickTargetMethod PickTargetMethodRef = PickTargetMethod.Closest;

        public enum AIAttacksPlayer { Never = 0, Always = 1, OnlyIfAttacked = 2 };
        public AIAttacksPlayer AIAttacksPlayerRef = AIAttacksPlayer.Always;

        public enum UseNonAITag { No = 0, Yes = 1 };
        public UseNonAITag UseNonAITagRef = UseNonAITag.No;

        public enum UseRunAttacks { Yes = 0, No = 1 };
        public UseRunAttacks UseRunAttacksRef = UseRunAttacks.No;

        public enum UseMagicEffectsPack { No = 0, Yes = 1 };
        public UseMagicEffectsPack UseMagicEffectsPackRef = UseMagicEffectsPack.No;

        public enum WeaponType { Melee = 0, Ranged = 1 };
        public WeaponType WeaponTypeRef = WeaponType.Melee;

        public enum EffectOnCollision { No = 0, Yes = 1 };
        public EffectOnCollision Attack1EffectOnCollisionRef = EffectOnCollision.No;
        public EffectOnCollision Attack2EffectOnCollisionRef = EffectOnCollision.No;
        public EffectOnCollision Attack3EffectOnCollisionRef = EffectOnCollision.No;
        public EffectOnCollision RunAttackEffectOnCollisionRef = EffectOnCollision.No;
        public EffectOnCollision Attack1SoundOnCollisionRef = EffectOnCollision.No;
        public EffectOnCollision Attack2SoundOnCollisionRef = EffectOnCollision.No;
        public EffectOnCollision Attack3SoundOnCollisionRef = EffectOnCollision.No;
        public EffectOnCollision RunAttackSoundOnCollisionRef = EffectOnCollision.No;

        public enum AvoidanceQuality { None = 0, Low = 1, Medium = 2, Good = 3, High = 4 };
        public AvoidanceQuality AvoidanceQualityRef = AvoidanceQuality.Medium;

        public enum TargetObstructedAction { StayStationary = 0, MoveCloserImmediately, MoveCloserAfterSetSeconds };
        public TargetObstructedAction TargetObstructedActionRef = TargetObstructedAction.StayStationary;

        public Transform RangedAttackTransform;
        public bool DisableProjectileOnCollision = false;

        public Transform CurrentTarget;
        public Transform CurrentFollowTarget;

        public string PlayerTag = "Player";
        public string FollowTag = "Player";
        public string NonAITag = "Water";

        public enum YesOrNo { No = 0, Yes = 1 };
        public YesOrNo UseRandomRotationOnStartRef = YesOrNo.No;
        public YesOrNo DisableAIWhenNotInViewRef = YesOrNo.No;
        public YesOrNo UseDeactivateDelayRef = YesOrNo.No;
        public YesOrNo UseWarningAnimationRef = YesOrNo.No;
        public YesOrNo AlignAIOnStartRef = YesOrNo.No;
        public YesOrNo UseAIAvoidance = YesOrNo.No;
        public YesOrNo UseEquipAnimation = YesOrNo.No;
        public YesOrNo UseWeaponObject = YesOrNo.No;
        public YesOrNo UseHitAnimations = YesOrNo.Yes;
        public YesOrNo UseDroppableWeapon = YesOrNo.Yes;
        public YesOrNo ArrowProjectileAttack1 = YesOrNo.No;
        public YesOrNo ArrowProjectileAttack2 = YesOrNo.No;
        public YesOrNo ArrowProjectileAttack3 = YesOrNo.No;
        public YesOrNo ArrowProjectileRunAttack = YesOrNo.No;
        public YesOrNo EnableDebugging = YesOrNo.No;
        public YesOrNo DrawRaycastsEnabled = YesOrNo.No;
        public YesOrNo DebugLogTargetsEnabled = YesOrNo.No;
        public YesOrNo DebugLogObstructionsEnabled = YesOrNo.No;
        public YesOrNo UseAggro = YesOrNo.Yes;
        public YesOrNo AutoEnableAnimatePhysics = YesOrNo.Yes;

        public enum TotalLODsEnum { Two = 0, Three = 1, Four = 2 };
        public TotalLODsEnum TotalLODsRef = TotalLODsEnum.Three;
        public YesOrNo HasMultipleLODsRef = YesOrNo.No;

        public bool ReturningToStartInProgress = false;
        public bool AIReachedDestination = false;
        public bool AIAgentActive = false;

        public bool HealthBarsFoldout = true;
        public bool CombatTextFoldout = true;
        public bool NameTextFoldout = true;
        public bool WaypointsFoldout = true;

        public bool WalkFoldout = true;
        public bool RunFoldout = true;
        public bool TurnFoldout = true;
        public bool CombatWalkFoldout = true;
        public bool CombatRunFoldout = true;
        public bool CombatTurnFoldout = true;

        public bool BehaviorFoldout = true;
        public bool ConfidenceFoldout = true;
        public bool WanderFoldout = true;
        public bool CombatStyleFoldout = true;
        public bool CurrentlyPlayingActionAnimation = false;
        public bool DamageSoundInhibitor = false;
        public bool DamageEffectInhibitor = false;

        public AudioClip SheatheWeapon;
        public AudioClip UnsheatheWeapon;
        public List<AudioClip> IdleSounds = new List<AudioClip>();
        public List<AudioClip> AttackSounds = new List<AudioClip>();
        public List<AudioClip> InjuredSounds = new List<AudioClip>();
        public List<AudioClip> WarningSounds = new List<AudioClip>();
        public List<AudioClip> DeathSounds = new List<AudioClip>();
        public List<AudioClip> FootStepSounds = new List<AudioClip>();
        public List<AudioClip> BlockingSounds = new List<AudioClip>();
        public List<AudioClip> ImpactSounds = new List<AudioClip>();

        [SerializeField]
        public List<int> AIFactionsList = new List<int>();
        public int TargetTagsIndex;

        public int OpposingFaction1 = 1;
        public int OpposingFaction2 = 1;
        public int OpposingFaction3 = 1;
        public int OpposingFaction4 = 1;
        public int OpposingFaction5 = 1;

        public Vector3 BloodPosOffset;
        public enum BloodEffectPositionType {BaseTransform = 0, HitTransform};
        public BloodEffectPositionType BloodEffectPositionTypeRef = BloodEffectPositionType.HitTransform;

        public GameObject BloodEffect;
        public GameObject HealthBarCanvas;
        public Sprite HealthBarImage;
        public Sprite HealthBarBackgroundImage;
        public Font CombatTextFont;
        public GameObject CombatTextObject;
        public Vector3 HealthBarPos = new Vector3(0, 1.75f, 0);
        public Vector3 CombatTextPos = new Vector3(0, 1.75f, 0);
        public float StartingCombatTextSize = 0.065f;
        public float EndingCombatTextSize = 0.09f;
        public Color HealthBarColor = new Color32(197, 41, 41, 255);
        public Color HealthBarBackgroundColor = new Color32(36, 36, 36, 255);
        public Color CombatTextColor = new Color32(197, 41, 41, 255);
        public Color NameTextColor = new Color32(255, 255, 255, 255);
        public Color LevelTextColor = new Color32(255, 255, 255, 255);
        public Vector3 HealthBarScale = new Vector3(0.75f, 1, 1);
        public Vector2 NameTextSize = new Vector2(0.15f, 0.15f);
        public GameObject WaypointParent;
        public string WaypointOrigin;
        public Vector3 AINamePos = new Vector3(0, 3, 0);

        public bool IsTurning = false;
        public bool FirstTimeInCombat = true;
        public bool WarningAnimationTriggered = false;
        public bool TargetObstructed = false;
        public bool IsDead = false;

        float CurrentHealthFloat;
        float CurrentVelocity;
        public int AttackSpeed = 1;

        public int WaitTime = 5;
        int CombatTextIndex = 0;
        int DamageReceived;

        public Transform HeadLookRef;
        public Transform LineOfSightRef;
        public Transform PassiveTargetRef;
        public Transform CompanionTargetRef;
        public NavMeshPath AIPath;
        public Animator AIAnimator;
        public Vector3 StartingDestination;
        public SphereCollider AICollider;
        public float CautiousTimer;
        public BoxCollider AIBoxCollider;
        public Renderer AIRenderer;
        public Rigidbody AIRigidbody;
        public EmeraldAISystem TargetEmerald;
        public GameObject HealthBar;
        public GameObject CombatTextParent;
        public List<TextMesh> CombatTextList = new List<TextMesh>();
        Vector3 previous;
        public TextMesh TextName;
        public TextMesh TextLevel;
        public Canvas HealthBarCanvasRef;
        public Quaternion lookRotation;
        Vector3 GroundAngle;
        Quaternion rot;
        public List<Vector3> WaypointsList = new List<Vector3>();
        public int WaypointIndex = 0;
        int m_LastWaypointIndex = 0;
        public GameObject WaypointHolder;
        Vector3 TurnDirection;
        Vector3 TurnDirectionOther;
        Quaternion TargetDirection;
        NavMeshHit NMH;
        GameObject HitObject;
        public Collider[] CollidersInArea;
        public int ReceivedFaction;
        public Vector3 distanceBetween;
        float WaitForAttackAnimation;
        Vector3 NewPosition;
        public bool BackingUp;
        float WanderAngle;
        float angleX;
        float angleZ;
        float BackUpSeconds;
        public Vector3 RandomOffset;
        float OffsetTimer;
        public string StartingTag;
        public int StartingLayer;

        public LayerMask DetectionLayerMask = 3;
        public LayerMask ObstructionDetectionLayerMask = 4;
        public LayerMask AlignmentLayerMask = 1;
        public LayerMask AIAvoidanceLayerMask;

        //Initialize Emerald AI and its components
        void Awake()
        {
            EmeraldInitializerComponent = GetComponent<EmeraldAIInitializer>();
            EmeraldInitializerComponent.Initialize();
        }

        /// <summary>
        /// Check our AI's path to ensure if it is reachable. If it isn't, regenerate, depending on the Wander Type.
        /// </summary>
        public void CheckPath(Vector3 Destination)
        {
            NavMeshPath path = new NavMeshPath();
            m_NavMeshAgent.CalculatePath(Destination, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                //Path is valid
            }
            else if (path.status == NavMeshPathStatus.PathPartial)
            {
                if (WanderTypeRef == EmeraldAISystem.WanderType.Destination)
                {
                    Debug.LogError("The AI ''" + gameObject.name + "'s'' Destination is not reachable. " +
                        "The AI's Wander Type has been set to Stationary. Please check the Destination and make sure it is on the NavMesh and is reachable.");
                    m_NavMeshAgent.stoppingDistance = StoppingDistance;
                    StartingDestination = transform.position + (transform.forward * StoppingDistance);
                    WanderTypeRef = EmeraldAISystem.WanderType.Stationary;
                }
                else if (WanderTypeRef == EmeraldAISystem.WanderType.Waypoints)
                {
                    Debug.LogError("The AI ''" + gameObject.name + "'s'' Waypoint #" + (WaypointIndex + 1) + " is not reachable. " +
                        "The AI's Wander Type has been set to Stationary. Please check the Waypoint #" + (WaypointIndex + 1) + " and make sure it is on the NavMesh and is reachable.");
                    m_NavMeshAgent.stoppingDistance = StoppingDistance;
                    StartingDestination = transform.position + (transform.forward * StoppingDistance);
                    WanderTypeRef = EmeraldAISystem.WanderType.Stationary;
                }
            }
            else if (path.status == NavMeshPathStatus.PathInvalid)
            {
                if (WanderTypeRef == EmeraldAISystem.WanderType.Destination)
                {
                    Debug.LogError("The AI ''" + gameObject.name + "'s'' Destination is not reachable. " +
                        "The AI's Wander Type has been set to Stationary. Please check the Destination and make sure it is on the NavMesh.");
                    m_NavMeshAgent.stoppingDistance = StoppingDistance;
                    StartingDestination = transform.position + (transform.forward * StoppingDistance);
                    WanderTypeRef = EmeraldAISystem.WanderType.Stationary;
                }
                else if (WanderTypeRef == EmeraldAISystem.WanderType.Waypoints)
                {
                    Debug.LogError("The AI ''" + gameObject.name + "'s'' Waypoint #" + (WaypointIndex + 1) + " is not reachable. " +
                        "The AI's Wander Type has been set to Stationary. Please check the Waypoint #" + (WaypointIndex + 1) + " and make sure it is on the NavMesh and is reachable.");
                    m_NavMeshAgent.stoppingDistance = StoppingDistance;
                    StartingDestination = transform.position + (transform.forward * StoppingDistance);
                    WanderTypeRef = EmeraldAISystem.WanderType.Stationary;
                }
            }
            else
            {
                Debug.Log("Path Invalid");
            }
        }

        void CheckAIRenderers()
        {
            if (OptimizedStateRef == OptimizedState.Inactive)
            {
                if (!Renderer1.isVisible && !Renderer2.isVisible && TotalLODsRef == TotalLODsEnum.Two)
                {
                    DeactivateTimer += Time.deltaTime;

                    if (UseDeactivateDelayRef == YesOrNo.Yes && DeactivateTimer >= DeactivateDelay || UseDeactivateDelayRef == YesOrNo.No)
                    {
                        Deactivate();
                    }
                }
                else if (!Renderer1.isVisible && !Renderer2.isVisible && !Renderer3.isVisible && TotalLODsRef == TotalLODsEnum.Three)
                {
                    DeactivateTimer += Time.deltaTime;

                    if (UseDeactivateDelayRef == YesOrNo.Yes && DeactivateTimer >= DeactivateDelay || UseDeactivateDelayRef == YesOrNo.No)
                    {
                        Deactivate();
                    }
                }
                else if (!Renderer1.isVisible && !Renderer2.isVisible && !Renderer3.isVisible && !Renderer4.isVisible && TotalLODsRef == TotalLODsEnum.Four)
                {
                    DeactivateTimer += Time.deltaTime;

                    if (UseDeactivateDelayRef == YesOrNo.Yes && DeactivateTimer >= DeactivateDelay || UseDeactivateDelayRef == YesOrNo.No)
                    {
                        Deactivate();
                    }
                }
            }
            else if (OptimizedStateRef == OptimizedState.Active)
            {
                if (TotalLODsRef == TotalLODsEnum.Two)
                {
                    if (Renderer1.isVisible || Renderer2.isVisible)
                    {
                        Activate();
                    }
                }
                else if (TotalLODsRef == TotalLODsEnum.Three)
                {
                    if (Renderer1.isVisible || Renderer2.isVisible || Renderer3.isVisible)
                    {
                        Activate();
                    }
                }
                else if (TotalLODsRef == TotalLODsEnum.Four)
                {
                    if (Renderer1.isVisible || Renderer2.isVisible || Renderer3.isVisible || Renderer4.isVisible)
                    {
                        Activate();
                    }
                }               
            }
        }

        void Update()
        {
            if (DisableAIWhenNotInViewRef == YesOrNo.Yes && HasMultipleLODsRef == YesOrNo.Yes)
            {
                CheckAIRenderers();
            }

            if (OptimizedStateRef == OptimizedState.Inactive || DisableAIWhenNotInViewRef == YesOrNo.No)
            {
                m_ProjectileCollisionPoint = new Vector3(transform.position.x, transform.position.y + ProjectileCollisionPointY, transform.position.z);

                if (DeathDelayActive)
                {
                    DeathDelayTimer += Time.deltaTime;

                    if (DeathDelayTimer > DeathDelay)
                    {
                        EmeraldBehaviorsComponent.DefaultState();
                    }
                }

                AIAgentActive = m_NavMeshAgent.enabled;
                if (AIAnimator.GetCurrentAnimatorClipInfo(0).Length != 0)
                {
                    CurrentAnimationClip = AIAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
                    EmeraldBehaviorsComponent.CheckAnimationStates();
                }

                //If our AI is not in combat, wander according to its Wander Type.
                if (AIAgentActive && CombatStateRef == CombatState.NotActive && !DeathDelayActive && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet)
                {
                    Wander();
                }
                else if (AIAgentActive && CombatStateRef == CombatState.NotActive && !DeathDelayActive)
                {                 
                    if (BehaviorRef == CurrentBehavior.Companion || BehaviorRef == CurrentBehavior.Pet)
                    {
                        if (CurrentFollowTarget != null)
                        {
                            FollowCompanionTarget();
                        }
                        else
                        {
                            Wander();
                        }
                    }
                }

                //If our AI is not moving, align it with the current surface
                if (!IsMoving && !IsDead)
                {
                    AlignAIStationary();
                }

                //Controls looking angle based on non-combat or during combat
                if (AIAgentActive)
                {
                    if (CombatStateRef == CombatState.NotActive)
                    {
                        if (m_NavMeshAgent.remainingDistance > m_NavMeshAgent.stoppingDistance)
                        {
                            AngleToTurn = 90;
                        }
                        else
                        {
                            AngleToTurn = NonCombatAngleToTurn;
                        }

                        LookAtLimit = NonCombatLookAtLimit;
                        AlignmentSpeed = NonCombatAlignmentSpeed;
                        WalkAnimationSpeed = NonCombatWalkAnimationSpeed;
                        RunAnimationSpeed = NonCombatRunAnimationSpeed;
                    }
                    else if (CombatStateRef == CombatState.Active)
                    {
                        if (m_NavMeshAgent.remainingDistance > m_NavMeshAgent.stoppingDistance)
                        {
                            AngleToTurn = 90;
                        }
                        else
                        {
                            AngleToTurn = CombatAngleToTurn;
                        }

                        LookAtLimit = CombatLookAtLimit;
                        AlignmentSpeed = CombatAlignmentSpeed;
                        WalkAnimationSpeed = CombatWalkAnimationSpeed;
                        RunAnimationSpeed = CombatRunAnimationSpeed;
                    }
                }

                //Handles our AI's Aggressive Behavior using the EmeraldBehaviorsComponent
                if (BehaviorRef == CurrentBehavior.Aggressive && CombatStateRef == CombatState.Active && AIAgentActive)
                {
                    EmeraldBehaviorsComponent.AggressiveBehavior();
                }
                //Handles our AI's Coward Behavior using the EmeraldBehaviorsComponent
                else if (BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef == ConfidenceType.Coward && CombatStateRef == CombatState.Active && AIAgentActive)
                {
                    EmeraldBehaviorsComponent.CowardBehavior();
                }
                //Handles our AI's Cautious Behavior using the EmeraldBehaviorsComponent
                else if (BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef != ConfidenceType.Coward && CombatStateRef == CombatState.Active && AIAgentActive)
                {
                    EmeraldBehaviorsComponent.CautiousBehavior();
                }
                //Handles our AI's Companion Behavior using the EmeraldBehaviorsComponent
                else if (BehaviorRef == CurrentBehavior.Companion && AIAgentActive)
                {
                    EmeraldBehaviorsComponent.CompanionBehavior();
                }

                //Calculates an AI's movement speed when using Root Motion
                if (AnimatorType == AnimatorTypeState.RootMotion && !IsDead)
                {
                    MoveAIRootMotion();
                }
                //Calculates an AI's movement speed when using NavMesh
                else if (AnimatorType == AnimatorTypeState.NavMeshDriven && !IsDead)
                {
                    MoveAINavMesh();
                }
            }
        }

        /// <summary>
        /// Moves our AI using Unity's NavMesh Agent 
        /// </summary>
        void MoveAINavMesh ()
        {
            float speed = m_NavMeshAgent.desiredVelocity.magnitude;
            Vector3 velocity = Quaternion.Inverse(transform.rotation) * m_NavMeshAgent.desiredVelocity;
            float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;

            if (AIAgentActive)
            {
                if (m_NavMeshAgent.remainingDistance > m_NavMeshAgent.stoppingDistance && !m_NavMeshAgent.pathPending && !GettingHit && !Attacking
                    && CurrentAnimationClip != PutAwayWeaponAnimation && CurrentAnimationClip != PullOutWeaponAnimation && CurrentAnimationClip != Emote1Animation)
                {
                    if (CurrentMovementState == MovementState.Run)
                    {
                        m_NavMeshAgent.speed = RunSpeed;
                    }
                    else if (CurrentMovementState == MovementState.Walk)
                    {
                        m_NavMeshAgent.speed = WalkSpeed;
                    }
                }
                else if (Attacking)
                {
                    m_NavMeshAgent.speed = 0;
                }

                AIAnimator.SetFloat("Speed", speed, DirectionDampTime, Time.deltaTime);
                AIAnimator.SetFloat("Direction", angle, DirectionDampTime, Time.deltaTime);
                AlignAIMoving();
            }
        }

        /// <summary>
        /// Moves our AI when using Root Motion
        /// </summary>
        void MoveAIRootMotion()
        {
            float speed = m_NavMeshAgent.desiredVelocity.magnitude;
            Vector3 velocity = Quaternion.Inverse(transform.rotation) * m_NavMeshAgent.desiredVelocity;
            float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;

            //Handles all of the AI's movement and speed calculations for Root Motion
            if (AIAgentActive)
            {
                if (m_NavMeshAgent.remainingDistance > m_NavMeshAgent.stoppingDistance && !m_NavMeshAgent.pathPending && !GettingHit && !Attacking
                    && CurrentAnimationClip != PutAwayWeaponAnimation && CurrentAnimationClip != PullOutWeaponAnimation && CurrentAnimationClip != Emote1Animation)
                {
                    AIAnimator.SetBool("Idle Active", false);
                    if (CurrentMovementState == MovementState.Run)
                    {
                        m_NavMeshAgent.speed = Mathf.Min(m_NavMeshAgent.speed + Acceleration * Time.deltaTime, RunAnimationSpeed);
                    }
                    else if (CurrentMovementState == MovementState.Walk)
                    {
                        if (WalkAnimationSpeed <= 1f)
                        {
                            m_NavMeshAgent.speed = Mathf.Max(m_NavMeshAgent.speed - 1 * Time.deltaTime, WalkAnimationSpeed * 0.1f);
                        }
                        else if (WalkAnimationSpeed > 1f)
                        {
                            m_NavMeshAgent.speed = Mathf.Max(m_NavMeshAgent.speed - 1 * Time.deltaTime, 0.1f);
                        }
                    }
                }
                else if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance && !m_NavMeshAgent.pathPending)
                {
                    m_NavMeshAgent.speed = Mathf.Max(m_NavMeshAgent.speed - Deceleration * Time.deltaTime, 0f);
                }
                else
                {
                    m_NavMeshAgent.speed = Mathf.Max(m_NavMeshAgent.speed - Deceleration * Time.deltaTime, 0f);
                }
            }

            AlignAIMoving();
            AIAnimator.SetFloat("Speed", speed, 0.3f, Time.deltaTime);
            AIAnimator.SetFloat("Direction", angle, DirectionDampTime, Time.deltaTime);
        }

        /// <summary>
        /// Aligns our AI to the current surface while moving
        /// </summary>
        void AlignAIMoving()
        {
            if (m_NavMeshAgent.velocity.magnitude > 0.05f)
            {
                if (!IsMoving)
                {
                    AIAnimator.SetBool("Turn Right", false);
                    AIAnimator.SetBool("Turn Left", false);
                    AIAnimator.ResetTrigger("Hit");
                    AIAnimator.SetBool("Idle Active", false);
                }

                IsMoving = true;

                if (AlignAIWithGroundRef == AlignAIWithGround.Yes)
                {
                    RayCastUpdateTimer += Time.deltaTime;

                    if (RayCastUpdateTimer >= RayCastUpdateSeconds)
                    {
                        RaycastHit HitDown;
                        if (Physics.Raycast(new Vector3(transform.localPosition.x, transform.localPosition.y + 0.25f, transform.localPosition.z), -transform.up, out HitDown, 2, AlignmentLayerMask))
                        {
                            if (HitDown.transform != this.transform)
                            {
                                SurfaceNormal = HitDown.normal;
                                SurfaceNormal.x = Mathf.Clamp(SurfaceNormal.x, -MaxNormalAngle, MaxNormalAngle);
                                SurfaceNormal.z = Mathf.Clamp(SurfaceNormal.z, -MaxNormalAngle, MaxNormalAngle);
                                RayCastUpdateTimer = 0;
                            }
                        }
                    }

                    NormalRotation = Quaternion.FromToRotation(transform.up, SurfaceNormal) * transform.rotation;
                    float angle = Quaternion.Angle(transform.rotation, NormalRotation);

                    //Only align the AI if the angle threshold is greater than 1.
                    if (angle > 1f)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, NormalRotation, Time.deltaTime * AlignmentSpeed);
                    }
                }
            }
            else
            {
                IsMoving = false;
            }
        }

        /// <summary>
        /// Aligns our AI to the current surface while stationary
        /// </summary>
        void AlignAIStationary()
        {
            RayCastUpdateTimer += Time.deltaTime;

            if (CombatStateRef == CombatState.Active && CurrentTarget && ConfidenceRef != ConfidenceType.Coward)
            {
                if (RayCastUpdateTimer >= RayCastUpdateSeconds && AlignAIWithGroundRef == AlignAIWithGround.Yes)
                {
                    RaycastHit HitDown;
                    if (Physics.Raycast(new Vector3(transform.localPosition.x, transform.localPosition.y + 0.25f, transform.localPosition.z), -transform.up, out HitDown, 2, AlignmentLayerMask))
                    {
                        if (HitDown.transform != this.transform)
                        {
                            SurfaceNormal = HitDown.normal;
                            SurfaceNormal.x = Mathf.Clamp(SurfaceNormal.x, -MaxNormalAngle, MaxNormalAngle);
                            SurfaceNormal.z = Mathf.Clamp(SurfaceNormal.z, -MaxNormalAngle, MaxNormalAngle);
                            RayCastUpdateTimer = 0;
                        }
                    }
                }
                RotateAIStationary();
            }
            else if (CombatStateRef == CombatState.NotActive || ConfidenceRef == ConfidenceType.Coward)
            {
                AngleCheckTimer += Time.deltaTime;

                //Once our AI has returned to its stantionary positon, adjust its position so it rotates to its original rotation.
                if (ReturnToStationaryPosition && AIAgentActive && m_NavMeshAgent.remainingDistance <= StoppingDistance)
                {
                    ReturnToStationaryPosition = false;
                }

                if (AngleCheckTimer > 0.1f)
                {
                    Vector3 Direction = new Vector3(m_NavMeshAgent.destination.x, 0, m_NavMeshAgent.destination.z) - new Vector3(transform.position.x, 0, transform.position.z);
                    float angle = Vector3.Angle(transform.forward, Direction);
                    DestinationDirection = Direction;

                    if (AlignAIWithGroundRef == AlignAIWithGround.Yes)
                    {
                        float RoationDifference = transform.localEulerAngles.x;
                        RoationDifference = (RoationDifference > 180) ? RoationDifference - 360 : RoationDifference;
                        DestinationAdjustedAngle = Mathf.Abs(angle) - Mathf.Abs(RoationDifference);
                    }
                    else if (AlignAIWithGroundRef == AlignAIWithGround.No)
                    {
                        DestinationAdjustedAngle = Mathf.Abs(angle);
                    }
                    AngleCheckTimer = 0;
                }

                if (DestinationAdjustedAngle >= AngleToTurn && DestinationDirection != Vector3.zero)
                {
                    RotateAIStationary();
                }
            }
        }

        /// <summary>
        /// Rotates our AI towards its target or destination
        /// </summary>
        void RotateAIStationary()
        {
            if (CombatStateRef == CombatState.Active && CurrentTarget && ConfidenceRef != ConfidenceType.Coward)
            {
                //Get the angle between the current target and the AI. If using the alignment feature,
                //adjust the angle to include the rotation difference of the AI's current surface angle.
                Vector3 Direction = new Vector3(CurrentTarget.position.x, 0, CurrentTarget.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
                float angle = Vector3.Angle(transform.forward, Direction);
                DestinationDirection = Direction;

                if (AlignAIWithGroundRef == AlignAIWithGround.Yes)
                {
                    float RoationDifference = transform.localEulerAngles.x;
                    RoationDifference = (RoationDifference > 180) ? RoationDifference - 360 : RoationDifference;
                    DestinationAdjustedAngle = Mathf.Abs(angle) - Mathf.Abs(RoationDifference);
                }
                else if (AlignAIWithGroundRef == AlignAIWithGround.No)
                {
                    DestinationAdjustedAngle = Mathf.Abs(angle);
                }

                if (DestinationAdjustedAngle >= AngleToTurn && DestinationDirection != Vector3.zero && !Attacking)
                {
                    if (AlignAIWithGroundRef == AlignAIWithGround.Yes)
                    {
                        Quaternion qTarget = Quaternion.LookRotation(DestinationDirection, Vector3.up);
                        Quaternion qGround = Quaternion.FromToRotation(Vector3.up, SurfaceNormal) * qTarget;
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, qGround, Time.deltaTime * StationaryTurningSpeedCombat);
                    }
                    else if (AlignAIWithGroundRef == AlignAIWithGround.No)
                    {
                        Quaternion qTarget = Quaternion.LookRotation(DestinationDirection, Vector3.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTarget, Time.deltaTime * StationaryTurningSpeedCombat);
                    }
                }
            }
            else if (AIAgentActive && m_NavMeshAgent.remainingDistance > StoppingDistance && ConfidenceRef == ConfidenceType.Coward)
            {
                if (DestinationAdjustedAngle >= AngleToTurn && DestinationDirection != Vector3.zero)
                {
                    Quaternion qTarget = Quaternion.LookRotation(DestinationDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, qTarget, Time.deltaTime * StationaryTurningSpeedNonCombat);
                }
            }

            if (CombatStateRef == CombatState.NotActive && DestinationAdjustedAngle >= AngleToTurn && DestinationDirection != Vector3.zero && AIAgentActive && m_NavMeshAgent.remainingDistance > m_NavMeshAgent.stoppingDistance
                || CombatStateRef == CombatState.Active && DestinationAdjustedAngle >= AngleToTurn && DestinationDirection != Vector3.zero && AIAgentActive && !Attacking)
            {
                Vector3 cross = Vector3.Cross(transform.rotation * Vector3.forward, Quaternion.LookRotation(DestinationDirection, Vector3.up) * Vector3.forward);

                if (cross.y < -AngleToTurn * 0.01f)
                {
                    AIAnimator.SetBool("Idle Active", false);
                    AIAnimator.SetBool("Turn Left", true);
                    AIAnimator.SetBool("Turn Right", false);
                }
                else if (cross.y > AngleToTurn * 0.01f)
                {
                    AIAnimator.SetBool("Idle Active", false);
                    AIAnimator.SetBool("Turn Right", true);
                    AIAnimator.SetBool("Turn Left", false);
                }
                else if (cross.y > -AngleToTurn * 0.01f)
                {
                    AIAnimator.SetBool("Idle Active", false);
                    AIAnimator.SetBool("Turn Left", true);
                    AIAnimator.SetBool("Turn Right", false);
                }
                else
                {
                    AIAnimator.SetBool("Turn Left", false);
                    AIAnimator.SetBool("Turn Right", false);
                }
            }
            else if (CombatStateRef == CombatState.Active)
            {
                AIAnimator.SetBool("Turn Left", false);
                AIAnimator.SetBool("Turn Right", false);
            }
        }

        /// <summary>
        /// Generates a random waypoint, once an AI reaches its destination
        /// </summary>
        public void Wander ()
        {
            if (WanderTypeRef == WanderType.Dynamic && m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                if (WaypointTimer == 0)
                {
                    AIAnimator.SetBool("Idle Active", true);
                }

                WaypointTimer += Time.deltaTime;

                if (WaypointTimer >= WaitTime)
                {
                    AIAnimator.SetBool("Idle Active", false);
                    GenerateWaypoint();
                    if (!m_IdleAnimaionIndexOverride)
                    {
                        AIAnimator.SetInteger("Idle Index", Random.Range(1, TotalIdleAnimations + 1));
                    }
                    WaitTime = Random.Range(MinimumWaitTime, MaximumWaitTime + 1);
                    WaypointTimer = 0;
                }
            }
            else if (WanderTypeRef == WanderType.Destination && m_NavMeshAgent.destination != SingleDestination && !AIReachedDestination)
            {
                if (m_NavMeshAgent.remainingDistance <= StoppingDistance)
                {
                    AIReachedDestination = true;
                    ReachedDestinationEvent.Invoke();
                }
            }
            else if (WanderTypeRef == WanderType.Waypoints && m_NavMeshAgent.destination != WaypointsList[WaypointIndex])
            {
                if (m_NavMeshAgent.remainingDistance <= StoppingDistance && !m_NavMeshAgent.pathPending)
                {
                    if (WaypointTypeRef == WaypointType.Random)
                    {
                        if (WaypointTimer <= 0.1f)
                        {
                            AIAnimator.SetBool("Idle Active", true);
                        }

                        WaypointTimer += Time.deltaTime;

                        if (WaypointTimer >= WaitTime)
                        {
                            AIAnimator.SetBool("Idle Active", false);
                            if (!m_IdleAnimaionIndexOverride)
                            {
                                AIAnimator.SetInteger("Idle Index", Random.Range(1, TotalIdleAnimations + 1));
                            }
                            WaitTime = Random.Range(MinimumWaitTime, MaximumWaitTime + 1);
                            WaypointTimer = 0;
                            NextWaypoint();
                        }
                    }
                    else if (WaypointTypeRef != WaypointType.Random)
                    {
                        NextWaypoint();
                    }
                }
            }
            else if (WanderTypeRef == WanderType.Stationary && !IsMoving)
            {
                StationaryIdleTimer += Time.deltaTime;
                if (StationaryIdleTimer >= StationaryIdleSeconds)
                {
                    AIAnimator.SetBool("Idle Active", true);
                    if (!m_IdleAnimaionIndexOverride)
                    {
                        AIAnimator.SetInteger("Idle Index", Random.Range(1, TotalIdleAnimations + 1));
                    }
                    StationaryIdleSeconds = Random.Range(StationaryIdleSecondsMin, StationaryIdleSecondsMax);
                    StationaryIdleTimer = 0;
                }
            }

            //Play an idle sound if the AI is not moving and the Idle Seconds have been met. 
            if (!IsMoving)
            {
                IdleSoundsTimer += Time.deltaTime;
                if (IdleSoundsTimer >= IdleSoundsSeconds)
                {
                    EmeraldEventsManagerComponent.PlayIdleSound();
                    IdleSoundsSeconds = Random.Range(IdleSoundsSecondsMin, IdleSoundsSecondsMax);
                    IdleSoundsTimer = 0;
                }
            }
            else
            {
                //If our AI moves, reset the Idle Sounds Timer so a sound doesn't play 
                //immediately on arrival of the next waypoint.
                IdleSoundsTimer = 0;
            }
        }

        /// <summary>
        /// Allows our Companion and Pet AI to follow their Follow Target
        /// </summary>
        public void FollowCompanionTarget ()
        {
            float DistanceFromFollower = Vector3.Distance(CurrentFollowTarget.position, transform.position);
            if (DistanceFromFollower > FollowingStoppingDistance && !EmoteAnimationActive)
            {
                m_NavMeshAgent.destination = CurrentFollowTarget.position;
            }
        }

        /// <summary>
        /// Handles our AI's waypoints when using the Waypoint Wander Type
        /// </summary>
        public void NextWaypoint()
        {
            if (WaypointsList.Count == 0)
                return;

            if (WaypointTypeRef != WaypointType.Random && WaypointsList.Count > 1)
            {
                WaypointIndex++;

                if (WaypointIndex == WaypointsList.Count)
                {
                    WaypointIndex = 0;

                    if (WaypointTypeRef == WaypointType.Reverse)
                    {
                        WaypointsList.Reverse();
                    }
                }

                if (m_NavMeshAgent.enabled)
                {
                    m_NavMeshAgent.destination = WaypointsList[WaypointIndex];
                }
            }
            else if (WaypointTypeRef == WaypointType.Random && WaypointsList.Count > 1)
            {
                WaypointTimer = 0;
                m_LastWaypointIndex = WaypointIndex;

                while (m_LastWaypointIndex == WaypointIndex)
                {
                    WaypointIndex = Random.Range(0, WaypointsList.Count);
                }

                if (m_NavMeshAgent.enabled)
                {
                    m_NavMeshAgent.destination = WaypointsList[WaypointIndex];
                }
            }

            //Check that our AI's path is valid.
            CheckPath(m_NavMeshAgent.destination);
        }

        /// <summary>
        /// If enabled, checks our dynamically generated waypoint to ensure it is far 
        /// enough way from the AI, as well as ensuring that the angle limit is met
        /// </summary>
        public void GenerateWaypoint()
        {
            int GenerateIndex = 0;

            while (DistanceFromDestination <= (m_NavMeshAgent.stoppingDistance + 2))
            {
                //Attempt to generate a new destination 10 times, if no new destination can be found, stop trying.
                if (GenerateIndex > 10)
                {
                    GenerateIndex = 0;
                    break;
                }

                NewDestination = StartingDestination + new Vector3(Random.insideUnitSphere.y, 0, Random.insideUnitSphere.z) * WanderRadius;

                RaycastHit HitDown;
                if (Physics.Raycast(new Vector3(NewDestination.x, NewDestination.y + 100, NewDestination.z), -transform.up, out HitDown))
                {
                    if (HitDown.transform != this.transform)
                    {
                        NewDestination = new Vector3(HitDown.point.x, HitDown.point.y, HitDown.point.z);

                        if (Vector3.Angle(Vector3.up, HitDown.normal) <= MaxSlopeLimit)
                        {
                            NewDestination = new Vector3(HitDown.point.x, HitDown.point.y, HitDown.point.z);
                            DistanceFromDestination = Vector3.Distance(NewDestination, transform.position);

                            if (DistanceFromDestination > m_NavMeshAgent.stoppingDistance + 2 && AIAgentActive)
                            {
                                NavMeshHit hit;

                                if (NavMesh.SamplePosition(NewDestination, out hit, 4, 1))
                                {
                                    AIAnimator.SetBool("Idle Active", false);
                                    m_NavMeshAgent.destination = hit.position;
                                }

                                DistanceFromDestination = 0;
                                GenerateIndex = 0;
                                break;
                            }
                        }
                    }
                }

                GenerateIndex++;
            }
        }

        /// <summary>
        /// Used for triggering a Ranged Attack with an Animation Event.
        /// </summary>
        void CreateEmeraldProjectile()
        {
            if (CurrentTarget != null)
            {
                float AdjustedAngle = TargetAngle();

                if (AdjustedAngle <= 75)
                {
                    if (AttackAnimationNumber == 1)
                    {
                        CurrentProjectile = EmeraldAIObjectPool.Spawn(Attack1Projectile, RangedAttackTransform.position, Quaternion.identity);
                    }
                    else if (AttackAnimationNumber == 2)
                    {
                        CurrentProjectile = EmeraldAIObjectPool.Spawn(Attack2Projectile, RangedAttackTransform.position, Quaternion.identity);
                    }
                    else if (AttackAnimationNumber == 3)
                    {
                        CurrentProjectile = EmeraldAIObjectPool.Spawn(Attack3Projectile, RangedAttackTransform.position, Quaternion.identity);
                    }
                    else if (UseRunAttacksRef == UseRunAttacks.Yes)
                    {
                        CurrentProjectile = EmeraldAIObjectPool.Spawn(RunAttackProjectile, RangedAttackTransform.position, Quaternion.identity);
                    }

                    CurrentProjectile.transform.SetParent(ObjectPool.transform);
                    CalculateRangedProjectile(CurrentProjectile);
                }
                else
                {
                    AIAnimator.ResetTrigger("Attack");
                    return;
                }
            }
            else
            {
                AIAnimator.ResetTrigger("Attack");
                return;
            }
        }

        void CalculateRangedProjectile(GameObject SentProjectile)
        {
            if (SentProjectile.GetComponent<EmeraldAIProjectile>() != null)
            {
                EmeraldAIProjectile Projectile = SentProjectile.GetComponent<EmeraldAIProjectile>();
                Projectile.TimeoutTimer = 0;
                Projectile.HeatSeekingFinished = false;
                Projectile.HeatSeekingTimer = 0;
                Projectile.ProjectileCurrentTarget = CurrentTarget;
                Projectile.HeatSeekingInitializeTimer = 0;
                Projectile.CollisionTimer = 0;
                Projectile.TimeoutTime = ProjectileTimeoutTime;
                Projectile.Collided = false;
                Projectile.ProjectileCollider.enabled = true;
                Projectile.EmeraldSystem = GetComponent<EmeraldAISystem>();

                if (CurrentTarget != null)
                {
                    ProjectileFirePosition = CurrentTarget.position;
                    Projectile.ProjectileDirection = (ProjectileFirePosition - Projectile.transform.position);
                }
            }
            else
            {
                Debug.Log("There is no EmeraldAIProjectile script attached to your " + SentProjectile.name + " GameObject. Please use the projectile settings located in the AI's Settings under the Combat section. Here, Emerald will be able to automatically apply said script.");
            }
        }

        /// <summary>
        /// Sends damage to the player or another AI triggered by an Animation Event
        /// </summary>
        public void SendEmeraldDamage()
        {
            AIAnimator.ResetTrigger("Hit");

            if (TargetEmerald != null && TargetEmerald.CurrentHealth <= 0)
            {
                lookWeight = 0f;
                ClearTarget();
                AIAnimator.ResetTrigger("Attack");
                EmeraldDetectionComponent.SearchForTarget();
            }

            if (CurrentTarget != null && m_NavMeshAgent.enabled)
            {
                if (TargetObstructedActionRef != TargetObstructedAction.StayStationary || WeaponTypeRef == WeaponType.Melee)
                {
                    m_NavMeshAgent.destination = CurrentTarget.position;
                }

                if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance || WeaponTypeRef == WeaponType.Ranged)
                {
                    float AdjustedAngle = TargetAngle();

                    if (AdjustedAngle <= MaxDamageAngle || WeaponTypeRef == WeaponType.Ranged)
                    {
                        if (TargetTypeRef == TargetType.Player)
                        {
                            //Send our damage to am external script here users can adjust it as needed. By default, the script damages Emerald's PlayerHealth script.
                            if (CurrentTarget.GetComponent<EmeraldAIPlayerDamage>() != null)
                            {
                                CurrentTarget.GetComponent<EmeraldAIPlayerDamage>().SendPlayerDamage(CurrentDamageAmount, this.transform, GetComponent<EmeraldAISystem>());
                                EmeraldEventsManagerComponent.PlayImpactSound();
                            }
                            else //If no EmeraldAIPlayerDamage script is detected on the player, add one.
                            {
                                CurrentTarget.gameObject.AddComponent<EmeraldAIPlayerDamage>();
                                CurrentTarget.GetComponent<EmeraldAIPlayerDamage>().SendPlayerDamage(CurrentDamageAmount, this.transform, GetComponent<EmeraldAISystem>());
                                EmeraldEventsManagerComponent.PlayImpactSound();
                            }
                        }
                        else if (TargetTypeRef == TargetType.AI && TargetEmerald != null)
                        {
                            TargetEmerald.Damage(CurrentDamageAmount, TargetType.AI, this.transform, SentRagdollForceAmount);

                            if (TargetEmerald.CurrentBlockingState != EmeraldAISystem.BlockingState.Blocking)
                            {
                                EmeraldEventsManagerComponent.PlayImpactSound();
                            }
                        }
                        else if (TargetTypeRef == TargetType.NonAITarget)
                        {
                            if (CurrentTarget.GetComponent<EmeraldAINonAIDamage>() != null)
                            {
                                CurrentTarget.GetComponent<EmeraldAINonAIDamage>().SendNonAIDamage(CurrentDamageAmount, this.transform);
                                EmeraldEventsManagerComponent.PlayImpactSound();
                            }
                            else
                            {
                                CurrentTarget.gameObject.AddComponent<EmeraldAINonAIDamage>();
                                CurrentTarget.GetComponent<EmeraldAINonAIDamage>().SendNonAIDamage(CurrentDamageAmount, this.transform);
                                EmeraldEventsManagerComponent.PlayImpactSound();
                            }
                        }

                        OnAttackEvent.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// Damages our AI and allows an AI to block and mitigate damage, if enabled. To use the ragdoll feature, all
        /// parameters need to be used where AttackerTransform is the current attacker.
        /// </summary>
        /// <param name="DamageAmount">Amount of damage caused during attack.</param>
        /// <param name="TypeOfTarget">The type of target who is causing the damage.</param>
        /// <param name="AttackerTransform">The transform of the current attacker.</param>
        /// <param name="RagdollForce">The amount of force to apply to this AI when they die. (Use Ragdoll must be enabled on this AI)</param>
        public void Damage (int DamageAmount, TargetType? TypeOfTarget = null, Transform AttackerTransform = null, int RagdollForce = 100)
        {
            if (CombatStateRef == CombatState.Active && AttackerTransform != CurrentTarget)
            {
                if (UseAggro == YesOrNo.Yes)
                {
                    CurrentAggroHits++;

                    if (CurrentAggroHits == TotalAggroHits)
                    {
                        if (AggroActionRef == AggroAction.LastAttacker)
                        {
                            lookWeight = 0f;
                            CurrentTarget = AttackerTransform;
                        }
                        else if (AggroActionRef == AggroAction.RandomAttacker)
                        {
                            EmeraldDetectionComponent.SearchForRandomTarget = true;
                            EmeraldDetectionComponent.SearchForTarget();
                        }
                        else if (AggroActionRef == AggroAction.ClosestAttacker)
                        {
                            EmeraldDetectionComponent.SearchForTarget();
                        }

                        if (CurrentTarget == null)
                        {
                            EmeraldDetectionComponent.SearchForTarget();
                        }

                        if (TypeOfTarget != null)
                        {
                            TargetTypeRef = (TargetType)TypeOfTarget;
                        }

                        if (TargetTypeRef == TargetType.AI && CurrentTarget != null)
                        {
                            TargetEmerald = CurrentTarget.GetComponent<EmeraldAISystem>();
                        }
                        else
                        {
                            TargetEmerald = null;
                        }

                        CurrentAggroHits = 0;
                    }
                }
            }

            //If our AI is hit and does not have a target, expand its detection to look for the damage source.
            if (CurrentTarget == null && CombatStateRef == CombatState.NotActive || DeathDelayActive || BehaviorRef == CurrentBehavior.Cautious && CurrentTarget != null && ConfidenceRef != ConfidenceType.Coward)
            {
                //Return, if our attacker is equal to the AI's follow target.
                if (CurrentFollowTarget != null && AttackerTransform == CurrentFollowTarget)
                {
                    if (BehaviorRef == CurrentBehavior.Companion || BehaviorRef == CurrentBehavior.Pet)
                    {
                        return;
                    }                 
                }

                if (BehaviorRef == CurrentBehavior.Aggressive ||
                    BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef != ConfidenceType.Coward ||
                    BehaviorRef == CurrentBehavior.Passive && ConfidenceRef != ConfidenceType.Coward)
                {
                    if (AttackerTransform) 
                    {
                        if (TypeOfTarget != null)
                        {
                            TargetTypeRef = (TargetType)TypeOfTarget;
                        }

                        if (TargetTypeRef == TargetType.AI || TargetTypeRef == TargetType.Player && AIAttacksPlayerRef != AIAttacksPlayer.Never || TargetTypeRef == TargetType.NonAITarget)
                        {
                            DeathDelayActive = false;
                            CurrentTarget = AttackerTransform;
                            m_NavMeshAgent.destination = CurrentTarget.position;
                            m_NavMeshAgent.stoppingDistance = AttackDistance;

                            if (BehaviorRef == CurrentBehavior.Passive || BehaviorRef == CurrentBehavior.Cautious)
                            {
                                if (ConfidenceRef != ConfidenceType.Coward)
                                {
                                    BehaviorRef = CurrentBehavior.Aggressive;
                                }
                            }

                            if (EnableDebugging == EmeraldAISystem.YesOrNo.Yes && DebugLogTargetsEnabled == EmeraldAISystem.YesOrNo.Yes && !DeathDelayActive)
                            {
                                if (CurrentTarget != null)
                                {
                                    Debug.Log("<b>" + "<color=green>" + gameObject.name + "'s Current Target: " + "</color>" + "<color=red>" + CurrentTarget.gameObject.name + "</color>" + "</b>");
                                }
                            }

                            if (TargetTypeRef == TargetType.AI)
                            {
                                TargetEmerald = CurrentTarget.GetComponent<EmeraldAISystem>();
                            }
                            else
                            {
                                TargetEmerald = null;
                            }

                            MaxChaseDistance = ExpandedChaseDistance;
                            EmeraldDetectionComponent.PreviousTarget = CurrentTarget;
                            EmeraldBehaviorsComponent.ActivateCombatState();
                        }
                    }
                    else
                    {
                        MaxChaseDistance = ExpandedChaseDistance;
                        DetectionRadius = ExpandedDetectionRadius;
                        AICollider.radius = ExpandedDetectionRadius;
                        fieldOfViewAngle = ExpandedFieldOfViewAngle;
                        EmeraldDetectionComponent.SearchForTarget();
                    }
                }
                else if (BehaviorRef == CurrentBehavior.Cautious && ConfidenceRef == ConfidenceType.Coward || BehaviorRef == CurrentBehavior.Passive && ConfidenceRef == ConfidenceType.Coward)
                {
                    if (AttackerTransform.CompareTag(PlayerTag))
                    {
                        AIAttacksPlayerRef = AIAttacksPlayer.Always;
                    }
                    MaxChaseDistance = ExpandedChaseDistance;
                    DetectionRadius = ExpandedDetectionRadius;
                    AICollider.radius = ExpandedDetectionRadius;
                    fieldOfViewAngle = ExpandedFieldOfViewAngle;
                    EmeraldDetectionComponent.SearchForTarget();
                }
            }

            if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null)
            {
                DamageReceived = DamageAmount;
                CombatText();
            }

            if (AttackerTransform != null)
            {
                ForceTransform = AttackerTransform;
                BlockAngle = Vector3.Angle(new Vector3(AttackerTransform.position.x, 0, AttackerTransform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), transform.forward);
                AdjustedBlockAngle = Mathf.Abs(BlockAngle);
            }

            if (AIAnimator.GetBool("Blocking") && AIAnimator.GetBool("Attack"))
            {
                CurrentBlockingState = BlockingState.NotBlocking;
                AIAnimator.SetBool("Blocking", false);
            }

            //Don't allow a Companion to receive damage from its own follower
            if (AttackerTransform != CurrentFollowTarget && BehaviorRef == CurrentBehavior.Companion || BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet)
            {
                if (!AIAnimator.GetBool("Blocking") || AdjustedBlockAngle > MaxBlockAngle)
                {
                    CurrentBlockingState = BlockingState.NotBlocking;
                    AIAnimator.SetBool("Blocking", false);
                    CurrentHealth -= DamageAmount;
                    DamageEvent.Invoke(); //Invoke our AI's Damage Event

                    if (UseBloodEffectRef == UseBloodEffect.Yes && BloodEffect != null && !DamageEffectInhibitor)
                    {
                        GameObject SpawnedBlood = EmeraldAIObjectPool.Spawn(BloodEffect, Vector3.zero, transform.rotation) as GameObject;
                        SpawnedBlood.transform.SetParent(transform);

                        if (BloodEffectPositionTypeRef == BloodEffectPositionType.BaseTransform)
                        {
                            SpawnedBlood.transform.position = transform.position + BloodPosOffset;
                        }
                        else if (BloodEffectPositionTypeRef == BloodEffectPositionType.HitTransform)
                        {
                            SpawnedBlood.transform.position = m_ProjectileCollisionPoint + BloodPosOffset;
                        }
                    }

                    DamageEffectInhibitor = false;
                    EmeraldEventsManagerComponent.PlayInjuredSound();

                    if (CombatStateRef == CombatState.NotActive || ConfidenceRef == ConfidenceType.Coward)
                    {
                        AIAnimator.SetInteger("Hit Index", Random.Range(1, TotalHitAnimations + 1));
                    }
                    else if (CombatStateRef == CombatState.Active && ConfidenceRef != ConfidenceType.Coward)
                    {
                        AIAnimator.SetInteger("Hit Index", Random.Range(1, TotalCombatHitAnimations + 1));
                    }

                    if (!IsMoving && !BackingUp && !Attacking && !GettingHit && !AIAnimator.GetBool("Hit"))
                    {
                        AIAnimator.ResetTrigger("Hit");
                        AIAnimator.ResetTrigger("Attack");
                        AIAnimator.SetTrigger("Hit");
                    }
                }
                else if (AIAnimator.GetBool("Blocking") && CurrentBlockingState == BlockingState.Blocking && AdjustedBlockAngle <= MaxBlockAngle)
                {
                    float BlockDamage = DamageAmount * ((MitigationAmount - 1) * 0.01f);
                    CurrentHealth -= (int)BlockDamage;
                    AIAnimator.ResetTrigger("Hit");
                    AIAnimator.ResetTrigger("Attack");
                    AIAnimator.SetTrigger("Hit");
                    EmeraldEventsManagerComponent.PlayBlockSound();
                }
            }

            //Make our Brave AI flee if its health reaches the proper amount
            if (BehaviorRef == CurrentBehavior.Aggressive && ConfidenceRef == ConfidenceType.Brave)
            {
                if (((float)CurrentHealth / (float)StartingHealth) <= ((float)HealthPercentageToFlee * 0.01f))
                {
                    EmeraldEventsManagerComponent.ChangeBehavior(CurrentBehavior.Cautious);
                    EmeraldEventsManagerComponent.ChangeConfidence(ConfidenceType.Coward);
                }
            }

            if (CurrentHealth <= 0)
            {
                ReceivedRagdollForceAmount = RagdollForce;
                EmeraldBehaviorsComponent.DeadState();
            }
        }

        /// <summary>
        /// Clears the AI's target
        /// </summary>
        public void ClearTarget()
        {
            CurrentTarget = null;
            LineOfSightTargets.Clear();
            potentialTargets.Clear();
            TargetEmerald = null;
        }

        /// <summary>
        /// Controls when an AI is deactivated while using the optimization system
        /// </summary>
        public void Deactivate()
        {
            if (CurrentTarget == null && !ReturningToStartInProgress &&
                BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet)
            {
                AICollider.enabled = false;
                AIBoxCollider.enabled = false;
                EmeraldDetectionComponent.enabled = false;
                OptimizedStateRef = OptimizedState.Active;
                DeactivateTimer = 0;

                if (HealthBarCanvas != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet)
                {
                    SetUI(false);
                }

                if (UseCombatTextRef == UseCombatText.Yes && CombatTextObject != null && BehaviorRef != CurrentBehavior.Companion && BehaviorRef != CurrentBehavior.Pet)
                {
                    CombatTextParent.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Controls when an AI is activated while using the optimization system
        /// </summary>
        public void Activate()
        {
            if (CurrentHealth > 0)
            {
                AICollider.enabled = true;
                AIBoxCollider.enabled = true;
                EmeraldDetectionComponent.enabled = true;
                OptimizedStateRef = OptimizedState.Inactive;
            }
        }

        /// <summary>
        /// Gets our AI's damage amount depending on the current attack animation
        /// </summary>
        public void GetDamageAmount()
        {
            if (RandomizeDamageRef == RandomizeDamage.Yes)
            {
                if (AttackAnimationNumber == 1)
                {
                    CurrentDamageAmount = Random.Range(MinimumDamageAmount1, MaximumDamageAmount1 + 1);
                }
                else if (AttackAnimationNumber == 2)
                {
                    CurrentDamageAmount = Random.Range(MinimumDamageAmount2, MaximumDamageAmount2 + 1);
                }
                else if (AttackAnimationNumber == 3)
                {
                    CurrentDamageAmount = Random.Range(MinimumDamageAmount3, MaximumDamageAmount3 + 1);
                }
            }
            else if (RandomizeDamageRef == RandomizeDamage.No)
            {
                if (AttackAnimationNumber == 1)
                {
                    CurrentDamageAmount = DamageAmount1;
                }
                else if (AttackAnimationNumber == 2)
                {
                    CurrentDamageAmount = DamageAmount2;
                }
                else if (AttackAnimationNumber == 3)
                {
                    CurrentDamageAmount = DamageAmount3;
                }
            }
        }

        //Handles the Combat Text system, if enabled. If an AI has a combat text enabled before
        //a previous text is fully faded, immediately disable it and enable a new one.
        void CombatText()
        {
            foreach (TextMesh T in CombatTextList)
            {
                if (T.gameObject.activeSelf)
                {
                    T.gameObject.SetActive(false);
                }
            }
            CombatTextList[CombatTextIndex].gameObject.SetActive(true);
            CombatTextList[CombatTextIndex].text = DamageReceived.ToString();
            CombatTextIndex++;
            if (CombatTextIndex == CombatTextList.Count)
            {
                CombatTextIndex = 0;
            }
        }

        public void SetUI(bool Enabled)
        {
            HealthBarCanvasRef.enabled = Enabled;
            if (CreateHealthBarsRef == CreateHealthBars.Yes)
            {
                HealthBar.SetActive(Enabled);

                if (DisplayAILevelRef == DisplayAILevel.Yes)
                {
                    TextLevel.gameObject.SetActive(Enabled);
                }
            }

            if (DisplayAINameRef == DisplayAIName.Yes)
            {
                TextName.gameObject.SetActive(Enabled);
            }
        }

        public float TargetAngle ()
        {
            Vector3 Direction = new Vector3(CurrentTarget.position.x, 0, CurrentTarget.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
            float angle = Vector3.Angle(transform.forward, Direction);
            float RoationDifference = transform.localEulerAngles.x;
            RoationDifference = (RoationDifference > 180) ? RoationDifference - 360 : RoationDifference;
            float AdjustedAngle = Mathf.Abs(angle) - Mathf.Abs(RoationDifference);
            return AdjustedAngle;
        }
    }
}