using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Dictionary<int, PlayerController> players;
    public List<GameObject> spinners;
    public List<GameObject> lasers;

    List<int> joinedPlayers = new List<int>();

    private int numPlayers = 0;

    public List<Transform> playerSpawnPoints;

    public Transform dynamicsParent;
    public GameObject playerPrefab;
    public GameObject spinnerPrefab;
    public GameObject laserPrefab;
    public GameObject bombSpawnerPrefab;

    public GameObject playerSetupUI;

    public TileManager tileManager;

    bool isRoundActive = false;
    bool isRoundReady = false;

    // Use this for initialization
    void Start () {
        SetupGame();
	}

    private void SetupGame()
    {
        playerSetupUI.SetActive(true);
    }

    void Init()
    {
        players = new Dictionary<int, PlayerController>();
        spinners = new List<GameObject>();
        lasers = new List<GameObject>();
        numPlayers = joinedPlayers.Count;
    }

    // Update is called once per frame
    void Update () {
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
                    int winningPlayer = livingPlayers[0].GetComponent<PlayerInput>().PlayerNum;
                    Debug.Log("Player " + winningPlayer + " Wins!");
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
    }

    public void RemovePlayer(int playerNum)
    {
        joinedPlayers.Remove(playerNum);
    }

    public void StartRound()
    {
        Init();
        playerSetupUI.SetActive(false);
        
        foreach (int playerNum in joinedPlayers)
        {
            GameObject player = GameObject.Instantiate(playerPrefab, dynamicsParent);
            player.transform.position = playerSpawnPoints[playerNum-1].position;
            players[playerNum] = player.GetComponent<PlayerController>();
            players[playerNum].GetComponent<PlayerInput>().PlayerNum = playerNum; //todo: clean this up
        }

        GameObject spinner = GameObject.Instantiate(spinnerPrefab, dynamicsParent);
        spinner.transform.position = Vector2.zero;
        spinners.Add(spinner);

        GameObject bombSpawner = GameObject.Instantiate(bombSpawnerPrefab, dynamicsParent);
        bombSpawner.transform.position = new Vector2(0.0f, 9.0f);

        CreateWallLaser();

        tileManager.gameObject.SetActive(true);
        tileManager.StartCollapsing();

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

    IEnumerator EndRound()
    {
        isRoundActive = false;
        Time.timeScale = 0.0f;

        yield return new WaitForSecondsRealtime(3.0f);

        foreach (PlayerController player in players.Values)
        {
            if (player != null)
            {
                Destroy(player.gameObject);
            }
        }

        foreach (Transform trans in dynamicsParent)
        {
            Destroy(trans.gameObject);
        }

        tileManager.Reset();

        Time.timeScale = 1.0f;

        isRoundReady = true;
    }
}
