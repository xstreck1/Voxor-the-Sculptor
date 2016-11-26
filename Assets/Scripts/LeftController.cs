using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class LeftController : MonoBehaviour
{
    public Colorer colorer = null;
    public Block block = null;

    public float scaleSpeed = 2.5f;
    public float scaleHue = 0.5f;

    SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    Valve.VR.EVRButtonId triggerButtonID = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        colorer.SetFunctional(false);
    }

    void Update()
    {
        if (Controller.GetPressDown(triggerButtonID))
        {
            colorer.SetFunctional(true);
        }

        if (Controller.GetPressUp(triggerButtonID))
        {
            colorer.SetFunctional(false);
        }

        if (Controller.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            float axis_y = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
            colorer.Scale(axis_y * scaleSpeed * Time.deltaTime);


            float axis_x = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
            colorer.ChangeColor(axis_x * scaleHue * Time.deltaTime);
        }
    }
}