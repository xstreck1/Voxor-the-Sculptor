using UnityEngine;
using System.Collections;

public class Colorer : MonoBehaviour
{
    public Transform sourceCube;
    float sourceSize;

    Collider myCollider = null;
    Material material = null;

    HSL myColor = new HSL(.5f, .5f, .5f);

    bool functional = false;

    void Awake()
    {
        myCollider = GetComponent<Collider>();
        material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        sourceSize = sourceCube.lossyScale.x;
        transform.localScale = Vector3.one * sourceSize;
        material.color = HSLColor.HSLToRGB(myColor);
    }

    private void OnTriggerStay(Collider other)
    {
        if (functional)
        {
            other.gameObject.GetComponent<Renderer>().material.SetColor("_Color", HSLColor.HSLToRGB(myColor));
        }
        else
        {
            other.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.gray);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
    }

    public void SetFunctional(bool isOn)
    {
        functional = isOn;
        material.color = new Color(material.color.r, material.color.g, material.color.b, functional ? .75f : .25f);
    }

    public void ChangeColor(float howMuch)
    {
        myColor.H += howMuch;
        myColor.H %= 1f;
        Color newColor = HSLColor.HSLToRGB(myColor);
        material.color = new Color(newColor.r, newColor.g, newColor.b, material.color.a);
    }

    public void Scale(float howMuch)
    {
        if ((transform.localScale.x <= .5f || howMuch < 0f) && (transform.localScale.x > .0001f || howMuch > 0f))
        {
            transform.localScale *= 1 + howMuch;
        }
    }
}
