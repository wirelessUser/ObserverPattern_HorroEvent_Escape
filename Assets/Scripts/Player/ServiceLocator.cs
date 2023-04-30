using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : GenericMonoSingleton<ServiceLocator>
{
    private PlayerController playerController;

    [Header("Player References")]
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerScriptableObject playerScriptableObject;


    private void Start()
    {
        playerController = new PlayerController(playerView, playerScriptableObject);
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

}
