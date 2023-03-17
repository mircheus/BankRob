using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCrusher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Roof obstacle))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
