using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuUIView : MonoBehaviour
{
    [SerializeField] Button playButton;

    private void OnEnable() => playButton.onClick.AddListener(OnPlayButtonClicked);

    private void OnPlayButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

}
