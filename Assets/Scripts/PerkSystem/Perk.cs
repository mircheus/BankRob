using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Perk : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private float _actionTime;

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(DeactivateAfterExecution());
    }

    private IEnumerator DeactivateAfterExecution()
    {
        yield return new WaitForSeconds(_actionTime);
        Deactivate();
    }

    protected virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
