using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmeraldAI.Utility
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody))]
    public class EmeraldAIProjectile : MonoBehaviour
    {
        [HideInInspector]
        public EmeraldAISystem EmeraldSystem;
        [HideInInspector]
        public Vector3 ProjectileDirection;
        [HideInInspector]
        public float TimeoutTimer;
        [HideInInspector]
        public float CollisionTimer;
        [HideInInspector]
        public bool Collided;
        [HideInInspector]
        public SphereCollider ProjectileCollider;
        GameObject SpawnedEffect;
        GameObject CollisionSound;
        GameObject CollisionSoundObject;
        Vector3 LastDirection;
        float m_TargetDeadTimer;

        //Customizable variables
        [HideInInspector]
        public int RagdollForce = 100;
        [HideInInspector]
        public float TimeoutTime = 4.5f;
        [HideInInspector]
        public float CollisionTime = 0;
        [HideInInspector]
        public int ProjectileSpeed = 30;
        [HideInInspector]
        public enum EffectOnCollision { No = 0, Yes = 1 };
        [HideInInspector]
        public EffectOnCollision EffectOnCollisionRef = EffectOnCollision.No;
        [HideInInspector]
        public EffectOnCollision SoundOnCollisionRef = EffectOnCollision.No;
        [HideInInspector]
        public enum HeatSeeking { No = 0, Yes = 1 };
        [HideInInspector]
        public HeatSeeking HeatSeekingRef = HeatSeeking.No;
        [HideInInspector]
        public AudioClip ImpactSound;
        [HideInInspector]
        public GameObject CollisionEffect;
        [HideInInspector]
        public Vector3 AdditionalHeight;
        [HideInInspector]
        public float HeatSeekingSeconds = 1;
        [HideInInspector]
        public float HeatSeekingTimer = 0;
        [HideInInspector]
        public float HeatSeekingInitializeTimer = 0;
        [HideInInspector]
        public bool HeatSeekingFinished = false;
        [HideInInspector]
        public Transform ProjectileCurrentTarget;
        [HideInInspector]
        public bool TargetInView = false;

        Vector3 AdjustTargetPosition;

        [HideInInspector]
        public GameObject ObjectToDisableOnCollision;
        public enum ArrowObject { No = 0, Yes = 1 };
        [HideInInspector]
        public ArrowObject ArrowProjectileRef = ArrowObject.No;

        //Setup our AI's projectile once on Awake
        void Awake()
        {
            gameObject.layer = 2;
            ProjectileCollider = GetComponent<SphereCollider>();
            ProjectileCollider.isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.isStatic = false;
            CollisionSoundObject = Resources.Load("Emerald Collision Sound") as GameObject;
        }

        void Start()
        {
            GetHeatSeekingAngle();
        }

        void OnEnable()
        {
            GetHeatSeekingAngle();
            m_TargetDeadTimer = 0;
            if (ObjectToDisableOnCollision != null)
            {
                ObjectToDisableOnCollision.SetActive(true);
            }
        }

        //Only use the heat seeking feature if your target is within 65 degrees of our AI
        //to prevent the AI from firing behind themselves.
        void GetHeatSeekingAngle()
        {
            if (HeatSeekingRef == HeatSeeking.Yes && ProjectileCurrentTarget != null && EmeraldSystem != null)
            {
                Vector3 direction = (ProjectileCurrentTarget.position) - EmeraldSystem.transform.position;
                float angle = Vector3.Angle(new Vector3(direction.x, 0, direction.z), new Vector3(EmeraldSystem.transform.forward.x, 0, EmeraldSystem.transform.forward.z));
                float AdjustedAngle = Mathf.Abs(angle);
                if (AdjustedAngle <= 65)
                {
                    TargetInView = true;
                }
                else
                {
                    TargetInView = false;
                }
            }
        }

        //TODO: Projectile calculations are really improved, as well as head look. However, some issues still remain. 1st) The heat seeking option needs to not use heat seeking for the first tenth of a second,
        //but still calculate properly like the heat seeking one. 2nd) Head look still glitches after switching targets. 3rd) Non-heating seeking projectile now rotates incorrectly. Fix this and make an option
        //to adjust the axis. 4th) When scaling an AI, their end raycast line goes higher as the shooter is scaled.
        void Update()
        {
            //Continue to have our AI projectile follow the direction of its target until it colliders with something
            if (!Collided && HeatSeekingRef == HeatSeeking.No && ProjectileDirection != Vector3.zero ||
                !TargetInView && !Collided && ProjectileDirection != Vector3.zero)
            {
                DeadTargetDetection();
                if (EmeraldSystem.TargetTypeRef == EmeraldAISystem.TargetType.AI && EmeraldSystem.TargetEmerald != null)
                {
                    AdjustTargetPosition = new Vector3(ProjectileDirection.x, ProjectileDirection.y + EmeraldSystem.TargetEmerald.ProjectileCollisionPointY, ProjectileDirection.z);
                }
                else
                {
                    AdjustTargetPosition = new Vector3(ProjectileDirection.x, ProjectileDirection.y + ProjectileCurrentTarget.localScale.y / 2, ProjectileDirection.z);
                }
                transform.position = transform.position + AdjustTargetPosition.normalized * Time.deltaTime * ProjectileSpeed;
                transform.rotation = Quaternion.LookRotation(AdjustTargetPosition);
            }

            //Give our fired projectile a 20th of a second to leave the caster's hand before following the target's position.
            //This is to stop the projectile from firing behind the caster if the target happens to get behind them while they're firing.
            if (!Collided && HeatSeekingRef == HeatSeeking.Yes && TargetInView)
            {
                if (!HeatSeekingFinished)
                {
                    HeatSeekingInitializeTimer += Time.deltaTime;

                    if (EmeraldSystem.TargetTypeRef == EmeraldAISystem.TargetType.AI && EmeraldSystem.TargetEmerald != null)
                    {
                        AdjustTargetPosition = new Vector3(ProjectileCurrentTarget.position.x, EmeraldSystem.TargetEmerald.m_ProjectileCollisionPoint.y, ProjectileCurrentTarget.position.z);
                    }
                    else
                    {
                        AdjustTargetPosition = new Vector3(ProjectileCurrentTarget.position.x, ProjectileCurrentTarget.position.y + ProjectileCurrentTarget.localScale.y / 2, ProjectileCurrentTarget.position.z);
                    }

                    if (HeatSeekingInitializeTimer < 0.1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, AdjustTargetPosition, Time.deltaTime * ProjectileSpeed);
                    }

                    if (HeatSeekingInitializeTimer >= 0.1f && ProjectileCurrentTarget != null)
                    {
                        DeadTargetDetection();
                        transform.position = Vector3.MoveTowards(transform.position, AdjustTargetPosition, Time.deltaTime * ProjectileSpeed);
                        HeatSeekingTimer += Time.deltaTime;

                        if (HeatSeekingTimer >= HeatSeekingSeconds)
                        {
                            LastDirection = Vector3.Normalize(((ProjectileCurrentTarget.position + AdditionalHeight) - transform.position));
                            HeatSeekingFinished = true;
                        }
                    }
                }
                else if (HeatSeekingFinished && LastDirection != Vector3.zero)
                {
                    DeadTargetDetection();
                    if (EmeraldSystem.TargetTypeRef == EmeraldAISystem.TargetType.AI && EmeraldSystem.TargetEmerald != null)
                    {
                        AdjustTargetPosition = new Vector3(ProjectileDirection.x, ProjectileDirection.y + EmeraldSystem.TargetEmerald.ProjectileCollisionPointY, ProjectileDirection.z);
                    }
                    else
                    {
                        AdjustTargetPosition = new Vector3(ProjectileDirection.x, ProjectileDirection.y + ProjectileCurrentTarget.localScale.y / 2, ProjectileDirection.z);
                    }
                    transform.position = transform.position + AdjustTargetPosition.normalized * Time.deltaTime * ProjectileSpeed;
                }
            }

            //Track our time since instantiation, once the Timeout time has been reachd, despawn
            TimeoutTimer += Time.deltaTime;
            if (TimeoutTimer >= TimeoutTime)
            {
                EmeraldAIObjectPool.Despawn(gameObject);
            }

            if (Collided)
            {
                CollisionTimer += Time.deltaTime;
                if (CollisionTimer >= CollisionTime)
                {
                    EmeraldAIObjectPool.Despawn(gameObject);
                }
            }
        }

        void DeadTargetDetection()
        {
            if (!Collided && ProjectileCurrentTarget != null && Vector3.Distance(ProjectileCurrentTarget.position, transform.position) <= 1)
            {
                if (ObjectToDisableOnCollision != null)
                {
                    ObjectToDisableOnCollision.SetActive(true);
                }

                m_TargetDeadTimer += Time.deltaTime;

                if (m_TargetDeadTimer >= 0.1f)
                {
                    if (ObjectToDisableOnCollision != null)
                    {
                        ObjectToDisableOnCollision.SetActive(false);
                    }
                    if (ImpactSound != null && SoundOnCollisionRef == EffectOnCollision.Yes)
                    {
                        CollisionSound = EmeraldAIObjectPool.Spawn(CollisionSoundObject, transform.position, Quaternion.identity);
                        CollisionSound.transform.SetParent(EmeraldAISystem.ObjectPool.transform);
                        AudioSource CollisionAudioSource = CollisionSound.GetComponent<AudioSource>();
                        CollisionAudioSource.PlayOneShot(ImpactSound);
                    }
                    if (EffectOnCollisionRef == EffectOnCollision.Yes)
                    {
                        if (CollisionEffect != null)
                        {
                            SpawnedEffect = EmeraldAIObjectPool.Spawn(CollisionEffect, transform.position, Quaternion.identity);
                            SpawnedEffect.transform.SetParent(EmeraldAISystem.ObjectPool.transform);
                        }
                    }
                    Collided = true;
                    ProjectileCollider.enabled = false;
                }
            }
        }

        //Handle all of our collision related calculations here. When this happens, effects and sound can be played before the object is despawned.
        void OnTriggerEnter(Collider C)
        {
            if (!Collided && EmeraldSystem != null && ProjectileCurrentTarget != null && C.gameObject == ProjectileCurrentTarget.gameObject && C.gameObject.layer != 2)
            {
                if (EffectOnCollisionRef == EffectOnCollision.Yes)
                {
                    if (CollisionEffect != null)
                    {
                        SpawnedEffect = EmeraldAIObjectPool.Spawn(CollisionEffect, transform.position, Quaternion.identity);
                        SpawnedEffect.transform.SetParent(EmeraldAISystem.ObjectPool.transform);
                    }
                }
                if (ImpactSound != null && SoundOnCollisionRef == EffectOnCollision.Yes)
                {
                    CollisionSound = EmeraldAIObjectPool.Spawn(CollisionSoundObject, transform.position, Quaternion.identity);
                    CollisionSound.transform.SetParent(EmeraldAISystem.ObjectPool.transform);
                    AudioSource CollisionAudioSource = CollisionSound.GetComponent<AudioSource>();
                    CollisionAudioSource.PlayOneShot(ImpactSound);
                }

                if (EmeraldSystem.TargetTypeRef == EmeraldAISystem.TargetType.AI && EmeraldSystem.TargetEmerald != null)
                {
                    EmeraldSystem.TargetEmerald.Damage(EmeraldSystem.CurrentDamageAmount, EmeraldAISystem.TargetType.AI, EmeraldSystem.transform, EmeraldSystem.SentRagdollForceAmount);
                }
                else if (EmeraldSystem.TargetTypeRef == EmeraldAISystem.TargetType.Player)
                {
                    EmeraldSystem.SendEmeraldDamage();
                }
                else if (EmeraldSystem.TargetTypeRef == EmeraldAISystem.TargetType.NonAITarget)
                {
                    EmeraldSystem.SendEmeraldDamage();
                }

                if (ObjectToDisableOnCollision != null)
                {
                    ObjectToDisableOnCollision.SetActive(false);
                }

                if (ArrowProjectileRef == ArrowObject.Yes)
                {
                    CollisionTime = 0;
                }
                Collided = true;
                ProjectileCollider.enabled = false;
            }
            else if (!Collided && EmeraldSystem != null && ProjectileCurrentTarget != null && C.gameObject != ProjectileCurrentTarget.gameObject && C.gameObject != EmeraldSystem.gameObject && C.gameObject.layer != 2)
            {
                Collided = true;
                ProjectileCollider.enabled = false;

                if (ObjectToDisableOnCollision != null)
                {
                    ObjectToDisableOnCollision.SetActive(false);
                }

                //If another AI is hit, disable projectile immediately
                if (C.GetComponent<EmeraldAISystem>() != null)
                {
                    CollisionTime = 0;
                }

                if (EffectOnCollisionRef == EffectOnCollision.Yes)
                {
                    if (CollisionEffect != null)
                    {
                        SpawnedEffect = EmeraldAIObjectPool.Spawn(CollisionEffect, transform.position, Quaternion.identity);
                        SpawnedEffect.transform.SetParent(EmeraldAISystem.ObjectPool.transform);
                    }
                }
                if (ImpactSound != null && SoundOnCollisionRef == EffectOnCollision.Yes)
                {
                    CollisionSound = EmeraldAIObjectPool.Spawn(CollisionSoundObject, transform.position, Quaternion.identity);
                    CollisionSound.transform.SetParent(EmeraldAISystem.ObjectPool.transform);
                    AudioSource CollisionAudioSource = CollisionSound.GetComponent<AudioSource>();
                    CollisionAudioSource.PlayOneShot(ImpactSound);
                }
            }
        }
    }
}
