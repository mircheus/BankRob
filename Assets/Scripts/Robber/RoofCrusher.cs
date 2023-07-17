using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofCrusher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Roof roof))
        {
            roof.GetDestroyed();
        }
    }
}
