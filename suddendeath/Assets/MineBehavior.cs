using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour {
    public Color unarmedColor;
    public Color armedColor1;
    public Color armedColor2;
    public float armedTime;
    private float elapsedTime;
    public AnimationCurve blinkFrequency;
    public float startFrequency;
    private float nextColorSwitch;
    private bool IsColor1 = true;
    public GameObject ExplosionPrefab;
    public bool onlyArmByPlayer = true;
    private bool isArmed = false;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().color = unarmedColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (isArmed)
        {
            // Increase time spent blinking
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime > nextColorSwitch)
            {
                nextColorSwitch = elapsedTime + (startFrequency - (blinkFrequency.Evaluate(elapsedTime / armedTime) * startFrequency));
                IsColor1 = !IsColor1;
            }

            if (IsColor1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = armedColor1;
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().color = armedColor2;
            }

            if (elapsedTime > armedTime)
            {
                Transform DynamicsParent = Globals.Instance.GetComponent<GameManager>().dynamicsParent;
                GameObject explosion = Instantiate(ExplosionPrefab, DynamicsParent);
                explosion.transform.position = transform.position;

                isArmed = false;
                nextColorSwitch = 0.0f;
                elapsedTime = 0.0f;
            }
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().color = unarmedColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onlyArmByPlayer)
        {
            Debug.Log("collision.collider.transform.parent.parent.name: " + collision.collider.transform.parent.parent.name);
            // Check if a player or his shield hit us
            if (collision.collider.gameObject.GetComponent<PlayerInput>() != null
                || collision.collider.transform.parent.parent.gameObject.GetComponent<PlayerInput>() != null)
            {
                isArmed = true;
            }
        } else
        {
            isArmed = true;
        }
    }
}
