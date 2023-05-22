using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    [SerializeField] private ObstacleCrusher _obstacleCrusher;

    public void Attack()
    {
        _obstacleCrusher.Attack();
    }
}
