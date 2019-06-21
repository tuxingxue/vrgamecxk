using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTigger : MonoBehaviour
{
    public int damage = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (inFlight)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            float rbSpeed = rb.velocity.sqrMagnitude;
            //bool canStick = (targetPhysMaterial != null && collision.collider.sharedMaterial == targetPhysMaterial && rbSpeed > 0.2f);
            //bool hitBalloon = collision.collider.gameObject.GetComponent<Balloon>() != null;

            bool hitAnimal = collision.collider.gameObject.tag == "Animal";
            /*if (travelledFrames < 2 && !canStick)
            {
                // Reset transform but halve your velocity
                transform.position = prevPosition - prevVelocity * Time.deltaTime;
                transform.rotation = prevRotation;

                Vector3 reflfectDir = Vector3.Reflect(arrowHeadRB.velocity, collision.contacts[0].normal);
                arrowHeadRB.velocity = reflfectDir * 0.25f;
                shaftRB.velocity = reflfectDir * 0.25f;

                travelledFrames = 0;
                return;
            }*/
            /*
            if (glintParticle != null)
            {
                glintParticle.Stop(true);
            }
            */
            // Only play hit sounds if we're moving quickly
            /*
            if (rbSpeed > 0.1f)
            {
                hitGroundSound.Play();
            }
            */
            /*
            FireSource arrowFire = gameObject.GetComponentInChildren<FireSource>();
            FireSource fireSourceOnTarget = collision.collider.GetComponentInParent<FireSource>();
            */
            /*
            if (arrowFire != null && arrowFire.isBurning && (fireSourceOnTarget != null))
            {
                if (!hasSpreadFire)
                {
                    collision.collider.gameObject.SendMessageUpwards("FireExposure", gameObject, SendMessageOptions.DontRequireReceiver);
                    hasSpreadFire = true;
                }
            }
            else
            {
                // Only count collisions with good speed so that arrows on the ground can't deal damage
                // always pop balloons
                if (rbSpeed > 0.1f || hitBalloon)
                {
                    collision.collider.gameObject.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);
                    gameObject.SendMessage("HasAppliedDamage", SendMessageOptions.DontRequireReceiver);
                }
            }

            if (hitBalloon)
            {
                // Revert my physics properties cause I don't want balloons to influence my travel
                transform.position = prevPosition;
                transform.rotation = prevRotation;
                arrowHeadRB.velocity = prevVelocity;
                Physics.IgnoreCollision(arrowHeadRB.GetComponent<Collider>(), collision.collider);
                Physics.IgnoreCollision(shaftRB.GetComponent<Collider>(), collision.collider);
            }
            */
            if (hitAnimal)
            {
                //TODO: 箭吸附到动物身上
                collision.collider.gameObject.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(damage, EmeraldAI.EmeraldAISystem.TargetType.Player, transform.root, 200);
                //collision.collider.gameObject.SetActive(false);
                //collision.collider.gameObject.GetComponent<NewBahaviorScript>();
            }
            /*
            if (canStick)
            {
                StickInTarget(collision, travelledFrames < 2);
            }
            */
            // Player Collision Check (self hit)
            /*
            if (Player.instance && collision.collider == Player.instance.headCollider)
            {
                Player.instance.PlayerShotSelf();
            }
            */
        }
    }
}
