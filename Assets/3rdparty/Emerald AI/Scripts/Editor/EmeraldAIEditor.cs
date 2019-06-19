using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEditorInternal;

namespace EmeraldAI.Utility
{
    [System.Serializable]
    [CustomEditor(typeof(EmeraldAISystem))]
    [CanEditMultipleObjects]
    public class EmeraldAIEditor : Editor
    {
        #region Serialized Properties
        //Head Look
        SerializedProperty HeadLookWeightCombatProp, BodyLookWeightCombatProp, HeadLookWeightNonCombatProp, BodyLookWeightNonCombatProp, MaxLookAtDistanceProp, HeadLookYOffsetProp,
            UseHeadLookProp, LookSmootherProp, NonCombatLookAtLimitProp, CombatLookAtLimitProp, HeadTransformProp;

        //System
        SerializedProperty AnimationListsChangedProp;

        //ints
        SerializedProperty TabNumberProp, TemperamentTabNumberProp, DetectionTagsTabNumberProp, AnimationTabNumberProp, SoundTabNumberProp, MovementTabNumberProp, fieldOfViewAngleProp, BackUpOddsProp,
        DetectionRadiusProp, WanderRadiusProp, MinimumWaitTimeProp, MaximumWaitTimeProp, WalkSpeedProp, RunSpeedProp, StartingHealthProp, MinimumDamageAmount1Prop, ExpandedChaseDistanceProp,
        MinimumDamageAmount2Prop, MinimumDamageAmount3Prop, MaximumDamageAmount1Prop, MaximumDamageAmount2Prop, MaximumDamageAmount3Prop, MinimumDamageAmountRunProp, TotalAggroHitsProp, SecondsToDisableProp,
            MaximumDamageAmountRunProp, DamageAmount1Prop, DamageAmount2Prop, DamageAmount3Prop, DamageAmountRunProp, StationaryTurningSpeedCombatProp, StationaryTurningSpeedNonCombatProp, BackupDistanceProp,
            MinimumFollowWaitTimeProp, MaximumFollowWaitTimeProp, MaxChaseDistanceProp, HealthPercentageToFleeProp, DeactivateDelayProp, HealthRegRateProp, RegenerateAmountProp, MaxSlopeLimitProp,
            DeathDelayMinProp, DeathDelayMaxProp, CurrentFactionProp, ExpandedFieldOfViewAngleProp, ExpandedDetectionRadiusProp, StationaryIdleSecondsMinProp, StationaryIdleSecondsMaxProp, CautiousSecondsProp,
            IdleSoundsSecondsMinProp, IdleSoundsSecondsMaxProp, SentRagdollForceProp, ObstructionSecondsProp, FactionRelation1Prop, FactionRelation2Prop, FactionRelation3Prop, EventTabNumberProp,
            FactionRelation4Prop, FactionRelation5Prop, Attack1ProjectileSpeedProp, Attack2ProjectileSpeedProp, Attack3ProjectileSpeedProp, RunAttackProjectileSpeedProp, CombatTabNumberProp;

        //floats
        SerializedProperty MinimumAttackSpeedProp, MaximumAttackSpeedProp, MinimumRunAttackSpeedProp, MaximumRunAttackSpeedProp, StoppingDistanceProp, FollowingStoppingDistanceProp,
            RunAttackDistanceProp, AgentRadiusProp, AgentBaseOffsetProp, AgentTurnSpeedProp, AgentAccelerationProp, MaxNormalAngleProp, NonCombatAlignSpeedProp, CombatAlignSpeedProp,
            NonCombatRunAnimationSpeedProp, CombatRunAnimationSpeedProp, IdleWarningAnimationSpeedProp, IdleNonCombatAnimationSpeedProp, StartingCombatTextSizeProp, EndingCombatTextSizeProp,
            IdleCombatAnimationSpeedProp, NonCombatWalkAnimationSpeedProp, CombatWalkAnimationSpeedProp, MaxDamageAngleProp, WalkFootstepVolumeProp, RunFootstepVolumeProp, BlockVolumeProp,
            TurnLeftAnimationSpeedProp, TurnRightAnimationSpeedProp, CombatTurnLeftAnimationSpeedProp, CombatTurnRightAnimationSpeedProp, AttackVolumeProp, DeathVolumeProp, IdleVolumeProp,
            NonCombatAngleToTurnProp, CombatAngleToTurnProp, AttackDistanceProp, ProjectileTimeoutTimeProp, InjuredVolumeProp, ImpactVolumeProp, EquipVolumeProp, UnequipVolumeProp,
            Attack1CollisionSecondsProp, Attack2CollisionSecondsProp, Attack3CollisionSecondsProp, RunAttackCollisionSecondsProp, Effect1TimeoutSecondsProp, Effect2TimeoutSecondsProp,
            Effect3TimeoutSecondsProp, RunEffectTimeoutSecondsProp, BloodEffectTimeoutSecondsProp, TooCloseDistanceProp, PlayerYOffsetProp, MitigationAmountProp, WarningVolumeProp,
            ProjectileCollisionPointYProp, Attack1HeatSeekingSecondsProp, Attack2HeatSeekingSecondsProp, Attack3HeatSeekingSecondsProp, RunAttackHeatSeekingSecondsProp;

        //enums
        SerializedProperty BehaviorProp, ConfidenceProp, RandomizeDamageProp, DetectionTypeProp, MaxChaseDistanceTypeProp, CombatTypeProp, CreateHealthBarsProp, UseCombatTextProp,
            CustomizeHealthBarProp, UseCustomFontProp, AnimateCombatTextProp, DisplayAINameProp, DisplayAITitleProp, DisplayAILevelProp, OpposingFactionsEnumProp, RefillHealthTypeProp,
            WanderTypeProp, WaypointTypeProp, AlignAIWithGroundProp, CurrentMovementStateProp, UseBloodEffectProp, UseWarningAnimationProp, TotalLODsProp, HasMultipleLODsProp,
            AlignAIOnStartProp, AlignmentQualityProp, PickTargetMethodProp, AIAttacksPlayerProp, UseNonAITagProp, BloodEffectPositionTypeProp, AggroActionProp,
            WeaponTypeProp, UseRunAttacksProp, ObstructionDetectionQualityProp, AvoidanceQualityProp, UseBlockingProp, BlockingOddsProp, UseAggroProp, SpawnedWithCruxProp,
            UseEquipAnimationProp, AnimatorTypeProp, UseWeaponObjectProp, UseHitAnimationsProp, TargetObstructedActionProp, EnableDebuggingPop, DrawRaycastsEnabledProp, UseAIAvoidanceProp,
            DebugLogTargetsEnabledProp, DebugLogObstructionsEnabledProp, Attack1HeatSeekingProp, Attack2HeatSeekingProp, Attack3HeatSeekingProp, RunAttackHeatSeekingProp,
            ArrowProjectileAttack1Prop, ArrowProjectileAttack2Prop, ArrowProjectileAttack3Prop, ArrowProjectileRunAttackProp, Attack1EffectOnCollisionProp, Attack2EffectOnCollisionProp,
            Attack3EffectOnCollisionProp, RunAttackEffectOnCollisionProp, Attack1SoundOnCollisionProp, Attack2SoundOnCollisionProp, Attack3SoundOnCollisionProp, RunAttackSoundOnCollisionProp,
            UseRandomRotationOnStartProp, UseDeactivateDelayProp, DisableAIWhenNotInViewProp, DeathTypeRefProp, UseDroppableWeaponProp, BackupTypeProp, AutoEnableAnimatePhysicsProp;

        //strings
        SerializedProperty AINameProp, AITitleProp, AILevelProp, PlayerTagProp, FollowTagProp, UITagProp, NonAITagProp, EmeraldTagProp, RagdollTagProp;

        //objects
        SerializedProperty HealthBarImageProp, HealthBarBackgroundImageProp, CombatTextFontProp, CombatTextPosProp, BloodEffectProp, Renderer1Prop, Renderer2Prop, Renderer3Prop, 
            Renderer4Prop, Attack1ProjectileProp, Attack2ProjectileProp, Attack3ProjectileProp, RunAttackProjectileProp, RangedAttackTransformProp, SheatheWeaponProp, UnsheatheWeaponProp,
            WeaponObjectProp, Attack1CollisionEffectProp, Attack2CollisionEffectProp, Attack3CollisionEffectProp, Attack1ImpactSoundProp, Attack2ImpactSoundProp, Attack3ImpactSoundProp,
            RunAttackCollisionEffectProp, RunAttackImpactSoundProp, AIRendererProp, DroppableWeaponObjectProp, BlockIdleAnimationProp, BlockHitAnimationProp;

        //vectors & Layer Masks
        SerializedProperty HealthBarPosProp, NameTextSizeProp, HealthBarScaleProp, BloodPosOffsetProp, AINamePosProp, DetectionLayerMaskProp, ObstructionDetectionLayerMaskProp,
            AlignmentLayerMaskProp, AIAvoidanceLayerMaskProp;

        //color
        SerializedProperty HealthBarColorProp, HealthBarBackgroundColorProp, CombatTextColorProp, NameTextColorProp, LevelTextColorProp;

        //Factions
        SerializedProperty OpposingFaction1Prop, OpposingFaction2Prop, OpposingFaction3Prop, OpposingFaction4Prop, OpposingFaction5Prop;

        //bools
        SerializedProperty HealthBarsFoldoutProp, CombatTextFoldoutProp, NameTextFoldoutProp, AnimationsUpdatedProp, BehaviorFoldout, ConfidenceFoldout, WanderFoldout, CombatStyleFoldout,
            WaypointsFoldout, Projectile1UpdatedProp, Projectile2UpdatedProp, Projectile3UpdatedProp, RunProjectileUpdatedProp, BloodEffectUpdatedProp, WalkFoldout, RunFoldout,
            TurnFoldout, CombatWalkFoldout, CombatRunFoldout, CombatTurnFoldout;

        //Animations
        SerializedProperty WalkLeftProp, WalkStraightProp, WalkRightProp, CombatWalkLeftProp, CombatWalkStraightProp, CombatWalkRightProp, WalkBackProp,
            RunLeftProp, RunStraightProp, RunRightProp, CombatRunLeftProp, CombatRunStraightProp, CombatRunRightProp, IdleWarningProp, IdleCombatProp, IdleNonCombatProp,
            TurnLeftProp, TurnRightProp, CombatTurnLeftProp, CombatTurnRightProp, RunAttackProp, PutAwayWeaponAnimationProp, PullOutWeaponAnimationProp;

        //Mirror Bools
        SerializedProperty MirrorWalkLeftProp, MirrorWalkRightProp, MirrorRunLeftProp, MirrorRunRightProp, MirrorCombatWalkLeftProp, MirrorCombatWalkRightProp, MirrorCombatRunLeftProp,
            MirrorCombatRunRightProp, MirrorCombatTurnLeftProp, MirrorCombatTurnRightProp, MirrorTurnLeftProp, MirrorTurnRightProp, ReverseWalkAnimationProp;

        //Events
        SerializedProperty DeathEventProp, DamageEventProp, ReachedDestinationEventProp, OnStartEventProp, OnAttackEventProp, OnFleeEventProp, OnStartCombatEventProp, OnEnabledEventProp;

        //Sound lists
        ReorderableList AttackSoundsList, InjuredSoundsList, WarningSoundsList, DeathSoundsList, FootStepSoundsList, IdleSoundsList, ImpactSoundsList, BlockSoundsList;

        //Animation Lists
        ReorderableList HitAnimationList, CombatHitAnimationList, IdleAnimationList, AttackAnimationList, RunAttackAnimationList, DeathAnimationList, EmoteAnimationList, InteractSoundsList,
            ItemList;
        #endregion

        Texture TemperamentIcon, SettingsIcon, DetectTagsIcon, UIIcon, SoundIcon, WaypointEditorIcon, AnimationIcon, DocumentationIcon;

