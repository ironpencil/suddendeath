using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    float deltaTime = 0.0f;
    public bool displayFrameRate = false;

    public Dictionary<int, PlayerController> livingPlayers;
    public List<GameObject> spinners;
    public List<GameObject> lasers;
    public List<GameObject> wallBlades;
    public List<GameObject> mines;
    public List<Kill> kills;
    public List<Text> hudScores;
    public Text hudRound;
    public Text hudTime;
    public Text hudFps;

    public List<int> joinedPlayers;

    private int numPlayers = 0;

    public int currentRound = 0;
    public int RoundsToWin = 10;
    public Dictionary<int, PlayerStats> playerStats;
    public int lastRoundWinner = 0;
    public float roundStartTime = 0.0f;

    public List<Transform> playerSpawnPoints;

    public Transform dynamicsParent;
    public GameObject playerPrefab;
    public GameObject spinnerPrefab;
    public GameObject laserPrefab;
    public GameObject bombSpawnerPrefab;
    public GameObject wallBladePrefab;
    public GameObject mineSpawnerPrefab;

    public GameObject playerSetupUI;
    public ScoreScreenBehavior scoreScreenUI;
    
    public TileManager tileManager;

    public float collapsingFloorDifficulty = 1.0f;
    public float spinnerDifficulty = 1.0f;
    public float laserDifficulty = 1.0f;
    public float bombDifficulty = 1.0f;
    public float wallBladeDifficulty = 1.0f;
    public float mineDifficulty = 1.0f;

    public bool isRoundActive = false;
    public bool isRoundReady = false;

    // Use this for initialization
    void Start () {
        
    }

    public void SetupGame()
    {
        joinedPlayers = new List<int>();
        playerStats = new Dictionary<int, PlayerStats>();
        kills = new List<Kill>();
        playerSetupUI.GetComponent<PlayerSetupBehavior>().Display();
    }
    
    void UpdateHud()
    {
        if (displayFrameRate)
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            hudFps.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        }

        TimeSpan t = TimeSpan.FromSeconds(Time.time - roundStartTime);

        string time = string.Format("{0:D2}:{1:D2}:{2:D3}",
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);

        hudTime.text = time;
    }

    // Update is called once per frame
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        if (isRoundActive)
        {
            List<PlayerController> livingPlayersList = livingPlayers.Values.ToList();

            if (livingPlayersList.Count > 1)
            {
                UpdateHud();
            }
            else if (livingPlayersList.Count == 1)
            {
                if (numPlayers > 1)
                {
                    //stop, this player won
                    lastRoundWinner = livingPlayersList[0].GetComponent<PlayerInput>().PlayerNum;
                    StartCoroutine(EndRound());
                }
            }
            else
            {
                StartCoroutine(EndRound());
            }
        } else
        {
            if (isRoundReady && Input.anyKeyDown)
            {
                StartRound();
            }
        }
	}

    public void AddPlayer(int playerNum)
    {
        if (!joinedPlayers.Contains(playerNum))
        {
            joinedPlayers.Add(playerNum);
            hudScores[playerNum - 1].gameObject.SetActive(true);
        }
        if (!playerStats.Keys.Contains(playerNum))
        {
            PlayerStats ps = new PlayerStats();
            ps.playerNum = playerNum;
            playerStats.Add(playerNum, ps);
        }
    }

    public void RemovePlayer(int playerNum)
    {
        joinedPlayers.Remove(playerNum);
    }

    public void StartRound()
    {
        hudTime.gameObject.SetActive(true);

        currentRound++;
        hudRound.text = "Round " + currentRound.ToString();
        hudRound.gameObject.SetActive(true);

        livingPlayers = new Dictionary<int, PlayerController>();
        spinners = new List<GameObject>();
        lasers = new List<GameObject>();
        wallBlades = new List<GameObject>();
        numPlayers = joinedPlayers.Count;

        playerSetupUI.SetActive(false);
        scoreScreenUI.gameObject.SetActive(false);
        roundStartTime = Time.time;

        foreach (int playerNum in joinedPlayers)
        {
            GameObject player = GameObject.Instantiate(playerPrefab, dynamicsParent);
            player.transform.position = playerSpawnPoints[playerNum-1].position;
            livingPlayers[playerNum] = player.GetComponent<PlayerController>();
            livingPlayers[playerNum].GetComponent<PlayerInput>().PlayerNum = playerNum; //todo: clean this up
        }

        if (spinnerDifficulty > 0)
        {
            for (int i = 0; i < spinnerDifficulty; i++)
            {
                GameObject spinner = GameObject.Instantiate(spinnerPrefab, dynamicsParent);
                spinner.transform.position = Vector2.zero;
                spinners.Add(spinner);
            }
        }

        if (bombDifficulty > 0)
        {
            GameObject bombSpawner = GameObject.Instantiate(bombSpawnerPrefab, dynamicsParent);
            bombSpawner.transform.position = new Vector2(0.0f, 9.0f);
        }

        if (mineDifficulty > 0)
        {
            GameObject mineSpawner = GameObject.Instantiate(mineSpawnerPrefab, dynamicsParent);
            mineSpawner.transform.position = new Vector2(0.0f, 0.0f);
        }

        if (laserDifficulty > 0)
        {
            for (int i = 0; i < laserDifficulty; i++)
            {
                CreateWallLaser();
            }
        }

        if (collapsingFloorDifficulty > 0)
        {
            tileManager.gameObject.SetActive(true);
            tileManager.StartCollapsing();
        }

        if (wallBladeDifficulty > 0)
        {
            GameObject wallBlade = GameObject.Instantiate(wallBladePrefab, dynamicsParent);
            wallBlade.transform.position = Vector2.zero;
            wallBlades.Add(wallBlade);
        }

        isRoundActive = true;
        isRoundReady = false;
    }

    void CreateWallLaser()
    {
        // generate Wall Laser
        // N = 1, E = 2, S = 3, W = 4
        int wall = UnityEngine.Random.Range(0, 4);
        //wall = 4;

        Vector2 laserpos = new Vector2();
        Vector2 facing = new Vector2();
        float laserrotation = 0.0f;
        bool IsVertical = false;

        switch (wall)
        {
            case 0:
                laserpos.x = 0.0f;
                laserpos.y = 8f;
                laserrotation = 90.0f;
                facing.x = 0.0f;
                facing.y = -1.0f;
                break;
            case 1:
                laserpos.x = 15f;
                laserpos.y = 0.0f;
                laserrotation = 0.0f;
                facing.x = -1.0f;
                facing.y = 0.0f;
                IsVertical = true;
                break;
            case 2:
                laserpos.x = 0.0f;
                laserpos.y = -8f;
                laserrotation = -90.0f;
                facing.x = 0.0f;
                facing.y = 1.0f;
                break;
            case 3:
                laserpos.x = -15f;
                laserpos.y = 0.0f;
                laserrotation = 180.0f;
                facing.x = 1.0f;
                facing.y = 0.0f;
                IsVertical = true;
                break;
        }

        GameObject wallLaser = GameObject.Instantiate(laserPrefab, dynamicsParent);
        wallLaser.transform.position = laserpos;
        wallLaser.transform.eulerAngles = new Vector3(0, 0, laserrotation);
        WallLaserBehavior wlb = wallLaser.GetComponent<WallLaserBehavior>();
        wlb.facing = facing;
        wlb.IsVertical = IsVertical;
        lasers.Add(wallLaser);
    }

    public void AddKill(int killer, int victim, Kill.Weapon weapon)
    {
        Kill kill = new Kill(killer, victim, weapon);

        playerStats[killer].kills.Add(kill);
        playerStats[victim].deaths.Add(kill);
        kills.Add(kill);
    }

    IEnumerator EndRound()
    {
        isRoundActive = false;
        Time.timeScale = 0.0f;

        // Last round ended in tie
        if (lastRoundWinner != 0)
        {
            playerStats[lastRoundWinner].wins++;
            hudScores[lastRoundWinner - 1].text = "Player " + lastRoundWinner + ":  " + playerStats[lastRoundWinner].wins;
        }

        yield return new WaitForSecondsRealtime(2.0f);

        foreach (PlayerController player in livingPlayers.Values)
        {
            if (player != null)
            {
                playerStats[player.GetComponent<PlayerInput>().PlayerNum].survivalTime += Time.time - roundStartTime;
                Destroy(player.gameObject);
            }
        }

        foreach (Transform trans in dynamicsParent)
        {
            Destroy(trans.gameObject);
        }

        tileManager.Reset();
        Time.timeScale = 1.0f;

        scoreScreenUI.Display();
    }

    public PlayerStats GetWinner()
    {
        PlayerStats winner = null;

        foreach (PlayerStats ps in playerStats.Values)
        {
            if (ps.wins >= RoundsToWin)
            {
                winner = ps;
            }
        }

        return winner;
    }
}
