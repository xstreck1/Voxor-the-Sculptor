using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class RightController : MonoBehaviour
{
    public GameObject cutter = null;
    public Block block = null;


    SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        /*Vector3 logical = block.ClosestLogical(cutter.transform.position);
        int ID = block.PoisitionToID(logical);
        if (true || ID != -1)
        {
             Debug.Log("src: " + cutter.transform.position.x + "," + cutter.transform.position.y + "," + cutter.transform.position.z + "; log: " + logical.x  + "," + logical.y + "," + logical.z  + "; ID: " + ID);
        }
        GameObject affectedBlock = block.IDToObject(ID);
        if (affectedBlock != null)
        {
            affectedBlock.SetActive(false);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}