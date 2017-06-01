using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    float deltaTime = 0.0f;
    public bool displayFrameRate = false;

    public Dictionary<int, PlayerController> players;
    public List<GameObject> spinners;
    public List<GameObject> lasers;
    public List<GameObject> wallBlades;
    public List<GameObject> mines;
    public List<Kill> kills;

    List<int> joinedPlayers = new List<int>();

    private int numPlayers = 0;
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
    public GameObject minePrefab;

    public GameObject playerSetupUI;
    public GameObject scoreScreenUI;
    
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
        SetupGame();
	}

    private void SetupGame()
    {
        playerStats = new Dictionary<int, PlayerStats>();
        kills = new List<Kill>();
        playerSetupUI.SetActive(true);
    }

    private void DisplayScore()
    {
        foreach (Kill kill in kills)
        {
            Debug.Log("Player" + kill.killerPlayerNum + " killed Player" + kill.victimPlayerNum + " with " + kill.weapon);
        }
        scoreScreenUI.SetActive(true);
    }

    void OnGUI()
    {
        if (displayFrameRate)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }

    // Update is called once per frame
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        if (isRoundActive)
        {
            List<PlayerController> livingPlayers = players.Values.ToList();

            if (livingPlayers.Count > 1)
            {
                //keep playing
            }
            else if (livingPlayers.Count == 1)
            {
                if (numPlayers > 1)
                {
                    //stop, this player won
                    lastRoundWinner = livingPlayers[0].GetComponent<PlayerInput>().PlayerNum;
                    Debug.Log("Player " + lastRoundWinner + " Wins!");
                    StartCoroutine(EndRound());
                }
            }
            else
            {
                //stop, everyone dead??
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
        }
        if (!playerStats.Keys.Contains(playerNum))
        {
            playerStats.Add(playerNum, new PlayerStats());
        }
    }

    public void RemovePlayer(int playerNum)
    {
        joinedPlayers.Remove(playerNum);
    }

    public void StartRound()
    {
        players = new Dictionary<int, PlayerController>();
        spinners = new List<GameObject>();
        lasers = new List<GameObject>();
        wallBlades = new List<GameObject>();
        numPlayers = joinedPlayers.Count;

        playerSetupUI.SetActive(false);
        scoreScreenUI.SetActive(false);
        roundStartTime = Time.time;

        foreach (int playerNum in joinedPlayers)
        {
            GameObject player = GameObject.Instantiate(playerPrefab, dynamicsParent);
            player.transform.position = playerSpawnPoints[playerNum-1].position;
            players[playerNum] = player.GetComponent<PlayerController>();
            players[playerNum].GetComponent<PlayerInput>().PlayerNum = playerNum; //todo: clean this up
        }

        if (spinnerDifficulty > 0)
        {
            GameObject spinner = GameObject.Instantiate(spinnerPrefab, dynamicsParent);
            spinner.transform.position = Vector2.zero;
            spinners.Add(spinner);
        }

        if (bombDifficulty > 0)
        {
            GameObject bombSpawner = GameObject.Instantiate(bombSpawnerPrefab, dynamicsParent);
            bombSpawner.transform.position = new Vector2(0.0f, 9.0f);
        }

        if (laserDifficulty > 0)
        {
            CreateWallLaser();
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

        if (mineDifficulty > 0)
        {
            GameObject mine = GameObject.Instantiate(minePrefab, dynamicsParent);
            mine.transform.position = new Vector2(UnityEngine.Random.Range(-3, 4), UnityEngine.Random.Range(-3, 4));
            mines.Add(mine);
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
        Debug.Log("Player" + killer + " killed Player" + victim + " with " + weapon);
        Kill kill = new Kill(killer, victim, weapon);

        playerStats[killer].kills.Add(kill);
        playerStats[victim].deaths.Add(kill);
        kills.Add(kill);
    }

    IEnumerator EndRound()
    {
        isRoundActive = false;
        Time.timeScale = 0.0f;

        playerStats[lastRoundWinner].wins++;

        yield return new WaitForSecondsRealtime(2.0f);

        foreach (PlayerController player in players.Values)
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
        DisplayScore();
    }
}
