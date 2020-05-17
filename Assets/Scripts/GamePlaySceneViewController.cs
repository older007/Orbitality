using System;
using System.Collections;
using System.Collections.Generic;
using BattleSystem;
using GameMode;
using OrbitalSystem;
using OrbitalSystem.Element;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class GamePlaySceneViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text planetName;
    [SerializeField] private TMP_Text planetHealthPoint;
    [SerializeField] private TMP_Text pauseButtonText;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private PlayerController playerView;
    [SerializeField] private GameModeManager gameModeManager;
    private OrbitalManager orbitalManager => ServiceLocator.Get<OrbitalManager>();
    private UpdateProvider updateProvider => ServiceLocator.Get<UpdateProvider>();
    private OrbitalModel modelData => orbitalManager.playerPlanet;

    private string hpString = string.Empty;
    private string timerString = string.Empty;
    private DateTime startTime;

    private void Start()
    {
        pauseButton.gameObject.SetActive(false);
        
        playButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        startTime = DateTime.Now;
        
        pauseButton.onClick.AddListener(ChangePauseState);
        saveButton.onClick.AddListener(SaveEndExit);
        orbitalManager.PlayerSubscribe(OnPlayerDataChanged);

        pauseButtonText.text = Constants.Pause;
        planetName.text = "Third planet from Sun";
        UpdateUi(modelData);
        
        updateProvider.UpdateEvent += StartTimer;

        playerView.Init();
        gameModeManager.StartGame();
    }

    private void ChangePauseState()
    {
        updateProvider.IsStoped = !updateProvider.IsStoped;
       
        //for bullet physics
        Time.timeScale = updateProvider.IsStoped ? 0 : 1;
        
        pauseButtonText.text = updateProvider.IsStoped ? Constants.Resume : Constants.Pause;
    }

    private void StartTimer()
    {
        var time = startTime - DateTime.Now;
        timerString = $"20 / {time}";
    }

    private void OnPlayerDataChanged(OrbitalModel obj)
    {
        UpdateUi(obj);   
    }

    private void UpdateUi(OrbitalModel data)
    {
        hpString = $"HP : {data.HealthPoint}";
        
        planetHealthPoint.text = hpString;
    }

    private void SaveEndExit()
    {
        updateProvider.IsStoped = true;   
        orbitalManager.Save();
        gameModeManager.Save();
        
        GameLoader.Reload();
    }
}
