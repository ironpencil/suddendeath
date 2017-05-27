using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public BaseMovement movementController;
    public FacingHandler facingHandler;
    public WeaponHandler weaponHandler;

    public bool canMoveWhileAttacking = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Globals.Instance.acceptPlayerGameInput)
        {
            HandleMovement();
            HandleAttack();
        }
	
	}

    private void HandleAttack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            weaponHandler.Attack(facingHandler.facing);
        }
    }

    void HandleMovement()
    {

        float horizontal = 0.0f;
        float vertical = 0.0f;

        if (canMoveWhileAttacking || !weaponHandler.IsAttacking())
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        float horizontalMovement = 0.0f;
        float verticalMovement = 0.0f;

        if (horizontal > 0.0f)
        {
            horizontalMovement = 1.0f;
        }
        else if (horizontal < 0.0f)
        {
            horizontalMovement = -1.0f;
        }

        if (vertical > 0.0f)
        {
            verticalMovement = 1.0f;
        }
        else if (vertical < 0.0f)
        {
            verticalMovement = -1.0f;
        }

        movementController.movementDirection = new Vector2(horizontalMovement, verticalMovement);

        facingHandler.UpdateFacing();
    }
}
