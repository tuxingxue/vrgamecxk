using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour 
{
	public AudioClip[] m_HitSounds;
	public float m_HitEffectLifeTime;
	[HideInInspector]
	public Vector3 m_Rot;
	// Use this for initialization
	void Start () 
	{
		GetComponent<AudioSource>().PlayOneShot(m_HitSounds[Random.Range(0,m_HitSounds.Length)]);


		Destroy(gameObject,m_HitEffectLifeTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
