using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Globals : Singleton<Globals>
{
    public GameObject pauseScreenUI;
    public bool paused = false;
    public bool acceptPlayerGameInput = true;

    public bool playIntro = true;
    public bool deletePlayerPrefs = false;

    public IntroPanel firstPanel;

    public ScreenTransition screenTransition;

    public SoundEffectHandler gameStartSound;
    public SoundEffectHandler pauseSound;
    public SoundEffectHandler unpauseSound;
    private bool playPauseSound = false;

    public float screenShakeFactor = 1.0f;

    public GameManager GameManager;

    //[MenuItem("Edit/Reset Playerprefs")]
    //public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }

    public override void Start()
    { 
        if (deletePlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        base.Start();

        if (this == null) { return; }

        if (playIntro && firstPanel != null)
        {
            //play intro   
            acceptPlayerGameInput = false;
            //GUIManager.Instance.FadeScreen(1.0f, 1.0f, 0.0f);
            firstPanel.introParent.SetActive(true);
            firstPanel.DisplayPanel();
        }
        else
        {
            StartGame();
        }

        Vector2 point1 = new Vector2(-5, 5);
        Vector2 point2 = new Vector2(5, -5);

        //float angle1 = Vector2.Angle(Vector2.right, (point2 - point1));
        //float angle2 = Vector2.Angle(Vector2.right, (point1 - point2));
        //Vector2 v1 = point2 - point1;
        //Vector2 v2 = point1 - point2;

        //float angle1 = Mathf.Atan2(v1.y, v1.x) * Mathf.Rad2Deg;
        //float angle2 = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

    public void IntroFinished()
    {
        if (playIntro)
        {
            playIntro = false;            
            StartGame();
        }
    }

    public void StartGame()
    {
        if (gameStartSound != null)
        {
            gameStartSound.PlayEffect();
        }
        StartCoroutine(DoStartGame());
    }

    private IEnumerator DoStartGame()
    {
        playPauseSound = false;
        //disable player object
        Globals.Instance.Pause(true);
        Globals.Instance.acceptPlayerGameInput = false;

        //TODO: do a screen transition
        //yield return StartCoroutine(screenTransition.TransitionCoverScreen(1.0f));

        try
        {
            firstPanel.introParent.SetActive(false);
        }
        catch { }

        yield return null;

        //yield return StartCoroutine(screenTransition.TransitionUncoverScreen(1.0f));

        //AudioManager.Instance.StartMusic();

        Globals.Instance.Pause(false);
        Globals.Instance.acceptPlayerGameInput = true;
        playPauseSound = true;

        //TODO: do screen transition in
        GameManager.SetupGame();
    }

    public void Pause(bool pause)
    {
        if (isQuitting) { return; }

        paused = pause;

        if (paused)
        {
            if (playPauseSound) { pauseSound.PlayEffect(); }
            Time.timeScale = 0.0f;
            acceptPlayerGameInput = false;
            pauseScreenUI.SetActive(true);
        }
        else
        {
            if (playPauseSound) { unpauseSound.PlayEffect(); }
            Time.timeScale = 1.0f;
            acceptPlayerGameInput = true;
            pauseScreenUI.SetActive(false);
        }
    }

    [ContextMenu("Toggle Pause")]
    public void TogglePause()
    {
        Pause(!paused);
    }

    public bool isQuitting = false;

    public void DoQuit()
    {
        Pause(false);
        isQuitting = true;
        StartCoroutine(WaitAndQuit(1.0f));
    }

    private IEnumerator WaitAndQuit(float time)
    {
        yield return new WaitForSeconds(time);

        Application.Quit();
    }    
}
