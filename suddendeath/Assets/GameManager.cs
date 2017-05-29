using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Dictionary<int, PlayerController> players;
    public List<GameObject> spinners;
    public List<GameObject> lasers;

    public int numPlayers = 2;

    public List<Transform> playerSpawnPoints;

    public Transform dynamicsParent;
    public GameObject playerPrefab;
    public GameObject spinnerPrefab;
    public GameObject laserPrefab;

    public TileManager tileManager;

    bool isRoundActive = false;
    bool isRoundReady = false;

    // Use this for initialization
    void Start () {
        tileManager.gameObject.SetActive(false);
        players = new Dictionary<int, PlayerController>();
        spinners = new List<GameObject>();
        StartRound();
	}
	
	// Update is called once per frame
	void Update () {
        if (isRoundActive)
        {
            List<PlayerController> livingPlayers = players.Values.Where(p => p != null).ToList();

            if (livingPlayers.Count > 1)
            {
                //keep playing
            }
            else if (livingPlayers.Count == 1)
            {
                //stop, this player won
                int winningPlayer = livingPlayers[0].GetComponent<PlayerInput>().PlayerNum;
                Debug.Log("Player " + winningPlayer + " Wins!");
                StartCoroutine(EndRound());
            }
            else
            {
                //stop, everyone dead??
            }
        } else
        {
            if (isRoundReady && Input.anyKeyDown)
            {
                StartRound();
            }
        }
	}

    public void StartRound()
    {
        tileManager.StartCollapsing();

        for (int i = 0; i < numPlayers; i++)
        {
            GameObject player = GameObject.Instantiate(playerPrefab, dynamicsParent);
            player.transform.position = playerSpawnPoints[i].position;
            players[i] = player.GetComponent<PlayerController>();
            players[i].GetComponent<PlayerInput>().PlayerNum = i + 1; //todo: clean this up
        }

        GameObject spinner = GameObject.Instantiate(spinnerPrefab, dynamicsParent);
        spinner.transform.position = Vector2.zero;
        spinners.Add(spinner);

        isRoundActive = true;
        isRoundReady = false;
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

        foreach (GameObject spinner in spinners)
        {
            Destroy(spinner);
        }

        foreach (GameObject laser in lasers)
        {
            Destroy(laser);
        }

        tileManager.Reset();

        Time.timeScale = 1.0f;

        isRoundReady = true;
    }
}
