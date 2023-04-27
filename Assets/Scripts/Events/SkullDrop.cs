using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO ->Remove -> Move this entire logic in SkullDropEventTrigger.cs
public class SkullDrop : MonoBehaviour
{
    [SerializeField] private Transform Skulls;

    private void OnEnable()
    {
        EventManager.OnSkullDrop += OnSkullDrop;
    }

    private void OnDisable()
    {
        EventManager.OnSkullDrop -= OnSkullDrop;
    }

    private void OnSkullDrop()
    {
        Skulls.gameObject.SetActive(true);
    }

}
