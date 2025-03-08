using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 
    PlayerInputs inputActions;
    SpriteRenderer sprite;
    Animator animator;
    Rigidbody2D body;
    public GameObject shotPrefab;

    public float speed = 4.5f;
    public float jump = 17.0f;
    public float attack = 10.0f;

    bool canJump = true;
    bool canAttack = true;

    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new PlayerInputs();
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var moveInputs = inputActions.Player_Map.Movement.ReadValue<Vector2>();
        //Debug.Log("Move Inputs: " + moveInputs);
        transform.position += speed * Time.deltaTime * new Vector3(moveInputs.x, 0, 0);

        animator.SetBool("b_isWalking", moveInputs.x != 0);

        if (moveInputs.x != 0)
        {
            sprite.flipX = moveInputs.x < 0;
        }

        canJump = Mathf.Abs(body.velocity.y) <= 0.001f;
        HandlerJumpAction();

        HandlerAttack();

    }

    void HandlerJumpAction()
    {
        var jumpPressed = inputActions.Player_Map.Jump.IsPressed();
        if( canJump && jumpPressed )
        {
            body.AddForce(jump * Vector2.up, ForceMode2D.Impulse);
        }
    }

    void HandlerAttack()
    {
        var attackPressed = inputActions.Player_Map.Attack.IsPressed();
        if( canAttack && attackPressed)
        {
            canAttack = false;

            animator.SetTrigger("t_attack");

        }

    }

    public void shootNewEgg()
    {
        var newShot = GameObject.Instantiate(shotPrefab);

        newShot.transform.position = transform.position;

        var isLookLeft = sprite.flipX;
        Vector2 shotDirection = attack * new Vector2(isLookLeft ? 1 : -1, 0);

        newShot.GetComponent<Rigidbody2D>().AddForce(shotDirection, ForceMode2D.Impulse);
        newShot.GetComponent<SpriteRenderer>().flipY = isLookLeft;
    }

    public void setCanAttack()
    {
        canAttack = true;
    }
}
