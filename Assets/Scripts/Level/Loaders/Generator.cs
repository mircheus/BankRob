using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private int _floorsQuantity;

    public int FloorsQuantity => _floorsQuantity;
}
