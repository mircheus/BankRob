using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VaultOpening : MonoBehaviour
{
    [SerializeField] private Vault[] _vaults;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(OpenVaults());
        }
    }

    private IEnumerator OpenVaults()
    {
        foreach (Vault vault in _vaults)
        {
            yield return new WaitForSeconds(.3f);
            vault.PlayOpenAnimation();
        }
    }
}
