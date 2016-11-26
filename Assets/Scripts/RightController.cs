using UnityEngine;
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

    static private bool menuPressed = false;
    static public bool MenuPressed { private set { menuPressed = value; } get { return menuPressed; } }

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        cutter.SetFunctional(false);
    }

    void Update()
    {
        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
        {
            MenuPressed = true;
        }

        if (Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
        {
            MenuPressed = false;
        }

        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            cutter.SetFunctional(true);
        }

        if (Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
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