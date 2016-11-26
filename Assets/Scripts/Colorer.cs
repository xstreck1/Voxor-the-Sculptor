using UnityEngine;
using System.Collections;

public class Colorer : MonoBehaviour
{
    public Transform sourceCube;
    float sourceSize;

    Collider myCollider = null;
    Material material = null;

    HSL myColor = new HSL(0.5f, 1f, .5f);

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

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Renderer>().material.color = HSLColor.HSLToRGB(myColor);
    }

    public void SetFunctional(bool isOn)
    {
        myCollider.enabled = isOn;
        material.color = new Color(material.color.r, material.color.g, material.color.b, isOn ? .75f : .15f);
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
        transform.localScale *= 1 + howMuch;
    }
}
