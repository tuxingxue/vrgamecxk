using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI.Utility;

namespace EmeraldAI
{
    public class EmeraldAIEventsManager : MonoBehaviour
    {
        EmeraldAISystem EmeraldComponent;

        void Awake()
        {
            EmeraldComponent = GetComponent<EmeraldAISystem>();
        }

        /// <summary>
        /// Plays a sound clip according to the Clip parameter.
        /// </summary>
        public void PlaySoundClip(AudioClip Clip)
        {
            EmeraldComponent.m_AudioSource.volume = 1;
            EmeraldComponent.m_SecondaryAudioSource.volume = 1;
            if (!EmeraldComponent.m_AudioSource.isPlaying)
            {
                EmeraldComponent.m_AudioSource.PlayOneShot(Clip);
            }
            else
            {
                EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(Clip);
            }
        }

        /// <summary>
        /// Plays a random attack sound based on your AI's Attack Sounds list. Can also be called through Animation Events.
        /// </summary>
        public void PlayIdleSound()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.IdleVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.IdleVolume;
            if (EmeraldComponent.IdleSounds.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.IdleSounds[Random.Range(0, EmeraldComponent.IdleSounds.Count)]);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.IdleSounds[Random.Range(0, EmeraldComponent.IdleSounds.Count)]);
                }
            }
        }

        /// <summary>
        /// Plays a random attack sound based on your AI's Attack Sounds list. Can also be called through Animation Events.
        /// </summary>
        public void PlayAttackSound()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.AttackVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.AttackVolume;
            if (EmeraldComponent.AttackSounds.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.AttackSounds[Random.Range(0, EmeraldComponent.AttackSounds.Count)]);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.AttackSounds[Random.Range(0, EmeraldComponent.AttackSounds.Count)]);
                }
            }
        }

        /// <summary>
        /// Plays a random attack sound based on your AI's Attack Sounds list. Can also be called through Animation Events.
        /// </summary>
        public void PlayWarningSound()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.WarningVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.WarningVolume;
            if (EmeraldComponent.WarningSounds.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.WarningSounds[Random.Range(0, EmeraldComponent.WarningSounds.Count)]);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.WarningSounds[Random.Range(0, EmeraldComponent.WarningSounds.Count)]);
                }
            }
        }

        /// <summary>
        /// Plays a random impact sound based on your AI's Impact Sounds list.
        /// </summary>
        public void PlayImpactSound()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.ImpactVolume;
            if (EmeraldComponent.ImpactSounds.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.ImpactSounds[Random.Range(0, EmeraldComponent.ImpactSounds.Count)]);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.ImpactSounds[Random.Range(0, EmeraldComponent.ImpactSounds.Count)]);
                }
            }
        }

        /// <summary>
        /// Plays a random block sound based on your AI's Block Sounds list.
        /// </summary>
        public void PlayBlockSound()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.BlockVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.BlockVolume;
            if (EmeraldComponent.BlockingSounds.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.BlockingSounds[Random.Range(0, EmeraldComponent.BlockingSounds.Count)]);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.BlockingSounds[Random.Range(0, EmeraldComponent.BlockingSounds.Count)]);
                }
            }
        }

        /// <summary>
        /// Plays a random injured sound based on your AI's Injured Sounds list.
        /// </summary>
        public void PlayInjuredSound()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.InjuredVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.InjuredVolume;
            if (EmeraldComponent.InjuredSounds.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.InjuredSounds[Random.Range(0, EmeraldComponent.InjuredSounds.Count)]);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.InjuredSounds[Random.Range(0, EmeraldComponent.InjuredSounds.Count)]);
                }
            }
        }

        /// <summary>
        /// Plays a random attack sound based on your AI's Attack Sounds list. Can also be called through Animation Events.
        /// </summary>
        public void PlayDeathSound()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.DeathVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.DeathVolume;
            if (EmeraldComponent.DeathSounds.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.DeathSounds[Random.Range(0, EmeraldComponent.DeathSounds.Count)]);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.DeathSounds[Random.Range(0, EmeraldComponent.DeathSounds.Count)]);
                }
            }
        }

        /// <summary>
        /// Plays a footstep sound from the AI's Footstep Sounds list to use when the AI is walking. This should be setup through an Animation Event.
        /// </summary>
        public void WalkFootstepSound()
        {
            if (EmeraldComponent.AIAnimator.GetFloat("Speed") > 0.05f && EmeraldComponent.AIAnimator.GetFloat("Speed") <= 0.1f)
            {
                EmeraldComponent.m_AudioSource.volume = EmeraldComponent.WalkFootstepVolume;
                EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.WalkFootstepVolume;
                if (EmeraldComponent.FootStepSounds.Count > 0)
                {
                    if (!EmeraldComponent.m_AudioSource.isPlaying)
                    {
                        EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.FootStepSounds[Random.Range(0, EmeraldComponent.FootStepSounds.Count)]);
                    }
                    else
                    {
                        EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.FootStepSounds[Random.Range(0, EmeraldComponent.FootStepSounds.Count)]);
                    }
                }
            }
        }

        /// <summary>
        /// Plays a footstep sound from the AI's Footstep Sounds list to use when the AI is running. This should be setup through an Animation Event.
        /// </summary>
        public void RunFootstepSound()
        {
            if (EmeraldComponent.AIAnimator.GetFloat("Speed") > 0.1f)
            {
                EmeraldComponent.m_AudioSource.volume = EmeraldComponent.RunFootstepVolume;
                EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.RunFootstepVolume;
                if (EmeraldComponent.FootStepSounds.Count > 0)
                {
                    if (!EmeraldComponent.m_AudioSource.isPlaying)
                    {
                        EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.FootStepSounds[Random.Range(0, EmeraldComponent.FootStepSounds.Count)]);
                    }
                    else
                    {
                        EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.FootStepSounds[Random.Range(0, EmeraldComponent.FootStepSounds.Count)]);
                    }
                }
            }
        }

        /// <summary>
        /// Plays a random sound effect from the AI's General Sounds list.
        /// </summary>
        public void PlayRandomSoundEffect()
        {
            EmeraldComponent.m_AudioSource.volume = 1;
            EmeraldComponent.m_SecondaryAudioSource.volume = 1;
            EmeraldComponent.m_EventAudioSource.volume = 1;

            if (EmeraldComponent.InteractSoundList.Count > 0)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.InteractSoundList[Random.Range(0, EmeraldComponent.InteractSoundList.Count)].SoundEffectClip);
                }
                else if (!EmeraldComponent.m_SecondaryAudioSource.isPlaying)
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.InteractSoundList[Random.Range(0, EmeraldComponent.InteractSoundList.Count)].SoundEffectClip);
                }
                else
                {
                    EmeraldComponent.m_EventAudioSource.PlayOneShot(EmeraldComponent.InteractSoundList[Random.Range(0, EmeraldComponent.InteractSoundList.Count)].SoundEffectClip);
                }
            }
        }

        /// <summary>
        /// Plays a sound effect from the AI's General Sounds list using the Sound Effect ID as the parameter.
        /// </summary>
        public void PlaySoundEffect(int SoundEffectID)
        {
            EmeraldComponent.m_AudioSource.volume = 1;
            EmeraldComponent.m_SecondaryAudioSource.volume = 1;
            EmeraldComponent.m_EventAudioSource.volume = 1;

            if (EmeraldComponent.InteractSoundList.Count > 0)
            {
                for (int i = 0; i < EmeraldComponent.InteractSoundList.Count; i++)
                {
                    if (EmeraldComponent.InteractSoundList[i].SoundEffectID == SoundEffectID)
                    {
                        if (!EmeraldComponent.m_AudioSource.isPlaying)
                        {
                            EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.InteractSoundList[i].SoundEffectClip);
                        }
                        else if (!EmeraldComponent.m_SecondaryAudioSource.isPlaying)
                        {
                            EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.InteractSoundList[i].SoundEffectClip);
                        }
                        else
                        {
                            EmeraldComponent.m_EventAudioSource.PlayOneShot(EmeraldComponent.InteractSoundList[i].SoundEffectClip);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Enables an item from your AI's Item list using the Item ID.
        /// </summary>
        public void EnableItem(int ItemID)
        {
            //Look through each item in the ItemList for the appropriate ID.
            //Once found, enable the item of the same index as the found ID.
            for (int i = 0; i < EmeraldComponent.ItemList.Count; i++)
            {
                if (EmeraldComponent.ItemList[i].ItemID == ItemID)
                {
                    EmeraldComponent.ItemList[i].ItemObject.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Disables an item from your AI's Item list using the Item ID.
        /// </summary>
        public void DisableItem(int ItemID)
        {
            //Look through each item in the ItemList for the appropriate ID.
            //Once found, enable the item of the same index as the found ID.
            for (int i = 0; i < EmeraldComponent.ItemList.Count; i++)
            {
                if (EmeraldComponent.ItemList[i].ItemID == ItemID)
                {
                    EmeraldComponent.ItemList[i].ItemObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Disables all items from your AI's Item list.
        /// </summary>
        public void DisableAllItems()
        {
            //Disable all of an AI's items
            for (int i = 0; i < EmeraldComponent.ItemList.Count; i++)
            {
                if (EmeraldComponent.ItemList[i].ItemObject != null)
                {
                    EmeraldComponent.ItemList[i].ItemObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Enables the AI's weapon object and plays the AI's equip sound effect, if one is applied.
        /// </summary>
        public void EnableWeapon()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.EquipVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.EquipVolume;
            if (EmeraldComponent.UnsheatheWeapon != null)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.UnsheatheWeapon);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.UnsheatheWeapon);
                }
            }
            EmeraldComponent.WeaponObject.SetActive(true);
        }

        /// <summary>
        /// Disables the AI's weapon object and plays the AI's unequip sound effect, if one is applied.
        /// </summary>
        public void DisableWeapon()
        {
            EmeraldComponent.m_AudioSource.volume = EmeraldComponent.UnequipVolume;
            EmeraldComponent.m_SecondaryAudioSource.volume = EmeraldComponent.UnequipVolume;
            if (EmeraldComponent.SheatheWeapon != null)
            {
                if (!EmeraldComponent.m_AudioSource.isPlaying)
                {
                    EmeraldComponent.m_AudioSource.PlayOneShot(EmeraldComponent.SheatheWeapon);
                }
                else
                {
                    EmeraldComponent.m_SecondaryAudioSource.PlayOneShot(EmeraldComponent.SheatheWeapon);
                }
            }
            EmeraldComponent.WeaponObject.SetActive(false);
        }

        /// <summary>
        /// Plays an emote animation according to the Animation Clip parameter. Note: This function will only work if
        /// an AI is not in active combat mode.
        /// </summary>
        public void PlayEmoteAnimation(int EmoteAnimationID)
        {
            //Look through each animation in the EmoteAnimationList for the appropriate ID.
            //Once found, play the animaition of the same index as the found ID.
            for (int i = 0; i < EmeraldComponent.EmoteAnimationList.Count; i++)
            {
                if (EmeraldComponent.EmoteAnimationList[i].AnimationID == EmoteAnimationID)
                {
                    if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                    {
                        EmeraldComponent.AIAnimator.SetInteger("Emote Index", EmoteAnimationID);
                        EmeraldComponent.AIAnimator.SetTrigger("Emote Trigger");
                        EmeraldComponent.IsMoving = false;
                    }
                }
            }
        }

        /// <summary>
        /// Loops an emote animation according to the Animation Clip parameter until it is called to stop. Note: This function will only work if
        /// an AI is not in active combat mode.
        /// </summary>
        public void LoopEmoteAnimation(int EmoteAnimationID)
        {
            //Look through each animation in the EmoteAnimationList for the appropriate ID.
            //Once found, play the animaition of the same index as the found ID.
            for (int i = 0; i < EmeraldComponent.EmoteAnimationList.Count; i++)
            {
                if (EmeraldComponent.EmoteAnimationList[i].AnimationID == EmoteAnimationID)
                {
                    if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                    {
                        EmeraldComponent.AIAnimator.SetInteger("Emote Index", EmoteAnimationID);
                        EmeraldComponent.AIAnimator.SetBool("Emote Loop", true);
                        EmeraldComponent.IsMoving = false;
                    }
                }
            }
        }

        //2.2 doesn't need a paramter fix
        /// <summary>
        /// Loops an emote animation according to the Animation Clip parameter until it is called to stop. Note: This function will only work if
        /// an AI is not in active combat mode.
        /// </summary>
        public void StopLoopEmoteAnimation(int EmoteAnimationID)
        {
            //Look through each animation in the EmoteAnimationList for the appropriate ID.
            //Once found, play the animaition of the same index as the found ID.
            for (int i = 0; i < EmeraldComponent.EmoteAnimationList.Count; i++)
            {
                if (EmeraldComponent.EmoteAnimationList[i].AnimationID == EmoteAnimationID)
                {
                    if (EmeraldComponent.CombatStateRef == EmeraldAISystem.CombatState.NotActive)
                    {
                        EmeraldComponent.AIAnimator.SetInteger("Emote Index", EmoteAnimationID);
                        EmeraldComponent.AIAnimator.SetBool("Emote Loop", false);
                        EmeraldComponent.IsMoving = false;
                    }
                }
            }
        }

        /// <summary>
        /// Spawns an additional effect object at the position of the AI's Blood Spawn Offset position.
        /// </summary>
        public void SpawnAdditionalEffect(GameObject EffectObject)
        {
            GameObject Effect = EmeraldAIObjectPool.Spawn(EffectObject, transform.position + EmeraldComponent.BloodPosOffset, Quaternion.identity);
            Effect.transform.SetParent(EmeraldAISystem.ObjectPool.transform);

            if (Effect.GetComponent<EmeraldAIProjectileTimeout>() == null)
            {
                Effect.AddComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = 3;
            }
        }

        /// <summary>
        /// Spawns an effect object at the position of the AI's target.
        /// </summary>
        public void SpawnEffectOnTarget(GameObject EffectObject)
        {
            if (EmeraldComponent.CurrentTarget != null)
            {
                GameObject Effect = EmeraldAIObjectPool.Spawn(EffectObject, new Vector3(EmeraldComponent.CurrentTarget.position.x, 
                    EmeraldComponent.CurrentTarget.position.y + EmeraldComponent.CurrentTarget.localScale.y/2, EmeraldComponent.CurrentTarget.position.z), Quaternion.identity);
                Effect.transform.SetParent(EmeraldAISystem.ObjectPool.transform);

                if (Effect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                {
                    Effect.AddComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = 2;
                }
            }
        }

        /// <summary>
        /// Spawns a blood splat effect object at the position of the AI's Blood Spawn Offset position. 
        /// The rotation of this object is then randomized and adjusted based off of your attacker's current location.
        /// </summary>
        public void SpawnBloodSplatEffect(GameObject BloodSplatObject)
        {
            if (EmeraldComponent.CurrentTarget != null)
            {
                GameObject Effect = EmeraldAIObjectPool.Spawn(BloodSplatObject, transform.position + EmeraldComponent.BloodPosOffset, Quaternion.Euler(Random.Range(110, 160), 
                    EmeraldComponent.CurrentTarget.localEulerAngles.y - Random.Range(120, 240), Random.Range(0, 360)));
                Effect.transform.SetParent(EmeraldAISystem.ObjectPool.transform);

                if (Effect.GetComponent<EmeraldAIProjectileTimeout>() == null)
                {
                    Effect.AddComponent<EmeraldAIProjectileTimeout>().TimeoutSeconds = 2;
                }
            }
        }

        /// <summary>
        /// Instantly kills this AI
        /// </summary>
        public void KillAI()
        {
            EmeraldComponent.Damage(9999999, EmeraldAISystem.TargetType.AI);
        }

        /// <summary>
        /// Manually sets the AI's next Idle animation instead of being generated randomly. This is useful for functionality such as playing a particular idle animation
        /// at certain location such as for an AI's schedule. Note: The animation numbers are from 1 to 3 and must exist in your AI's Idle Animation list. You must call 
        /// DisableOverrideIdleAnimation() to have idle animations randomly generate again and to disable this feature.
        /// </summary>
        public void OverrideIdleAnimation(int IdleIndex)
        {
            EmeraldComponent.m_IdleAnimaionIndexOverride = true;
            EmeraldComponent.AIAnimator.SetInteger("Idle Index", IdleIndex+1);
        }

        /// <summary>
        /// Disables the OverrideIdleAnimation feature.
        /// </summary>
        public void DisableOverrideIdleAnimation()
        {
            EmeraldComponent.m_IdleAnimaionIndexOverride = false;
        }

        /// <summary>
        /// Changes the AI's behavior
        /// </summary>
        public void ChangeBehavior(EmeraldAISystem.CurrentBehavior NewBehavior)
        {
            EmeraldComponent.BehaviorRef = NewBehavior;
            EmeraldComponent.StartingBehaviorRef = (int)EmeraldComponent.BehaviorRef;
        }

        /// <summary>
        /// Changes the AI's behavior
        /// </summary>
        public void ChangeConfidence(EmeraldAISystem.ConfidenceType NewConfidence)
        {
            EmeraldComponent.ConfidenceRef = NewConfidence;
            EmeraldComponent.StartingConfidenceRef = (int)EmeraldComponent.ConfidenceRef;
        }

        /// <summary>
        /// Changes the AI's behavior
        /// </summary>
        public void ChangeWanderType(EmeraldAISystem.WanderType NewWanderType)
        {
            EmeraldComponent.WanderTypeRef = NewWanderType;
        }

        /// <summary>
        /// Instantiates an AI's Droppable Weapon Object on death. 
        /// </summary>
        public void CreateDroppableWeapon()
        {
            if (EmeraldComponent.UseDroppableWeapon == EmeraldAISystem.YesOrNo.Yes && EmeraldComponent.DroppableWeaponObject != null)
            {
                if (EmeraldComponent.WeaponObject.activeSelf)
                {
                    Instantiate(EmeraldComponent.DroppableWeaponObject, EmeraldComponent.WeaponObject.transform.position, EmeraldComponent.WeaponObject.transform.rotation);
                    EmeraldComponent.WeaponObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Assigns a new combat target for your AI to attack. Using this setting will override your AI's chance limit.
        /// </summary>
        public void SetCombatTarget(Transform Target)
        {
            if (EmeraldComponent.ConfidenceRef != EmeraldAISystem.ConfidenceType.Coward)
            {
                EmeraldComponent.CurrentTarget = Target;
                EmeraldComponent.m_NavMeshAgent.stoppingDistance = EmeraldComponent.AttackDistance;
                EmeraldComponent.m_NavMeshAgent.destination = Target.position;
                EmeraldComponent.EmeraldDetectionComponent.PreviousTarget = Target;
                EmeraldComponent.CombatStateRef = EmeraldAISystem.CombatState.Active;
                EmeraldComponent.AIAnimator.SetBool("Idle Active", false);
                EmeraldComponent.AIAnimator.SetBool("Combat State Active", true);
                EmeraldComponent.MaxChaseDistance = 2000;
            }
        }

        /// <summary>
        /// Assigns a new follow target for your companion AI to follow.
        /// </summary>
        public void SetFollowerTarget(Transform Target)
        {
            EmeraldComponent.CurrentFollowTarget = Target;
            EmeraldComponent.CurrentMovementState = EmeraldAISystem.MovementState.Run;
            EmeraldComponent.UseAIAvoidance = EmeraldAISystem.YesOrNo.No;
        }

        /// <summary>
        /// Tames the AI to become the Target's companion. Note: The tameable AI must have a Cautious Behavior Type and 
        /// a Brave or Foolhardy Confidence Type. The AI must be tamed before the AI turns Aggressive to be successful.
        /// </summary>
        public void TameAI(Transform Target)
        {
            if (EmeraldComponent.BehaviorRef == EmeraldAISystem.CurrentBehavior.Cautious)
            {
                if (EmeraldComponent.ConfidenceRef == EmeraldAISystem.ConfidenceType.Brave ||
                    EmeraldComponent.ConfidenceRef == EmeraldAISystem.ConfidenceType.Foolhardy)
                {
                    EmeraldComponent.CurrentTarget = null;
                    EmeraldComponent.CombatStateRef = EmeraldAISystem.CombatState.NotActive;
                    EmeraldComponent.BehaviorRef = EmeraldAISystem.CurrentBehavior.Companion;
                    EmeraldComponent.StartingBehaviorRef = (int)EmeraldComponent.BehaviorRef;
                    EmeraldComponent.CurrentMovementState = EmeraldAISystem.MovementState.Run;
                    EmeraldComponent.StartingMovementState = EmeraldAISystem.MovementState.Run;
                    EmeraldComponent.UseAIAvoidance = EmeraldAISystem.YesOrNo.No;
                    EmeraldComponent.CurrentFollowTarget = Target;
                }
            }
        }

        /// <summary>
        /// Updates the AI's Health Bar color
        /// </summary>
        public void UpdateUIHealthBarColor(Color NewColor)
        {
            if (EmeraldComponent.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.Yes)
            {
                GameObject HealthBarChild = EmeraldComponent.HealthBar.transform.Find("AI Health Bar Background").gameObject;
                UnityEngine.UI.Image HealthBarRef = HealthBarChild.transform.Find("AI Health Bar").GetComponent<UnityEngine.UI.Image>();
                HealthBarRef.color = NewColor;
                UnityEngine.UI.Image HealthBarBackgroundImageRef = HealthBarChild.GetComponent<UnityEngine.UI.Image>();
                HealthBarBackgroundImageRef.color = EmeraldComponent.HealthBarBackgroundColor;
            }
        }

        /// <summary>
        /// Updates the AI's Health Bar Background color
        /// </summary>
        public void UpdateUIHealthBarBackgroundColor(Color NewColor)
        {
            if (EmeraldComponent.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.Yes)
            {
                GameObject HealthBarChild = EmeraldComponent.HealthBar.transform.Find("AI Health Bar Background").gameObject;
                UnityEngine.UI.Image HealthBarBackgroundImageRef = HealthBarChild.GetComponent<UnityEngine.UI.Image>();
                HealthBarBackgroundImageRef.color = NewColor;
            }
        }

        /// <summary>
        /// Updates the AI's Name color
        /// </summary>
        public void UpdateUINameColor(Color NewColor)
        {
            if (EmeraldComponent.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.Yes && EmeraldComponent.DisplayAINameRef == EmeraldAISystem.DisplayAIName.Yes)
            {
                EmeraldComponent.TextName.color = NewColor;
            }
        }

        /// <summary>
        /// Updates the AI's Name text
        /// </summary>
        public void UpdateUINameText(string NewName)
        {
            if (EmeraldComponent.CreateHealthBarsRef == EmeraldAISystem.CreateHealthBars.Yes && EmeraldComponent.DisplayAINameRef == EmeraldAISystem.DisplayAIName.Yes)
            {
                EmeraldComponent.TextName.text = NewName;
            }
        }


        /// <summary>
        /// Updates the AI's dynamic wandering position to the AI's current positon.
        /// </summary>
        public void UpdateDynamicWanderPosition()
        {
            EmeraldComponent.StartingDestination = this.transform.position;
        }

        /// <summary>
        /// Sets the AI's dynamic wandering position to the position of the Destination transform. 
        /// This is useful for functionality such as custom AI schedules. Note: This will automatically change
        /// your AI's Wander Type to Dynamic.
        /// </summary>
        public void SetDynamicWanderPosition(Transform Destination)
        {
            ChangeWanderType(EmeraldAISystem.WanderType.Dynamic);
            EmeraldComponent.StartingDestination = Destination.position;
        }

        /// <summary>
        /// Updates the AI's starting position to the AI's current position.
        /// </summary>
        public void UpdateStartingPosition()
        {
            EmeraldComponent.StartingDestination = this.transform.position;
        }

        /// <summary>
        /// Sets the AI's destination to the transform's position.
        /// </summary>
        public void SetDestination(Transform Destination)
        {
            EmeraldComponent.AIReachedDestination = false;
            EmeraldComponent.m_NavMeshAgent.destination = Destination.position;
            EmeraldComponent.SingleDestination = Destination.position;
            EmeraldComponent.AIAnimator.SetBool("Idle Active", false);
            EmeraldComponent.StartingDestination = Destination.position;
        }

        /// <summary>
        /// Sets the AI's destination to the position of the Vector3 position.
        /// </summary>
        public void SetDestinationPosition(Vector3 DestinationPosition)
        {
            EmeraldComponent.AIReachedDestination = false;
            EmeraldComponent.m_NavMeshAgent.destination = DestinationPosition;
            EmeraldComponent.SingleDestination = DestinationPosition;
            EmeraldComponent.AIAnimator.SetBool("Idle Active", false);
            EmeraldComponent.StartingDestination = DestinationPosition;
        }

        /// <summary>
        /// Refills the AI's health to full instantly
        /// </summary>
        public void InstantlyRefillAIHeath ()
        {
            EmeraldComponent.CurrentHealth = EmeraldComponent.StartingHealth;
        }

        /// <summary>
        /// Stops an AI from moving. This is useful for functionality like dialogue.
        /// </summary>
        public void StopMovement()
        {
            EmeraldComponent.m_NavMeshAgent.isStopped = true;
        }

        /// <summary>
        /// Resumes an AI's movement after using the StopMovement function.
        /// </summary>
        public void ResumeMovement()
        {
            EmeraldComponent.m_NavMeshAgent.isStopped = false;
        }

        /// <summary>
        /// Stops a Companion AI from moving.
        /// </summary>
        public void StopFollowing()
        {
            EmeraldComponent.m_NavMeshAgent.isStopped = true;
        }

        /// <summary>
        /// Allows a Companion AI to resume following its follower.
        /// </summary>
        public void ResumeFollowing()
        {
            EmeraldComponent.m_NavMeshAgent.isStopped = false;
        }

        /// <summary>
        /// Allows a Companion AI to guard the assigned position.
        /// </summary>
        public void CompanionGuardPosition(Vector3 PositionToGuard)
        {
            Transform TempFollower = new GameObject(EmeraldComponent.AIName+"'s position to guard").transform;
            TempFollower.position = PositionToGuard;
            SetFollowerTarget(TempFollower);
        }

        /// <summary>
        /// Resets an AI to its default state. This is useful if an AI is being respawned. 
        /// </summary>
        public void ResetAI()
        {
            //Re-enable all of the AI's components.
            EmeraldComponent.EmeraldInitializerComponent.DisableRagdoll();
            EmeraldComponent.EmeraldInitializerComponent.enabled = true;
            EmeraldComponent.EmeraldEventsManagerComponent.enabled = true;
            EmeraldComponent.EmeraldDetectionComponent.enabled = true;
            EmeraldComponent.CurrentHealth = EmeraldComponent.StartingHealth;
            gameObject.tag = EmeraldComponent.StartingTag;
            gameObject.layer = EmeraldComponent.StartingLayer;
            EmeraldComponent.IsDead = false;
            EmeraldComponent.AIBoxCollider.enabled = true;
            EmeraldComponent.AICollider.enabled = true;
            EmeraldComponent.AIAnimator.enabled = true;
            EmeraldComponent.m_NavMeshAgent.enabled = true;
            EmeraldComponent.StartingDestination = transform.position;
            EmeraldComponent.enabled = true;
            EmeraldComponent.EmeraldBehaviorsComponent.DefaultState();
            EmeraldComponent.OnEnabledEvent.Invoke();
            EmeraldComponent.AIAnimator.Rebind();

            //Reapply the AI's Animator Controller settings applied on Start because, when the
            //Animator Controller is disabled, they're reset to their default settings. 
            if (EmeraldComponent.UseEquipAnimation == EmeraldAISystem.YesOrNo.Yes)
            {
                EmeraldComponent.AIAnimator.SetBool("Animate Weapon State", true);
            }
            else if (EmeraldComponent.UseEquipAnimation == EmeraldAISystem.YesOrNo.No
                || EmeraldComponent.PutAwayWeaponAnimation == null
                || EmeraldComponent.PullOutWeaponAnimation == null)
            {
                EmeraldComponent.AIAnimator.SetBool("Animate Weapon State", false);
            }

            if (EmeraldComponent.UseHitAnimations == EmeraldAISystem.YesOrNo.Yes)
            {
                EmeraldComponent.AIAnimator.SetBool("Use Hit", true);
            }
            else
            {
                EmeraldComponent.AIAnimator.SetBool("Use Hit", false);
            }

            if (EmeraldComponent.ReverseWalkAnimation)
            {
                EmeraldComponent.AIAnimator.SetFloat("Backup Speed", -1);
            }
            else
            {
                EmeraldComponent.AIAnimator.SetFloat("Backup Speed", 1);
            }

            EmeraldComponent.AIAnimator.SetBool("Idle Active", false);
        }
    }
}
