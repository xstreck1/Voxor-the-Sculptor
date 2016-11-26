﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class RightController : MonoBehaviour
{
    public Cutter cutter = null;
    public Block block = null;

    public float scaleSpeed = 2.5f;

    SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    Valve.VR.EVRButtonId triggerButtonID = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        cutter.SetFunctional(false);
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

        if (Controller.GetPressDown(triggerButtonID))
        {
            cutter.SetFunctional(true);
        }

        if (Controller.GetPressUp(triggerButtonID))
        {
            cutter.SetFunctional(false);
        }
        
        if (Controller.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            float axis_y = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
            cutter.Scale(axis_y * scaleSpeed * Time.deltaTime);
        }
    }
}