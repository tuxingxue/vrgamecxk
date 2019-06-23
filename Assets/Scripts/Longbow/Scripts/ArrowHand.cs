﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: The object attached to the player's hand that spawns and fires the
//			arrow
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class ArrowHand : MonoBehaviour
	{
		private Hand hand;
		private Longbow bow;

		private GameObject currentArrow;
		public GameObject arrowPrefab;

		public Transform arrowNockTransform;

		public float nockDistance = 0.1f;
		public float lerpCompleteDistance = 0.08f;
		public float rotationLerpThreshold = 0.15f;
		public float positionLerpThreshold = 0.15f;

		private bool allowArrowSpawn = true;
		private bool nocked;
        private GrabTypes nockedWithType = GrabTypes.None;

		private bool inNockRange = false;
		private bool arrowLerpComplete = false;

		public SoundPlayOneshot arrowSpawnSound;

		private AllowTeleportWhileAttachedToHand allowTeleport = null;
        public Texture2D texture;
        public int maxArrowCount = 100;
		private List<GameObject> arrowList;
        private Vector3 hitPoint;
        public float firetime = 1f;
        private bool fired;

        public int gunstatus = 0;
        public GameObject currentGun;
        public GameObject BtnTrigger;
        public GameObject BtnTrigger2;
        public GameObject BtnTrigger3;

        public int maximummove;
        public LayerMask teleportmask, teleportmask1;
        public GameObject shooting;
        public SteamVR_Input_Sources handType; // 1
        public SteamVR_Action_Boolean teleportAction; // 2
        public SteamVR_Action_Boolean grabAction; // 3

        public GameObject holePrefab;

        public GameObject rightback;
        public bool GetTeleportDown() // 1
        {
            return teleportAction.GetStateDown(handType);
        }
        public bool GetTeleportUp()
        {
            return teleportAction.GetStateUp(handType);
        }
        public bool GetGrab() // 2
        {
            return grabAction.GetStateDown(handType);
        }
        //-------------------------------------------------
        void Awake()
		{
			allowTeleport = GetComponent<AllowTeleportWhileAttachedToHand>();
			//allowTeleport.teleportAllowed = true;
			allowTeleport.overrideHoverLock = false;

			arrowList = new List<GameObject>();
		}


		//-------------------------------------------------
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand;
			FindBow();
		}


		//-------------------------------------------------
		private GameObject InstantiateArrow()
		{
			GameObject arrow = Instantiate( arrowPrefab, arrowNockTransform.position, arrowNockTransform.rotation ) as GameObject;
			arrow.name = "Bow Arrow";
			arrow.transform.parent = arrowNockTransform;
			Util.ResetTransform( arrow.transform );

			arrowList.Add( arrow );

			while ( arrowList.Count > maxArrowCount )
			{
				GameObject oldArrow = arrowList[0];
				arrowList.RemoveAt( 0 );
				if ( oldArrow )
				{
					Destroy( oldArrow );
				}
			}

			return arrow;
		}


		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
			if ( bow == null )
			{
				FindBow();
			}

			if ( bow == null )
			{
				return;
			}

            if(GetGrab())
            {
                gunstatus ^= 1;
                if(gunstatus == 1)
                {
                    currentArrow.SetActive(false);
                    currentGun.SetActive(true);
                }
                else
                {
                    currentArrow.SetActive(true);
                    currentGun.SetActive(false);
                }
            }
            addshoot();
            if (gunstatus == 1)
            {
                
                /*RaycastHit hit;
                Vector3 posh= transform.GetChild(0).position;
                if (Physics.Raycast(posh, transform.GetChild(1).position - posh, out hit, maximummove, teleportmask))
                {
                    hitPoint = hit.point;
                    shooting.SetActive(true);
                    shooting.transform.position = hitPoint;
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(hitPoint);

                }
                else
                {
                    shooting.SetActive(false);
                }*/

                if (GetTeleportDown())
                {
                    BtnTrigger.transform.localPosition = BtnTrigger2.transform.localPosition;
                    BtnTrigger.transform.eulerAngles = BtnTrigger2.transform.eulerAngles;
                }
                if(GetTeleportDown()&& !fired)
                {
                    createhole();
                    GetComponentInChildren<SimpleShoot>().Shoot();
                    hand.TriggerHapticPulse(0.05f,100f,30f);
                    fired = true;
                    gameObject.GetComponent<Animation>().Play();
                    //transform.parent.parent.Translate(new Vector3(0, 0.1f, 0));
                    StartCoroutine(killshoot());
                }
                if (GetTeleportUp())
                {
                    BtnTrigger.transform.localPosition = BtnTrigger3.transform.localPosition;
                    BtnTrigger.transform.eulerAngles = BtnTrigger3.transform.eulerAngles;
                }
            }
            else
            {
                if (allowArrowSpawn && (currentArrow == null)) // If we're allowed to have an active arrow in hand but don't yet, spawn one
                {
                    currentArrow = InstantiateArrow();
                    arrowSpawnSound.Play();
                }

                float distanceToNockPosition = Vector3.Distance(transform.parent.position, bow.nockTransform.position);

                // If there's an arrow spawned in the hand and it's not nocked yet
                if (!nocked)
                {
                    // If we're close enough to nock position that we want to start arrow rotation lerp, do so
                    if (distanceToNockPosition < rotationLerpThreshold)
                    {
                        float lerp = Util.RemapNumber(distanceToNockPosition, rotationLerpThreshold, lerpCompleteDistance, 0, 1);

                        arrowNockTransform.rotation = Quaternion.Lerp(arrowNockTransform.parent.rotation, bow.nockRestTransform.rotation, lerp);
                    }
                    else // Not close enough for rotation lerp, reset rotation
                    {
                        arrowNockTransform.localRotation = Quaternion.identity;
                    }

                    // If we're close enough to the nock position that we want to start arrow position lerp, do so
                    if (distanceToNockPosition < positionLerpThreshold)
                    {
                        float posLerp = Util.RemapNumber(distanceToNockPosition, positionLerpThreshold, lerpCompleteDistance, 0, 1);

                        posLerp = Mathf.Clamp(posLerp, 0f, 1f);

                        arrowNockTransform.position = Vector3.Lerp(arrowNockTransform.parent.position, bow.nockRestTransform.position, posLerp);
                    }
                    else // Not close enough for position lerp, reset position
                    {
                        arrowNockTransform.position = arrowNockTransform.parent.position;
                    }


                    // Give a haptic tick when lerp is visually complete
                    if (distanceToNockPosition < lerpCompleteDistance)
                    {
                        if (!arrowLerpComplete)
                        {
                            arrowLerpComplete = true;
                            hand.TriggerHapticPulse(500);
                        }
                    }
                    else
                    {
                        if (arrowLerpComplete)
                        {
                            arrowLerpComplete = false;
                        }
                    }

                    // Allow nocking the arrow when controller is close enough
                    if (distanceToNockPosition < nockDistance)
                    {
                        if (!inNockRange)
                        {
                            inNockRange = true;
                            bow.ArrowInPosition();
                        }
                    }
                    else
                    {
                        if (inNockRange)
                        {
                            inNockRange = false;
                        }
                    }

                    GrabTypes bestGrab = hand.GetBestGrabbingType(GrabTypes.Pinch, true);

                    // If arrow is close enough to the nock position and we're pressing the trigger, and we're not nocked yet, Nock
                    if ((distanceToNockPosition < nockDistance) && bestGrab != GrabTypes.None && !nocked)
                    {
                        if (currentArrow == null)
                        {
                            currentArrow = InstantiateArrow();
                        }

                        nocked = true;
                        nockedWithType = bestGrab;
                        bow.StartNock(this);
                        hand.HoverLock(GetComponent<Interactable>());
                        allowTeleport.teleportAllowed = false;
                        currentArrow.transform.parent = bow.nockTransform;
                        Util.ResetTransform(currentArrow.transform);
                        Util.ResetTransform(arrowNockTransform);
                    }
                }
            }


			// If arrow is nocked, and we release the trigger
			if ( nocked && hand.IsGrabbingWithType(nockedWithType) == false )
			{
				if ( bow.pulled ) // If bow is pulled back far enough, fire arrow, otherwise reset arrow in arrowhand
				{
					FireArrow();
				}
				else
				{
					arrowNockTransform.rotation = currentArrow.transform.rotation;
					currentArrow.transform.parent = arrowNockTransform;
					Util.ResetTransform( currentArrow.transform );
					nocked = false;
                    nockedWithType = GrabTypes.None;
					bow.ReleaseNock();
					hand.HoverUnlock( GetComponent<Interactable>() );
					allowTeleport.teleportAllowed = true;
				}

				bow.StartRotationLerp(); // Arrow is releasing from the bow, tell the bow to lerp back to controller rotation
			}
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}


		//-------------------------------------------------
		private void FireArrow()
		{
			currentArrow.transform.parent = null;

			Arrow arrow = currentArrow.GetComponent<Arrow>();
			arrow.shaftRB.isKinematic = false;
			arrow.shaftRB.useGravity = true;
			arrow.shaftRB.transform.GetComponent<BoxCollider>().enabled = true;

			arrow.arrowHeadRB.isKinematic = false;
			arrow.arrowHeadRB.useGravity = true;
			arrow.arrowHeadRB.transform.GetComponent<BoxCollider>().enabled = true;

			arrow.arrowHeadRB.AddForce( currentArrow.transform.forward * bow.GetArrowVelocity(), ForceMode.VelocityChange );
			arrow.arrowHeadRB.AddTorque( currentArrow.transform.forward * 10 );

			nocked = false;
            nockedWithType = GrabTypes.None;

			currentArrow.GetComponent<Arrow>().ArrowReleased( bow.GetArrowVelocity() );
			bow.ArrowReleased();

			allowArrowSpawn = false;
			Invoke( "EnableArrowSpawn", 0.5f );
			StartCoroutine( ArrowReleaseHaptics() );

			currentArrow = null;
			allowTeleport.teleportAllowed = true;
		}


		//-------------------------------------------------
		private void EnableArrowSpawn()
		{
			allowArrowSpawn = true;
		}

        

        void createhole()
        {
            RaycastHit hit;
            Vector3 posh = transform.GetChild(0).position;
            if (Physics.Raycast(posh, transform.GetChild(1).position - posh, out hit, maximummove, teleportmask1))
            {
                GameObject tmpHole = Instantiate(holePrefab, hit.point, Quaternion.identity);
                tmpHole.transform.LookAt(hit.point - hit.normal);
                tmpHole.transform.Translate(Vector3.back * 0.01f);
            }
        }

        void addshoot()
        {
            if (gunstatus == 1)
            {
                RaycastHit hit;
                Vector3 posh = transform.GetChild(0).position;
                if (Physics.Raycast(posh, transform.GetChild(1).position - posh, out hit, maximummove, teleportmask))
                {
                    //print(hit.transform.gameObject);
                    hitPoint = hit.point;
                    Vector3 cantran = transform.parent.parent.parent.GetChild(2).position;
                    Vector3 shootdir = hitPoint - cantran;
                    shooting.SetActive(true);
                    float fm = Mathf.Pow(shootdir.magnitude,0.7f)*0.02f;
                    shooting.transform.localScale = new Vector3(fm, fm, fm);
                    if(shootdir.magnitude>2)
                    {
                        shootdir = shootdir * 1f / shootdir.magnitude;
                    }
                    else
                    {
                        shootdir = shootdir / 2f;
                    }
                    
                    
                    shooting.transform.position = hitPoint - shootdir + new Vector3(0,0.5f*fm,0);
                }
                
            }
            else
            {
                shooting.SetActive(false);
            }
                
          
        }
        //-------------------------------------------------
        private IEnumerator ArrowReleaseHaptics()
		{
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.TriggerHapticPulse( 1500 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.TriggerHapticPulse( 800 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.TriggerHapticPulse( 500 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.TriggerHapticPulse( 300 );
		}


		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}


		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
			gameObject.SetActive( true );
		}


		//-------------------------------------------------
		private void FindBow()
		{
			bow = hand.otherHand.GetComponentInChildren<Longbow>();
		}

        private IEnumerator killshoot()
        {
            yield return new WaitForSeconds(firetime);
            fired = false;
        }
    }
}
