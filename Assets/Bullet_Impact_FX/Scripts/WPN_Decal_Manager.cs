using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WPN_Decal_Manager : Singleton<WPN_Decal_Manager>
{
    [System.Serializable]
    public struct DecalInfo
    {
        [SerializeField]
        public string name;

        [SerializeField]
        public PhysicMaterial Material;

        [SerializeField]
        public GameObject[] m_PFDecals;

        [SerializeField]
        public Vector2 rotMinMax;
        [SerializeField]
        public Vector2 scaleMinMax;

        [SerializeField]
        public GameObject[] PS_HitEffects;
    }

    public int PoolSize = 30;

    public PhysicMaterial[] Materials;

    private Hashtable physMaterials;

    public DecalInfo[] m_DecalInfo;

    private GameObject[,] DecalArr;

    [HideInInspector]
    public int[] cDecalIndex;

    void Start()
    {
        DecalArr = new GameObject[m_DecalInfo.Length, PoolSize];
        cDecalIndex = new int[m_DecalInfo.Length];

        GameObject cDecalPF;

        for (int atype = 0; atype < m_DecalInfo.Length; atype++)
        {
            cDecalIndex[atype] = 0;

            m_DecalInfo[atype].name = m_DecalInfo[atype].Material.name;

            if (m_DecalInfo[atype].m_PFDecals.Length > 0)
            {
                for (int i = 0; i < PoolSize; i++)
                {
                    cDecalPF = m_DecalInfo[atype].m_PFDecals[Random.Range(0, m_DecalInfo[atype].m_PFDecals.Length)];
                    DecalArr[atype, i] = Instantiate(cDecalPF, Vector3.zero, Quaternion.identity) as GameObject;

                    Transform ObjTranform = DecalArr[atype, i].GetComponent<Transform>();

                    ObjTranform.parent = transform;

                    DecalArr[atype, i].SetActive(false);
                }
            }
        }

        physMaterials = new Hashtable();

        for (int i=0; i< Materials.Length; i++)
        {
            physMaterials.Add(i, Materials[i].name);
        }
    }

    public void SpawnBulletHitEffects(Vector3 position, Vector3 normal, PhysicMaterial pMat, GameObject hGo)
    {        
        int decalIndex = 0;
        int cIndex = 0;
        bool bFound = false;

        foreach (DictionaryEntry pEntry in physMaterials)
        {
            if (pMat.name.ToString().Contains(pEntry.Value.ToString()))
            {
                decalIndex = cIndex;
                bFound = true;
                break;
            }
            cIndex++;
        }

        //Decal
        if ( bFound && m_DecalInfo[decalIndex].m_PFDecals.Length > 0 )
        {
            cDecalIndex[decalIndex] = (cDecalIndex[decalIndex] + 1) % PoolSize;

            GameObject SpawnAmmo = DecalArr[decalIndex, cDecalIndex[decalIndex]];

            if (SpawnAmmo != null)
            {
                SpawnAmmo.SetActive(true);

                SpawnAmmo.transform.position = position;
                SpawnAmmo.transform.rotation = Quaternion.LookRotation(-normal);

                SpawnAmmo.transform.parent = hGo.transform;

                float rotMin = m_DecalInfo[decalIndex].rotMinMax[0];
                float rotMax = m_DecalInfo[decalIndex].rotMinMax[1];

                float scaleMin = m_DecalInfo[decalIndex].scaleMinMax[0];
                float scaleMax = m_DecalInfo[decalIndex].scaleMinMax[1];

                SpawnAmmo.transform.RotateAround(SpawnAmmo.transform.position, SpawnAmmo.transform.forward, Random.Range(rotMin, rotMax));

                float scaleFact = Random.Range(scaleMin, scaleMax);

                SpawnAmmo.transform.localScale = new Vector3(scaleFact / hGo.transform.localScale.x, scaleFact / hGo.transform.localScale.y, 1f) / 10f;
            }
        }
        
        if (m_DecalInfo[decalIndex].PS_HitEffects.Length > 0)
        {
            int maxEff = m_DecalInfo[decalIndex].PS_HitEffects.Length;

            GameObject vFXEff = Instantiate(m_DecalInfo[decalIndex].PS_HitEffects[Random.Range(0, maxEff)], position, Quaternion.FromToRotation(Vector3.forward, normal)) as GameObject;


            ParticleSystem vfxPS = vFXEff.GetComponent<ParticleSystem>();

            if (vfxPS !=null && !vfxPS.isPlaying)
            {
                vfxPS.randomSeed = (uint)Random.Range(0f, 100f);

                ParticleSystem[] PChilds = vFXEff.GetComponentsInChildren<ParticleSystem>();

                foreach(ParticleSystem ps in PChilds)
                {
					ps.Play(true);
                }
            }

            Destroy(vFXEff, 3.0f);
        }
        
    }
}