        void OnEnable()
        {
            EmeraldAISystem self = (EmeraldAISystem)target;
            LoadFactionData();

            if (self.m_ProjectileCollisionPoint == Vector3.zero)
            {
                self.m_ProjectileCollisionPoint = self.transform.position;
            }

            #region Serialized Properties
            //Head Look
            HeadLookWeightCombatProp = serializedObject.FindProperty("HeadLookWeightCombat");
            BodyLookWeightCombatProp = serializedObject.FindProperty("BodyLookWeightCombat");
            HeadLookWeightNonCombatProp = serializedObject.FindProperty("HeadLookWeightNonCombat");
            BodyLookWeightNonCombatProp = serializedObject.FindProperty("BodyLookWeightNonCombat");
            MaxLookAtDistanceProp = serializedObject.FindProperty("MaxLookAtDistance");
            HeadLookYOffsetProp = serializedObject.FindProperty("HeadLookYOffset");
            UseHeadLookProp = serializedObject.FindProperty("UseHeadLookRef");
            LookSmootherProp = serializedObject.FindProperty("LookSmoother");
            NonCombatLookAtLimitProp = serializedObject.FindProperty("NonCombatLookAtLimit");
            CombatLookAtLimitProp = serializedObject.FindProperty("CombatLookAtLimit");
            HeadTransformProp = serializedObject.FindProperty("HeadTransform");

            //System
            AnimationListsChangedProp = serializedObject.FindProperty("AnimationListsChanged");

            //ints
            TabNumberProp = serializedObject.FindProperty("TabNumber");
            TemperamentTabNumberProp = serializedObject.FindProperty("TemperamentTabNumber");
            DetectionTagsTabNumberProp = serializedObject.FindProperty("DetectionTagsTabNumber");
            AnimationTabNumberProp = serializedObject.FindProperty("AnimationTabNumber");
            SoundTabNumberProp = serializedObject.FindProperty("SoundTabNumber");
            MovementTabNumberProp = serializedObject.FindProperty("MovementTabNumber");
            CombatTabNumberProp = serializedObject.FindProperty("CombatTabNumber");
            EventTabNumberProp = serializedObject.FindProperty("EventTabNumber");
            fieldOfViewAngleProp = serializedObject.FindProperty("fieldOfViewAngle");
            DetectionRadiusProp = serializedObject.FindProperty("DetectionRadius");
            WanderRadiusProp = serializedObject.FindProperty("WanderRadius");
            MinimumWaitTimeProp = serializedObject.FindProperty("MinimumWaitTime");
            MaximumWaitTimeProp = serializedObject.FindProperty("MaximumWaitTime");
            WalkSpeedProp = serializedObject.FindProperty("WalkSpeed");
            RunSpeedProp = serializedObject.FindProperty("RunSpeed");
            StartingHealthProp = serializedObject.FindProperty("StartingHealth");
            MinimumDamageAmount1Prop = serializedObject.FindProperty("MinimumDamageAmount1");
            MaximumDamageAmount1Prop = serializedObject.FindProperty("MaximumDamageAmount1");
            MinimumDamageAmount2Prop = serializedObject.FindProperty("MinimumDamageAmount2");
            MaximumDamageAmount2Prop = serializedObject.FindProperty("MaximumDamageAmount2");
            MinimumDamageAmount3Prop = serializedObject.FindProperty("MinimumDamageAmount3");
            MaximumDamageAmount3Prop = serializedObject.FindProperty("MaximumDamageAmount3");
            MinimumDamageAmountRunProp = serializedObject.FindProperty("MinimumDamageAmountRun");
            MaximumDamageAmountRunProp = serializedObject.FindProperty("MaximumDamageAmountRun");
            DamageAmount1Prop = serializedObject.FindProperty("DamageAmount1");
            DamageAmount2Prop = serializedObject.FindProperty("DamageAmount2");
            DamageAmount3Prop = serializedObject.FindProperty("DamageAmount3");
            DamageAmountRunProp = serializedObject.FindProperty("DamageAmountRun");
            StationaryTurningSpeedNonCombatProp = serializedObject.FindProperty("StationaryTurningSpeedNonCombat");
            StationaryTurningSpeedCombatProp = serializedObject.FindProperty("StationaryTurningSpeedCombat");
            MinimumFollowWaitTimeProp = serializedObject.FindProperty("MinimumFollowWaitTime");
            MaximumFollowWaitTimeProp = serializedObject.FindProperty("MaximumFollowWaitTime");
            MaxChaseDistanceProp = serializedObject.FindProperty("MaxChaseDistance");
            HealthPercentageToFleeProp = serializedObject.FindProperty("HealthPercentageToFlee");
            DeactivateDelayProp = serializedObject.FindProperty("DeactivateDelay");
            RegenerateAmountProp = serializedObject.FindProperty("RegenerateAmount");
            DeathDelayMinProp = serializedObject.FindProperty("DeathDelayMin");
            DeathDelayMaxProp = serializedObject.FindProperty("DeathDelayMax");
            CurrentFactionProp = serializedObject.FindProperty("CurrentFaction");
            StationaryIdleSecondsMinProp = serializedObject.FindProperty("StationaryIdleSecondsMin");
            StationaryIdleSecondsMaxProp = serializedObject.FindProperty("StationaryIdleSecondsMax");
            IdleSoundsSecondsMinProp = serializedObject.FindProperty("IdleSoundsSecondsMin");
            IdleSoundsSecondsMaxProp = serializedObject.FindProperty("IdleSoundsSecondsMax");
            SentRagdollForceProp = serializedObject.FindProperty("SentRagdollForceAmount");
            ObstructionSecondsProp = serializedObject.FindProperty("ObstructionSeconds");
            TotalAggroHitsProp = serializedObject.FindProperty("TotalAggroHits");
            MaxSlopeLimitProp = serializedObject.FindProperty("MaxSlopeLimit");
            SecondsToDisableProp = serializedObject.FindProperty("SecondsToDisable");
            BackUpOddsProp = serializedObject.FindProperty("BackupOdds");
            BackupDistanceProp = serializedObject.FindProperty("BackupDistance");
            CautiousSecondsProp = serializedObject.FindProperty("CautiousSeconds");

            FactionRelation1Prop = serializedObject.FindProperty("FactionRelation1");
            FactionRelation2Prop = serializedObject.FindProperty("FactionRelation2");
            FactionRelation3Prop = serializedObject.FindProperty("FactionRelation3");
            FactionRelation4Prop = serializedObject.FindProperty("FactionRelation4");
            FactionRelation5Prop = serializedObject.FindProperty("FactionRelation5");

            Attack1ProjectileSpeedProp = serializedObject.FindProperty("Attack1ProjectileSpeed");
            Attack2ProjectileSpeedProp = serializedObject.FindProperty("Attack2ProjectileSpeed");
            Attack3ProjectileSpeedProp = serializedObject.FindProperty("Attack3ProjectileSpeed");
            RunAttackProjectileSpeedProp = serializedObject.FindProperty("RunAttackProjectileSpeed");

            //floats
            MinimumAttackSpeedProp = serializedObject.FindProperty("MinimumAttackSpeed");
            MaximumAttackSpeedProp = serializedObject.FindProperty("MaximumAttackSpeed");
            MinimumRunAttackSpeedProp = serializedObject.FindProperty("MinimumRunAttackSpeed");
            MaximumRunAttackSpeedProp = serializedObject.FindProperty("MaximumRunAttackSpeed");
            StoppingDistanceProp = serializedObject.FindProperty("StoppingDistance");
            FollowingStoppingDistanceProp = serializedObject.FindProperty("FollowingStoppingDistance");
            RunAttackDistanceProp = serializedObject.FindProperty("RunAttackDistance");
            AgentRadiusProp = serializedObject.FindProperty("AgentRadius");
            AgentBaseOffsetProp = serializedObject.FindProperty("AgentBaseOffset");
            AgentTurnSpeedProp = serializedObject.FindProperty("AgentTurnSpeed");
            AgentAccelerationProp = serializedObject.FindProperty("AgentAcceleration");
            MaxNormalAngleProp = serializedObject.FindProperty("MaxNormalAngleEditor");
            NonCombatAlignSpeedProp = serializedObject.FindProperty("NonCombatAlignmentSpeed");
            CombatAlignSpeedProp = serializedObject.FindProperty("CombatAlignmentSpeed");
            IdleWarningAnimationSpeedProp = serializedObject.FindProperty("IdleWarningAnimationSpeed");
            IdleCombatAnimationSpeedProp = serializedObject.FindProperty("IdleCombatAnimationSpeed");
            IdleNonCombatAnimationSpeedProp = serializedObject.FindProperty("IdleNonCombatAnimationSpeed");
            NonCombatRunAnimationSpeedProp = serializedObject.FindProperty("NonCombatRunAnimationSpeed");
            CombatRunAnimationSpeedProp = serializedObject.FindProperty("CombatRunAnimationSpeed");
            NonCombatWalkAnimationSpeedProp = serializedObject.FindProperty("NonCombatWalkAnimationSpeed");
            CombatWalkAnimationSpeedProp = serializedObject.FindProperty("CombatWalkAnimationSpeed");
            TurnLeftAnimationSpeedProp = serializedObject.FindProperty("TurnLeftAnimationSpeed");
            TurnRightAnimationSpeedProp = serializedObject.FindProperty("TurnRightAnimationSpeed");
            CombatTurnLeftAnimationSpeedProp = serializedObject.FindProperty("CombatTurnLeftAnimationSpeed");
            CombatTurnRightAnimationSpeedProp = serializedObject.FindProperty("CombatTurnRightAnimationSpeed");
            CombatAngleToTurnProp = serializedObject.FindProperty("CombatAngleToTurn");
            NonCombatAngleToTurnProp = serializedObject.FindProperty("NonCombatAngleToTurn");
            AttackDistanceProp = serializedObject.FindProperty("AttackDistance");
            ProjectileTimeoutTimeProp = serializedObject.FindProperty("ProjectileTimeoutTime");
            HealthRegRateProp = serializedObject.FindProperty("HealthRegRate");
            Attack1CollisionSecondsProp = serializedObject.FindProperty("Attack1CollisionSeconds");
            Attack2CollisionSecondsProp = serializedObject.FindProperty("Attack2CollisionSeconds");
            Attack3CollisionSecondsProp = serializedObject.FindProperty("Attack3CollisionSeconds");
            RunAttackCollisionSecondsProp = serializedObject.FindProperty("RunAttackCollisionSeconds");
            Effect1TimeoutSecondsProp = serializedObject.FindProperty("Effect1TimeoutSeconds");
            Effect2TimeoutSecondsProp = serializedObject.FindProperty("Effect2TimeoutSeconds");
            Effect3TimeoutSecondsProp = serializedObject.FindProperty("Effect3TimeoutSeconds");
            RunEffectTimeoutSecondsProp = serializedObject.FindProperty("RunEffectTimeoutSeconds");
            BloodEffectTimeoutSecondsProp = serializedObject.FindProperty("BloodEffectTimeoutSeconds");
            TooCloseDistanceProp = serializedObject.FindProperty("TooCloseDistance");
            PlayerYOffsetProp = serializedObject.FindProperty("PlayerYOffset");
            Attack1HeatSeekingSecondsProp = serializedObject.FindProperty("Attack1HeatSeekingSeconds");
            Attack2HeatSeekingSecondsProp = serializedObject.FindProperty("Attack2HeatSeekingSeconds");
            Attack3HeatSeekingSecondsProp = serializedObject.FindProperty("Attack3HeatSeekingSeconds");
            RunAttackHeatSeekingSecondsProp = serializedObject.FindProperty("RunAttackHeatSeekingSeconds");
            ExpandedFieldOfViewAngleProp = serializedObject.FindProperty("ExpandedFieldOfViewAngle");
            ExpandedDetectionRadiusProp = serializedObject.FindProperty("ExpandedDetectionRadius");
            ExpandedChaseDistanceProp = serializedObject.FindProperty("ExpandedChaseDistance");
            MitigationAmountProp = serializedObject.FindProperty("MitigationAmount");
            ProjectileCollisionPointYProp = serializedObject.FindProperty("ProjectileCollisionPointY");
            StartingCombatTextSizeProp = serializedObject.FindProperty("StartingCombatTextSize");
            EndingCombatTextSizeProp = serializedObject.FindProperty("EndingCombatTextSize");
            MaxDamageAngleProp = serializedObject.FindProperty("MaxDamageAngle");
            WalkFootstepVolumeProp = serializedObject.FindProperty("WalkFootstepVolume");
            RunFootstepVolumeProp = serializedObject.FindProperty("RunFootstepVolume");
            BlockVolumeProp = serializedObject.FindProperty("BlockVolume");
            InjuredVolumeProp = serializedObject.FindProperty("InjuredVolume");
            ImpactVolumeProp = serializedObject.FindProperty("ImpactVolume");
            AttackVolumeProp = serializedObject.FindProperty("AttackVolume");
            DeathVolumeProp = serializedObject.FindProperty("DeathVolume");
            EquipVolumeProp = serializedObject.FindProperty("EquipVolume");
            UnequipVolumeProp = serializedObject.FindProperty("UnequipVolume");
            IdleVolumeProp = serializedObject.FindProperty("IdleVolume");
            WarningVolumeProp = serializedObject.FindProperty("WarningVolume");

            //enums
            BehaviorProp = serializedObject.FindProperty("BehaviorRef");
            ConfidenceProp = serializedObject.FindProperty("ConfidenceRef");
            RandomizeDamageProp = serializedObject.FindProperty("RandomizeDamageRef");
            DetectionTypeProp = serializedObject.FindProperty("DetectionTypeRef");
            MaxChaseDistanceTypeProp = serializedObject.FindProperty("MaxChaseDistanceTypeRef");
            CombatTypeProp = serializedObject.FindProperty("CombatTypeRef");
            CreateHealthBarsProp = serializedObject.FindProperty("CreateHealthBarsRef");
            UseCombatTextProp = serializedObject.FindProperty("UseCombatTextRef");
            CustomizeHealthBarProp = serializedObject.FindProperty("CustomizeHealthBarRef");
            UseCustomFontProp = serializedObject.FindProperty("UseCustomFontRef");
            AnimateCombatTextProp = serializedObject.FindProperty("AnimateCombatTextRef");
            DisplayAINameProp = serializedObject.FindProperty("DisplayAINameRef");
            DisplayAITitleProp = serializedObject.FindProperty("DisplayAITitleRef");
            DisplayAILevelProp = serializedObject.FindProperty("DisplayAILevelRef");
            OpposingFactionsEnumProp = serializedObject.FindProperty("OpposingFactionsEnumRef");
            RefillHealthTypeProp = serializedObject.FindProperty("RefillHealthType");
            WanderTypeProp = serializedObject.FindProperty("WanderTypeRef");
            WaypointTypeProp = serializedObject.FindProperty("WaypointTypeRef");
            AlignAIWithGroundProp = serializedObject.FindProperty("AlignAIWithGroundRef");
            CurrentMovementStateProp = serializedObject.FindProperty("CurrentMovementState");
            UseBloodEffectProp = serializedObject.FindProperty("UseBloodEffectRef");
            UseWarningAnimationProp = serializedObject.FindProperty("UseWarningAnimationRef");
            TotalLODsProp = serializedObject.FindProperty("TotalLODsRef");
            HasMultipleLODsProp = serializedObject.FindProperty("HasMultipleLODsRef");
            AlignAIOnStartProp = serializedObject.FindProperty("AlignAIOnStartRef");
            AlignmentQualityProp = serializedObject.FindProperty("AlignmentQualityRef");
            PickTargetMethodProp = serializedObject.FindProperty("PickTargetMethodRef");
            AIAttacksPlayerProp = serializedObject.FindProperty("AIAttacksPlayerRef");
            UseNonAITagProp = serializedObject.FindProperty("UseNonAITagRef");
            WeaponTypeProp = serializedObject.FindProperty("WeaponTypeRef");
            UseRunAttacksProp = serializedObject.FindProperty("UseRunAttacksRef");
            ObstructionDetectionQualityProp = serializedObject.FindProperty("ObstructionDetectionQualityRef");
            AvoidanceQualityProp = serializedObject.FindProperty("AvoidanceQualityRef");
            UseBlockingProp = serializedObject.FindProperty("UseBlockingRef");
            BlockingOddsProp = serializedObject.FindProperty("BlockOdds");
            UseEquipAnimationProp = serializedObject.FindProperty("UseEquipAnimation");
            AnimatorTypeProp = serializedObject.FindProperty("AnimatorType");
            UseWeaponObjectProp = serializedObject.FindProperty("UseWeaponObject");
            UseHitAnimationsProp = serializedObject.FindProperty("UseHitAnimations");
            TargetObstructedActionProp = serializedObject.FindProperty("TargetObstructedActionRef");
            BloodEffectPositionTypeProp = serializedObject.FindProperty("BloodEffectPositionTypeRef");
            AggroActionProp = serializedObject.FindProperty("AggroActionRef");
            UseAggroProp = serializedObject.FindProperty("UseAggro");
            UseAIAvoidanceProp = serializedObject.FindProperty("UseAIAvoidance");
            EnableDebuggingPop = serializedObject.FindProperty("EnableDebugging");
            DrawRaycastsEnabledProp = serializedObject.FindProperty("DrawRaycastsEnabled");
            DebugLogTargetsEnabledProp = serializedObject.FindProperty("DebugLogTargetsEnabled");
            DebugLogObstructionsEnabledProp = serializedObject.FindProperty("DebugLogObstructionsEnabled");
            BackupTypeProp = serializedObject.FindProperty("BackupTypeRef");
            AutoEnableAnimatePhysicsProp = serializedObject.FindProperty("AutoEnableAnimatePhysics");

            #if CRUX_PRESENT
            SpawnedWithCruxProp = serializedObject.FindProperty("UseMagicEffectsPackRef");
            #endif

            Attack1HeatSeekingProp = serializedObject.FindProperty("Attack1HeatSeekingRef");
            Attack2HeatSeekingProp = serializedObject.FindProperty("Attack2HeatSeekingRef");
            Attack3HeatSeekingProp = serializedObject.FindProperty("Attack3HeatSeekingRef");
            RunAttackHeatSeekingProp = serializedObject.FindProperty("RunAttackHeatSeekingRef");
            ArrowProjectileAttack1Prop = serializedObject.FindProperty("ArrowProjectileAttack1");
            ArrowProjectileAttack2Prop = serializedObject.FindProperty("ArrowProjectileAttack2");
            ArrowProjectileAttack3Prop = serializedObject.FindProperty("ArrowProjectileAttack3");
            ArrowProjectileRunAttackProp = serializedObject.FindProperty("ArrowProjectileRunAttack");

            Attack1EffectOnCollisionProp = serializedObject.FindProperty("Attack1EffectOnCollisionRef");
            Attack2EffectOnCollisionProp = serializedObject.FindProperty("Attack2EffectOnCollisionRef");
            Attack3EffectOnCollisionProp = serializedObject.FindProperty("Attack3EffectOnCollisionRef");
            RunAttackEffectOnCollisionProp = serializedObject.FindProperty("RunAttackEffectOnCollisionRef");
            Attack1SoundOnCollisionProp = serializedObject.FindProperty("Attack1SoundOnCollisionRef");
            Attack2SoundOnCollisionProp = serializedObject.FindProperty("Attack2SoundOnCollisionRef");
            Attack3SoundOnCollisionProp = serializedObject.FindProperty("Attack3SoundOnCollisionRef");
            RunAttackSoundOnCollisionProp = serializedObject.FindProperty("RunAttackSoundOnCollisionRef");

            UseRandomRotationOnStartProp = serializedObject.FindProperty("UseRandomRotationOnStartRef");
            UseDeactivateDelayProp = serializedObject.FindProperty("UseDeactivateDelayRef");
            DisableAIWhenNotInViewProp = serializedObject.FindProperty("DisableAIWhenNotInViewRef");
            DeathTypeRefProp = serializedObject.FindProperty("DeathTypeRef");
            UseDroppableWeaponProp = serializedObject.FindProperty("UseDroppableWeapon");

            //string
            AINameProp = serializedObject.FindProperty("AIName");
            AITitleProp = serializedObject.FindProperty("AITitle");
            AILevelProp = serializedObject.FindProperty("AILevel");
            PlayerTagProp = serializedObject.FindProperty("PlayerTag");
            FollowTagProp = serializedObject.FindProperty("FollowTag");
            UITagProp = serializedObject.FindProperty("UITag");
            EmeraldTagProp = serializedObject.FindProperty("EmeraldTag");
            NonAITagProp = serializedObject.FindProperty("NonAITag");
            RagdollTagProp = serializedObject.FindProperty("RagdollTag");

            //objects
            HealthBarImageProp = serializedObject.FindProperty("HealthBarImage");
            HealthBarBackgroundImageProp = serializedObject.FindProperty("HealthBarBackgroundImage");
            CombatTextPosProp = serializedObject.FindProperty("CombatTextPos");
            CombatTextFontProp = serializedObject.FindProperty("CombatTextFont");
            BloodEffectProp = serializedObject.FindProperty("BloodEffect");
            Attack1ProjectileProp = serializedObject.FindProperty("Attack1Projectile");
            Attack2ProjectileProp = serializedObject.FindProperty("Attack2Projectile");
            Attack3ProjectileProp = serializedObject.FindProperty("Attack3Projectile");
            RunAttackProjectileProp = serializedObject.FindProperty("RunAttackProjectile");
            RangedAttackTransformProp = serializedObject.FindProperty("RangedAttackTransform");
            SheatheWeaponProp = serializedObject.FindProperty("SheatheWeapon");
            UnsheatheWeaponProp = serializedObject.FindProperty("UnsheatheWeapon");
            WeaponObjectProp = serializedObject.FindProperty("WeaponObject");
            AIRendererProp = serializedObject.FindProperty("AIRenderer");
            DroppableWeaponObjectProp = serializedObject.FindProperty("DroppableWeaponObject");
            BlockIdleAnimationProp = serializedObject.FindProperty("BlockIdleAnimation");
            BlockHitAnimationProp = serializedObject.FindProperty("BlockHitAnimation");

            Attack1CollisionEffectProp = serializedObject.FindProperty("Attack1CollisionEffect");
            Attack1ImpactSoundProp = serializedObject.FindProperty("Attack1ImpactSound");
            Attack2CollisionEffectProp = serializedObject.FindProperty("Attack2CollisionEffect");
            Attack2ImpactSoundProp = serializedObject.FindProperty("Attack2ImpactSound");
            Attack3CollisionEffectProp = serializedObject.FindProperty("Attack3CollisionEffect");
            Attack3ImpactSoundProp = serializedObject.FindProperty("Attack3ImpactSound");
            RunAttackCollisionEffectProp = serializedObject.FindProperty("RunAttackCollisionEffect");
            RunAttackImpactSoundProp = serializedObject.FindProperty("RunAttackImpactSound");

            //vector
            HealthBarPosProp = serializedObject.FindProperty("HealthBarPos");
            NameTextSizeProp = serializedObject.FindProperty("NameTextSize");
            HealthBarScaleProp = serializedObject.FindProperty("HealthBarScale");
            BloodPosOffsetProp = serializedObject.FindProperty("BloodPosOffset");
            AINamePosProp = serializedObject.FindProperty("AINamePos");

            //color
            HealthBarColorProp = serializedObject.FindProperty("HealthBarColor");
            HealthBarBackgroundColorProp = serializedObject.FindProperty("HealthBarBackgroundColor");
            CombatTextColorProp = serializedObject.FindProperty("CombatTextColor");
            NameTextColorProp = serializedObject.FindProperty("NameTextColor");
            LevelTextColorProp = serializedObject.FindProperty("LevelTextColor");

            //bool
            HealthBarsFoldoutProp = serializedObject.FindProperty("HealthBarsFoldout");
            CombatTextFoldoutProp = serializedObject.FindProperty("CombatTextFoldout");
            NameTextFoldoutProp = serializedObject.FindProperty("NameTextFoldout");
            AnimationsUpdatedProp = serializedObject.FindProperty("AnimationsUpdated");
            WaypointsFoldout = serializedObject.FindProperty("WaypointsFoldout");
            WalkFoldout = serializedObject.FindProperty("WalkFoldout");
            RunFoldout = serializedObject.FindProperty("RunFoldout");
            TurnFoldout = serializedObject.FindProperty("TurnFoldout");
            CombatWalkFoldout = serializedObject.FindProperty("CombatWalkFoldout");
            CombatRunFoldout = serializedObject.FindProperty("CombatRunFoldout");
            CombatTurnFoldout = serializedObject.FindProperty("CombatTurnFoldout");

            //Mirror Bools
            MirrorWalkLeftProp = serializedObject.FindProperty("MirrorWalkLeft");
            MirrorWalkRightProp = serializedObject.FindProperty("MirrorWalkRight");
            MirrorRunLeftProp = serializedObject.FindProperty("MirrorRunLeft");
            MirrorRunRightProp = serializedObject.FindProperty("MirrorRunRight");
            MirrorCombatWalkLeftProp = serializedObject.FindProperty("MirrorCombatWalkLeft");
            MirrorCombatWalkRightProp = serializedObject.FindProperty("MirrorCombatWalkRight");
            MirrorCombatRunLeftProp = serializedObject.FindProperty("MirrorCombatRunLeft");
            MirrorCombatRunRightProp = serializedObject.FindProperty("MirrorCombatRunRight");
            MirrorCombatTurnLeftProp = serializedObject.FindProperty("MirrorCombatTurnLeft");
            MirrorCombatTurnRightProp = serializedObject.FindProperty("MirrorCombatTurnRight");
            MirrorTurnLeftProp = serializedObject.FindProperty("MirrorTurnLeft");
            MirrorTurnRightProp = serializedObject.FindProperty("MirrorTurnRight");
            ReverseWalkAnimationProp = serializedObject.FindProperty("ReverseWalkAnimation");

            Projectile1UpdatedProp = serializedObject.FindProperty("Projectile1Updated");
            Projectile2UpdatedProp = serializedObject.FindProperty("Projectile2Updated");
            Projectile3UpdatedProp = serializedObject.FindProperty("Projectile3Updated");
            RunProjectileUpdatedProp = serializedObject.FindProperty("RunProjectileUpdated");
            BloodEffectUpdatedProp = serializedObject.FindProperty("BloodEffectUpdated");

            BehaviorFoldout = serializedObject.FindProperty("BehaviorFoldout");
            ConfidenceFoldout = serializedObject.FindProperty("ConfidenceFoldout");
            WanderFoldout = serializedObject.FindProperty("WanderFoldout");
            CombatStyleFoldout = serializedObject.FindProperty("CombatStyleFoldout");

            OpposingFaction1Prop = serializedObject.FindProperty("OpposingFaction1");
            OpposingFaction2Prop = serializedObject.FindProperty("OpposingFaction2");
            OpposingFaction3Prop = serializedObject.FindProperty("OpposingFaction3");
            OpposingFaction4Prop = serializedObject.FindProperty("OpposingFaction4");
            OpposingFaction5Prop = serializedObject.FindProperty("OpposingFaction5");

            Renderer1Prop = serializedObject.FindProperty("Renderer1");
            Renderer2Prop = serializedObject.FindProperty("Renderer2");
            Renderer3Prop = serializedObject.FindProperty("Renderer3");
            Renderer4Prop = serializedObject.FindProperty("Renderer4");

            //Animations
            WalkLeftProp = serializedObject.FindProperty("WalkLeftAnimation");
            WalkStraightProp = serializedObject.FindProperty("WalkStraightAnimation");
            WalkRightProp = serializedObject.FindProperty("WalkRightAnimation");
            CombatWalkLeftProp = serializedObject.FindProperty("CombatWalkLeftAnimation");
            CombatWalkStraightProp = serializedObject.FindProperty("CombatWalkStraightAnimation");
            CombatWalkRightProp = serializedObject.FindProperty("CombatWalkRightAnimation");
            WalkBackProp = serializedObject.FindProperty("CombatWalkBackAnimation");
            RunLeftProp = serializedObject.FindProperty("RunLeftAnimation");
            RunStraightProp = serializedObject.FindProperty("RunStraightAnimation");
            RunRightProp = serializedObject.FindProperty("RunRightAnimation");
            CombatRunLeftProp = serializedObject.FindProperty("CombatRunLeftAnimation");
            CombatRunStraightProp = serializedObject.FindProperty("CombatRunStraightAnimation");
            CombatRunRightProp = serializedObject.FindProperty("CombatRunRightAnimation");
            IdleWarningProp = serializedObject.FindProperty("IdleWarningAnimation");
            IdleCombatProp = serializedObject.FindProperty("CombatIdleAnimation");
            IdleNonCombatProp = serializedObject.FindProperty("NonCombatIdleAnimation");
            TurnLeftProp = serializedObject.FindProperty("NonCombatTurnLeftAnimation");
            TurnRightProp = serializedObject.FindProperty("NonCombatTurnRightAnimation");
            CombatTurnLeftProp = serializedObject.FindProperty("CombatTurnLeftAnimation");
            CombatTurnRightProp = serializedObject.FindProperty("CombatTurnRightAnimation");
            PutAwayWeaponAnimationProp = serializedObject.FindProperty("PutAwayWeaponAnimation");
            PullOutWeaponAnimationProp = serializedObject.FindProperty("PullOutWeaponAnimation");

            //Layer Masks
            DetectionLayerMaskProp = serializedObject.FindProperty("DetectionLayerMask");
            ObstructionDetectionLayerMaskProp = serializedObject.FindProperty("ObstructionDetectionLayerMask");
            AlignmentLayerMaskProp = serializedObject.FindProperty("AlignmentLayerMask");
            AIAvoidanceLayerMaskProp = serializedObject.FindProperty("AIAvoidanceLayerMask");

            //Events
            DeathEventProp = serializedObject.FindProperty("DeathEvent");
            DamageEventProp = serializedObject.FindProperty("DamageEvent");
            ReachedDestinationEventProp = serializedObject.FindProperty("ReachedDestinationEvent");
            OnStartEventProp = serializedObject.FindProperty("OnStartEvent");
            OnEnabledEventProp = serializedObject.FindProperty("OnEnabledEvent");
            OnAttackEventProp = serializedObject.FindProperty("OnAttackEvent");
            OnFleeEventProp = serializedObject.FindProperty("OnFleeEvent");
            OnStartCombatEventProp = serializedObject.FindProperty("OnStartCombatEvent");
#endregion

#region Editor Icons
            if (TemperamentIcon == null) TemperamentIcon = Resources.Load("TemperamentIcon") as Texture;
            if (SettingsIcon == null) SettingsIcon = Resources.Load("SettingsIcon") as Texture;
            if (DetectTagsIcon == null) DetectTagsIcon = Resources.Load("DetectTagsIcon") as Texture;
            if (UIIcon == null) UIIcon = Resources.Load("UIIcon") as Texture;
            if (SoundIcon == null) SoundIcon = Resources.Load("SoundIcon") as Texture;
            if (WaypointEditorIcon == null) WaypointEditorIcon = Resources.Load("WaypointEditorIcon") as Texture;
            if (AnimationIcon == null) AnimationIcon = Resources.Load("AnimationIcon") as Texture;
            if (DocumentationIcon == null) DocumentationIcon = Resources.Load("DocumentationIcon") as Texture;
#endregion

#region Reorderable Lists
            //Put our sound lists into reorderable lists because Unity allows these lists to be serialized
            //Attack Sounds
            AttackSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("AttackSounds"), true, true, true, true);
            AttackSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Attack Sounds List", EditorStyles.boldLabel);
            };
            AttackSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = AttackSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Injured Sounds
            InjuredSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("InjuredSounds"), true, true, true, true);
            InjuredSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Injured Sounds List", EditorStyles.boldLabel);
            };
            InjuredSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = InjuredSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Warning Sounds
            WarningSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("WarningSounds"), true, true, true, true);
            WarningSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Warning Sounds List", EditorStyles.boldLabel);
            };
            WarningSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = WarningSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Death Sounds
            DeathSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("DeathSounds"), true, true, true, true);
            DeathSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Death Sounds List", EditorStyles.boldLabel);
            };
            DeathSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = DeathSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Footstep Sounds
            FootStepSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("FootStepSounds"), true, true, true, true);
            FootStepSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "FootStep Sounds List", EditorStyles.boldLabel);
            };
            FootStepSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = FootStepSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Idle Sounds
            IdleSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("IdleSounds"), true, true, true, true);
            IdleSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Idle Sounds List", EditorStyles.boldLabel);
            };
            IdleSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = IdleSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Impact Sounds
            ImpactSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("ImpactSounds"), true, true, true, true);
            ImpactSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Weapon Impact Sounds List", EditorStyles.boldLabel);
            };
            ImpactSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = ImpactSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Block Sounds
            BlockSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("BlockingSounds"), true, true, true, true);
            BlockSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Block Sounds List", EditorStyles.boldLabel);
            };
            BlockSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = BlockSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
                };

            //Hit Animations
            HitAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("HitAnimationList"), true, true, true, true);
            HitAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = HitAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationSpeed"), GUIContent.none);

                    if (element.FindPropertyRelative("AnimationSpeed").floatValue == 0)
                    {
                        element.FindPropertyRelative("AnimationSpeed").floatValue = 1;
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };

            HitAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   Speed  " + "     Clip", EditorStyles.boldLabel);
            };
            HitAnimationList.onChangedCallback = (HitAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };

            //Combat Hit Animations
            CombatHitAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("CombatHitAnimationList"), true, true, true, true);
            CombatHitAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = CombatHitAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationSpeed"), GUIContent.none);

                    if (element.FindPropertyRelative("AnimationSpeed").floatValue == 0)
                    {
                        element.FindPropertyRelative("AnimationSpeed").floatValue = 1;
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };

            CombatHitAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   Speed  " + "     Clip", EditorStyles.boldLabel);
            };
            CombatHitAnimationList.onChangedCallback = (CombatHitAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };

            /*
            //Old Hit Animations
            CombatHitAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("CombatHitAnimationList"), true, true, true, true);
            CombatHitAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Combat Hit Animation List", EditorStyles.boldLabel);
            };
            CombatHitAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = CombatHitAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);

                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };
            CombatHitAnimationList.onChangedCallback = (CombatHitAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };
            */

            //New Idle
            IdleAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("IdleAnimationList"), true, true, true, true);
            IdleAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = IdleAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationSpeed"), GUIContent.none);

                    if (element.FindPropertyRelative("AnimationSpeed").floatValue == 0)
                    {
                        element.FindPropertyRelative("AnimationSpeed").floatValue = 1;
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };

            IdleAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   Speed  " + "     Clip", EditorStyles.boldLabel);
            };
            IdleAnimationList.onChangedCallback = (IdleAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };

            //Attack Animations
            AttackAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("AttackAnimationList"), true, true, true, true);
            AttackAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = AttackAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationSpeed"), GUIContent.none);

                    if (element.FindPropertyRelative("AnimationSpeed").floatValue == 0)
                    {
                        element.FindPropertyRelative("AnimationSpeed").floatValue = 1;
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };

            AttackAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   Speed  " + "     Clip", EditorStyles.boldLabel);
            };
            AttackAnimationList.onChangedCallback = (AttackAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };

            //Run Attack Animations
            RunAttackAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("RunAttackAnimationList"), true, true, true, true);
            RunAttackAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = RunAttackAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationSpeed"), GUIContent.none);

                    if (element.FindPropertyRelative("AnimationSpeed").floatValue == 0)
                    {
                        element.FindPropertyRelative("AnimationSpeed").floatValue = 1;
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };

            RunAttackAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   Speed  " + "     Clip", EditorStyles.boldLabel);
            };
            RunAttackAnimationList.onChangedCallback = (RunAttackAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };

            //Death Animations
            DeathAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("DeathAnimationList"), true, true, true, true);
            DeathAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = DeathAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 70, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationSpeed"), GUIContent.none);

                    if (element.FindPropertyRelative("AnimationSpeed").floatValue == 0)
                    {
                        element.FindPropertyRelative("AnimationSpeed").floatValue = 1;
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };

            DeathAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   Speed  " + "     Clip", EditorStyles.boldLabel);
            };
            DeathAnimationList.onChangedCallback = (DeathAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };

            //Emote Animations
            EmoteAnimationList = new ReorderableList(serializedObject, serializedObject.FindProperty("EmoteAnimationList"), true, true, true, true);
            EmoteAnimationList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = EmoteAnimationList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 120, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("EmoteAnimationClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AnimationID"), GUIContent.none);
                    if (EditorGUI.EndChangeCheck())
                    {
                        AnimationListsChangedProp.boolValue = true;
                    }
                };

            EmoteAnimationList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   ID  " + "         Emote Animation Clip", EditorStyles.boldLabel);
            };
            EmoteAnimationList.onChangedCallback = (EmoteAnimationList) =>
            {
                AnimationListsChangedProp.boolValue = true;
            };

            //Interact Sounds
            InteractSoundsList = new ReorderableList(serializedObject, serializedObject.FindProperty("InteractSoundList"), true, true, true, true);
            InteractSoundsList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = InteractSoundsList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 120, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("SoundEffectClip"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("SoundEffectID"), GUIContent.none);
                };

            InteractSoundsList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   ID  " + "         Interact Sound Clip", EditorStyles.boldLabel);
            };

            //Item Objects
            ItemList = new ReorderableList(serializedObject, serializedObject.FindProperty("ItemList"), true, true, true, true);
            ItemList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var element = ItemList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 120, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("ItemObject"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("ItemID"), GUIContent.none);
                };

            ItemList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "   ID  " + "         Item Object", EditorStyles.boldLabel);
            };
