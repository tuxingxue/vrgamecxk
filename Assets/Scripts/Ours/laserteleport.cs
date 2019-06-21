using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class laserteleport : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject Camera;
    public GameObject laser_yellow;
    public float maximummove;
    private GameObject reticle;
    private GameObject laser1;
    private Transform lasertrans1;
    private Vector3 hitPoint;

    public float timetele = 0.5f;
    private bool cantele;

    public LayerMask teleportmask;
    public GameObject reticle_pre;
    private Transform reticletrans;
    public Transform camerarigtrans;
    public Transform cameratrans;
    public Vector3 reticle_offset;
    private bool whetherTeleport;
    // Start is called before the first frame update
    void Start()
    {
        laser1 = Instantiate(laser_yellow);
        lasertrans1 = laser1.transform;
        reticle = Instantiate(reticle_pre);
        reticletrans = reticle.transform;
        cantele = true;
    }

    // Update is called once per frame
    void Update()
    {
        int status = Camera.GetComponent<set_position>().humanstatus;
        if (teleportAction.GetState(handType) && status == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, maximummove, teleportmask))
            {
                hitPoint = hit.point;
                reticle.SetActive(true);
                reticletrans.position = hitPoint + reticle_offset;
                whetherTeleport = true;
                EmitLaser(hit);
            }
            else
            {
                laser1.SetActive(false);
                reticle.SetActive(false);
            }
        }
        else
        {
            laser1.SetActive(false);
            reticle.SetActive(false);
        }
        if (teleportAction.GetStateUp(handType) && whetherTeleport && cantele)
        {
            Teleport();
            cantele = false;
            StartCoroutine(recovertele());
        }
    }

    private IEnumerator recovertele()
    {
        yield return new WaitForSeconds(timetele);
        cantele = true;
    }

    private void EmitLaser(RaycastHit hit)
    {
        laser1.SetActive(true);
        lasertrans1.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        lasertrans1.LookAt(hitPoint);
        lasertrans1.localScale = new Vector3(lasertrans1.localScale.x, lasertrans1.localScale.y, hit.distance);
    }

    private void Teleport()
    {
        whetherTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = camerarigtrans.position - cameratrans.position;
        difference.y = 0;
        camerarigtrans.position = hitPoint + difference;
    }
}
