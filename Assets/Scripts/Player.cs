using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpVelocity; 
    public float bounceVelocity; 
    public Vector2 velocity;
    public float gravity; 
    public LayerMask wallMask;
    public LayerMask floorMask; 
     
    private bool walk, walk_left, walk_right, jump; 

    public enum PlayerState { 
        jumping, 
        idle,
        walking,
        bouncing, 
    }

    private bool grounded = false;
    private bool bounce = false; 
    private PlayerState playerState = PlayerState.idle;


    // Start is called before the first frame update
    void Start()
    {
        //Fall(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerInput();
        updatePlayerPosition();
        updateAnimationStates();
        
    }

    void updatePlayerPosition () 
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale; 
        if (walk)
        { 
            if (walk_left)
            {
                pos.x -= velocity.x * Time.deltaTime;
                scale.x = -1; 
            }

            if (walk_right)
            {
                pos.x += velocity.x * Time.deltaTime;
                scale.x = 1;
            
            }
            pos = checkWallRays(pos, scale.x); 
        }

        if (jump && playerState != PlayerState.jumping)
        {
            playerState = PlayerState.jumping;
            velocity = new Vector2(velocity.x, jumpVelocity);
            //Debug.Log(velocity);
        }

        if (playerState == PlayerState.jumping)
        {
            pos.y += velocity.y * Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
            //Debug.Log(playerState);
        }

        if ( bounce && playerState != PlayerState.bouncing)
        {
            playerState = PlayerState.bouncing; 
            velocity = new Vector2 (velocity.x, bounceVelocity); 
        }
        if (playerState == PlayerState.bouncing)
        {
            pos.y += velocity.y * Time.deltaTime; 
            velocity.y -= gravity * Time.deltaTime;
        }

        if (velocity.y <= 0)
        { 
            pos = CheckFloorRay(pos);
        }

        if (velocity.y >= 0)
        { 
            pos = checkCeilingRays(pos); 
        }
         
        transform.localPosition = pos;
        transform.localScale = scale;
        
    }

    void updateAnimationStates () { 
        if (grounded && !walk)
        {
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", false); 
        }
        
        if (grounded && walk) 
        {
            GetComponent<Animator>().SetBool("isJumping", false); 
            GetComponent<Animator>().SetBool("isRunning", true); 
        }

        if (playerState == PlayerState.jumping)
        { 
            GetComponent<Animator>().SetBool("isJumping", true); 
            GetComponent<Animator>().SetBool("isRunning", false); 
        }
    }

    void checkPlayerInput () {
        bool input_left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool input_right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool input_jump = Input.GetKey(KeyCode.Space);

        walk = input_left || input_right;
        walk_left = input_left && !input_right;
        walk_right = input_right && !input_left;

        jump = input_jump; 
    }

    Vector3 checkWallRays(Vector3 pos, float direction)
    {
        Vector2 originTop = new Vector2(pos.x + direction * .4f, pos.y + 1f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * .4f, pos.y - 1f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        //Debug.Log(Time.deltaTime); 

        if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null)
        {
            pos.x -= velocity.x * Time.deltaTime * direction;
        }
        return pos; 
    }

    Vector3 CheckFloorRay( Vector3 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - 1f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - 1f);

        RaycastHit2D floorLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask); 
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask); 
        RaycastHit2D floorRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask); 

        if (floorMiddle.collider != null || floorLeft.collider != null || floorRight.collider != null)
        {
            RaycastHit2D hitRay = floorRight; 

            if (floorLeft)
            {
                hitRay = floorLeft; 
            }
            else if (floorMiddle)
            {
                hitRay = floorMiddle; 
            } else if (floorRight)
            {
                hitRay = floorRight; 
            }

            if (hitRay.collider.tag == "Enemy")
            {
                bounce = true; 
                hitRay.collider.GetComponent<Enemy>().Crush();
            }

            playerState = PlayerState.idle;
            grounded = true;
            velocity.y = 0;

            pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 1;

        }
        else
        { 
            if (playerState != PlayerState.jumping)
            {
                Fall(); 
            }
        }

        return pos; 

    }

    Vector3 checkCeilingRays(Vector3 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y + 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y + 1f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y + 1f);

        RaycastHit2D ceilLeft = Physics2D.Raycast(originLeft, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilMiddle = Physics2D.Raycast(originMiddle, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilRight = Physics2D.Raycast(originRight, Vector2.up, velocity.y * Time.deltaTime, floorMask);

        if (ceilLeft.collider != null || ceilMiddle.collider != null || ceilRight.collider != null)
        {
            RaycastHit2D hitRay = ceilLeft; 

            if (ceilLeft)
            {
                hitRay = ceilLeft; 
            }
            else if (ceilMiddle)
            {
                hitRay = ceilMiddle; 
            }
            else if (ceilRight)
            {
                hitRay = ceilRight;
            }

            if (hitRay.collider.tag == "QuestionBlock")
            {
                hitRay.collider.GetComponent<QuestionBlock>().questionBlockBounce(); 
            }
            pos.y = hitRay.collider.bounds.center.y - hitRay.collider.bounds.size.y / 2 - 1;
            Fall(); 
        }
        return pos; 
    }

    void Fall() {
        velocity.y = 0;
        playerState = PlayerState.jumping;
        bounce = false; 
        grounded = false; 
    }
}