#endregion
        }

        void LoadFactionData()
        {
            string path = AssetDatabase.GetAssetPath(Resources.Load("EmeraldAIFactions"));

            TextAsset FactionData = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));

            if (FactionData != null)
            {
                string[] textLines = FactionData.text.Split(',');

                foreach (string s in textLines)
                {
                    if (!EmeraldAISystem.StringFactionList.Contains(s) && s != "")
                    {
                        EmeraldAISystem.StringFactionList.Add(s);
                    }
                }
            }
        }

        public override void OnInspectorGUI()
        {
            EmeraldAISystem self = (EmeraldAISystem)target;

            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (!self.AnimatorControllerGenerated)
            {
                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                EditorGUILayout.HelpBox("There is currently no generated Animator Controller for this AI. " +
                    "Please go to the Animation tab, apply all needed animations, and create one before using this AI.", MessageType.Warning);
                GUI.backgroundColor = Color.white;
                EditorGUILayout.Space();
            }
            else if (!self.HeadTransform)
            {
                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                EditorGUILayout.HelpBox("The AI's Head Transform has not been applied and is needed for accurate raycast calculations, " +
                    "please apply it. This is located under Detections & Tags>Detection options.", MessageType.Warning);
                GUI.backgroundColor = Color.white;
                EditorGUILayout.Space();
            }

            GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
            myFoldoutStyle.fontStyle = FontStyle.Bold;
            myFoldoutStyle.fontSize = 12;
            myFoldoutStyle.active.textColor = Color.black;
            myFoldoutStyle.focused.textColor = Color.black;
            myFoldoutStyle.onHover.textColor = Color.black;
            myFoldoutStyle.normal.textColor = Color.black;
            myFoldoutStyle.onNormal.textColor = Color.black;
            myFoldoutStyle.onActive.textColor = Color.black;
            myFoldoutStyle.onFocused.textColor = Color.black;
            Color myStyleColor = Color.black;

            GUIContent[] TabButtons = new GUIContent[8] {new GUIContent("Temperament", TemperamentIcon), new GUIContent("AI's Settings", SettingsIcon), new GUIContent(" Detection \n & Tags", DetectTagsIcon),
            new GUIContent("UI Settings", UIIcon), new GUIContent("Sounds", SoundIcon), new GUIContent(" Waypoint\nEditor", WaypointEditorIcon), new GUIContent("Animations", AnimationIcon), new GUIContent("Docs", DocumentationIcon)};

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TabNumberProp.intValue = GUILayout.SelectionGrid(TabNumberProp.intValue, TabButtons, 4, EditorStyles.miniButtonRight, GUILayout.Height(68), GUILayout.Width(85 * Screen.width / Screen.dpi));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            var HelpButtonStyle = new GUIStyle(GUI.skin.button);
            HelpButtonStyle.normal.textColor = Color.white;
            HelpButtonStyle.fontStyle = FontStyle.Bold;

            //Behavior Options
            if (TabNumberProp.intValue == 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical("Box");
                var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(new GUIContent(TemperamentIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                EditorGUILayout.LabelField("Temperament Options", style, GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField("The Temperament Options allow you to control an AI's behavior, how it reacts to certain situations, and how it chooses to wander or move to a destination.", EditorStyles.helpBox);
                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                BehaviorFoldout.boolValue = CustomEditorProperties.Foldout(BehaviorFoldout.boolValue, "Behavior", true, myFoldoutStyle);

                if (BehaviorFoldout.boolValue)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("The Behavior Type allows you to define the behavior your AI will have. All AI will only react to targets that have targets' tags set within the AI's Tags List.", EditorStyles.helpBox);

                    EditorGUILayout.PropertyField(BehaviorProp, new GUIContent("Behavior Type"));

                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                    if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Passive)
                    {
                        EditorGUILayout.LabelField("Passive - Passive AI will not attack. They will simply wander around. If they are attacked, they will react according to their " +
                            "Confidence Level. If you would like your AI to not attack, even if attacked first, simply enable 'Does Not Attack'.", EditorStyles.helpBox);
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious)
                    {
                        EditorGUILayout.LabelField("Cautious - Cautious AI will either flee or act territorial depending on their Confidence Level. Territorial AI will warn targets " +
                            "before attacking their target. An AI is set as territorial if their Confidence Level is set to Brave or higher.", EditorStyles.helpBox);
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        EditorGUILayout.LabelField("Companion - Companion AI will follow around a target and help them fight. Companion AI will wander until their follow target is set. " +
                            "This is best done with a script and calling the public function SetTarget.", EditorStyles.helpBox);
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Aggressive)
                    {
                        EditorGUILayout.LabelField("Aggressive - Aggressive AI will attack any target that comes within their trigger radius.", EditorStyles.helpBox);
                    }
                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                ConfidenceFoldout.boolValue = CustomEditorProperties.Foldout(ConfidenceFoldout.boolValue, "Confidence", true, myFoldoutStyle);

                if (ConfidenceFoldout.boolValue)
                {
                    EditorGUILayout.BeginVertical("Box");
                    if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Not Usable with Companion AI)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Not Usable with Pet AI)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.LabelField("The Confidence Level gives you more control over how your AI reacts with their Behavior Type.", EditorStyles.helpBox);

                    EditorGUILayout.PropertyField(ConfidenceProp, new GUIContent("Confidence Level"));

                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                    if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious)
                    {
                        if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward)
                        {
                            EditorGUILayout.LabelField("Coward - AI with a Coward confidence level will flee when they encounter a target.", EditorStyles.helpBox);
                        }
                        else if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Brave)
                        {
                            EditorGUILayout.LabelField("Brave - AI with a Brave confidence level will become territorial when a target enters their trigger radius. " +
                                "If the target doesn't leave its radius before its territorial seconds have been reached, the AI will attack the target. This AI will " +
                                "flee once its health reaches the amount you've set below.", EditorStyles.helpBox);
                        }
                        else if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Foolhardy)
                        {
                            EditorGUILayout.LabelField("Foolhardy - AI with a Foolhardy confidence level will become territorial when a target enters their trigger radius. " +
                                "If the target doesn't leave its radius before its territorial seconds have been reached, the AI will attack the target. " +
                                "This AI will never flee and continue to fight until dead or the target has escaped the AI.", EditorStyles.helpBox);

                        }
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Aggressive)
                    {
                        if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("Aggressive AI cannot be set to Coward. This AI will automatically be set to Brave on Start.", EditorStyles.helpBox);
                        }
                        else if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Brave)
                        {
                            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                            EditorGUILayout.LabelField("Brave - Brave Aggressive AI will fight any target on sight or detection, but attempt to flee once its health " +
                                "reaches the percentage you've set.", EditorStyles.helpBox);
                        }
                        else if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Foolhardy)
                        {
                            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                            EditorGUILayout.LabelField("Foolhardy - Foolhardy Aggressive AI will fight any target on sight or detection and will never flee. " +
                                "They will continue to fight until dead or the target has escaped the AI.", EditorStyles.helpBox);

                        }
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Passive)
                    {
                        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                        if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward)
                        {
                            EditorGUILayout.LabelField("Coward - Coward Passive AI will wander according to their wander settings, but only flee when attacked.", EditorStyles.helpBox);
                        }
                        else if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Brave)
                        {
                            EditorGUILayout.LabelField("Brave - Brave Passive AI will wander according to their wander settings, but only attack when attacked. They will " +
                                "attempt to flee once its health reaches the percentage you've set.", EditorStyles.helpBox);
                        }
                        else if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Foolhardy)
                        {
                            EditorGUILayout.LabelField("Foolhardy - Foolhardy Passive AI will wander according to their wander settings, but only attack when attacked. They " +
                                "will never flee and continue to fight until dead or the target has escaped the AI.", EditorStyles.helpBox);

                        }
                    }
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (self.ConfidenceRef != EmeraldAISystem.ConfidenceType.Brave)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Brave Confidence Level Only)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), HealthPercentageToFleeProp, "Health Level to Flee", 1, 99);
                    CustomHelpLabelField("Controls the percent of health loss that's needed to trigger a flee attempt.", false);

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                WanderFoldout.boolValue = CustomEditorProperties.Foldout(WanderFoldout.boolValue, "Wander Type", true, myFoldoutStyle);

                if (WanderFoldout.boolValue)
                {
                    EditorGUILayout.BeginVertical("Box");
                    if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Companion AI will only wander when they don't have a current follower)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Not Usable with Pet AI)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.LabelField("Controls the type of wandering technique this AI will use. AI will react to targets according to their Behavior Type.", EditorStyles.helpBox);
                    EditorGUILayout.PropertyField(WanderTypeProp, new GUIContent("Wander Type"));

                    if (self.WanderTypeRef == EmeraldAISystem.WanderType.Dynamic)
                    {
                        CustomHelpLabelField("Dynamic - Allows an AI to randomly wander by dynamically generate waypoints around their Wander Radius.", true);
                    }
                    else if (self.WanderTypeRef == EmeraldAISystem.WanderType.Waypoints)
                    {
                        CustomHelpLabelField("Waypoints - Allows you to define waypoints that the AI will move between.", true);

                        CustomHelpLabelField("You can edit and create Waypoints by going to the Waypoint Editor tab. Would you like to do this now?", false);
                        if (GUILayout.Button("Open the Waypoint Editor"))
                        {
                            TabNumberProp.intValue = 5;
                        }
                    }
                    else if (self.WanderTypeRef == EmeraldAISystem.WanderType.Stationary)
                    {
                        CustomHelpLabelField("Stationary - Allows an AI to stay stationary in the same position and will not move unless a target enters their trigger radius.", true);

                        CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), StationaryIdleSecondsMinProp, "Min Idle Animation Seconds");
                        CustomHelpLabelField("When using more than 1 idle animation, this controls the minimum amount of seconds needed before switching to the next idle " +
                            "animation. This will be randomized with the Max Idle Animation Seconds.", true);

                        CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), StationaryIdleSecondsMaxProp, "Max Idle Animation Seconds");
                        CustomHelpLabelField("When using more than 1 idle animation, this controls the maximum amount of seconds needed before switching to the next idle " +
                            "animation. This will be randomized with the Min Idle Animation Seconds.", true);
                    }
                    else if (self.WanderTypeRef == EmeraldAISystem.WanderType.Destination)
                    {
                        CustomHelpLabelField("Destination - Allows an AI to travle to a single destination. Once it reaches it destination, it will stay stationary.", true);

                        if (GUILayout.Button("Reset Destination Point"))
                        {
                            self.SingleDestination = self.transform.position + self.transform.forward * 2;
                        }
                    }

                    EditorGUILayout.Space();

                    if (self.WanderTypeRef == EmeraldAISystem.WanderType.Dynamic)
                    {
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), WanderRadiusProp, "Dynamic Wander Radius", ((int)self.StoppingDistance+3), 300);
                        CustomHelpLabelField("Controls the radius that the AI uses to wander. The AI will randomly pick waypoints within this radius.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaxSlopeLimitProp, "Max Slope Limit", 10, 60);
                        CustomHelpLabelField("Controls the maximum slope that a waypoint can be generated on.", true);
                    }

                    EditorGUILayout.EndVertical();
                }

                if (self.WanderTypeRef == EmeraldAISystem.WanderType.Destination)
                {
                    if (self.SingleDestination == Vector3.zero)
                    {
                        self.SingleDestination = new Vector3(self.transform.position.x, self.transform.position.y, self.transform.position.z + 5);
                    }
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                CombatStyleFoldout.boolValue = CustomEditorProperties.Foldout(CombatStyleFoldout.boolValue, "Combat Style", true, myFoldoutStyle);

                if (CombatStyleFoldout.boolValue)
                {
                    EditorGUILayout.BeginVertical("Box");
                    if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Companion AI Only)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.LabelField("Controls whether a Companion AI will fight Offensively or Defensively.", EditorStyles.helpBox);
                    EditorGUILayout.PropertyField(CombatTypeProp, new GUIContent("Combat Style"));
                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);

                    if (self.CombatTypeRef == EmeraldAISystem.CombatType.Defensive)
                    {
                        EditorGUILayout.LabelField("Defensive - Defensive Companioin AI will only attack a target if it is in active combat mode.", EditorStyles.helpBox);
                    }
                    else if (self.CombatTypeRef == EmeraldAISystem.CombatType.Offensive)
                    {
                        EditorGUILayout.LabelField("Offensive - Offensive Companioin AI will attack any target that is within their trigger radius.", EditorStyles.helpBox);
                    }
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                }
            }

            //AI's Settings
            if (TabNumberProp.intValue == 1)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(new GUIContent(SettingsIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                EditorGUILayout.LabelField("AI's Settings", style, GUILayout.ExpandWidth(true));
                GUILayout.Space(4);
                GUIContent[] AIStatsButtons = new GUIContent[7] { new GUIContent("Stats"), new GUIContent("Movement"), new GUIContent("Combat"), new GUIContent("NavMesh"), new GUIContent("Optimize"), new GUIContent("Events"), new GUIContent("Items") };
                TemperamentTabNumberProp.intValue = GUILayout.Toolbar(TemperamentTabNumberProp.intValue, AIStatsButtons, EditorStyles.miniButton, GUILayout.Height(25), GUILayout.Width(82 * Screen.width / Screen.dpi));
                GUILayout.Space(1);
                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (TemperamentTabNumberProp.intValue == 0)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Controls all of an AI's stats such as health, name, and level.", EditorStyles.helpBox);
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(AINameProp, new GUIContent("AI's Name"));
                    CustomHelpLabelField("The name of the AI. This can be displayed with Emerald's built-in UI system or a custom one.", true);
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(AITitleProp, new GUIContent("AI's Title"));
                    CustomHelpLabelField("The title of the AI. This can be displayed with Emerald's built-in UI system or a custom one.", true);
                    EditorGUILayout.Space();

                    CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), AILevelProp, "AI's Level");
                    CustomHelpLabelField("The level of the AI. This can be displayed with Emerald's built-in UI system or a custom one.", true);
                    EditorGUILayout.Space();

                    CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), StartingHealthProp, "Health");
                    CustomHelpLabelField("Controls how much health this AI has.", true);
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(RefillHealthTypeProp, new GUIContent("Regenerate Health Type"));
                    CustomHelpLabelField("Controls how your AI regenerates health when not in combat or their max chase/flee distance is met or exceeded.", true);
                    EditorGUILayout.Space();

                    if (self.RefillHealthType == EmeraldAISystem.RefillHealth.OverTime)
                    {
                        CustomEditorProperties.CustomFloatField(new Rect(), new GUIContent(), HealthRegRateProp, "Regen Rate");
                        CustomHelpLabelField("Controls, in seconds, the rate in which health is regenerated.", true);

                        CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), RegenerateAmountProp, "Regen Amount");
                        CustomHelpLabelField("Controls how much health is refilled after each Regen Rate.", true);
                    }

                    EditorGUILayout.EndVertical();
                }

                if (TemperamentTabNumberProp.intValue == 2)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));
                    EditorGUILayout.LabelField("Combat Settings", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("The Combat section controls all combat related settings. Some AI, such as Cautious AI with a Coward Confidence Type, " +
                        "will not use the Combat section.", EditorStyles.helpBox);

                    GUIContent[] CombatButtons = new GUIContent[3] {new GUIContent("Damage Settings"), new GUIContent("Combat Actions"), new GUIContent("Hit Effect")};
                    CombatTabNumberProp.intValue = GUILayout.Toolbar(CombatTabNumberProp.intValue, CombatButtons, EditorStyles.miniButton, GUILayout.Height(25));

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (CombatTabNumberProp.intValue == 0)
                    {
                        EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));

                        GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                        EditorGUILayout.BeginVertical("Box");

                        EditorGUILayout.LabelField("Damage Settings", EditorStyles.boldLabel);
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndVertical();
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                        {
                            EditorGUILayout.Space();
                            CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), AttackDistanceProp, "Attack Distance", 1.5f, 40);
                        }
                        else if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                        {
                            EditorGUILayout.Space();
                            CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), AttackDistanceProp, "Attack Distance", 1.5f, 50);
                        }
                        CustomHelpLabelField("Controls the distance in which an AI will attack.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), TooCloseDistanceProp, "Too Close Distance", 0f, 25);
                        CustomHelpLabelField("Controls the distance when an AI will backup if they get too close to their target. This is also useful for ranged AI keeping their" +
                            " distance from attackers.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MinimumAttackSpeedProp, "Min Attack Speed", 1, 8);
                        CustomHelpLabelField("Controls the minimum amount of time it takes for your AI to attack. This amount is randomized with Maximum Attack Speed.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaximumAttackSpeedProp, "Max Attack Speed", 1, 8);
                        CustomHelpLabelField("Controls the maximum amount of time it takes for your AI to attack. This amount is randomized with Minimum Attack Speed.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), SentRagdollForceProp, "Ragdoll Force", 50, 4000);
                        CustomHelpLabelField("Controls the amount of force that is applied to this all of this AI's attacks which affects the target's ragdoll when they die.", false);
                        EditorGUILayout.Space();

                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                        {
                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaxDamageAngleProp, "Max Damage Angle", 45, 180);
                            CustomHelpLabelField("Controls the max angle that will allow attacks to damage targets. For example, if this setting is set to 90, the AI will be able to damage its" +
                                " target while it's within 90 degrees of the AI. Anything greater than this amount will stop damage from being inflicted.", true);
                        }
                        else if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                        {
                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaxDamageAngleProp, "Max Firing Angle", 30, 180);
                            CustomHelpLabelField("Controls the max angle that will allow the AI to fire a projectile.", true);
                        }

                        EditorGUILayout.PropertyField(WeaponTypeProp, new GUIContent("Weapon Type"));
                        CustomHelpLabelField("Controls whether your AI will use Ranged or Melee combat.", true);

                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(15);
                            EditorGUILayout.BeginVertical();

                            CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), RangedAttackTransformProp, "Ranged Attack Transform", typeof(Transform), true);
                            CustomHelpLabelField("The transfrom that's used to for spawning ranged attacks. This transform can be anything on your AI such as their hand " +
                                "for magic effects or their bow for bow and arrows.", true);

                            EditorGUILayout.PropertyField(TargetObstructedActionProp, new GUIContent("Obstructed Action"));
                            CustomHelpLabelField("Controls what action is done when the AI's target is not visible when using the Ranged Weapon Type.", true);

                            if (self.TargetObstructedActionRef == EmeraldAISystem.TargetObstructedAction.MoveCloserAfterSetSeconds)
                            {
                                EditorGUILayout.BeginHorizontal();
                                GUILayout.Space(15);
                                EditorGUILayout.BeginVertical();

                                CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), ObstructionSecondsProp, "Obstruction Seconds", 1, 10);
                                CustomHelpLabelField("Controls how many seconds before the AI will move closer to its target after it has become obstructed.", false);

                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space();
                            }

                            CustomEditorProperties.CustomFloatField(new Rect(), new GUIContent(), ProjectileTimeoutTimeProp, "Projectile Timeout Time");
                            CustomHelpLabelField("Controls, in seconds, how fast projectiles will timeout and be disabled.", true);

                            CustomEditorProperties.CustomFloatField(new Rect(), new GUIContent(), PlayerYOffsetProp, "Player Offset Y Position");
                            CustomHelpLabelField("Controls the Y position offset when firing projectiles at a player. Depending on the character controller, this may need to be " +
                                "adjusted to ensure the projectiles are not firing too high or too low.", true);

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }

                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                        {
                            EditorGUILayout.LabelField("Projectiles", EditorStyles.boldLabel);
                            CustomHelpLabelField("Total projectile amounts are based on how many attack animations your AI is currently using.", false);
                        }
                        else if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                        {
                            EditorGUILayout.LabelField("Damage Amounts", EditorStyles.boldLabel);
                            CustomHelpLabelField("Total damage amounts are based on how many attack animations your AI is currently using.", false);
                        }
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(15);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.Space();
                        EditorGUILayout.PropertyField(RandomizeDamageProp, new GUIContent("Use Random Damage"));
                        CustomHelpLabelField("Use Random Damage controls whether your AI will randomize their damage or use a constant damage amount.", true);

                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                        {
                            GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                            EditorGUILayout.BeginVertical("Box");
                            EditorGUILayout.LabelField("Attack 1 Projectile", EditorStyles.boldLabel);
                            GUI.backgroundColor = Color.white;
                            EditorGUILayout.EndVertical();
                            GUI.backgroundColor = Color.white;

                            CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack1ProjectileProp, "Projectile Object", typeof(GameObject), true, 1);
                            CustomHelpLabelField("The projectile gameobject your AI will use for the Attack 1 animation.", false);
                        }
                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                        {
                            EditorGUILayout.Space();
                            GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                            EditorGUILayout.BeginVertical("Box");
                            EditorGUILayout.LabelField("Attack 1", EditorStyles.boldLabel);
                            GUI.backgroundColor = Color.white;
                            EditorGUILayout.EndVertical();
                            GUI.backgroundColor = Color.white;
                        }
                        if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.Yes)
                        {
                            CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmount1Prop, "Min Damage");
                            CustomHelpLabelField("Controls the minimum damage your AI can do with Attack 1. This amount will be randomized with your maximum damage.", false);
                            CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmount1Prop, "Max Damage");
                            CustomHelpLabelField("Controls the maximum damage your AI can do with Attack 1. This amount will be randomized with your minimum damage.", false);
                        }
                        if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.No)
                        {
                            CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), DamageAmount1Prop, "Damage");
                            CustomHelpLabelField("Controls how much damage your AI can do with Attack 1.", false);
                        }

                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                        {
                            CustomIntFieldProjectiles(new Rect(), new GUIContent(), Attack1ProjectileSpeedProp, "Projectile Speed", 1);
                            CustomHelpLabelField("Controls how fast the Attack 1 Projectile will go.", false);

                            CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack1CollisionSecondsProp, "Collision Timeout", 1);
                            CustomHelpLabelField("Controls how fast the Attack 1 Projectile will deactivate after it has collided with an object. A delay is useful if your projectile " +
                                "has a trail or effect. A value of 0 can be used if you'd like to deactivate instantly.", true);

                            CustomPopupProjectiles(new Rect(), new GUIContent(), Attack1HeatSeekingProp, "Heat Seeking?", typeof(EmeraldAISystem.HeatSeeking), 1);
                            CustomHelpLabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for an AI to hit " +
                                "their target and makes it more challanging for them to avoid.", false);

                            if (self.Attack1HeatSeekingRef == EmeraldAISystem.HeatSeeking.Yes)
                            {
                                EditorGUILayout.BeginHorizontal();
                                GUILayout.Space(15);
                                EditorGUILayout.BeginVertical();

                                CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack1HeatSeekingSecondsProp, "Heat Seeking Seconds", 1);
                                CustomHelpLabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this happens, " +
                                    "the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", false);

                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space();
                            }

                            CustomPopupProjectiles(new Rect(), new GUIContent(), Attack1EffectOnCollisionProp, "Effect on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 1);
                            CustomHelpLabelField("Controls whether or not this projectile will play an effect upon collision.", false);

                            if (self.Attack1EffectOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                            {
                                EditorGUILayout.BeginHorizontal();
                                GUILayout.Space(15);
                                EditorGUILayout.BeginVertical();

                                CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack1CollisionEffectProp, "Collision Effect", typeof(GameObject), true, 1);
                                CustomHelpLabelField("The effect that happens after your projectile has collided with an object.", false);

                                CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Effect1TimeoutSecondsProp, "Effect Timeout Seconds", 1);
                                CustomHelpLabelField("Controls how long the effect will be visible before being deactivated.", false);

                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space();
                            }

                            CustomPopupProjectiles(new Rect(), new GUIContent(), Attack1SoundOnCollisionProp, "Sound on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 1);
                            CustomHelpLabelField("Controls whether or not this projectile will play a sound upon collision.", false);

                            if (self.Attack1SoundOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                            {
                                EditorGUILayout.BeginHorizontal();
                                GUILayout.Space(15);
                                EditorGUILayout.BeginVertical();
                                CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack1ImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 1);
                                CustomHelpLabelField("The sound effect that happens after your projectile has collided with an object.", false);
                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space();
                            }

                            CustomPopupProjectiles(new Rect(), new GUIContent(), ArrowProjectileAttack1Prop, "Arrow Projectile?", typeof(EmeraldAISystem.YesOrNo), 1);
                            CustomHelpLabelField("Controls whether or not this projectile will deactivate after hitting a target, " +
                                "but stay active for the collision seconds if hitting a non-target object. This is useful for projectiles like arrows.", false);

                            if (Projectile1UpdatedProp.boolValue && Attack1ProjectileProp.objectReferenceValue != null)
                            {
                                EditorGUILayout.BeginHorizontal("Box");
                                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                EditorGUILayout.LabelField("Projectile 1 has changed, please update.", EditorStyles.helpBox);
                                GUI.backgroundColor = Color.white;
                                if (GUILayout.Button("Update Projectile 1"))
                                {
                                    if (self.Attack1Projectile.GetComponent<EmeraldAIProjectile>() == null)
                                    {
                                        self.Attack1Projectile.AddComponent<EmeraldAIProjectile>();
                                        self.Attack1Projectile.GetComponent<SphereCollider>().radius = 0.02f;
                                        EmeraldAIProjectile ProjectileScript = self.Attack1Projectile.GetComponent<EmeraldAIProjectile>();

                                        //Set custom variables
                                        ProjectileScript.CollisionEffect = (GameObject)Attack1CollisionEffectProp.objectReferenceValue;
                                        ProjectileScript.ImpactSound = (AudioClip)Attack1ImpactSoundProp.objectReferenceValue;
                                        ProjectileScript.ProjectileSpeed = Attack1ProjectileSpeedProp.intValue;
                                        ProjectileScript.CollisionTime = Attack1CollisionSecondsProp.floatValue;
                                        ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack1EffectOnCollisionProp.intValue;
                                        ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack1SoundOnCollisionProp.intValue;
                                        ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)Attack1HeatSeekingProp.intValue;
                                        ProjectileScript.HeatSeekingSeconds = Attack1HeatSeekingSecondsProp.floatValue;
                                        ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileAttack1Prop.intValue;

                                        if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                        {
                                            self.Attack1CollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                            self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
                                            EditorUtility.SetDirty(self.Attack1CollisionEffect);
                                        }
                                        else if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                        {
                                            self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
                                            EditorUtility.SetDirty(self.Attack1CollisionEffect);
                                        }
                                    }
                                    else
                                    {
                                        EmeraldAIProjectile ProjectileScript = self.Attack1Projectile.GetComponent<EmeraldAIProjectile>();
                                        self.Attack1Projectile.GetComponent<SphereCollider>().radius = 0.02f;

                                        //Set custom variables
                                        ProjectileScript.CollisionEffect = (GameObject)Attack1CollisionEffectProp.objectReferenceValue;
                                        ProjectileScript.ImpactSound = (AudioClip)Attack1ImpactSoundProp.objectReferenceValue;
                                        ProjectileScript.ProjectileSpeed = Attack1ProjectileSpeedProp.intValue;
                                        ProjectileScript.CollisionTime = Attack1CollisionSecondsProp.floatValue;
                                        ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack1EffectOnCollisionProp.intValue;
                                        ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack1SoundOnCollisionProp.intValue;
                                        ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)Attack1HeatSeekingProp.intValue;
                                        ProjectileScript.HeatSeekingSeconds = Attack1HeatSeekingSecondsProp.floatValue;
                                        ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileAttack1Prop.intValue;

                                        if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                        {
                                            self.Attack1CollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                            self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
                                            EditorUtility.SetDirty(self.Attack1CollisionEffect);
                                        }
                                        else if (self.Attack1CollisionEffect != null && self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                        {
                                            self.Attack1CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect1TimeoutSecondsProp.floatValue;
                                            EditorUtility.SetDirty(self.Attack1CollisionEffect);
                                        }
                                    }
                                    EditorUtility.SetDirty(self);
                                    EditorUtility.SetDirty(self.Attack1Projectile);
                                    Projectile1UpdatedProp.boolValue = false;
                                }
                                EditorGUILayout.EndHorizontal();
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();
                        }

                        if (self.TotalAttackAnimations == 2 || self.TotalAttackAnimations == 3)
                        {
                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                            {

                                EditorGUILayout.Space();
                                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                                EditorGUILayout.BeginVertical("Box");
                                EditorGUILayout.LabelField("Attack 2 Projectile", EditorStyles.boldLabel);
                                GUI.backgroundColor = Color.white;
                                EditorGUILayout.EndVertical();
                                GUI.backgroundColor = Color.white;

                                CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack2ProjectileProp, "Projectile Object", typeof(GameObject), true, 2);
                                CustomHelpLabelField("The projectile gameobject your AI will use for the Attack 2 animation.", false);
                            }
                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                            {
                                EditorGUILayout.Space();
                                EditorGUILayout.Space();
                                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                                EditorGUILayout.BeginVertical("Box");
                                EditorGUILayout.LabelField("Attack 2", EditorStyles.boldLabel);
                                GUI.backgroundColor = Color.white;
                                EditorGUILayout.EndVertical();
                                GUI.backgroundColor = Color.white;
                            }
                            if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.Yes)
                            {
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmount2Prop, "Min Damage");
                                CustomHelpLabelField("Controls the minimum damage your AI can do with Attack 2. This amount will be randomized with your maximum damage.", false);
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmount2Prop, "Max Damage");
                                CustomHelpLabelField("Controls the maximum damage your AI can do with Attack 2. This amount will be randomized with your minimum damage.", false);
                            }
                            if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.No)
                            {
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), DamageAmount2Prop, "Attack 2 Damage");
                                CustomHelpLabelField("Controls how much damage your AI can do with Attack 2.", false);
                            }

                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                            {
                                CustomIntFieldProjectiles(new Rect(), new GUIContent(), Attack2ProjectileSpeedProp, "Projectile Speed", 2);
                                CustomHelpLabelField("Controls how fast the Attack 2 Projectile will go.", false);

                                CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack2CollisionSecondsProp, "Collision Timeout", 2);
                                CustomHelpLabelField("Controls how fast the Attack 2 Projectile will deactivate after it has collided with an object. A delay is useful if your projectile " +
                                    "has a trail or effect. A value of 0 can be used if you'd like to deactivate instantly.", true);

                                CustomPopupProjectiles(new Rect(), new GUIContent(), Attack2HeatSeekingProp, "Heat Seeking?", typeof(EmeraldAISystem.HeatSeeking), 2);
                                CustomHelpLabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for an AI to " +
                                    "hit their target and makes it more challanging for them to avoid.", false);

                                if (self.Attack2HeatSeekingRef == EmeraldAISystem.HeatSeeking.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();

                                    CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack2HeatSeekingSecondsProp, "Heat Seeking Seconds", 2);
                                    CustomHelpLabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this happens, " +
                                        "the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", false);

                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), Attack2EffectOnCollisionProp, "Effect on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 2);
                                CustomHelpLabelField("Controls whether or not this projectile will play an effect upon collision.", false);

                                if (self.Attack2EffectOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();

                                    CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack2CollisionEffectProp, "Collision Effect", typeof(GameObject), true, 2);
                                    CustomHelpLabelField("The effect that happens after your projectile has collided with an object.", false);

                                    CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Effect2TimeoutSecondsProp, "Effect Timeout Seconds", 2);
                                    CustomHelpLabelField("Controls how long the effect will be visible before being deactivated.", false);

                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), Attack2SoundOnCollisionProp, "Sound on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 2);
                                CustomHelpLabelField("Controls whether or not this projectile will play a sound upon collision.", false);

                                if (self.Attack2SoundOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();
                                    CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack2ImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 2);
                                    CustomHelpLabelField("The sound effect that happens after your projectile has collided with an object.", false);
                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), ArrowProjectileAttack2Prop, "Arrow Projectile?", typeof(EmeraldAISystem.YesOrNo), 2);
                                CustomHelpLabelField("Controls whether or not this projectile will deactivate after hitting a target, " +
                                    "but stay active for the collision seconds if hitting a non-target object. This is useful for projectiles like arrows.", false);

                                if (Projectile2UpdatedProp.boolValue && Attack2ProjectileProp.objectReferenceValue != null)
                                {
                                    EditorGUILayout.BeginHorizontal("Box");
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("Projectile 2 has changed, please update.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                    if (GUILayout.Button("Update Projectile 2"))
                                    {
                                        if (self.Attack2Projectile.GetComponent<EmeraldAIProjectile>() == null)
                                        {
                                            self.Attack2Projectile.AddComponent<EmeraldAIProjectile>();
                                            self.Attack2Projectile.GetComponent<SphereCollider>().radius = 0.02f;
                                            EmeraldAIProjectile ProjectileScript = self.Attack2Projectile.GetComponent<EmeraldAIProjectile>();

                                            //Set custom variables
                                            ProjectileScript.CollisionEffect = (GameObject)Attack2CollisionEffectProp.objectReferenceValue;
                                            ProjectileScript.ImpactSound = (AudioClip)Attack2ImpactSoundProp.objectReferenceValue;
                                            ProjectileScript.ProjectileSpeed = Attack2ProjectileSpeedProp.intValue;
                                            ProjectileScript.CollisionTime = Attack2CollisionSecondsProp.floatValue;
                                            ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack2EffectOnCollisionProp.intValue;
                                            ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack2SoundOnCollisionProp.intValue;
                                            ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)Attack2HeatSeekingProp.intValue;
                                            ProjectileScript.HeatSeekingSeconds = Attack2HeatSeekingSecondsProp.floatValue;
                                            ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileAttack2Prop.intValue;

                                            if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                            {
                                                self.Attack2CollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                                self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack2CollisionEffect);
                                            }
                                            else if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                            {
                                                self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack2CollisionEffect);
                                            }
                                        }
                                        else
                                        {
                                            EmeraldAIProjectile ProjectileScript = self.Attack2Projectile.GetComponent<EmeraldAIProjectile>();
                                            self.Attack2Projectile.GetComponent<SphereCollider>().radius = 0.02f;

                                            //Set custom variables
                                            ProjectileScript.CollisionEffect = (GameObject)Attack2CollisionEffectProp.objectReferenceValue;
                                            ProjectileScript.ImpactSound = (AudioClip)Attack2ImpactSoundProp.objectReferenceValue;
                                            ProjectileScript.ProjectileSpeed = Attack2ProjectileSpeedProp.intValue;
                                            ProjectileScript.CollisionTime = Attack2CollisionSecondsProp.floatValue;
                                            ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack2EffectOnCollisionProp.intValue;
                                            ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack2SoundOnCollisionProp.intValue;
                                            ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)Attack2HeatSeekingProp.intValue;
                                            ProjectileScript.HeatSeekingSeconds = Attack2HeatSeekingSecondsProp.floatValue;
                                            ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileAttack2Prop.intValue;

                                            if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                            {
                                                self.Attack2CollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                                self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack2CollisionEffect);
                                            }
                                            else if (self.Attack2CollisionEffect != null && self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                            {
                                                self.Attack2CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect2TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack2CollisionEffect);
                                            }
                                        }
                                        EditorUtility.SetDirty(self);
                                        EditorUtility.SetDirty(self.Attack2Projectile);
                                        Projectile2UpdatedProp.boolValue = false;
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.Space();
                                EditorGUILayout.Space();
                            }

                        }

                        if (self.TotalAttackAnimations == 3)
                        {
                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                            {

                                EditorGUILayout.Space();
                                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                                EditorGUILayout.BeginVertical("Box");
                                EditorGUILayout.LabelField("Attack 3 Projectile", EditorStyles.boldLabel);
                                GUI.backgroundColor = Color.white;
                                EditorGUILayout.EndVertical();
                                GUI.backgroundColor = Color.white;

                                CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack3ProjectileProp, "Projectile Object", typeof(GameObject), true, 3);
                                CustomHelpLabelField("The projectile gameobject your AI will use for the Attack 3 animation.", false);
                            }
                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                            {
                                EditorGUILayout.Space();
                                EditorGUILayout.Space();
                                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                                EditorGUILayout.BeginVertical("Box");
                                EditorGUILayout.LabelField("Attack 3", EditorStyles.boldLabel);
                                GUI.backgroundColor = Color.white;
                                EditorGUILayout.EndVertical();
                                GUI.backgroundColor = Color.white;
                            }
                            if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.Yes)
                            {
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmount3Prop, "Min Damage");
                                CustomHelpLabelField("Controls the minimum damage your AI can do with Attack 3. This amount will be randomized with your maximum damage.", false);
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmount3Prop, "Max Damage");
                                CustomHelpLabelField("Controls the maximum damage your AI can do with Attack 3. This amount will be randomized with your minimum damage.", true);
                            }
                            if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.No)
                            {
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), DamageAmount3Prop, "Attack 3 Damage");
                                CustomHelpLabelField("Controls how much damage your AI can do with Attack 3.", true);
                            }

                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                            {
                                CustomIntFieldProjectiles(new Rect(), new GUIContent(), Attack3ProjectileSpeedProp, "Projectile Speed", 3);
                                CustomHelpLabelField("Controls how fast the Attack 3 Projectile will go.", false);

                                CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack3CollisionSecondsProp, "Collision Timeout", 3);
                                CustomHelpLabelField("Controls how fast the Attack 3 Projectile will deactivate after it has collided with an object. A delay is useful if your projectile " +
                                    "has a trail or effect. A value of 0 can be used if you'd like to deactivate instantly.", true);

                                CustomPopupProjectiles(new Rect(), new GUIContent(), Attack3HeatSeekingProp, "Heat Seeking?", typeof(EmeraldAISystem.HeatSeeking), 3);
                                CustomHelpLabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for " +
                                    "an AI to hit their target and makes it more challanging for them to avoid.", false);

                                if (self.Attack3HeatSeekingRef == EmeraldAISystem.HeatSeeking.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();

                                    CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Attack3HeatSeekingSecondsProp, "Heat Seeking Seconds", 3);
                                    CustomHelpLabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this " +
                                        "happens, the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", false);

                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), Attack3EffectOnCollisionProp, "Effect on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 3);
                                CustomHelpLabelField("Controls whether or not this projectile will play an effect upon collision.", false);

                                if (self.Attack3EffectOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();

                                    CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack3CollisionEffectProp, "Collision Effect", typeof(GameObject), true, 3);
                                    CustomHelpLabelField("The effect that happens after your projectile has collided with an object.", false);

                                    CustomFloatFieldProjectiles(new Rect(), new GUIContent(), Effect3TimeoutSecondsProp, "Effect Timeout Seconds", 3);
                                    CustomHelpLabelField("Controls how long the effect will be visible before being deactivated.", false);

                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), Attack3SoundOnCollisionProp, "Sound on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 3);
                                CustomHelpLabelField("Controls whether or not this projectile will play a sound upon collision.", false);

                                if (self.Attack3SoundOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();
                                    CustomObjectFieldProjectiles(new Rect(), new GUIContent(), Attack3ImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 3);
                                    CustomHelpLabelField("The sound effect that happens after your projectile has collided with an object.", false);
                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), ArrowProjectileAttack3Prop, "Arrow Projectile?", typeof(EmeraldAISystem.YesOrNo), 3);
                                CustomHelpLabelField("Controls whether or not this projectile will deactivate after hitting a target, " +
                                    "but stay active for the collision seconds if hitting a non-target object. This is useful for projectiles like arrows.", false);

                                if (Projectile3UpdatedProp.boolValue && Attack3ProjectileProp.objectReferenceValue != null)
                                {
                                    EditorGUILayout.BeginHorizontal("Box");
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("Projectile 3 has changed, please update.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                    if (GUILayout.Button("Update Projectile 3"))
                                    {
                                        if (self.Attack3Projectile.GetComponent<EmeraldAIProjectile>() == null)
                                        {
                                            self.Attack3Projectile.AddComponent<EmeraldAIProjectile>();
                                            self.Attack3Projectile.GetComponent<SphereCollider>().radius = 0.02f;
                                            EmeraldAIProjectile ProjectileScript = self.Attack3Projectile.GetComponent<EmeraldAIProjectile>();

                                            //Set custom variables
                                            ProjectileScript.CollisionEffect = (GameObject)Attack3CollisionEffectProp.objectReferenceValue;
                                            ProjectileScript.ImpactSound = (AudioClip)Attack3ImpactSoundProp.objectReferenceValue;
                                            ProjectileScript.ProjectileSpeed = Attack3ProjectileSpeedProp.intValue;
                                            ProjectileScript.CollisionTime = Attack3CollisionSecondsProp.floatValue;
                                            ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack3EffectOnCollisionProp.intValue;
                                            ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack3SoundOnCollisionProp.intValue;
                                            ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)Attack3HeatSeekingProp.intValue;
                                            ProjectileScript.HeatSeekingSeconds = Attack3HeatSeekingSecondsProp.floatValue;
                                            ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileAttack3Prop.intValue;

                                            if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                            {
                                                self.Attack3CollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                                self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect3TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack3CollisionEffect);
                                            }
                                            else if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                            {
                                                self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect3TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack3CollisionEffect);
                                            }
                                        }
                                        else
                                        {
                                            EmeraldAIProjectile ProjectileScript = self.Attack3Projectile.GetComponent<EmeraldAIProjectile>();
                                            self.Attack3Projectile.GetComponent<SphereCollider>().radius = 0.02f;

                                            //Set custom variables
                                            ProjectileScript.CollisionEffect = (GameObject)Attack3CollisionEffectProp.objectReferenceValue;
                                            ProjectileScript.ImpactSound = (AudioClip)Attack3ImpactSoundProp.objectReferenceValue;
                                            ProjectileScript.ProjectileSpeed = Attack3ProjectileSpeedProp.intValue;
                                            ProjectileScript.CollisionTime = Attack3CollisionSecondsProp.floatValue;
                                            ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack3EffectOnCollisionProp.intValue;
                                            ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)Attack3SoundOnCollisionProp.intValue;
                                            ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)Attack3HeatSeekingProp.intValue;
                                            ProjectileScript.HeatSeekingSeconds = Attack3HeatSeekingSecondsProp.floatValue;
                                            ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileAttack3Prop.intValue;

                                            if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                            {
                                                self.Attack3CollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                                self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect3TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack3CollisionEffect);
                                            }
                                            else if (self.Attack3CollisionEffect != null && self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                            {
                                                self.Attack3CollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = Effect3TimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.Attack3CollisionEffect);
                                            }
                                        }
                                        EditorUtility.SetDirty(self);
                                        EditorUtility.SetDirty(self.Attack3Projectile);
                                        Projectile3UpdatedProp.boolValue = false;
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.Space();
                                EditorGUILayout.Space();
                            }
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();
                        EditorGUILayout.PropertyField(UseRunAttacksProp, new GUIContent("Use Run Attacks"));
                        CustomHelpLabelField("Controls whether or not this AI will use run attacks.", true);

                        if (self.UseRunAttacksRef == EmeraldAISystem.UseRunAttacks.Yes)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(15);
                            EditorGUILayout.BeginVertical();

                            EditorGUILayout.Space();
                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MinimumRunAttackSpeedProp, "Min Run Attack Speed", 1, 8);
                            CustomHelpLabelField("Controls the minimum amount of time it takes for your AI to attack with a run attack. This amount is randomized with Maximum Run Attack Speed.", true);

                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaximumRunAttackSpeedProp, "Max Run Attack Speed", 1, 8);
                            CustomHelpLabelField("Controls the maximum amount of time it takes for your AI to attack with a run attack. This amount is randomized with Minimum Run Attack Speed.", true);

                            CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), RunAttackDistanceProp, "Run Attack Distance", 1f, 15);
                            CustomHelpLabelField("Controls the additional distance in which an AI will attack with its running animation. This value is based off of your AI's Attack Distance.", true);

                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                            {
                                EditorGUILayout.Space();
                                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                                EditorGUILayout.BeginVertical("Box");
                                EditorGUILayout.LabelField("Run Attack Projectile", EditorStyles.boldLabel);
                                GUI.backgroundColor = Color.white;
                                EditorGUILayout.EndVertical();
                                GUI.backgroundColor = Color.white;

                                CustomObjectFieldProjectiles(new Rect(), new GUIContent(), RunAttackProjectileProp, "Projectile Object", typeof(GameObject), true, 4);
                                CustomHelpLabelField("The projectile gameobject your AI will use for the Run Attack animation.", false);
                            }
                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                            {
                                EditorGUILayout.Space();
                                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                                EditorGUILayout.BeginVertical("Box");
                                EditorGUILayout.LabelField("Run Attack", EditorStyles.boldLabel);
                                GUI.backgroundColor = Color.white;
                                EditorGUILayout.EndVertical();
                                GUI.backgroundColor = Color.white;
                            }
                            if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.Yes)
                            {
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MinimumDamageAmountRunProp, "Min Damage");
                                CustomHelpLabelField("Controls the minimum damage your AI can do with a Run Attack. This amount will be randomized with your maximum damage.", false);
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MaximumDamageAmountRunProp, "Max Damage");
                                CustomHelpLabelField("Controls the maximum damage your AI can do with a Run Attack. This amount will be randomized with your minimum damage.", true);
                            }
                            if (self.RandomizeDamageRef == EmeraldAISystem.RandomizeDamage.No)
                            {
                                CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), DamageAmountRunProp, "Run Attack Damage");
                                CustomHelpLabelField("Controls how much damage your AI can do with a Run Attack.", true);
                            }

                            if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Ranged)
                            {
                                CustomIntFieldProjectiles(new Rect(), new GUIContent(), RunAttackProjectileSpeedProp, "Projectile Speed", 4);
                                CustomHelpLabelField("Controls how fast the Run Attack Projectile will go.", false);

                                CustomFloatFieldProjectiles(new Rect(), new GUIContent(), RunAttackCollisionSecondsProp, "Collision Timeout", 4);
                                CustomHelpLabelField("Controls how fast the Run Attacl Projectile will deactivate after it has collided with an object. A delay is useful if your " +
                                    "projectile has a trail or effect. A value of 0 can be used if you'd like to deactivate instantly.", true);

                                CustomPopupProjectiles(new Rect(), new GUIContent(), RunAttackHeatSeekingProp, "Heat Seeking?", typeof(EmeraldAISystem.HeatSeeking), 4);
                                CustomHelpLabelField("Controls whether or not this projectile will follow their target. A Heat Seeking projectile makes it easier for " +
                                    "an AI to hit their target and makes it more challanging for them to avoid.", false);

                                if (self.RunAttackHeatSeekingRef == EmeraldAISystem.HeatSeeking.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();

                                    CustomFloatFieldProjectiles(new Rect(), new GUIContent(), RunAttackHeatSeekingSecondsProp, "Heat Seeking Seconds", 4);
                                    CustomHelpLabelField("Controls how many seconds the projectile will heat seek for until the heat seeking feature is disabled. After this " +
                                        "happens, the projectile will continue to fly in the direction it was going after the heat seeking feature was disabled.", false);

                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), RunAttackEffectOnCollisionProp, "Effect on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 4);
                                CustomHelpLabelField("Controls whether or not this projectile will play an effect upon collision.", false);

                                if (self.RunAttackEffectOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();

                                    CustomObjectFieldProjectiles(new Rect(), new GUIContent(), RunAttackCollisionEffectProp, "Collision Effect", typeof(GameObject), true, 4);
                                    CustomHelpLabelField("The effect that happens after your projectile has collided with an object.", false);

                                    CustomFloatFieldProjectiles(new Rect(), new GUIContent(), RunEffectTimeoutSecondsProp, "Effect Timeout Seconds", 4);
                                    CustomHelpLabelField("Controls how long the effect will be visible before being deactivated.", false);

                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), RunAttackSoundOnCollisionProp, "Sound on Collision?", typeof(EmeraldAISystem.EffectOnCollision), 4);
                                CustomHelpLabelField("Controls whether or not this projectile will play a sound upon collision.", false);

                                if (self.RunAttackSoundOnCollisionRef == EmeraldAISystem.EffectOnCollision.Yes)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    GUILayout.Space(15);
                                    EditorGUILayout.BeginVertical();
                                    CustomObjectFieldProjectiles(new Rect(), new GUIContent(), RunAttackImpactSoundProp, "Collision Sound", typeof(AudioClip), true, 4);
                                    CustomHelpLabelField("The sound effect that happens after your projectile has collided with an object.", false);
                                    EditorGUILayout.EndVertical();
                                    EditorGUILayout.EndHorizontal();
                                    EditorGUILayout.Space();
                                }

                                CustomPopupProjectiles(new Rect(), new GUIContent(), ArrowProjectileRunAttackProp, "Arrow Projectile?", typeof(EmeraldAISystem.YesOrNo), 4);
                                CustomHelpLabelField("Controls whether or not this projectile will deactivate after hitting a target, " +
                                    "but stay active for the collision seconds if hitting a non-target object. This is useful for projectiles like arrows.", false);

                                if (RunProjectileUpdatedProp.boolValue && RunAttackProjectileProp.objectReferenceValue != null)
                                {
                                    EditorGUILayout.BeginHorizontal("Box");
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The Run Projectile has changed, please update.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                    if (GUILayout.Button("Update Run Projectile"))
                                    {
                                        if (self.RunAttackProjectile.GetComponent<EmeraldAIProjectile>() == null)
                                        {
                                            self.RunAttackProjectile.AddComponent<EmeraldAIProjectile>();
                                            self.RunAttackProjectile.GetComponent<SphereCollider>().radius = 0.1f;
                                            EmeraldAIProjectile ProjectileScript = self.RunAttackProjectile.GetComponent<EmeraldAIProjectile>();

                                            //Set custom variables
                                            ProjectileScript.CollisionEffect = (GameObject)RunAttackCollisionEffectProp.objectReferenceValue;
                                            ProjectileScript.ImpactSound = (AudioClip)RunAttackImpactSoundProp.objectReferenceValue;
                                            ProjectileScript.ProjectileSpeed = RunAttackProjectileSpeedProp.intValue;
                                            ProjectileScript.CollisionTime = RunAttackCollisionSecondsProp.floatValue;
                                            ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)RunAttackEffectOnCollisionProp.intValue;
                                            ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)RunAttackSoundOnCollisionProp.intValue;
                                            ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)RunAttackHeatSeekingProp.intValue;
                                            ProjectileScript.HeatSeekingSeconds = RunAttackHeatSeekingSecondsProp.floatValue;
                                            ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileRunAttackProp.intValue;

                                            if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                            {
                                                self.RunAttackCollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                                self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.RunAttackCollisionEffect);
                                            }
                                            else if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                            {
                                                self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.RunAttackCollisionEffect);
                                            }
                                        }
                                        else
                                        {
                                            EmeraldAIProjectile ProjectileScript = self.RunAttackProjectile.GetComponent<EmeraldAIProjectile>();
                                            self.RunAttackProjectile.GetComponent<SphereCollider>().radius = 0.1f;

                                            //Set custom variables
                                            ProjectileScript.CollisionEffect = (GameObject)RunAttackCollisionEffectProp.objectReferenceValue;
                                            ProjectileScript.ImpactSound = (AudioClip)RunAttackImpactSoundProp.objectReferenceValue;
                                            ProjectileScript.ProjectileSpeed = RunAttackProjectileSpeedProp.intValue;
                                            ProjectileScript.CollisionTime = RunAttackCollisionSecondsProp.floatValue;
                                            ProjectileScript.EffectOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)RunAttackEffectOnCollisionProp.intValue;
                                            ProjectileScript.SoundOnCollisionRef = (EmeraldAIProjectile.EffectOnCollision)RunAttackSoundOnCollisionProp.intValue;
                                            ProjectileScript.HeatSeekingRef = (EmeraldAIProjectile.HeatSeeking)RunAttackHeatSeekingProp.intValue;
                                            ProjectileScript.HeatSeekingSeconds = RunAttackHeatSeekingSecondsProp.floatValue;
                                            ProjectileScript.ArrowProjectileRef = (EmeraldAIProjectile.ArrowObject)ArrowProjectileRunAttackProp.intValue;

                                            if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                            {
                                                self.RunAttackCollisionEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                                self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.RunAttackCollisionEffect);
                                            }
                                            else if (self.RunAttackCollisionEffect != null && self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                            {
                                                self.RunAttackCollisionEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = RunEffectTimeoutSecondsProp.floatValue;
                                                EditorUtility.SetDirty(self.RunAttackCollisionEffect);
                                            }
                                        }
                                        EditorUtility.SetDirty(self);
                                        EditorUtility.SetDirty(self.RunAttackProjectile);
                                        RunProjectileUpdatedProp.boolValue = false;
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }

                                EditorGUILayout.Space();
                                EditorGUILayout.Space();
                            }

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }

                    if (CombatTabNumberProp.intValue == 1)
                    {
                        EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));

                        GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                        EditorGUILayout.BeginVertical("Box");
                        EditorGUILayout.LabelField("Combat Actions", EditorStyles.boldLabel);
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndVertical();
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(UseWeaponObjectProp, new GUIContent("Use Weapon Object"));
                        CustomHelpLabelField("Controls whether or not this AI will enable and disable its weapon object when switching between combat and non-combat mode.", true);

                        if (self.UseWeaponObject == EmeraldAISystem.YesOrNo.Yes)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            EditorGUILayout.BeginVertical();

                            CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), WeaponObjectProp, "Weapon Object", typeof(GameObject), true);
                            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                            EditorGUILayout.LabelField("The weapon model that the AI uses. This can be enabled and disabled with an Animation Event and " +
                                "should be an item that's placed on your AI's hand transform.", EditorStyles.helpBox);
                            GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                            EditorGUILayout.LabelField("Note: You will need to manually create an EnableWeapon or DisableWeapon Animation Event to use this feature. " +
                                "Please refer to Emerlad's documentation for a tutorial on how to do this.", EditorStyles.helpBox);
                            GUI.backgroundColor = new Color(0, 0.65f, 0, 0.8f);
                            if (GUILayout.Button("See Tutorial", HelpButtonStyle, GUILayout.Height(20)))
                            {
                                Application.OpenURL("https://docs.google.com/document/d/1_zXR1gg61soAX_bZscs6HC-7as2njM7Jx9pYlqgbtM8/edit#heading=h.tpf8u6pakj4k");
                            }
                            GUI.backgroundColor = Color.white;
                            EditorGUILayout.Space();

                            EditorGUILayout.PropertyField(UseDroppableWeaponProp, new GUIContent("Use Droppable Weapon"));
                            CustomHelpLabelField("Controls whether or not this AI will drop a weapon object when it dies.", true);

                            if (self.UseDroppableWeapon == EmeraldAISystem.YesOrNo.Yes)
                            {
                                EditorGUILayout.BeginHorizontal();
                                GUILayout.Space(10);
                                EditorGUILayout.BeginVertical();

                                EditorGUILayout.PropertyField(DroppableWeaponObjectProp, new GUIContent("Droppable Weapon Object"));
                                CustomHelpLabelField("The object that is dropped when the AI dies.", false);
                                GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                                EditorGUILayout.LabelField("Note: This should be a copy of the AI's Weapon Object and should have a collider and Rigidbody attached to it. " +
                                    "The AI's above Weapon Object will automatically be disabled on death.", EditorStyles.helpBox);
                                GUI.backgroundColor = Color.white;
                                EditorGUILayout.Space();

                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
                            }

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.PropertyField(UseBlockingProp, new GUIContent("Use Blocking"));
                        CustomHelpLabelField("Controls whether or not this AI will have the ability to block. AI who use block must have a Block and Block Hit animation " +
                            "as well as have Use Hit Animations enabled.", true);

                        if (self.UseBlockingRef == EmeraldAISystem.YesOrNo.Yes)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            EditorGUILayout.BeginVertical();

                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), BlockingOddsProp, "Blocking Odds", 1, 100);
                            CustomHelpLabelField("Controls the odds, in percent, of an AI blocking. Currently, your AI will use blocking "
                                + self.BlockOdds + "% of the time while not attacking.", true);

                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MitigationAmountProp, "Block Mitigation", 0, 100);
                            CustomHelpLabelField("Controls the percentage of damage that is mitigated from blocking. Currently, damage will be reduced by " + self.MitigationAmount + "%.", true);

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.PropertyField(UseAggroProp, new GUIContent("Use Aggro"));
                        CustomHelpLabelField("Controls whether or not this AI will use the Aggro system. This allows AI to switch targets after a certain amount of hits have been met.", true);

                        if (self.UseAggro == EmeraldAISystem.YesOrNo.Yes)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            EditorGUILayout.BeginVertical();

                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), TotalAggroHitsProp, "Total Aggro Hits", 3, 50);
                            CustomHelpLabelField("Controls how many hits are needed before an AI switches targets according to their Aggro Action.", true);

                            EditorGUILayout.PropertyField(AggroActionProp, new GUIContent("Aggro Action"));

                            if (self.AggroActionRef == EmeraldAISystem.AggroAction.LastAttacker)
                            {
                                CustomHelpLabelField("Last Attacker - Will switch the AI's current target to the last attacker, after the total aggro hits have been met.", true);
                            }
                            else if (self.AggroActionRef == EmeraldAISystem.AggroAction.RandomAttacker)
                            {
                                CustomHelpLabelField("Random Attacker - Will switch the AI's current target to a random attacker within " +
                                    "the AI's attack radius, after the total aggro hits have been met.", true);
                            }
                            else if (self.AggroActionRef == EmeraldAISystem.AggroAction.ClosestAttacker)
                            {
                                CustomHelpLabelField("Closest Attacker - Will switch the AI's current target to the closest attacker within " +
                                    "the AI's attack radius, after the total aggro hits have been met.", true);
                            }

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.PropertyField(BackupTypeProp, new GUIContent("Backup Type"));
                        CustomHelpLabelField("Controls how this AI's backup type with the option to disable the back up feature, if desried.", true);

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        if (self.BackupTypeRef == EmeraldAISystem.BackupType.Off)
                        {
                            CustomHelpLabelField("Off - Disables the AI's backup feature.", true);
                        }
                        else if (self.BackupTypeRef == EmeraldAISystem.BackupType.Instant)
                        {
                            CustomHelpLabelField("Instant - Allows the AI to backup instantly whenever a target reaches their 'Too Close Distance'.", true);
                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), BackupDistanceProp, "Backup Distance", (int)self.StoppingDistance + 4, 20);
                            CustomHelpLabelField("Controls how far this AI will backup.", true);
                        }
                        else if (self.BackupTypeRef == EmeraldAISystem.BackupType.Odds)
                        {
                            CustomHelpLabelField("Odds - Allows the AI to backup whenever a target reaches their 'Too Close Distance', but only if the generated odds have been met.", true);
                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), BackUpOddsProp, "Backup Odds", 1, 99);
                            CustomHelpLabelField("Controls how this AI's backup type with the option to disable the back up feature, if desried.", true);
                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), BackupDistanceProp, "Backup Distance", (int)self.StoppingDistance + 4, 20);
                            CustomHelpLabelField("Controls how far this AI will backup.", true);
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();

                        if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("(Not Usable with Companion AI)", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }
                        else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Pet)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("(Not Usable with Pet AI)", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }

                        EditorGUILayout.Space();
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), DeathDelayMinProp, "Min Resume Wandering", 0, 6);
                        CustomHelpLabelField("Controls the minimum amount of time with how quickly an AI will resume wandering according to its Wandering Type after it has engaged " +
                            "in combat. This amount will be randomized with the Maximum Resume Wandering Delay.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), DeathDelayMaxProp, "Max Resume Wandering", 0, 6);
                        CustomHelpLabelField("Controls the maximum amount of time with how quickly an AI will resume wandering according to its Wandering Type after it has engaged " +
                            "in combat. This amount will be randomized with the Minimum Resume Wandering Delay.", true);

                        if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("(Not Usable with Companion AI)", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }
                        else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Pet)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("(Not Usable with Pet AI)", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }

                        if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Cautious)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("(Cautious AI Only)", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }
                        EditorGUILayout.PropertyField(UseWarningAnimationProp, new GUIContent("Use Warning"));
                        CustomHelpLabelField("Controls whether or not this AI will play a warning animation and sound if they feel threatened. The Warning animation can be set " +
                            "within the Animations tab under the Idle section.", true);
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), CautiousSecondsProp, "Cautious Seconds", 3, 12);
                        CustomHelpLabelField("Controls the amount of seconds before a Cautious AI will turn aggressive and attack.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), ProjectileCollisionPointYProp, "Hit Transform", 0, self.transform.localScale.y + 4);
                        CustomHelpLabelField("Controls the transform that other AI will use for using the head look feature allowing them to look at this transform. " +
                            "This transform is also used for ranged combat allowing other AI's projectiles to hit this spot. This is to ensure an AI is always seen, " +
                            "regardless of how large or small they are.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(DeathTypeRefProp, new GUIContent("Death Type"));
                        CustomHelpLabelField("Controls what method an AI uses when they die.", false);

                        if (self.DeathTypeRef == EmeraldAISystem.DeathType.Animation)
                        {
                            CustomHelpLabelField("Animation - Plays a random death animation when an AI dies.", true);
                            EditorGUILayout.PropertyField(SecondsToDisableProp, new GUIContent("Seconds to Disable"));
                            CustomHelpLabelField("Controls how many seconds until your AI is completely disabled. " +
                                "This is to ensure your AI has enough time for its Death Animation to finish playing.", false);
                        }
                        else
                        {
                            CustomHelpLabelField("Ragdoll - Enables an AI's ragdoll components on death allowng its current animation to blend with the ragdoll physics. " +
                                "Note: You must setup an AI's ragdoll components using Unity's built-in system or a custom ragdoll setup system.", false);
                            CustomEditorProperties.CustomTagField(new Rect(), new GUIContent(), RagdollTagProp, "Ragdoll Tag");
                            CustomHelpLabelField("Controls what tag the ragdoll components will be set to when an AI dies.", true);
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }

                    if (CombatTabNumberProp.intValue == 2)
                    {
                        EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));

                        GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                        EditorGUILayout.BeginVertical("Box");
                        EditorGUILayout.LabelField("Hit Effect", EditorStyles.boldLabel);
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndVertical();
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(UseBloodEffectProp, new GUIContent("Use Hit Effect"));
                        CustomHelpLabelField("Controls whether or not this AI will use a hit effect when it receives melee damage.", true);

                        if (self.UseBloodEffectRef == EmeraldAISystem.UseBloodEffect.Yes)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            EditorGUILayout.BeginVertical();

                            CustomObjectFieldHitEffect(new Rect(), new GUIContent(), BloodEffectProp, "Hit Effect", typeof(GameObject), true);
                            CustomHelpLabelField("The blood effect that will appear when this AI receives damage.", true);

                            CustomFloatFieldHitEffect(new Rect(), new GUIContent(), BloodEffectTimeoutSecondsProp, "Hit Effect Timeout Seconds");
                            CustomHelpLabelField("Controls how long the hit effect will be visible before being deactivated.", true);

                            if (BloodEffectUpdatedProp.boolValue && BloodEffectProp.objectReferenceValue != null)
                            {
                                EditorGUILayout.BeginHorizontal("Box");
                                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                EditorGUILayout.LabelField("Hit Effect has changed, please update.", EditorStyles.helpBox);
                                GUI.backgroundColor = Color.white;
                                if (GUILayout.Button("Update Hit Effect"))
                                {
                                    if (self.BloodEffect != null && self.BloodEffect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                                    {
                                        self.BloodEffect.AddComponent<EmeraldAIProjectileTimeout>();
                                        self.BloodEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = BloodEffectTimeoutSecondsProp.floatValue;
                                        EditorUtility.SetDirty(self.BloodEffect);
                                    }
                                    else if (self.BloodEffect != null && self.BloodEffect.GetComponent<EmeraldAIProjectileTimeout>() != null)
                                    {
                                        self.BloodEffect.GetComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = BloodEffectTimeoutSecondsProp.floatValue;
                                        EditorUtility.SetDirty(self.BloodEffect);
                                    }
                                    EditorUtility.SetDirty(self);
                                    EditorUtility.SetDirty(self.BloodEffect);
                                    BloodEffectUpdatedProp.boolValue = false;
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUILayout.Space();

                            EditorGUILayout.PropertyField(BloodEffectPositionTypeProp, new GUIContent("Hit Effect Transform Type"));
                            CustomHelpLabelField("Controls the type of transform the Hit Effect Position will use.", false);

                            if (self.BloodEffectPositionTypeRef == EmeraldAISystem.BloodEffectPositionType.BaseTransform)
                            {
                                CustomHelpLabelField("Base Trasnfrom - Will spawn the hit effect according to the base position of the AI.", true);
                                EditorGUILayout.PropertyField(BloodPosOffsetProp, new GUIContent("Hit Effect Position"));
                                CustomHelpLabelField("Controls the offset position of the hit effect using the AI's base position.", true);
                            }
                            else
                            {
                                CustomHelpLabelField("Hit Transform - Will spawn the hit effect according to the AI's Hit Transform position.", true);
                                EditorGUILayout.PropertyField(BloodPosOffsetProp, new GUIContent("Hit Effect Position"));
                                CustomHelpLabelField("Controls the offset position of the hit effect using the AI's Hit Transform position.", true);
                            }
                            EditorGUILayout.Space();

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }
                }

                if (TemperamentTabNumberProp.intValue == 1)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Movement Settings", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.LabelField("Controls all of an AI's movement related settings such as movement speeds, alignment, turning, wait times, and stopping distances.", EditorStyles.helpBox);
                    EditorGUILayout.EndVertical();

                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.BeginVertical("Box");

                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Speed Settings", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.PropertyField(AnimatorTypeProp, new GUIContent("Movement Type"));
                    CustomHelpLabelField("Controls how an AI is moved. This is either driven by the Root Motion animation or by the NavMesh component.", true);

                    if ((EmeraldAISystem.AnimatorTypeState)self.m_LastAnimatorType != self.AnimatorType)
                    {
                        self.m_LastAnimatorType = (int)self.AnimatorType;
                        if (self.AnimatorControllerGenerated)
                        {
                            EmeraldAIAnimatorGenerator.GenerateAnimatorController(self);
                        }
                    }

                    if (self.AnimatorType == EmeraldAISystem.AnimatorTypeState.RootMotion)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(AutoEnableAnimatePhysicsProp, new GUIContent("Auto Enable Animate Physics"));
                        CustomHelpLabelField("Controls whether or not this AI's Animator Controller will use Animate Physics. When enabled, animation quality and animation syncing is " +
                            "improved. Enable this if you are having issues with your animations not syncing properly with your AI's movement. ", true);

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    if (self.AnimatorType == EmeraldAISystem.AnimatorTypeState.NavMeshDriven)
                    {
                        CustomFloatAnimationField(new Rect(), new GUIContent(), WalkSpeedProp, "Walk Speed", 0.5f, 5);
                        CustomHelpLabelField("Controls how fast your AI walks and directly affects an AI's Root Motion walk animation speed.", true);

                        CustomFloatAnimationField(new Rect(), new GUIContent(), RunSpeedProp, "Run Speed", 0.5f, 10);
                        CustomHelpLabelField("Controls how fast your AI runs and directly affects an AI's Root Motion run animation speed.", true);

                        //Only auto-update the Animator Controller if inside the Unity Editor as runtime auto-updating is not possible.
#if UNITY_EDITOR
                        if (self.AnimatorControllerGenerated)
                        {
                            if (self.AnimationsUpdated || self.AnimationListsChanged)
                            {
                                EmeraldAIAnimatorGenerator.GenerateAnimatorController(self);
                            }
                        }
#endif
                    }
                    else if (self.AnimatorType == EmeraldAISystem.AnimatorTypeState.RootMotion)
                    {
                        GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                        EditorGUILayout.LabelField("When using Root Motion, an AI's Movement Speed is controlled by its animation speed. Please adjust this under the AI's Animation Settings.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                        EditorGUI.BeginDisabledGroup(self.AnimatorType == EmeraldAISystem.AnimatorTypeState.RootMotion);
                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), WalkSpeedProp, "Walk Speed", 0.5f, 5);
                        CustomHelpLabelField("Controls how fast your AI walks and directly affects an AI's Root Motion walk animation speed.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), RunSpeedProp, "Run Speed", 0.5f, 10);
                        CustomHelpLabelField("Controls how fast your AI runs and directly affects an AI's Root Motion run animation speed.", true);
                        EditorGUI.EndDisabledGroup();
                    }

                    EditorGUILayout.PropertyField(CurrentMovementStateProp, new GUIContent("Movement Animation"));
                    CustomHelpLabelField("Controls the type of animation your AI will use when using waypoints, moving to its destination, or wandering. " +
                        "Note: If needed, this can be changed programmatically during runtime.", true);
                    EditorGUILayout.Space();

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), StoppingDistanceProp, "Stopping Distance", 0.25f, 40);
                    CustomHelpLabelField("Controls the distance in which an AI will stop before waypoints and targets.", true);

                    if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        EditorGUILayout.Space();
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Companion/Pet AI Only)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), FollowingStoppingDistanceProp, "Following Stopping Distance", 1, 20);
                    CustomHelpLabelField("Controls the distance in which an AI will stop in front of their following targets.", true);

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");

                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Turn Settings", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), NonCombatAngleToTurnProp, "Turning Angle", 5, 90);
                    CustomHelpLabelField("Controls the angle needed to play a turn animation while an AI is not in combat. Emerald can automatically detect whether an AI is " +
                        "turing left or right. Note: You can use a walking animation in place of a turning animation if your AI doesn't one.", true);

                    CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), CombatAngleToTurnProp, "Combat Turning Angle", 1, 90);
                    CustomHelpLabelField("Controls the angle needed to play a turn animation while an AI is in combat. Emerald can automatically detect whether an AI is " +
                        "turing left or right. Note: You can use a walking animation in place of a turning animation if your AI doesn't one.", true);

                    CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), StationaryTurningSpeedNonCombatProp, "Turn Speed", 5, 300);
                    CustomHelpLabelField("Controls how fast your AI turns while not in combat.", true);

                    if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward && self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious)
                    {
                        EditorGUILayout.Space();
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Not Usable with Cautious Coward AI)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), StationaryTurningSpeedCombatProp, "Combat Turn Speed", 5, 300);
                    CustomHelpLabelField("Controls how fast your AI turns while in combat.", true);

                    EditorGUILayout.PropertyField(UseRandomRotationOnStartProp, new GUIContent("Random Roation on Start"));
                    CustomHelpLabelField("Controls whether or not AI will be randomly rotated on Start.", true);

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");

                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Wait Settings", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MinimumWaitTimeProp, "Min Wait Time");
                    CustomHelpLabelField("Controls the minimum amount of seconds before generating a new waypoint, when using the Dynamic and Random waypoint Wander Type. This amount is " +
                        "randomized with the Maximim Wait Time.", true);

                    CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MaximumWaitTimeProp, "Max Wait Time");
                    CustomHelpLabelField("Controls the maximum amount of seconds before generating a new waypoint, when using the Dynamic and Random waypoint Wander Type. This amount " +
                        "is randomized with the Minimum Wait Time.", true);

                    if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        EditorGUILayout.Space();
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Companion/Pet AI Only)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MinimumFollowWaitTimeProp, "Min Follow Time");
                    CustomHelpLabelField("Controls how quickly a Companion AI will react to following their target.", true);

                    if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        EditorGUILayout.Space();
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Companion/Pet AI Only)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), MaximumFollowWaitTimeProp, "Max Follow Time");
                    CustomHelpLabelField("Controls how quickly a Companion AI will react to following their target.", true);

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");

                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Alignment Settings", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.PropertyField(AlignAIWithGroundProp, new GUIContent("Align AI?"));
                    CustomHelpLabelField("Aligns the AI to the angle of the terrain and other objects for added realism. Disable this feature for improved performance per AI.", true);

                    if (self.AlignAIWithGroundRef == EmeraldAISystem.AlignAIWithGround.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUI.BeginChangeCheck();
                        var layersSelection = EditorGUILayout.MaskField("Alignment Layers", LayerMaskToField(AlignmentLayerMaskProp.intValue), InternalEditorUtility.layers);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(self, "Layers changed");
                            AlignmentLayerMaskProp.intValue = FieldToLayerMask(layersSelection);
                        }
                        CustomHelpLabelField("The layers the AI will use for aligning itself with the angles of surfaces. Any layers not included above will be ignred.", true);

                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(AlignmentQualityProp, new GUIContent("Align Quality"));
                        CustomHelpLabelField("Controls the quality of the Align AI feature by controlling how often it's updated.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), NonCombatAlignSpeedProp, "Non-Combat Align Speed", 10, 35);
                        CustomHelpLabelField("Controls the speed in which the AI is aligned with the ground while not in combat.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), CombatAlignSpeedProp, "Combat Align Speed", 10, 40);
                        CustomHelpLabelField("Controls the speed in which the AI is aligned with the ground while in combat.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaxNormalAngleProp, "Max Angle", 5, 50);
                        CustomHelpLabelField("Controls the maximum angle for an AI to rotate to when aligning with the ground.", true);

                        EditorGUILayout.PropertyField(AlignAIOnStartProp, new GUIContent("Align on Start?"));
                        CustomHelpLabelField("Calculates the Align AI feature on Start.", true);

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndVertical();
                }

                if (TemperamentTabNumberProp.intValue == 3)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("NavMesh Settings", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Controls all of an AI's NavMesh related settings.", EditorStyles.helpBox);
                    EditorGUILayout.Space();

                    CustomEditorProperties.CustomFloatField(new Rect(), new GUIContent(), AgentBaseOffsetProp, "Agent Base Offset");
                    CustomHelpLabelField("Controls the NavMesh Agent's base offset. The base offset gives you control over how high your AI will be above the ground. " +
                        "If you AI is a flying AI type, you will want to adjust this value higher so your AI will be above the ground.", true);

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), AgentRadiusProp, "Agent Radius", 0.1f, 8);
                    CustomHelpLabelField("Controls the NavMesh Agent's avoidence radius.", true);

                    CustomEditorProperties.CustomFloatField(new Rect(), new GUIContent(), AgentTurnSpeedProp, "Agent Turn Speed");
                    CustomHelpLabelField("Controls the NavMesh Agent's turn speed when not in combat.", true);

                    CustomEditorProperties.CustomFloatField(new Rect(), new GUIContent(), AgentAccelerationProp, "Agent Acceleration");
                    CustomHelpLabelField("Controls the NavMesh Agent's acceleration speed. The acceleration speed gives you control over how fast your AI will get to its max speed.", true);

                    EditorGUILayout.PropertyField(AvoidanceQualityProp, new GUIContent("Agent Avoidance Quality"));
                    CustomHelpLabelField("Controls the NavMesh Agent's avoidance quality which controls how accurately your AI will navigate through avoidable objects at " +
                        "the cost of perfomance. These objects include other AI and Nav Mesh Obstacles. If you would like to not use this feature, set the Avoidance Quality to None.", true);

                    EditorGUILayout.EndVertical();
                }

                if (TemperamentTabNumberProp.intValue == 4)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Optimization Settings", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Controls all of the settings that can help optimize an AI such as disabling an AI when their model is culled or not visible.", EditorStyles.helpBox);
                    EditorGUILayout.Space();

                    #if CRUX_PRESENT
                    EditorGUILayout.PropertyField(SpawnedWithCruxProp, new GUIContent("Spawned with Crux"));
                    CustomHelpLabelField("Controls whether or not this AI is being spawned with Crux - Procedural Spawner. This allows Crux to automatically remove and adjust the population of this AI when it's killed.", true);
                    #endif

                    EditorGUILayout.PropertyField(DisableAIWhenNotInViewProp, new GUIContent("Disable when Off-Screen"));
                    CustomHelpLabelField("Controls whether or not this AI is disabled when off screen or is culled.", true);

                    if (self.DisableAIWhenNotInViewRef == EmeraldAISystem.YesOrNo.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(UseDeactivateDelayProp, new GUIContent("Use Deactivate Delay"));
                        CustomHelpLabelField("Controls whether or not there is a delay when using the Disable when Off-Screen feature. If set to No, the AI will be disabled instantly.", true);

                        if (self.UseDeactivateDelayRef == EmeraldAISystem.YesOrNo.Yes)
                        {
                            CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), DeactivateDelayProp, "Deactivate Delay", 1, 30);
                            CustomHelpLabelField("Controls the amount of seconds until the AI will be disabled when either culled or off-screen.", true);
                        }

                        EditorGUILayout.PropertyField(HasMultipleLODsProp, new GUIContent("Has Multiple LODs"));
                        CustomHelpLabelField("Controls whether or not the Disable when Off-Screen feature will use multiple LODs. An AI using this feature must have at have " +
                            "an LOD Group with at least 2 levels. Note: If your AI has multiple LODs, this feature needs to be enabled in order for the Disable when " +
                            "Off-Screen feature to work.", true);

                        if (self.HasMultipleLODsRef == EmeraldAISystem.YesOrNo.Yes)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            EditorGUILayout.BeginVertical();

                            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                            EditorGUILayout.LabelField("Auto Grab LODs will attempt to automatically grab your AI's LODs. Your AI must have a LOD Group component with at least 2 levels to use this feature.", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                            if (GUILayout.Button("Auto Grab LODs"))
                            {
                                LODGroup _LODGroup = self.GetComponentInChildren<LODGroup>();

                                if (_LODGroup == null)
                                {
                                    EditorUtility.DisplayDialog("Error", "No LOD Group could be found. Please ensure that your AI has an LOD group that has at least 2 levels. The Multiple LOD Feature has been disabled.", "Okay");
                                    self.HasMultipleLODsRef = EmeraldAISystem.YesOrNo.No;
                                }
                                else if (_LODGroup != null)
                                {
                                    LOD[] AllLODs = _LODGroup.GetLODs();

                                    if (_LODGroup.lodCount <= 4)
                                    {
                                        TotalLODsProp.intValue = (_LODGroup.lodCount - 2);
                                    }

                                    if (_LODGroup.lodCount > 1)
                                    {
                                        for (int i = 0; i < _LODGroup.lodCount; i++)
                                        {
                                            if (i == 0)
                                            {
                                                Renderer1Prop.objectReferenceValue = AllLODs[i].renderers[0];
                                            }
                                            if (i == 1)
                                            {
                                                Renderer2Prop.objectReferenceValue = AllLODs[i].renderers[0];
                                            }
                                            if (i == 2)
                                            {
                                                Renderer3Prop.objectReferenceValue = AllLODs[i].renderers[0];
                                            }
                                            if (i == 3)
                                            {
                                                Renderer4Prop.objectReferenceValue = AllLODs[i].renderers[0];
                                            }
                                        }
                                    }
                                    else if (_LODGroup.lodCount == 1)
                                    {
                                        EditorUtility.DisplayDialog("Warning", "Your AI's LOD Group only has 1 level and it needs to have at least 2. The Multiple LOD feature has been disabled.", "Okay");
                                        self.HasMultipleLODsRef = EmeraldAISystem.YesOrNo.No;
                                    }
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            EditorGUILayout.PropertyField(TotalLODsProp, new GUIContent("Total LODs"));
                            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                            EditorGUILayout.LabelField("Controls the amount of LODs the Disable when Off-Screen feature will use. You will need to apply each level from your AI's " +
                                "LOD Group below. If any renderers are missing, this feature will be disabled.", EditorStyles.helpBox);
                            EditorGUILayout.LabelField("This feature only supports 1 model per level. Your AI's main model should be used.", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                            EditorGUILayout.Space();

                            if (self.TotalLODsRef == EmeraldAISystem.TotalLODsEnum.Two)
                            {
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer1Prop, "Renderer 1", typeof(Renderer), true);
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer2Prop, "Renderer 2", typeof(Renderer), true);
                            }
                            else if (self.TotalLODsRef == EmeraldAISystem.TotalLODsEnum.Three)
                            {
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer1Prop, "Renderer 1", typeof(Renderer), true);
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer2Prop, "Renderer 2", typeof(Renderer), true);
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer3Prop, "Renderer 3", typeof(Renderer), true);
                            }
                            else if (self.TotalLODsRef == EmeraldAISystem.TotalLODsEnum.Four)
                            {
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer1Prop, "Renderer 1", typeof(Renderer), true);
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer2Prop, "Renderer 2", typeof(Renderer), true);
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer3Prop, "Renderer 3", typeof(Renderer), true);
                                CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), Renderer4Prop, "Renderer 4", typeof(Renderer), true);
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }

                        if (self.HasMultipleLODsRef == EmeraldAISystem.YesOrNo.No)
                        {
                            EditorGUILayout.PropertyField(AIRendererProp, new GUIContent("AI Main Renderer"));
                            CustomHelpLabelField("The AI's Main Renderer.", true);
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndVertical();
                }

                if (TemperamentTabNumberProp.intValue == 5)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Controls all of an AI's events.", EditorStyles.helpBox);
                    GUIContent[] EventButtons = new GUIContent[2] { new GUIContent("General"), new GUIContent("Combat")};
                    EventTabNumberProp.intValue = GUILayout.Toolbar(EventTabNumberProp.intValue, EventButtons, EditorStyles.miniButton, GUILayout.Height(25));
                    EditorGUILayout.EndVertical();

                    if (EventTabNumberProp.intValue == 0)
                    {
                        EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));

                        GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                        EditorGUILayout.BeginVertical("Box");
                        EditorGUILayout.LabelField("General Events", EditorStyles.boldLabel);
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndVertical();
                        GUI.backgroundColor = Color.white;

                        CustomHelpLabelField("Triggers an event on Start. This can be useful for initializing custom mechanics and quests as well as spawning animations.", false);
                        EditorGUILayout.PropertyField(OnStartEventProp);

                        CustomHelpLabelField("Triggers an event when an AI is enabled. This can be useful for events that need to be called when an AI is being respawned.", false);
                        EditorGUILayout.PropertyField(OnEnabledEventProp);

                        EditorGUILayout.Space();
                        CustomHelpLabelField("Triggers an event when the AI reaches their destination when using the Destination Wander Type.", false);
                        EditorGUILayout.PropertyField(ReachedDestinationEventProp);

                        EditorGUILayout.EndVertical();
                    }

                    if (EventTabNumberProp.intValue == 1)
                    {
                        EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));

                        GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                        EditorGUILayout.BeginVertical("Box");
                        EditorGUILayout.LabelField("Combat Events", EditorStyles.boldLabel);
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndVertical();
                        GUI.backgroundColor = Color.white;

                        CustomHelpLabelField("Triggers an event when an AI first starts combat. This can be useful for playing additional sounds or other added functionality.", false);
                        EditorGUILayout.PropertyField(OnStartCombatEventProp);

                        EditorGUILayout.Space();
                        CustomHelpLabelField("Triggers an event when the AI attacks. This can be useful for attack effects.", false);
                        EditorGUILayout.PropertyField(OnAttackEventProp);

                        EditorGUILayout.Space();
                        CustomHelpLabelField("Triggers an event when the AI is damaged.", false);
                        EditorGUILayout.PropertyField(DamageEventProp);

                        CustomHelpLabelField("Triggers an event when an AI flees. This can be useful for fleeing sounds or other added functionality.", false);
                        EditorGUILayout.PropertyField(OnFleeEventProp);

                        EditorGUILayout.Space();
                        CustomHelpLabelField("Triggers an event when the AI dies. This can be useful for triggering loot generation, quest mechanics, or other death related events.", false);
                        EditorGUILayout.PropertyField(DeathEventProp);

                        EditorGUILayout.EndVertical();
                    }
                }

                if (TemperamentTabNumberProp.intValue == 6)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Item Settings", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Objects that are attached to your AI that can be enabled or disable through Animation Events or programmatically " +
                    "using the item ID. This can be useful for quests items, animation effects, animation specific items, etc. For more information regarding this, please see the Documentation.", EditorStyles.helpBox);
                    EditorGUILayout.Space();

                    CustomHelpLabelField("Each Item below has an ID number. This ID is used to find that particular item and to either enable or disable it using Emerald AI's API.", true);
                    ItemList.DoLayoutList();

                    EditorGUILayout.EndVertical();
                }
            }

            //Detection
            if (TabNumberProp.intValue == 2)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(new GUIContent(DetectTagsIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                EditorGUILayout.LabelField("Detection & Tags", style, GUILayout.ExpandWidth(true));
                GUILayout.Space(4);
                GUIContent[] DetectionTagsButtons = new GUIContent[3] { new GUIContent("Detection Options"), new GUIContent("Tag Options"), new GUIContent("Head Look Options") };
                DetectionTagsTabNumberProp.intValue = GUILayout.Toolbar(DetectionTagsTabNumberProp.intValue, DetectionTagsButtons, EditorStyles.miniButton, GUILayout.Height(25), GUILayout.Width(85 * Screen.width / Screen.dpi));
                GUILayout.Space(1);
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();

                if (DetectionTagsTabNumberProp.intValue == 0)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));
                    EditorGUILayout.LabelField("Detection Options", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Controls various detection related options and settings such as radius distances, target detection, and field of view.", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (self.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight && self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("Companion AI cannot use the Line of Sight Detection Type. It will automatically be switched to Trigger on Start.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    else if (self.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight && self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Passive)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("Passive AI cannot use the Line of Sight Detection Type. It will automatically be switched to Trigger on Start.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.PropertyField(DetectionTypeProp, new GUIContent("Detection Type"));
                    CustomHelpLabelField("Controls the type of detection that is used for detecting targets.", true);

                    if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Companion)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("Companion AI will automatically use the Closest Pick Target Method.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.PropertyField(PickTargetMethodProp, new GUIContent("Pick Target Method"));

                    CustomHelpLabelField("Controls the type of method used to pick an AI's first target.", false);

                    if (self.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.Closest)
                    {
                        CustomHelpLabelField("Closest - When a target is first detected or seen, the AI will search for the nearest target within range sometimes resulting " +
                            "in the AI picking a different target that may not currently be seen. However, this usually ends up with the best results and keeps AI evenly " +
                            "distibuted during larger battles. AI will not initialize an attack unless one is seen first.", true);
                    }
                    else if (self.PickTargetMethodRef == EmeraldAISystem.PickTargetMethod.FirstDetected)
                    {
                        CustomHelpLabelField("First Detected - Picks the target that is first seen or detected. This is the most realistic, but can sometimes result in " +
                            "multiple AI picking the same target.", true);
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(HeadTransformProp, new GUIContent("Head Transform"));
                    CustomHelpLabelField("The head transform of your AI. This is used for accurate head looking and raycast calculations related to sight and obstruction detection. " +
                        "This should be your AI's head object within its bone objects.", true);

                    EditorGUI.BeginChangeCheck();
                    var layersSelection = EditorGUILayout.MaskField("Obstruction Ignore Layers", LayerMaskToField(ObstructionDetectionLayerMaskProp.intValue), InternalEditorUtility.layers);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(self, "Layers changed");
                        ObstructionDetectionLayerMaskProp.intValue = FieldToLayerMask(layersSelection);
                    }
                    CustomHelpLabelField("The layers that should be ignored when an AI is using its obstruction detection for attacking with melee and ranged attacks. " +
                        "These are objects that may prevent an AI from seeing the player target object. If your player has nothing that will block the AI's sight, you can " +
                        "set the layermask to Nothing.", true);

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(ObstructionDetectionQualityProp, new GUIContent("Detection Quality"));
                    CustomHelpLabelField("Controls the quality of the Obstruction Detection feature by controlling how often it's updated", true);

                    if (self.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight)
                    {
                        EditorGUILayout.Space();
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), fieldOfViewAngleProp, "Field of View", 1, 360);
                        CustomHelpLabelField("Controls the field of view your AI uses to detect targets.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), ExpandedFieldOfViewAngleProp, "Expanded Field of View", 1, 360);
                        CustomHelpLabelField("Controls the field of view after your AI has been damaged and no target has been found to allow the AI a better " +
                            "opportunity to find its attacker.", true);
                        EditorGUILayout.Space();

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), DetectionRadiusProp, "Detection Distance", 1, 100);
                        CustomHelpLabelField("Controls the distance of the field of view as well as the AI's detection radius. When enabled, AI can go into " +
                            "'Alert Mode' when an target is near, but not visible.", true);

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), ExpandedDetectionRadiusProp, "Expanded Detection Distance", 1, 100);
                        CustomHelpLabelField("Controls the distance of the field of view, as well as the AI's detection radius, after your AI has been damaged, but no " +
                            "target has found. This allows the AI a better opportunity to find its attacker.", true);
                    }

                    if (self.DetectionTypeRef == EmeraldAISystem.DetectionType.Trigger)
                    {
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), DetectionRadiusProp, "Detection Distance", 1, 100);
                        CustomHelpLabelField("Controls the distance of the AI's trigger radius. When a valid target is within this radius, " +
                            "the AI will react according to its Behavior Type.", true);
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward && self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious)
                    {
                        EditorGUILayout.PropertyField(MaxChaseDistanceTypeProp, new GUIContent("Distance Type"));
                        CustomHelpLabelField("Controls the AI's target for detecting the distance to stop fleeing.", true);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(MaxChaseDistanceTypeProp, new GUIContent("Distance Type"));
                        CustomHelpLabelField("Controls the AI's target for detecting the distance to stop attacking.", true);
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (MaxChaseDistanceProp.intValue <= DetectionRadiusProp.intValue)
                    {
                        MaxChaseDistanceProp.intValue = DetectionRadiusProp.intValue + 10;
                    }

                    if (self.ConfidenceRef != EmeraldAISystem.ConfidenceType.Coward && self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Cautious)
                    {
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaxChaseDistanceProp, "Chase Distance", DetectionRadiusProp.intValue + 5, 200);
                        CustomHelpLabelField("Controls the maximum amount of distance the AI will travel before giving up on their target. This distance can be based " +
                            "on either distance away from its target or its starting position.", true);
                        //EditorGUILayout.Space();
                    }
                    else if (self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward && self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious)
                    {
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaxChaseDistanceProp, "Flee Range", DetectionRadiusProp.intValue + 5, 200);
                        CustomHelpLabelField("Controls the maximum amount of distance the AI will travel before they will stop fleeing. This distance can be " +
                            "based on either distance away from its target or its starting position.", true);
                        EditorGUILayout.Space();
                    }

                    CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), ExpandedChaseDistanceProp, "Expanded Chase Distance", DetectionRadiusProp.intValue + 10, 300);
                    CustomHelpLabelField("Controls the max chase distance after your AI has been damaged so the AI can reach its attacker.", true);
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Avoidance Options", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(UseAIAvoidanceProp, new GUIContent("Use Object Avoidance"));
                    CustomHelpLabelField("Controls whether or not this AI will avoid objects of the appropriate layer such as other AI and players. " +
                        "Note: This is different avoidance than Unity's NavMesh avoidance and should typically be used for other AI and players.", true);

                    if (self.UseAIAvoidance == EmeraldAISystem.YesOrNo.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUI.BeginChangeCheck();
                        var AIAvoidanceLayersSelection = EditorGUILayout.MaskField("AI Avoidance Layers", LayerMaskToField(AIAvoidanceLayerMaskProp.intValue), InternalEditorUtility.layers);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(self, "Layers changed");
                            AIAvoidanceLayerMaskProp.intValue = FieldToLayerMask(AIAvoidanceLayersSelection);
                        }
                        CustomHelpLabelField("The layers the AI will use for avoiding objects such as other AI and players. Note: This feature cannot be used with Companion or Pet AI.", true);

                        EditorGUILayout.Space();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (DetectionTagsTabNumberProp.intValue == 1)
                {
                    EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                    EditorGUILayout.LabelField("Tag Options", EditorStyles.boldLabel);
                    EditorGUILayout.HelpBox("Here you can setup your AI's tags and layers. The Target Tags are tags that the AI will see as targets. It will act according to " +
                        "its behavior type. For more infromation regarding the Tag Options, please see the docs section within the Emerald Editor.", MessageType.None, true);
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    CustomEditorProperties.CustomTagField(new Rect(), new GUIContent(), EmeraldTagProp, "Emerald Unity Tag");
                    CustomHelpLabelField("The Unity Tag used to define other Emerald AI objects. This is the tag that was created using Unity's Tag pulldown at the top of " +
                        "the gameobject.", true);
                    EditorGUILayout.Space();

                    EditorGUI.BeginChangeCheck();
                    var layersSelection = EditorGUILayout.MaskField("Detection Layers", LayerMaskToField(DetectionLayerMaskProp.intValue), InternalEditorUtility.layers);
                    CustomHelpLabelField("The Detection Layers controls what layers this AI can detect as possible targets, if the AI has the appropriate Emerald Unity Tag.", false);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(self, "Layers changed");
                        DetectionLayerMaskProp.intValue = FieldToLayerMask(layersSelection);
                    }

                    if (DetectionLayerMaskProp.intValue == 0 || DetectionLayerMaskProp.intValue == 1)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("The Detection Layers cannot contain Nothing, Default, or Everything.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    CustomEditorProperties.CustomEnum(new Rect(), new GUIContent(), CurrentFactionProp, "Faction");
                    CustomHelpLabelField("An AI's Faction is the name used to control combat reaction with other AI. This is the name other AI will use when " +
                        "looking for opposing targets.", true);

                    CustomHelpLabelField("Factions can be created and removed using the Faction Manager. ", false);
                    if (GUILayout.Button("Open Faction Manager"))
                    {
                        EditorWindow APS = EditorWindow.GetWindow(typeof(EmeraldAIFactionManager));
                        APS.minSize = new Vector2(600f, 725f);
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Companion && self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Companion AI Only)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.HelpBox("The Follower Tag controls the Tag that this AI will follow. This happens when this AI's trigger radius hits the said tag. This feature does not have to be used. " +
                        "If you'd like to manually set the AI's follower, you can do so programmatically.", MessageType.None, true);
                    CustomEditorProperties.CustomTagField(new Rect(), new GUIContent(), FollowTagProp, "Follower Tag");
                    CustomHelpLabelField("If you would like to not use this feature, you can set the Follower Tag to Untagged.", true);
                    EditorGUILayout.Space();

                    EditorGUILayout.HelpBox("You must define your Player's Unity Tag separately. This allows the AI to determine if the target is another AI or not.", MessageType.None, true);
                    CustomEditorProperties.CustomTagField(new Rect(), new GUIContent(), PlayerTagProp, "Player Tag");
                    CustomHelpLabelField("The Player Tag is the Unity Tag used to detect player targets.", true);

                    if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Pet)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("Not Usable with Pet AI.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }

                    if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Cautious && self.ConfidenceRef != EmeraldAISystem.ConfidenceType.Coward 
                        || self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious && self.ConfidenceRef != EmeraldAISystem.ConfidenceType.Coward)
                    {
                        EditorGUILayout.PropertyField(AIAttacksPlayerProp, new GUIContent("AI Attacks Player?"));
                        CustomHelpLabelField("Controls whether or not this AI will attack a player with the given tag.", true);
                    }
                    else if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious && self.ConfidenceRef == EmeraldAISystem.ConfidenceType.Coward)
                    {
                        EditorGUILayout.PropertyField(AIAttacksPlayerProp, new GUIContent("AI Flees From Player?"));
                        CustomHelpLabelField("Controls whether or not this AI will flee from a player with the given tag.", true);
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    CustomEditorProperties.CustomTagField(new Rect(), new GUIContent(), UITagProp, "UI Tag");
                    CustomHelpLabelField("The UI Tag is the Unity Tag that will trigger the AI's UI, when enabled.", true);

                    EditorGUILayout.PropertyField(UseNonAITagProp, new GUIContent("Use Non-AI Tag?"));
                    CustomHelpLabelField("Controls whether or not this AI will attack a non-player object with the given tag.", true);

                    if (self.UseNonAITagRef == EmeraldAISystem.UseNonAITag.Yes)
                    {
                        EditorGUILayout.HelpBox("The Non-AI Unity Tag is another type of tag AI can use for the behavior types such as avoid cars, areas of water, or other avoidable objects that are not AI objects.", MessageType.None, true);
                        CustomEditorProperties.CustomTagField(new Rect(), new GUIContent(), NonAITagProp, "Non-AI Tag");
                        GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                        EditorGUILayout.LabelField("Note: The layer of a Non-AI object must be included in an AI's 'Number of Layers' list.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(OpposingFactionsEnumProp, new GUIContent("Total Faction Relations"));
                    CustomHelpLabelField("Controls which factions this AI sees as enemies and allies.", false);
                    GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                    EditorGUILayout.LabelField("Note: The Faction Relations use an AI's Faction not Unity tags.", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    EditorGUILayout.BeginVertical();

                    if (self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.One ||
                        self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Two ||
                        self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Three ||
                        self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Four ||
                        self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Five)
                    {

                        if (FactionRelation1Prop.intValue == 0)
                        {
                            GUI.backgroundColor = new Color(0.8f, 0f, 0f, 1f);
                        }
                        else if (FactionRelation1Prop.intValue == 1)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.0f, 1f, 0.4f);
                        }
                        else if (FactionRelation1Prop.intValue == 2)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.75f, 0f, 1f);
                        }
                        EditorGUILayout.BeginVertical("Box");
                        GUILayout.Space(5);

                        var RelationStyle = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                        RelationStyle.normal.textColor = Color.white;
                        EditorGUILayout.LabelField("Faction Relation 1", RelationStyle, GUILayout.ExpandWidth(true));
                        GUILayout.Space(4);
                        GUILayout.FlexibleSpace();

                        GUILayout.Space(8);
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction1Prop, "Faction", Color.white);
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomPopupColor(new Rect(), new GUIContent(), FactionRelation1Prop, "Relation", typeof(EmeraldAISystem.RelationType), Color.white);
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(8);
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                    }
                    if (self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Two || self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Three
                        || self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Four || self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Five)
                    {

                        if (FactionRelation2Prop.intValue == 0)
                        {
                            GUI.backgroundColor = new Color(0.8f, 0f, 0f, 1f);
                        }
                        else if (FactionRelation2Prop.intValue == 1)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.0f, 1f, 0.4f);
                        }
                        else if (FactionRelation2Prop.intValue == 2)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.75f, 0f, 1f);
                        }
                        EditorGUILayout.BeginVertical("Box");
                        GUILayout.Space(5);

                        var RelationStyle = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                        RelationStyle.normal.textColor = Color.white;
                        EditorGUILayout.LabelField("Faction Relation 2", RelationStyle, GUILayout.ExpandWidth(true));
                        GUILayout.Space(4);
                        GUILayout.FlexibleSpace();

                        GUILayout.Space(8);
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction2Prop, "Faction", Color.white);
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomPopupColor(new Rect(), new GUIContent(), FactionRelation2Prop, "Relation", typeof(EmeraldAISystem.RelationType), Color.white);
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(8);
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                    }
                    if (self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Three
                        || self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Four
                        || self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Five)
                    {

                        if (FactionRelation3Prop.intValue == 0)
                        {
                            GUI.backgroundColor = new Color(0.8f, 0f, 0f, 1f);
                        }
                        else if (FactionRelation3Prop.intValue == 1)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.0f, 1f, 0.4f);
                        }
                        else if (FactionRelation3Prop.intValue == 2)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.75f, 0f, 1f);
                        }
                        EditorGUILayout.BeginVertical("Box");
                        GUILayout.Space(5);

                        var RelationStyle = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                        RelationStyle.normal.textColor = Color.white;
                        EditorGUILayout.LabelField("Faction Relation 3", RelationStyle, GUILayout.ExpandWidth(true));
                        GUILayout.Space(4);
                        GUILayout.FlexibleSpace();

                        GUILayout.Space(8);
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction3Prop, "Faction", Color.white);
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomPopupColor(new Rect(), new GUIContent(), FactionRelation3Prop, "Relation", typeof(EmeraldAISystem.RelationType), Color.white);
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(8);
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                    }
                    if (self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Four || self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Five)
                    {

                        if (FactionRelation4Prop.intValue == 0)
                        {
                            GUI.backgroundColor = new Color(0.8f, 0f, 0f, 1f);
                        }
                        else if (FactionRelation4Prop.intValue == 1)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.0f, 1f, 0.4f);
                        }
                        else if (FactionRelation4Prop.intValue == 2)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.75f, 0f, 1f);
                        }
                        EditorGUILayout.BeginVertical("Box");
                        GUILayout.Space(5);

                        var RelationStyle = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                        RelationStyle.normal.textColor = Color.white;
                        EditorGUILayout.LabelField("Faction Relation 4", RelationStyle, GUILayout.ExpandWidth(true));
                        GUILayout.Space(4);
                        GUILayout.FlexibleSpace();

                        GUILayout.Space(8);
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction4Prop, "Faction", Color.white);
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomPopupColor(new Rect(), new GUIContent(), FactionRelation4Prop, "Relation", typeof(EmeraldAISystem.RelationType), Color.white);
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(8);
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                    }
                    if (self.OpposingFactionsEnumRef == EmeraldAISystem.OpposingFactionsEnum.Five)
                    {

                        if (FactionRelation5Prop.intValue == 0)
                        {
                            GUI.backgroundColor = new Color(0.8f, 0f, 0f, 1f);
                        }
                        else if (FactionRelation5Prop.intValue == 1)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.0f, 1f, 0.4f);
                        }
                        else if (FactionRelation5Prop.intValue == 2)
                        {
                            GUI.backgroundColor = new Color(0.0f, 0.75f, 0f, 1f);
                        }
                        EditorGUILayout.BeginVertical("Box");
                        GUILayout.Space(5);

                        var RelationStyle = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                        RelationStyle.normal.textColor = Color.white;
                        EditorGUILayout.LabelField("Faction Relation 5", RelationStyle, GUILayout.ExpandWidth(true));
                        GUILayout.Space(4);
                        GUILayout.FlexibleSpace();

                        GUILayout.Space(8);
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomEnumColor(new Rect(), new GUIContent(), OpposingFaction5Prop, "Faction", Color.white);
                        GUILayout.FlexibleSpace();
                        CustomEditorProperties.CustomPopupColor(new Rect(), new GUIContent(), FactionRelation5Prop, "Relation", typeof(EmeraldAISystem.RelationType), Color.white);
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(8);
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }

                if (DetectionTagsTabNumberProp.intValue == 2)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginVertical("Box", GUILayout.Width(87 * Screen.width / Screen.dpi));
                    EditorGUILayout.LabelField("Head Look Options", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Controls the AI's Head Look settings.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                    EditorGUILayout.LabelField("Note: Only models with a Humanoid Animation Type can use this feature. " +
                        "The Non-Combat head look feature is only usable on players. The Combat head look feature works on all targets, including other AI.", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(UseHeadLookProp, new GUIContent("Use Head Look"));
                    CustomHelpLabelField("Enables or disables the head look feature.", true);

                    if (self.UseHeadLookRef == EmeraldAISystem.YesOrNo.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), LookSmootherProp, "Head Look Speed", 0.1f, 3);
                        CustomHelpLabelField("Controls the speed of the AI's head look.", true);
                        EditorGUILayout.Space();

                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), MaxLookAtDistanceProp, "Max Head Look Distance", 5, 50);
                        CustomHelpLabelField("Controls the max distance an AI will use the head look feature.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), HeadLookYOffsetProp, "Player Y Offset", -4, 4);
                        CustomHelpLabelField("Controls the Y offset of the AI's look at position when looking at the Player. If an AI is looking too high or too " +
                            "low at their target, you can adjust it with this setting.", true);

                        EditorGUILayout.LabelField("Non-Combat", EditorStyles.boldLabel);
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), NonCombatLookAtLimitProp, "Angle Limit", 10, 90);
                        CustomHelpLabelField("Controls the max angle an will use the look at feature while not in combat.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), HeadLookWeightNonCombatProp, "Head Weight", 0, 1);
                        CustomHelpLabelField("Controls the Head Look intensity of the AI's head while in not combat.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), BodyLookWeightNonCombatProp, "Body Weight", 0, 1);
                        CustomHelpLabelField("Controls the Head Look intensity of the AI's body while in not combat.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.LabelField("Combat", EditorStyles.boldLabel);
                        CustomEditorProperties.CustomIntSlider(new Rect(), new GUIContent(), CombatLookAtLimitProp, "Angle Limit", 10, 90);
                        CustomHelpLabelField("Controls the max angle an will use the look at feature while in combat.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), HeadLookWeightCombatProp, "Combat Head Weight", 0, 1);
                        CustomHelpLabelField("Controls the Head Look intensity of the AI's head while in combat.", true);

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), BodyLookWeightCombatProp, "Combat Body Weight", 0, 1);
                        CustomHelpLabelField("Controls the Head Look intensity of the AI's body while in combat.", true);

                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }

                    EditorGUILayout.EndHorizontal();
                }

            }

            if (TabNumberProp.intValue == 3)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(new GUIContent(UIIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                EditorGUILayout.LabelField("UI Settings", style, GUILayout.ExpandWidth(true));
                GUILayout.Space(2);
                EditorGUILayout.LabelField("Controls the use and settings of Emerald's built-in Health Bars and Combat Text. In order for UI to be visible, a player of the appropriate tag must enter an AI's trigger radius. " +
                    "You can set an AI's UI Tag under the Detection and Tag tab.", EditorStyles.helpBox);
                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                NameTextFoldoutProp.boolValue = CustomEditorProperties.Foldout(NameTextFoldoutProp.boolValue, "Name Text Settings", true, myFoldoutStyle);

                if (NameTextFoldoutProp.boolValue)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(DisplayAINameProp, new GUIContent("Display AI Name"));
                    CustomHelpLabelField("Enables or disables the display of the AI's name. When enabled, the AI's name will be visible above its health bar.", true);

                    if (self.DisplayAINameRef == EmeraldAISystem.DisplayAIName.Yes)
                    {

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(AINamePosProp, new GUIContent("AI Name Position"));
                        CustomHelpLabelField("Controls the position of the AI's name text.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(NameTextSizeProp, new GUIContent("AI Name Text Size"));
                        CustomHelpLabelField("Controls the size of the AI's name text.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(NameTextColorProp, new GUIContent("AI Name Color"));
                        CustomHelpLabelField("Controls color of the AI's name text.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(DisplayAITitleProp, new GUIContent("Display AI Title"));
                        CustomHelpLabelField("Enables or disables the display of the AI's title. When enabled, the AI's title will be visible above its health bar.", true);

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    if (self.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.No)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("You must have Auto Create Health Bars enabled to use this feature.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.PropertyField(DisplayAILevelProp, new GUIContent("Display AI Level"));
                    CustomHelpLabelField("Enables or disables the display of the AI's level. When enabled, the AI's level will be visible to the left of its health bar.", true);

                    if (self.DisplayAILevelRef == EmeraldAISystem.DisplayAILevel.Yes)
                    {

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(LevelTextColorProp, new GUIContent("Level Color"));
                        CustomHelpLabelField("Controls color of the AI's Level Text.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                HealthBarsFoldoutProp.boolValue = CustomEditorProperties.Foldout(HealthBarsFoldoutProp.boolValue, "Health Bar Settings", true, myFoldoutStyle);

                if (HealthBarsFoldoutProp.boolValue)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(CreateHealthBarsProp, new GUIContent("Auto Create Health Bars"));
                    CustomHelpLabelField("Enables or disables the use of Emerald automatically creating health bars for your AI. Enabling this will open up additional settings.", true);
                    EditorGUILayout.Space();

                    if (self.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(HealthBarPosProp, new GUIContent("Health Bar Position"));
                        CustomHelpLabelField("Controls the starting position of the AI's created health bar.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(HealthBarScaleProp, new GUIContent("Health Bar Scale"));
                        CustomHelpLabelField("Controls the scale of the AI's created health bar.", true);
                        EditorGUILayout.Space();

                        if (self.BehaviorRef == EmeraldAISystem.CurrentBehavior.Pet)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("Not Usable with Pet AI. Health bars will be disabled.", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }

                        EditorGUILayout.PropertyField(HealthBarColorProp, new GUIContent("Health Bar Color"));
                        CustomHelpLabelField("Controls color of the AI's health bar.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(HealthBarBackgroundColorProp, new GUIContent("Background Color"));
                        CustomHelpLabelField("Controls the background color of the AI's health bar.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(CustomizeHealthBarProp, new GUIContent("Customize Health Bar?"));
                        CustomHelpLabelField("Gives you controls over using custom sprites for the AI's health bar.", true);
                        EditorGUILayout.Space();

                        if (self.CustomizeHealthBarRef == EmeraldAISystem.CustomizeHealthBar.Yes)
                        {
                            EditorGUILayout.LabelField("Health Bar Sprites", EditorStyles.boldLabel);
                            EditorGUILayout.Space();

                            CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), HealthBarImageProp, "Bar", typeof(Sprite), true);
                            CustomHelpLabelField("Customizes the health bar sprite for the AI's health bar.", true);

                            CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), HealthBarBackgroundImageProp, "Bar Background", typeof(Sprite), true);
                            CustomHelpLabelField("Customizes the health bar's background sprite for the AI's health bar.", true);
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                CombatTextFoldoutProp.boolValue = CustomEditorProperties.Foldout(CombatTextFoldoutProp.boolValue, "Combat Text Settings", true, myFoldoutStyle);

                if (CombatTextFoldoutProp.boolValue)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(UseCombatTextProp, new GUIContent("Auto Create Combat Text"));
                    CustomHelpLabelField("Enables or disables the use of Emerald automatically creating combat text for your AI. Enabling this will open up additional settings.", true);
                    EditorGUILayout.Space();

                    if (self.UseCombatTextRef == EmeraldAISystem.UseCombatText.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.PropertyField(AnimateCombatTextProp, new GUIContent("Animate Type"));
                        CustomHelpLabelField("Controls whether not the AI's combat text is animated. If enabled, the AI's combat text will animate upwards each time a " +
                            "damage number appears. If disabled, the combat text will stay in the same place each time a damage number appears.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(CombatTextPosProp, new GUIContent("Combat Text Position"));
                        CustomHelpLabelField("Controls the starting position of the AI's Combat Text.", true);
                        EditorGUILayout.Space();

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), StartingCombatTextSizeProp, "Starting Text Size", 0.001f, 0.5f);
                        CustomHelpLabelField("Controls the starting size of the AI's Combat Text.", true);
                        EditorGUILayout.Space();

                        CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), EndingCombatTextSizeProp, "Ending Text Size", self.StartingCombatTextSize, 0.5f);
                        CustomHelpLabelField("Controls the ending size of the AI's Combat Text.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(CombatTextColorProp, new GUIContent("Combat Text Color"));
                        CustomHelpLabelField("Controls color of the AI's Combat Text color.", true);
                        EditorGUILayout.Space();

                        EditorGUILayout.PropertyField(UseCustomFontProp, new GUIContent("Use Custom Font?"));
                        CustomHelpLabelField("Allows you to customize the Combat Text's font.", true);

                        if (self.UseCustomFontRef == EmeraldAISystem.UseCustomFont.Yes)
                        {
                            CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), CombatTextFontProp, "Custom Font", typeof(Font), true);
                            CustomHelpLabelField("The custom font the Combat Text will use.", true);
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndVertical();
                }
            }

            if (TabNumberProp.intValue == 4)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(new GUIContent(SoundIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                EditorGUILayout.LabelField("Sounds", style, GUILayout.ExpandWidth(true));
                GUILayout.Space(2);
                GUIContent[] SoundButtons = new GUIContent[2] { new GUIContent("General"), new GUIContent("Combat") };
                SoundTabNumberProp.intValue = GUILayout.Toolbar(SoundTabNumberProp.intValue, SoundButtons, EditorStyles.miniButton, GUILayout.Height(25), GUILayout.Width(82 * Screen.width / Screen.dpi));
                GUILayout.Space(1);
                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                //General
                if (SoundTabNumberProp.intValue == 0)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("General Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.LabelField("Emerald AI's general sounds such as footstep, idle, and other sounds an AI can play with Animation Events.", EditorStyles.helpBox);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Idle Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(),  IdleVolumeProp, "Idle Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of idle sounds.", true);

                    CustomHelpLabelField("Controls how many idle sounds this AI will use.", true);

                    IdleSoundsList.DoLayoutList();

                    if (self.IdleSounds.Count != 0)
                    {
                        EditorGUILayout.Space();
                        CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), IdleSoundsSecondsMinProp, "Min Idle Sound Seconds");
                        CustomHelpLabelField("Controls the minimum amount of seconds needed before playing a random idle sound from the list below. This amuont will be " +
                            "randomized with the Max Idle Sound Seconds.", true);

                        CustomEditorProperties.CustomIntField(new Rect(), new GUIContent(), IdleSoundsSecondsMaxProp, "Max Idle Sound Seconds");
                        CustomHelpLabelField("Controls the maximum amount of seconds needed before playing a random idle sound from the list below. This amuont will be " +
                            "randomized with the Min Idle Sound Seconds.", true);
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Footstep Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), WalkFootstepVolumeProp, "Walk Footsteps Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of walk footstep sounds.", false);
                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), RunFootstepVolumeProp, "Run Footsteps Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of Run footstep sounds.", true);

                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                    EditorGUILayout.LabelField("Controls how many footstep sounds this AI will use.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                    EditorGUILayout.LabelField("Note: You will need to manually create a WalkFootstepSound and/or RunFootstepSound Animation Event to use this feature. " +
                        "Please refer to Emerlad's documentation for a tutorial on how to do this.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(0, 0.65f, 0, 0.8f);
                    if (GUILayout.Button("See Tutorial", HelpButtonStyle, GUILayout.Height(20)))
                    {
                        Application.OpenURL("https://docs.google.com/document/d/1_zXR1gg61soAX_bZscs6HC-7as2njM7Jx9pYlqgbtM8/edit#heading=h.aisc1wq6w60");
                    }
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.Space();

                    FootStepSoundsList.DoLayoutList();

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Interact Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomHelpLabelField("Various sounds that can be called through Animation Events or programmatically using the sound effect ID. " +
                        "This can be useful for quests, dialouge, animation sound effects, etc.", true);

                    InteractSoundsList.DoLayoutList();
                    EditorGUILayout.EndVertical();
                }

                if (SoundTabNumberProp.intValue == 1)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Combat Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.LabelField("Emerald AI's sounds that play during combat such as attack sounds, injured sounds, death sounds, and more. " +
                        "Some sounds will need to be played with an Animation Event.", EditorStyles.helpBox);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Equip and Unequip Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    EditorGUILayout.LabelField("Controls the sounds that play when the AI is equiping or unequiping their weapon, when Use Equip Animation is enabled.", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    if (self.UseEquipAnimation == EmeraldAISystem.YesOrNo.No)
                    {
                        GUI.backgroundColor = new Color(1f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("Use Equip Animations needs to be enabled in order to use this feature. This can be found under the Combat Animation Settings.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUI.BeginDisabledGroup(self.UseEquipAnimation == EmeraldAISystem.YesOrNo.No);
                    EditorGUILayout.Space();
                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), EquipVolumeProp, "Equip Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of the Equip Sound.", false);
                    CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), UnsheatheWeaponProp, "Equip Sound", typeof(AudioClip), true);
                    CustomHelpLabelField("The sound that plays when this AI is equiping their weapon.", true);
                    EditorGUILayout.Space();
                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), UnequipVolumeProp, "Unequip Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of the Unequip Sound.", false);
                    CustomEditorProperties.CustomObjectField(new Rect(), new GUIContent(), SheatheWeaponProp, "Unequip Sound", typeof(AudioClip), true);
                    CustomHelpLabelField("The sound that plays when this AI is unequiping their weapon.", true);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Attack Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), AttackVolumeProp, "Attack Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of Attack Sounds.", true);

                    CustomHelpLabelField("Controls how many attack sounds this AI will use.", true);

                    AttackSoundsList.DoLayoutList();
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Injured Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), InjuredVolumeProp, "Injured Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of Injured Sounds.", true);

                    CustomHelpLabelField("Controls how many injured sounds this AI will use.", true);

                    InjuredSoundsList.DoLayoutList();
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Block Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), BlockVolumeProp, "Block Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of Block Sounds.", true);

                    CustomHelpLabelField("Controls the sound that happens when this AI is hit while blocking. " +
                        "Note: Blocking must be enabled with the proper animations setup in order for this to work.", true);
                    BlockSoundsList.DoLayoutList();
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Weapon Impact Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), ImpactVolumeProp, "Impact Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of Impact Sounds.", true);

                    CustomHelpLabelField("Controls the sound that happens when this AI hits its target. This is typically the AI's weapon impact sound.", true);

                    ImpactSoundsList.DoLayoutList();
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Death Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), DeathVolumeProp, "Death Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of Death Sounds.", true);

                    CustomHelpLabelField("Controls how many death sounds this AI will use.", true);
                    DeathSoundsList.DoLayoutList();

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical("Box");
                    GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Warning Sounds", EditorStyles.boldLabel);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = Color.white;

                    CustomEditorProperties.CustomFloatSlider(new Rect(), new GUIContent(), WarningVolumeProp, "Warning Volume", 0, 1);
                    CustomHelpLabelField("Controls the volume of Warning Sounds.", true);

                    CustomHelpLabelField("Controls how many warning sounds this AI will use.", true);

                    WarningSoundsList.DoLayoutList();
                    EditorGUILayout.EndVertical();
                }
            }

            if (TabNumberProp.intValue == 5)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (self.WanderTypeRef != EmeraldAISystem.WanderType.Waypoints)
                {
                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                    EditorGUILayout.LabelField("You can only use the Waypoint Editor if your AI's Wander Type is set to Waypoints, would you like to enable this AI to use Waypoints?", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    if (GUILayout.Button("Enable Waypoints"))
                    {
                        WanderTypeProp.intValue = 1;
                    }
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                }


                if (self.WanderTypeRef == EmeraldAISystem.WanderType.Waypoints)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                    var style4 = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                    EditorGUILayout.LabelField(new GUIContent(WaypointEditorIcon), style4, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                    EditorGUILayout.LabelField("Waypoint Editor", style4, GUILayout.ExpandWidth(true));
                    GUILayout.Space(2);
                    EditorGUILayout.HelpBox("Here you can define waypoints for your AI to follow. Simply press the 'Add Waypoint' button to create a waypoint. The AI will " +
                        "follow each created waypoint in the order they are created. A line will be drawn to visually represent this.", MessageType.None, true);
                    EditorGUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (self.WaypointsList != null && Selection.objects.Length == 1)
                    {
                        EditorGUILayout.BeginVertical("Box");
                        EditorGUILayout.LabelField("Controls what an AI will do when it reaches its last waypoint.", EditorStyles.helpBox);
                        EditorGUILayout.PropertyField(WaypointTypeProp, new GUIContent("Waypoint Type"));
                        GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                        GUI.backgroundColor = Color.white;

                        if (self.WaypointTypeRef == (EmeraldAISystem.WaypointType.Loop))
                        {
                            CustomHelpLabelField("Loop - When an AI reaches its last waypoint, it will set the first waypoint as its next waypoint thus creating a loop.", false);
                        }
                        else if (self.WaypointTypeRef == (EmeraldAISystem.WaypointType.Reverse))
                        {
                            CustomHelpLabelField("Reverse - When an AI reaches its last waypoint, it will reverse the AI's waypoints making the last waypoint its first. " +
                                "This allows AI to patorl back and forth through narrow or one way areas.", false);
                        }
                        else if (self.WaypointTypeRef == (EmeraldAISystem.WaypointType.Random))
                        {
                            CustomHelpLabelField("Random - This allows an AI to patrol randomly through all waypoints. An AI will idle each time it reaches a waypoint " +
                                "for as long as its wait time seconds are set.", false);
                        }

                        EditorGUILayout.Space();

                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        if (GUILayout.Button("Add Waypoint"))
                        {
                            Vector3 newPoint = new Vector3(0, 0, 0);

                            if (self.WaypointsList.Count == 0)
                            {
                                newPoint = new Vector3(self.transform.position.x, self.transform.position.y, self.transform.position.z + 5);
                            }
                            else if (self.WaypointsList.Count > 0)
                            {
                                newPoint = new Vector3(self.WaypointsList[self.WaypointsList.Count - 1].x, self.WaypointsList[self.WaypointsList.Count - 1].y, self.WaypointsList[self.WaypointsList.Count - 1].z + 5);
                            }

                            self.WaypointsList.Add(newPoint);
                            EditorUtility.SetDirty(self);
                        }

                        var style = new GUIStyle(GUI.skin.button);
                        style.normal.textColor = Color.red;

                        if (GUILayout.Button("Clear All Waypoints", style) && EditorUtility.DisplayDialog("Clear Waypoints?", "Are you sure you want to clear all of this AI's waypoints? This process cannot be undone.", "Yes", "Cancel"))
                        {
                            self.WaypointsList.Clear();
                            EditorUtility.SetDirty(self);
                        }
                        GUI.contentColor = Color.white;
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        WaypointsFoldout.boolValue = CustomEditorProperties.Foldout(WaypointsFoldout.boolValue, "Waypoints", true, myFoldoutStyle);

                        if (WaypointsFoldout.boolValue)
                        {
                            EditorGUILayout.BeginVertical("Box");
                            EditorGUILayout.LabelField("Waypoints", EditorStyles.boldLabel);
                            EditorGUILayout.LabelField("All of this AI's current waypoints. Waypoints can be individually removed by pressing the ''Remove Point'' button.", EditorStyles.helpBox);
                            EditorGUILayout.Space();

                            if (self.WaypointsList.Count > 0)
                            {
                                for (int j = 0; j < self.WaypointsList.Count; ++j)
                                {
                                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                                    EditorGUILayout.LabelField("Waypoint " + (j + 1), EditorStyles.toolbarButton);
                                    GUI.backgroundColor = Color.white;

                                    if (GUILayout.Button("Remove Point", EditorStyles.miniButton, GUILayout.Height(18)))
                                    {
                                        self.WaypointsList.RemoveAt(j);
                                        --j;
                                        EditorUtility.SetDirty(self);
                                    }
                                    GUILayout.Space(10);
                                }
                            }


                            EditorGUILayout.EndVertical();
                        }

                    }
                    else if (self.WaypointsList != null && Selection.objects.Length > 1)
                    {
                        EditorGUILayout.BeginVertical("Box");
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("Waypoints do not support multi-object editing. If you'd like to edit an AI's waypoints, please only have 1 AI selected at a time.", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndVertical();
                    }
                }
            }

            if (TabNumberProp.intValue == 6)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                //Only auto-update the Animator Controller if inside the Unity Editor as runtime auto-updating is not possible.
#if UNITY_EDITOR
                if (self.AnimatorControllerGenerated)
                {
                    if (self.AnimationsUpdated || self.AnimationListsChanged)
                    {
                        EmeraldAIAnimatorGenerator.GenerateAnimatorController(self);
                    }
                }
#endif

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                var style3 = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(new GUIContent(AnimationIcon), style3, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                EditorGUILayout.LabelField("Animation Settings", style3, GUILayout.ExpandWidth(true));

                if (!self.AnimatorControllerGenerated)
                {
                    EditorGUILayout.LabelField("To create an Animator Controller, press the 'Create Animator Controller' button below. " +
                        "This will create an Animator Controller and assign the animations you've entered below.", EditorStyles.helpBox);
                }
                else if (self.AnimatorControllerGenerated)
                {
                    GUI.backgroundColor = new Color(0.1f, 1f, 0.1f, 0.5f);
                    EditorGUILayout.LabelField("The Animator Controller is automatically updated as changes are made.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                    EditorGUILayout.LabelField("Important - Please remember all animation slots that are enabled must have animations applied to avoid errors. Please ensure you have applied all " +
                        "of the neccesary animations before using this AI. For more information regarding this, please see the Documentation or tutorial videos. (This is a reminder, not an error message)", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.LabelField("Note: If you have multiple AI using the same Animator Controller, such as duplicated or copied AI, you will " +
                        "need to have all said objects selected when updating the Animator Controller.", EditorStyles.helpBox);
                }

                GUILayout.Space(6);
                GUI.backgroundColor = Color.white;
                GUILayout.Space(2);
                GUIContent[] AnimationButtons = new GUIContent[4] { new GUIContent("Idle"), new GUIContent("Movement"), new GUIContent("Combat"), new GUIContent("Emotes") };
                AnimationTabNumberProp.intValue = GUILayout.Toolbar(AnimationTabNumberProp.intValue, AnimationButtons, EditorStyles.miniButton, GUILayout.Height(20), GUILayout.Width(82 * Screen.width / Screen.dpi));
                GUILayout.Space(1);
                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (AnimationTabNumberProp.intValue == 0)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Idle Animations", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("The Idle Animations section allows you to set all idle animations that this AI will use when wandering and the idle animations that will be used in combat. ", EditorStyles.helpBox);
                    EditorGUILayout.Space();

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Idle Animations", EditorStyles.boldLabel);
                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                    EditorGUILayout.LabelField("Controls the idle animations that will randomly play when the AI is wandering or grazing. A max of 6 can be used.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                    GUI.backgroundColor = Color.white;
                    IdleAnimationList.DoLayoutList();
                    EditorGUILayout.Space();

                    //Idle
                    if (self.IdleAnimationList.Count == 3)
                    {
                        IdleAnimationList.displayAdd = false;
                    }
                    else
                    {
                        IdleAnimationList.displayAdd = true;
                    }

                    CustomAnimationField(new Rect(), new GUIContent(), IdleNonCombatProp, "Idle Non-Combat", typeof(AnimationClip), false);
                    CustomHelpLabelField("Controls the default idle animation.", false);
                    if (self.NonCombatIdleAnimation != null)
                    {
                        var settings = AnimationUtility.GetAnimationClipSettings(self.NonCombatIdleAnimation);
                        if (!settings.loopTime)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("The 'Idle Non-Combat' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }
                    }
                    CustomFloatAnimationField(new Rect(), new GUIContent(), IdleNonCombatAnimationSpeedProp, "Animation Speed", 0.1f ,2);

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    CustomAnimationField(new Rect(), new GUIContent(), IdleCombatProp, "Idle Combat", typeof(AnimationClip), false);
                    CustomHelpLabelField("Controls the idle animation that the AI will play while an AI is in Combat Mode.", false);
                    if (self.CombatIdleAnimation != null)
                    {
                        var settings = AnimationUtility.GetAnimationClipSettings(self.CombatIdleAnimation);
                        if (!settings.loopTime)
                        {
                            GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                            EditorGUILayout.LabelField("The 'Idle Combat' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                            GUI.backgroundColor = Color.white;
                        }
                    }
                    CustomFloatAnimationField(new Rect(), new GUIContent(), IdleCombatAnimationSpeedProp, "Animation Speed", 0.1f, 2);

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (self.BehaviorRef != EmeraldAISystem.CurrentBehavior.Cautious)
                    {
                        GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                        EditorGUILayout.LabelField("(Cautious AI Only)", EditorStyles.helpBox);
                        GUI.backgroundColor = Color.white;
                    }
                    EditorGUILayout.PropertyField(UseWarningAnimationProp, new GUIContent("Use Warning"));
                    CustomHelpLabelField("Controls whether or not this AI will play a warning animation and sound if they feel threatened", true);

                    if (self.UseWarningAnimationRef == EmeraldAISystem.YesOrNo.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();                     

                        CustomAnimationField(new Rect(), new GUIContent(), IdleWarningProp, "Idle Warning", typeof(AnimationClip), false);
                        CustomHelpLabelField("Controls the animation that the AI will play to warn a target that they will attack, if the target doesn't leave their attack radius soon.", false);
                        CustomFloatAnimationField(new Rect(), new GUIContent(), IdleWarningAnimationSpeedProp, "Animation Speed", 0.1f, 2);

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.EndVertical();
                }

                if (AnimationTabNumberProp.intValue == 1)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Movement Animations", style3, GUILayout.ExpandWidth(true));
                    EditorGUILayout.Space();
                    GUIContent[] MovementButtons = new GUIContent[2] { new GUIContent("Non-Combat Movement"), new GUIContent("Combat Movement") };
                    MovementTabNumberProp.intValue = GUILayout.Toolbar(MovementTabNumberProp.intValue, MovementButtons, EditorStyles.miniButton, GUILayout.Height(25));
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (MovementTabNumberProp.intValue == 0)
                    {
                        WalkFoldout.boolValue = CustomEditorProperties.Foldout(WalkFoldout.boolValue, "Walk Animations", true, myFoldoutStyle);

                        if (WalkFoldout.boolValue)
                        {
                            EditorGUILayout.BeginVertical("Box");
                            CustomFloatAnimationField(new Rect(), new GUIContent(), NonCombatWalkAnimationSpeedProp, "Walk Speed", 0.5f, 2);
                            CustomHelpLabelField("Controls how fast your AI's Walk straight, left, and right animations play. When using Root Motion, this will also " +
                                "control your AI's walk movement speed.", true);

                            //Walk Straight
                            CustomAnimationField(new Rect(), new GUIContent(), WalkStraightProp, "Walk Straight Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The walk animation that plays when your AI is walking straight when not in combat.", false);
                            if (self.WalkStraightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.WalkStraightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Walk Straight' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Walk Left
                            CustomAnimationField(new Rect(), new GUIContent(), WalkLeftProp, "Walk Left Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The walk animation that plays when your AI is walking left when not in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorWalkLeftProp, "Mirror Walk Left");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.WalkLeftAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.WalkLeftAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Walk Left' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Walk Right
                            CustomAnimationField(new Rect(), new GUIContent(), WalkRightProp, "Walk Right Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The walk animation that plays when your AI is walking right when not in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorWalkRightProp, "Mirror Walk Right");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.WalkRightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.WalkRightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Walk Right' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.EndVertical();
                        }

                        EditorGUILayout.Space();

                        RunFoldout.boolValue = CustomEditorProperties.Foldout(RunFoldout.boolValue, "Run Animations", true, myFoldoutStyle);

                        if (RunFoldout.boolValue)
                        {
                            EditorGUILayout.BeginVertical("Box");
                            CustomFloatAnimationField(new Rect(), new GUIContent(), NonCombatRunAnimationSpeedProp, "Run Speed", 0.5f, 2);
                            CustomHelpLabelField("Controls how fast your AI's Run straight, left, and right animations play. When using Root Motion, " +
                                "this will also control your AI's run movement speed.", true);

                            //Run Straight
                            CustomAnimationField(new Rect(), new GUIContent(), RunStraightProp, "Run Straight Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The run animation that plays when your AI is running straight when not in combat.", false);
                            if (self.RunStraightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.RunStraightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Run Straight' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Run Left
                            CustomAnimationField(new Rect(), new GUIContent(), RunLeftProp, "Run Left Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The run animation that plays when your AI is running left when not in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorRunLeftProp, "Mirror Run Left");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.RunLeftAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.RunLeftAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Run Left' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Run Right
                            CustomAnimationField(new Rect(), new GUIContent(), RunRightProp, "Run Right Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The run animation that plays when your AI is running right when not in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorRunRightProp, "Mirror Run Right");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.RunRightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.RunRightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Run Right' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.EndVertical();
                        }

                        EditorGUILayout.Space();

                        TurnFoldout.boolValue = CustomEditorProperties.Foldout(TurnFoldout.boolValue, "Turn Animations", true, myFoldoutStyle);

                        if (TurnFoldout.boolValue)
                        {
                            EditorGUILayout.BeginVertical("Box");
                            //Turn Left
                            CustomAnimationField(new Rect(), new GUIContent(), TurnLeftProp, "Turn Left", typeof(AnimationClip), false);
                            CustomHelpLabelField("The animation clip for turning right when not in combat.", false);
                            CustomFloatAnimationField(new Rect(), new GUIContent(), TurnLeftAnimationSpeedProp, "Turn Left Animation Speed", 0.1f, 2);
                            CustomHelpLabelField("The speed in which the turn right animation will play.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorTurnLeftProp, "Mirror Turn Left");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.NonCombatTurnLeftAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.NonCombatTurnLeftAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Turn Left' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Turn Right
                            CustomAnimationField(new Rect(), new GUIContent(), TurnRightProp, "Turn Right", typeof(AnimationClip), false);
                            CustomHelpLabelField("The animation clip for turning right when not in combat.", false);
                            CustomFloatAnimationField(new Rect(), new GUIContent(), TurnRightAnimationSpeedProp, "Turn Right Animation Speed", 0.1f, 2);
                            CustomHelpLabelField("The speed in which the turn right animation will play.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorTurnRightProp, "Mirror Turn Right");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.NonCombatTurnRightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.NonCombatTurnRightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Turn Right' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.EndVertical();
                        }
                    }

                    if (MovementTabNumberProp.intValue == 1)
                    {
                        CombatWalkFoldout.boolValue = CustomEditorProperties.Foldout(CombatWalkFoldout.boolValue, "Combat Walk Animations", true, myFoldoutStyle);

                        if (CombatWalkFoldout.boolValue)
                        {
                            EditorGUILayout.BeginVertical("Box");
                            CustomFloatAnimationField(new Rect(), new GUIContent(), CombatWalkAnimationSpeedProp, "Combat Walk Speed", 0.5f, 2);
                            CustomHelpLabelField("Controls how fast your AI's Combat Walk straight, left, and right animations play. When using Root Motion, " +
                                "this will also control your AI's walk movement speed.", true);

                            //Combat Walk Straight
                            CustomAnimationField(new Rect(), new GUIContent(), CombatWalkStraightProp, "Combat Walk Straight Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The walk animation that plays when your AI is walking straight when not in combat.", false);
                            if (self.CombatWalkStraightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatWalkStraightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Walk Straight' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Combat Walk Left
                            CustomAnimationField(new Rect(), new GUIContent(), CombatWalkLeftProp, "Combat Walk Left Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The walk animation that plays when your AI is walking left when in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorCombatWalkLeftProp, "Mirror Combat Walk Left");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.CombatWalkLeftAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatWalkLeftAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Walk Left' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Combat Walk Right
                            CustomAnimationField(new Rect(), new GUIContent(), CombatWalkRightProp, "Combat Walk Right Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The walk animation that plays when your AI is walking right when in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorCombatWalkRightProp, "Mirror Combat Walk Right");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.CombatWalkRightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatWalkRightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Walk Right' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Walk Back
                            CustomAnimationField(new Rect(), new GUIContent(), WalkBackProp, "Combat Walk Back Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The walk animation that plays when your AI is walking backwards when in combat.", false);
                            EditorGUILayout.PropertyField(ReverseWalkAnimationProp, new GUIContent("Reverse Walk Back"));
                            CustomHelpLabelField("Reverses the Combat Walk Back animation. This is useful if a model doesn't have a walk backwards animation. " +
                                "The model's walk animation can be used and will be reversed.", false);
                            if (self.CombatWalkBackAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatWalkBackAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Walk Back' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.EndVertical();
                        }

                        EditorGUILayout.Space();

                        CombatRunFoldout.boolValue = CustomEditorProperties.Foldout(CombatRunFoldout.boolValue, "Combat Run Animations", true, myFoldoutStyle);

                        if (CombatRunFoldout.boolValue)
                        {
                            EditorGUILayout.BeginVertical("Box");
                            CustomFloatAnimationField(new Rect(), new GUIContent(), CombatRunAnimationSpeedProp, "Run Speed", 0.5f, 2);
                            CustomHelpLabelField("Controls how fast your AI's Combat Run straight, left, and right animations play. When using Root Motion, " +
                                "this will also control your AI's run movement speed.", true);

                            //Combat Run Straight
                            CustomAnimationField(new Rect(), new GUIContent(), CombatRunStraightProp, "Combat Run Straight Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The run animation that plays when your AI is running straight when in combat.", false);
                            if (self.CombatRunStraightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatRunStraightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Run Straight' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Combat Run Left
                            CustomAnimationField(new Rect(), new GUIContent(), CombatRunLeftProp, "Combat Run Left Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The run animation that plays when your AI is running left when in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorCombatRunLeftProp, "Mirror Combat Run Left");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.CombatRunLeftAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatRunLeftAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Run Left' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Combat Run Right
                            CustomAnimationField(new Rect(), new GUIContent(), CombatRunRightProp, "Combat Run Right Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The run animation that plays when your AI is running right when in combat.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorCombatRunRightProp, "Mirror Combat Run Right");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.CombatRunRightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatRunRightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Run Right' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.EndVertical();
                        }

                        EditorGUILayout.Space();

                        CombatTurnFoldout.boolValue = CustomEditorProperties.Foldout(CombatTurnFoldout.boolValue, "Combat Turn Animations", true, myFoldoutStyle);

                        if (CombatTurnFoldout.boolValue)
                        {
                            EditorGUILayout.BeginVertical("Box");
                            //Combat Turn Left
                            CustomAnimationField(new Rect(), new GUIContent(), CombatTurnLeftProp, "Combat Turn Left", typeof(AnimationClip), false);
                            CustomHelpLabelField("The turn animation that plays when your AI is turning left when in combat.", false);
                            CustomFloatAnimationField(new Rect(), new GUIContent(), CombatTurnLeftAnimationSpeedProp, "Turn Left Animation Speed", 0.1f, 2);
                            CustomHelpLabelField("The speed in which the combat turn left animation will play.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorCombatTurnLeftProp, "Mirror Combat Turn Left");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.CombatTurnLeftAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatTurnLeftAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Turn Left' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.Space();

                            //Combat Turn Right
                            CustomAnimationField(new Rect(), new GUIContent(), CombatTurnRightProp, "Combat Turn Right", typeof(AnimationClip), false);
                            CustomHelpLabelField("The turn animation that plays when your AI is turning right when in combat.", false);
                            CustomFloatAnimationField(new Rect(), new GUIContent(), CombatTurnRightAnimationSpeedProp, "Turn Right Animation Speed", 0.1f, 2);
                            CustomHelpLabelField("The speed in which the combat turn left animation will play.", false);
                            CustomBoolAnimationField(new Rect(), new GUIContent(), MirrorCombatTurnRightProp, "Mirror Combat Turn Right");
                            CustomHelpLabelField("Mirroring an animation allows you to play an animation in the opposite direction.", false);

                            if (self.CombatTurnRightAnimation != null)
                            {
                                var settings = AnimationUtility.GetAnimationClipSettings(self.CombatTurnRightAnimation);
                                if (!settings.loopTime)
                                {
                                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                                    EditorGUILayout.LabelField("The 'Combat Turn Right' animation must be set to loop. To do so, go to your animation settings and set 'Loop Time' to true.", EditorStyles.helpBox);
                                    GUI.backgroundColor = Color.white;
                                }
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.EndVertical();
                        }
                    }
                }

                if (AnimationTabNumberProp.intValue == 2)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.Space();

                    EditorGUILayout.PropertyField(UseEquipAnimationProp, new GUIContent("Use Equip Animations"));
                    CustomHelpLabelField("Enables or disables the use of equip and unequip animations.", false);

                    if (self.UseEquipAnimation == EmeraldAISystem.YesOrNo.Yes)
                    {
                        GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                        EditorGUILayout.LabelField("Note: This requires an AI to have an EnableWeapon and DisableWeapon Animation Events setup on both their equip and unequip animations. " +
                            "For a guide on how to do this, refer to the Emerald AI Documentation, if you haven't yet set them up and would like to use this feature.", EditorStyles.helpBox);
                        GUI.backgroundColor = new Color(0, 0.65f, 0, 0.8f);
                        if (GUILayout.Button("See Tutorial", HelpButtonStyle, GUILayout.Height(20)))
                        {
                            Application.OpenURL("https://docs.google.com/document/d/1_zXR1gg61soAX_bZscs6HC-7as2njM7Jx9pYlqgbtM8/edit#heading=h.uae7izlggy0q");
                        }
                        GUI.backgroundColor = Color.white;

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.LabelField("Equip Animations", EditorStyles.boldLabel);

                        //Pullout Weapon
                        CustomAnimationField(new Rect(), new GUIContent(), PullOutWeaponAnimationProp, "Equip Weapon", typeof(AnimationClip), false);
                        CustomHelpLabelField("The animation that plays when the AI is pulling out their weapon.", false);

                        //Put Away Weapon
                        CustomAnimationField(new Rect(), new GUIContent(), PutAwayWeaponAnimationProp, "Unequip Weapon", typeof(AnimationClip), false);
                        CustomHelpLabelField("The animation that plays when the AI is putting away their weapon.", false);

                        EditorGUILayout.Space();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Attack Animations", EditorStyles.boldLabel);
                    CustomHelpLabelField("Controls the attack animations that will randomly play when the AI in combat. A max of 3 can be used.", false);
                    GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                    EditorGUILayout.LabelField("Note: You will need to manually create an Animation Event on your AI's attack animations to allow your AI to cause Damage. " +
                        "Please refer to Emerlad's documentation for a tutorial on how to do this.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(0, 0.65f, 0, 0.8f);
                    if (GUILayout.Button("See Tutorial", HelpButtonStyle, GUILayout.Height(20)))
                    {
                        if (self.WeaponTypeRef == EmeraldAISystem.WeaponType.Melee)
                        {
                            Application.OpenURL("https://docs.google.com/document/d/1_zXR1gg61soAX_bZscs6HC-7as2njM7Jx9pYlqgbtM8/edit#heading=h.k6lcoyggv3p");
                        }
                        else
                        {
                            Application.OpenURL("https://docs.google.com/document/d/1_zXR1gg61soAX_bZscs6HC-7as2njM7Jx9pYlqgbtM8/edit#heading=h.1t5n7bjsrgr1");
                        }
                    }
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.Space();
                    AttackAnimationList.DoLayoutList();
                    EditorGUILayout.Space();

                    //Attack
                    if (self.AttackAnimationList.Count == 3)
                    {
                        AttackAnimationList.displayAdd = false;
                    }
                    else
                    {
                        AttackAnimationList.displayAdd = true;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(UseRunAttacksProp, new GUIContent("Use Run Attacks"));
                    CustomHelpLabelField("Controls whether or not this AI will use run attacks.", true);

                    if (self.UseRunAttacksRef == EmeraldAISystem.UseRunAttacks.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.Space();
                        EditorGUILayout.LabelField("Run Attack Animations", EditorStyles.boldLabel);
                        CustomHelpLabelField("Controls the run attack animations that will randomly play when the AI in combat and moving. A max of 3 can be used.", false);
                        RunAttackAnimationList.DoLayoutList();

                        EditorGUILayout.Space();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }

                    //Run Attack
                    if (self.RunAttackAnimationList.Count == 3)
                    {
                        RunAttackAnimationList.displayAdd = false;
                    }
                    else
                    {
                        RunAttackAnimationList.displayAdd = true;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(UseHitAnimationsProp, new GUIContent("Use Hit Animation"));
                    CustomHelpLabelField("Controls whether or not this AI will use hit animations for both combat and non-combat.", true);

                    if (self.UseHitAnimations == EmeraldAISystem.YesOrNo.Yes)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.Space();
                        EditorGUILayout.LabelField("Blocking Animations", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(UseBlockingProp, new GUIContent("Use Blocking"));
                        CustomHelpLabelField("Controls whether or not this AI will have the ability to block. AI who use block must have a Block and Block Hit animation.", true);

                        if (self.UseBlockingRef == EmeraldAISystem.YesOrNo.Yes)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            EditorGUILayout.BeginVertical();

                            CustomAnimationField(new Rect(), new GUIContent(), BlockIdleAnimationProp, "Block Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The animation that plays when your AI is blocking.", false);

                            CustomAnimationField(new Rect(), new GUIContent(), BlockHitAnimationProp, "Block Impact Animation", typeof(AnimationClip), false);
                            CustomHelpLabelField("The animation that plays when your AI is blocking and is hit with an attack.", false);

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();

                        EditorGUILayout.LabelField("Combat Hit Animations", EditorStyles.boldLabel);
                        CustomHelpLabelField("Controls the animations that will randomly play when an AI receives damage when in combat.", false);
                        CombatHitAnimationList.DoLayoutList();

                        //Combat Hit
                        if (self.CombatHitAnimationList.Count == 3)
                        {
                            CombatHitAnimationList.displayAdd = false;
                        }
                        else
                        {
                            CombatHitAnimationList.displayAdd = true;
                        }

                        EditorGUILayout.Space();
                        EditorGUILayout.LabelField("Hit Animations", EditorStyles.boldLabel);
                        CustomHelpLabelField("Controls the animations that will randomly play when an AI receives damage when not in combat.", false);
                        HitAnimationList.DoLayoutList();
                        EditorGUILayout.Space();

                        //Hit
                        if (self.HitAnimationList.Count == 3)
                        {
                            HitAnimationList.displayAdd = false;
                        }
                        else
                        {
                            HitAnimationList.displayAdd = true;
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Death Animations", EditorStyles.boldLabel);
                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                    EditorGUILayout.LabelField("Controls the animations that will randomly play when an AI receives damage. The speed of each animation can be " +
                        "adjusted by changing the speed parameter.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                    EditorGUILayout.LabelField("Note: Death animations will only work when using the Animation Death Type. This setting is located under AI's " +
                        "Settings>Combat>Combat Actions & Effect Settings.", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    DeathAnimationList.DoLayoutList();
                    EditorGUILayout.Space();

                    //Death
                    if (self.DeathAnimationList.Count == 3)
                    {
                        DeathAnimationList.displayAdd = false;
                    }
                    else
                    {
                        DeathAnimationList.displayAdd = true;
                    }

                    EditorGUILayout.EndVertical();
                }

                if (AnimationTabNumberProp.intValue == 3)
                {
                    EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Emote Animations", EditorStyles.boldLabel);
                    GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
                    EditorGUILayout.LabelField("Controls the emote animations that will play when an AI's PlayEmoteAnimation function is called. The speed of each animation can be adjusted by changing the speed parameter.", EditorStyles.helpBox);
                    GUI.backgroundColor = new Color(1f, 1, 0.25f, 0.25f);
                    EditorGUILayout.LabelField("Note: Emote animations are not applied to the AI's Animator Controller. These animations are applied when the PlayEmoteAnimation function is called passing the emote ID as the parameter. " +
                        "There is no cap to how many emote animations an AI can have. Each Emote must have its own unique ID.", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                    EmoteAnimationList.DoLayoutList();

                    //Emote
                    if (self.EmoteAnimationList.Count == 3)
                    {
                        EmoteAnimationList.displayAdd = false;
                    }
                    else
                    {
                        EmoteAnimationList.displayAdd = true;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginVertical("Box");
                if (!self.AnimatorControllerGenerated && Selection.gameObjects.Length == 1)
                {
                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                    EditorGUILayout.LabelField("In order for this AI to have animations, you must create an Animator Controller for it. To do so, press the 'Create Animator Controller' button below.'", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                }

                if (self.AnimatorControllerGenerated)
                {
                    GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                    EditorGUILayout.LabelField("Clears the current Animator Controller so a new one can be created.", EditorStyles.helpBox);
                    GUI.backgroundColor = Color.white;
                }

                if (!self.AnimatorControllerGenerated)
                {
                    if (GUILayout.Button("Create Animator Controller"))
                    {
                        self.FilePath = EditorUtility.SaveFilePanelInProject("Save as OverrideController", "New OverrideController", "overrideController", "Please enter a file name to save the file to");
                        if (self.FilePath != string.Empty)
                        {
                            string UserFilePath = self.FilePath;
                            string SourceFilePath = AssetDatabase.GetAssetPath(Resources.Load("Emerald Animator Controller"));
                            AssetDatabase.CopyAsset(SourceFilePath, UserFilePath);
                            self.AIAnimator = self.gameObject.GetComponent<Animator>();
                            self.AIAnimator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath(UserFilePath, typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

                            if (Selection.gameObjects.Length > 1)
                            {
                                foreach (GameObject G in Selection.gameObjects)
                                {
                                    if (G.GetComponent<EmeraldAISystem>() != null)
                                    {
                                        EmeraldAISystem EmeraldComponent = G.GetComponent<EmeraldAISystem>();
                                        EmeraldComponent.AIAnimator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath(UserFilePath, typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                                        EmeraldComponent.AIAnimator = G.GetComponent<Animator>();
                                    }
                                }
                            }

                            EmeraldAIAnimatorGenerator.GenerateAnimatorController(self);
                            serializedObject.Update();
                            self.AnimatorControllerGenerated = true;
                            AnimationsUpdatedProp.boolValue = false;
                            EditorUtility.SetDirty(self);
                        }
                    }
                }

                if (self.AnimatorControllerGenerated)
                {
                    var style = new GUIStyle(GUI.skin.button);
                    style.normal.textColor = Color.red;
                    if (GUILayout.Button("Clear Animator Controller", style) && EditorUtility.DisplayDialog("Clear Animator Controller?", "Are you sure you want to clear this AI's Animator Controller? This process cannot be undone.", "Yes", "Cancel"))
                    {
                        foreach (GameObject G in Selection.gameObjects)
                        {
                            if (G.GetComponent<EmeraldAISystem>() != null)
                            {
                                G.GetComponent<EmeraldAISystem>().AnimatorControllerGenerated = false;
                            }
                        }
                    }
                    GUI.contentColor = Color.white;
                    GUI.backgroundColor = Color.white;
                }

                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }

            if (TabNumberProp.intValue == 7)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical("Box", GUILayout.Width(85 * Screen.width / Screen.dpi));
                var style = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(new GUIContent(DocumentationIcon), style, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                EditorGUILayout.LabelField("Documentation", style, GUILayout.ExpandWidth(true));
                GUILayout.Space(2);
                EditorGUILayout.LabelField("Emerald's Docs can all be found below. This is to give users easy access to tutorials, script references, and documentation all from " +
                    "within the Emerald Editor. Each section is online so users always get the most up to date material.", EditorStyles.helpBox);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Documentation", EditorStyles.boldLabel);
                CustomHelpLabelField("Contains detailed guides and tutorials to help get you started and familiar with Emerald.", false);
                if (GUILayout.Button("Documentation", GUILayout.Height(28)))
                {
                    Application.OpenURL("https://docs.google.com/document/d/1_zXR1gg61soAX_bZscs6HC-7as2njM7Jx9pYlqgbtM8/edit?usp=sharing");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Integration Tutorials", EditorStyles.boldLabel);
                CustomHelpLabelField("Written tutorials covering the integration of some of the top character controller systems such as UFPS, RFPS, Game Kit Controller, " +
                    "Invector Third Person Controller, and Ootii Motion Controller.", false);
                if (GUILayout.Button("Integration Tutorials", GUILayout.Height(28)))
                {
                    Application.OpenURL("https://docs.google.com/document/d/1QGSEdc2-6bks22KIelYlBw_601uHce2BVLM3QTq5axg/edit?usp=sharing");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Written Tutorials", EditorStyles.boldLabel);
                CustomHelpLabelField("Various written tutorials covering a wide range of features and implementations.", false);
                if (GUILayout.Button("Written Tutorials", GUILayout.Height(28)))
                {
                    Application.OpenURL("https://docs.google.com/document/d/1FTK1gdwfCHn1fNelDp2zf5k6nLB9IcVHUV3PMjgK6ho/edit?usp=sharing");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Video Tutorials", EditorStyles.boldLabel);
                GUI.backgroundColor = new Color(0.1f,0.1f,0.1f,0.19f);
                EditorGUILayout.LabelField("Various video tutorials covering a wide range of features and implementations.", EditorStyles.helpBox);
                GUI.backgroundColor = Color.white;
                if (GUILayout.Button("Video Tutorials", GUILayout.Height(28))){
                    Application.OpenURL("https://www.youtube.com/playlist?list=PLlyiPBj7FznY7q4bdDQgGYgUByYpeCe07");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Scripting Reference", EditorStyles.boldLabel);
                CustomHelpLabelField("A site that has all of Emerald's API documented with script examples.", false);
                if (GUILayout.Button("Scripting Reference", GUILayout.Height(28)))
                {
                    Application.OpenURL("http://www.blackhorizonstudios.com/docs/emerald-script-reference/");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Solutions to Possible Issues", EditorStyles.boldLabel);
                CustomHelpLabelField("Get solutions to possible issues you may have. If you've encountered an issue, there is most likely a solution for it here.", false);
                if (GUILayout.Button("Solutions to Possible Issues", GUILayout.Height(28)))
                {
                    Application.OpenURL("https://docs.google.com/document/d/1_NjuySY0x7OjRv0lTZzEEP4-AOozmItoJkxgbDgtRQ8/edit?usp=sharing");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Emerald AI Forums", EditorStyles.boldLabel);
                CustomHelpLabelField("The forums are a great way to get quick support as well being kept update to date on upcoming updates.", false);
                if (GUILayout.Button("Emerald AI Forums", GUILayout.Height(28)))
                {
                    Application.OpenURL("https://forum.unity.com/threads/update-coming-soon-emerald-ai-dynamic-wildlife-breeding-predators-prey-herds-npcs-more.336521/");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Customer Support", EditorStyles.boldLabel);
                CustomHelpLabelField("All the contact information you will need to get quick support.", false);
                if (GUILayout.Button("Customer Support", GUILayout.Height(28)))
                {
                    Application.OpenURL("http://www.blackhorizonstudios.com/contact/");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Report a Bug", EditorStyles.boldLabel);
                CustomHelpLabelField("If you've encountered a bug, you can fill out a bug report here. This allows bugs to be well documented so they can be fixed as soon as possible.", false);
                if (GUILayout.Button("Report a Bug", GUILayout.Height(28)))
                {
                    Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdzHNlLSCyv2k3LPT5STMSRuWPLFIanci0rTuC7BjQQgAoDgA/viewform?usp=sf_link");
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(EnableDebuggingPop, new GUIContent("Enable Debugging"));
                CustomHelpLabelField("Enables certain debugging options to assist development or help find issues.", true);

                if (self.EnableDebugging == EmeraldAISystem.YesOrNo.Yes)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.PropertyField(DrawRaycastsEnabledProp, new GUIContent("Draw Raycasts"));
                    CustomHelpLabelField("Allows raycasts to be draw, while in the Unity Editor. " +
                        "This can be useful for ensuring raycast are being positioned correctly.", true);

                    EditorGUILayout.PropertyField(DebugLogTargetsEnabledProp, new GUIContent("Debug Log Targets"));
                    CustomHelpLabelField("Allows the target objects to be displayed in the Unity Console. " +
                        "This can be useful for ensure the proper object is being hit when the AI is targeting an object.", true);

                    EditorGUILayout.PropertyField(DebugLogObstructionsEnabledProp, new GUIContent("Debug Log Obstructions"));
                    CustomHelpLabelField("Allows the AI's obstructions to be displayed in the Unity Console. " +
                        "This can be useful for ensure the proper object is being hit when the AI is targeting an object.", true);

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

        //Draw all of our Editor related controls, settings, and effects
        void OnSceneGUI()
        {
            EmeraldAISystem self = (EmeraldAISystem)target;

            if (TabNumberProp.intValue == 1 && TemperamentTabNumberProp.intValue == 2 && CombatTabNumberProp.intValue == 1)
            {
                Handles.color = new Color(255, 0, 0, 0.5f);
                self.m_ProjectileCollisionPoint = new Vector3(self.transform.position.x, self.transform.position.y + self.ProjectileCollisionPointY, self.transform.position.z);
                Handles.SphereHandleCap(0, self.m_ProjectileCollisionPoint, Quaternion.identity, 0.5f, EventType.Repaint);
                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.white;
                style.alignment = TextAnchor.MiddleCenter;
                Handles.Label(self.m_ProjectileCollisionPoint + Vector3.right/5 + Vector3.up / 2, "Hit Transform", style);
            }

            //Draw two arcs each with 50% of the field of view in opposite directions
            Handles.color = new Color(255, 0, 0, 0.07f);
            if (self.DetectionTypeRef == EmeraldAISystem.DetectionType.LineOfSight && TabNumberProp.intValue == 2 && DetectionTagsTabNumberProp.intValue == 0)
            {
                Handles.DrawSolidArc(self.transform.position, self.transform.up, self.transform.forward, self.fieldOfViewAngle / 2, (float)self.DetectionRadius);
                Handles.DrawSolidArc(self.transform.position, self.transform.up, self.transform.forward, -self.fieldOfViewAngle / 2, (float)self.DetectionRadius);
                Handles.color = Color.white;
            }
            else if (self.DetectionTypeRef == EmeraldAISystem.DetectionType.Trigger && TabNumberProp.intValue == 2 && DetectionTagsTabNumberProp.intValue == 0)
            {
                Handles.DrawSolidDisc(self.transform.position, self.transform.up, (float)self.DetectionRadius);
                Handles.color = Color.white;
            }

            if (self.WanderTypeRef == EmeraldAISystem.WanderType.Dynamic && TabNumberProp.intValue == 0)
            {
                Handles.color = new Color(0, 255, 0, 1.0f);
                Handles.DrawWireDisc(self.transform.position, self.transform.up, (float)self.WanderRadius);
                Handles.color = Color.white;
            }

            if (TabNumberProp.intValue == 3)
            {
                if (self.DisplayAINameRef == EmeraldAISystem.DisplayAIName.Yes)
                {
                    Handles.color = self.NameTextColor;
                    Handles.DrawLine(new Vector3(self.transform.localPosition.x, self.transform.localPosition.y, self.transform.localPosition.z), 
                        new Vector3(self.AINamePos.x, self.AINamePos.y, self.AINamePos.z) + self.transform.localPosition);
                    Handles.color = Color.white;
                }

                if (self.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.Yes)
                {
                    Handles.color = self.HealthBarColor;
                    Handles.DrawLine(new Vector3(self.transform.localPosition.x + 0.25f, self.transform.localPosition.y, self.transform.localPosition.z), 
                        new Vector3(self.HealthBarPos.x + 0.25f, self.HealthBarPos.y, self.HealthBarPos.z) + self.transform.localPosition);
                    Handles.color = Color.white;
                }

                if (self.UseCombatTextRef == EmeraldAISystem.UseCombatText.Yes)
                {
                    Handles.color = self.CombatTextColor;
                    Handles.DrawLine(new Vector3(self.transform.localPosition.x - 0.25f, self.transform.localPosition.y, self.transform.localPosition.z), 
                        new Vector3((self.CombatTextPos.x - 0.25f) + self.transform.localPosition.x, self.CombatTextPos.y + self.transform.localPosition.y, 
                        self.CombatTextPos.z + self.transform.localPosition.z));
                    Handles.color = Color.white;
                }
            }

            if (TabNumberProp.intValue == 5 && self.WanderTypeRef == EmeraldAISystem.WanderType.Waypoints)
            {
                if (self.WaypointsList.Count > 0 && self.WaypointsList != null)
                {
                    Handles.color = Color.blue;
                    Handles.DrawLine(self.transform.position, self.WaypointsList[0]);
                    Handles.color = Color.white;

                    Handles.color = Color.green;
                    if (self.WaypointTypeRef != (EmeraldAISystem.WaypointType.Random))
                    {
                        for (int i = 0; i < self.WaypointsList.Count - 1; i++)
                        {
                            Handles.DrawLine(self.WaypointsList[i], self.WaypointsList[i + 1]);
                        }
                    }
                    else if (self.WaypointTypeRef == (EmeraldAISystem.WaypointType.Random))
                    {
                        for (int i = 0; i < self.WaypointsList.Count; i++)
                        {
                            for (int j = (i + 1); j < self.WaypointsList.Count; j++)
                            {
                                Handles.DrawLine(self.WaypointsList[i], self.WaypointsList[j]);
                            }
                        }
                    }
                    Handles.color = Color.white;

                    Handles.color = Color.green;
                    if (self.WaypointTypeRef == (EmeraldAISystem.WaypointType.Loop))
                    {
                        Handles.DrawLine(self.WaypointsList[0], self.WaypointsList[self.WaypointsList.Count - 1]);
                    }
                    Handles.color = Color.white;

                    Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
                    for (int i = 0; i < self.WaypointsList.Count; i++)
                    {
                        Handles.SphereHandleCap(0, self.WaypointsList[i], Quaternion.identity, 0.5f, EventType.Repaint);
                        CustomEditorProperties.DrawString("Waypoint " + (i + 1), self.WaypointsList[i] + Vector3.up, Color.white);
                    }

                    Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
                    for (int i = 0; i < self.WaypointsList.Count; i++)
                    {
                        self.WaypointsList[i] = Handles.PositionHandle(self.WaypointsList[i], Quaternion.identity);
                    }

#if UNITY_EDITOR
                    EditorUtility.SetDirty(self);
#endif
                }
            }

            if (TabNumberProp.intValue == 0 && self.WanderTypeRef == EmeraldAISystem.WanderType.Destination && self.SingleDestination != Vector3.zero)
            {
                Handles.color = Color.green;
                Handles.DrawLine(self.transform.position, self.SingleDestination);
                Handles.color = Color.white;

                Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
                Handles.SphereHandleCap(0, self.SingleDestination, Quaternion.identity, 0.5f, EventType.Repaint);
                CustomEditorProperties.DrawString("Destination Point", self.SingleDestination + Vector3.up, Color.white);

                Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
                self.SingleDestination = Handles.PositionHandle(self.SingleDestination, Quaternion.identity);

#if UNITY_EDITOR
                EditorUtility.SetDirty(self);
#endif
            }
        }

#region Local Properties
        void CustomIntFieldProjectiles(Rect position, GUIContent label, SerializedProperty property, string Name, int ProjectileNumber)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.IntField(Name, property.intValue);

            if (newValue != property.intValue)
            {
                if (ProjectileNumber == 1)
                {
                    Projectile1UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 2)
                {
                    Projectile2UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 3)
                {
                    Projectile3UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 4)
                {
                    RunProjectileUpdatedProp.boolValue = true;
                }
            }

            if (EditorGUI.EndChangeCheck())
                property.intValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomFloatAnimationField(Rect position, GUIContent label, SerializedProperty property, string Name, float Min, float Max)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.Slider(Name, property.floatValue, Min, Max);

            if (newValue != property.floatValue)
            {
                AnimationsUpdatedProp.boolValue = true;
            }

            if (EditorGUI.EndChangeCheck())
                property.floatValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomBoolAnimationField(Rect position, GUIContent label, SerializedProperty property, string Name)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.Toggle(Name, property.boolValue);

            if (newValue != property.boolValue)
            {
                AnimationsUpdatedProp.boolValue = true;
            }

            if (EditorGUI.EndChangeCheck())
                property.boolValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomObjectFieldProjectiles(Rect position, GUIContent label, SerializedProperty property, string Name, Type typeOfObject, bool IsEssential, int ProjectileNumber)
        {
            if (IsEssential && property.objectReferenceValue == null)
            {
                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                EditorGUILayout.LabelField("This field cannot be left blank", EditorStyles.helpBox);
                GUI.backgroundColor = Color.white;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.ObjectField(Name, property.objectReferenceValue, typeOfObject, true);

            if (newValue != property.objectReferenceValue)
            {
                if (ProjectileNumber == 1)
                {
                    Projectile1UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 2)
                {
                    Projectile2UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 3)
                {
                    Projectile3UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 4)
                {
                    RunProjectileUpdatedProp.boolValue = true;
                }
            }

            if (EditorGUI.EndChangeCheck())
                property.objectReferenceValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomObjectFieldHitEffect(Rect position, GUIContent label, SerializedProperty property, string Name, Type typeOfObject, bool IsEssential)
        {
            if (IsEssential && property.objectReferenceValue == null)
            {
                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                EditorGUILayout.LabelField("This field cannot be left blank", EditorStyles.helpBox);
                GUI.backgroundColor = Color.white;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.ObjectField(Name, property.objectReferenceValue, typeOfObject, true);

            if (newValue != property.objectReferenceValue)
            {
                BloodEffectUpdatedProp.boolValue = true;
            }

            if (EditorGUI.EndChangeCheck())
                property.objectReferenceValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomFloatFieldProjectiles(Rect position, GUIContent label, SerializedProperty property, string Name, int ProjectileNumber)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.FloatField(Name, property.floatValue);

            if (newValue != property.floatValue)
            {
                if (ProjectileNumber == 1)
                {
                    Projectile1UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 2)
                {
                    Projectile2UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 3)
                {
                    Projectile3UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 4)
                {
                    RunProjectileUpdatedProp.boolValue = true;
                }
            }

            if (EditorGUI.EndChangeCheck())
                property.floatValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomFloatFieldHitEffect(Rect position, GUIContent label, SerializedProperty property, string Name)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.FloatField(Name, property.floatValue);

            if (newValue != property.floatValue)
            {
                BloodEffectUpdatedProp.boolValue = true;
            }

            if (EditorGUI.EndChangeCheck())
                property.floatValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomPopupProjectiles(Rect position, GUIContent label, SerializedProperty property, string nameOfLabel, Type typeOfEnum, int ProjectileNumber)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            string[] enumNamesList = System.Enum.GetNames(typeOfEnum);

            var newValue = EditorGUILayout.Popup(nameOfLabel, property.intValue, enumNamesList);

            if (newValue != property.intValue)
            {
                if (ProjectileNumber == 1)
                {
                    Projectile1UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 2)
                {
                    Projectile2UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 3)
                {
                    Projectile3UpdatedProp.boolValue = true;
                }
                else if (ProjectileNumber == 4)
                {
                    RunProjectileUpdatedProp.boolValue = true;
                }
            }

            if (EditorGUI.EndChangeCheck())
                property.intValue = newValue;

            EditorGUI.EndProperty();
        }

        void CustomAnimationField(Rect position, GUIContent label, SerializedProperty property, string Name, Type typeOfObject, bool IsEssential)
        {
            if (IsEssential && property.objectReferenceValue == null)
            {
                GUI.backgroundColor = new Color(10f, 0.0f, 0.0f, 0.25f);
                EditorGUILayout.LabelField("This field cannot be left blank", EditorStyles.helpBox);
                GUI.backgroundColor = Color.white;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.ObjectField(Name, property.objectReferenceValue, typeOfObject, true);

            if (newValue != property.objectReferenceValue)
            {
                AnimationsUpdatedProp.boolValue = true;
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.objectReferenceValue = newValue;
            }


            EditorGUI.EndProperty();
        }

        void CustomHelpLabelField (string TextInfo, bool UseSpace)
        {
            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.19f);
            EditorGUILayout.LabelField(TextInfo, EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
            if (UseSpace)
            {
                EditorGUILayout.Space();
            }
        }

        // Converts the field value to a LayerMask
        private LayerMask FieldToLayerMask(int field)
        {
            LayerMask mask = 0;
            var layers = InternalEditorUtility.layers;
            for (int c = 0; c < layers.Length; c++)
            {
                if ((field & (1 << c)) != 0)
                {
                    mask |= 1 << LayerMask.NameToLayer(layers[c]);
                }
            }
            return mask;
        }
        // Converts a LayerMask to a field value
        private int LayerMaskToField(LayerMask mask)
        {
            int field = 0;
            var layers = InternalEditorUtility.layers;
            for (int c = 0; c < layers.Length; c++)
            {
                if ((mask & (1 << LayerMask.NameToLayer(layers[c]))) != 0)
                {
                    field |= 1 << c;
                }
            }
            return field;
        }
#endregion
    }
}