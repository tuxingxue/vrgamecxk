using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {

    public GameObject PF_OnHit;
    public bool isPlayer = false;
    public AudioClip[] Gun_Ricochets;
    
    // Use this for initialization
    void Start ()
    {
        Destroy(gameObject, 3.0f);
        Invoke("TurnOnCollider",0.1f);
	}

    void TurnOnCollider()
    {
        CancelInvoke("TurnOnCollider");
        GetComponent<Collider>().enabled = true;
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void PlayHitSound()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.clip = Gun_Ricochets[Random.Range(0, Gun_Ricochets.Length)];
        audio.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BHIT : " + collision.gameObject.name);

        if ( isPlayer && collision.gameObject.tag == "Enemy" )
        {
            //collision.gameObject.GetComponent<EnemyLogic>().GetHit();

            Debug.Log(collision.gameObject.name);
        }

        if (isPlayer && PF_OnHit != null)
        {
           GameObject iHit = Instantiate(PF_OnHit, transform.position, Quaternion.identity) as GameObject;

           Destroy(iHit, 2f);
        }

        if ( !isPlayer && collision.gameObject.tag != "Enemy_Drone")
        {
            Destroy(gameObject);
        }
    }
}
