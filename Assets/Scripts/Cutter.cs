using UnityEngine;
using System.Collections;

public class Cutter : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
