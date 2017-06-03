using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour, Explosive {
    public Color unarmedColor;
    public Color armedColor1;
    public Color armedColor2;
    public float armedTime;
    public AnimationCurve blinkFrequency;
    public float startFrequency;
    public GameObject ExplosionPrefab;
    public bool onlyArmByPlayer = true;
    public bool bodyCollisionKillsPlayer = true;
    public MineSpawnerBehavior MineSpawnerBehavior;
    public bool IsMineDestroyedOnExplosion = false;
    public bool StartsArmed = false;
    public float MaxLifetime = 0.0f;

    public SoundEffectHandler beepSound;
    public AudioSource slideSource;
    public float slideMaxVolume = 1.0f;

    private float elapsedTime;
    private float elapsedArmedTime;
    private float nextColorSwitch;
    private bool IsColor1 = true;
    private bool isArmed = false;
    private List<int> colliders;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().color = unarmedColor;
        colliders = new List<int>();
        isArmed = StartsArmed;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        elapsedTime += Time.deltaTime;

        if (MaxLifetime > 0 && elapsedTime > MaxLifetime) isArmed = true;

        if (isArmed)
        {
            // Increase time spent blinking
            elapsedArmedTime += Time.deltaTime;
            
            if (elapsedArmedTime > nextColorSwitch)
            {
                //e + (.5 - (easing * .5))

                nextColorSwitch = elapsedArmedTime + (startFrequency - (blinkFrequency.Evaluate(elapsedArmedTime / armedTime) * startFrequency));
                IsColor1 = !IsColor1;
                if (!IsColor1) { beepSound.PlayEffect(); }
            }

            if (IsColor1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = armedColor1;
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().color = armedColor2;
            }

            if (elapsedArmedTime > armedTime)
            {
                Explode();
            }
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().color = unarmedColor;
        }
    }

    void FixedUpdate()
    {
        slideSource.volume = slideMaxVolume * Mathf.Min((rb2d.velocity.sqrMagnitude / 128), 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collided with a player?
        PlayerInput pi = collision.collider.gameObject.GetComponent<PlayerInput>();
        
        if (pi == null)
        {
            try
            {
                // Collided with a player's shield?
                pi = collision.collider.transform.parent.parent.gameObject.GetComponent<PlayerInput>();
            } catch (NullReferenceException)
            {
                // Ignore, mine collided with something other than a player
            }
        } else if (bodyCollisionKillsPlayer)
        {
            Explode();
        }

        if (pi != null)
        {
            colliders.Add(pi.PlayerNum);
        }

        if (!onlyArmByPlayer)
        {
            isArmed = true;
        } else if (pi != null)
        {
            isArmed = true;
        }
    }

    void Explode()
    {
        Transform DynamicsParent = Globals.Instance.GetComponent<GameManager>().dynamicsParent;
        GameObject explosion = Instantiate(ExplosionPrefab, DynamicsParent);
        explosion.transform.position = transform.position;
        ExplosionBehavior eb = explosion.GetComponent<ExplosionBehavior>();
        eb.explosives.Add(this);
        isArmed = false;
        nextColorSwitch = 0.0f;
        elapsedArmedTime = 0.0f;

        // Remove the mine from visibility for now
        // Destroy gameObject when explosion finishes
        if (IsMineDestroyedOnExplosion)
        {
            gameObject.SetActive(false);
        }
    }

    public int GetLastCollider(int playerNum)
    {
        int lastCollider = playerNum;

        for (int i = colliders.Count - 1; i >= 0; i--)
        {
            lastCollider = colliders[i];

            if (colliders[i] != playerNum)
            {
                break;
            }
        }
        
        return lastCollider;
    }

    public void KilledPlayer(int playerNum)
    {
        int killer = GetLastCollider(playerNum);
        Globals.Instance.GetComponent<GameManager>().AddKill(killer, playerNum, Kill.Weapon.Mine);
    }

    public void StartExploding()
    {
        // Do nothing
    }

    public void EndExploding()
    {
        colliders.Clear();
        if (IsMineDestroyedOnExplosion)
        {
            MineSpawnerBehavior.RemoveMine(gameObject);
            Destroy(gameObject);
        }
    }
}
