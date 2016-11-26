﻿using UnityEngine;
using System.Collections;

public class Cutter : MonoBehaviour {
    public Transform sourceCube;
    float sourceSize;

    Collider myCollider = null;
    Material material = null;

    void Awake()
    {
        myCollider = GetComponent<Collider>();
        material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        sourceSize = sourceCube.lossyScale.x;
        transform.localScale = Vector3.one * sourceSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }

    public void SetFunctional(bool isOn)
    {
        myCollider.enabled = isOn;
        material.color = new Color(material.color.r, material.color.g, material.color.b, isOn ? .75f : .15f);
    }

    public void Scale(float howMuch)
    {
        transform.localScale *= 1 + howMuch;
    } 
}
