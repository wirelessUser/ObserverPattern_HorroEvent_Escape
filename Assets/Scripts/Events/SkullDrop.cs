using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO ->Remove -> Move this entire logic in SkullDropEventTrigger.cs
public class SkullDrop : MonoBehaviour
{
    [SerializeField] private Transform Skulls;

    private void OnEnable()
    {
        EventService.Instance.SkullDropEvent.AddListener(OnSkullDrop);
    }

    private void OnDisable()
    {
        EventService.Instance.SkullDropEvent.RemoveListener(OnSkullDrop);
    }

    private void OnSkullDrop()
    {
        Skulls.gameObject.SetActive(true);
    }

}
