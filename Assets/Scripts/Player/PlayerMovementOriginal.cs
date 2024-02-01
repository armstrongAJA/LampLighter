using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOriginal : MonoBehaviour
{
    //Declare variables
    public PlayerData PlayerData;

    public bool isSceneTransitionActive = false;
    public bool isMovingEnabled = true;
    public bool WallJumpActive = false;
    private bool isJumping = false;
    public Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    public float coyoteTime = .1f;

    [SerializeField] private LayerMask JumpableGround;
    [SerializeField] private LayerMask JumpableWall;

    private float dirX;
    public int sceneTransitionMovementDirectionHorizontal;
    public int sceneTransitionMovementDirectionVertical;

    private float maxSpeed;
    private float JumpSpeed;
    private float WallJumpSpeedx;
    private float WallJumpSpeedy;
    private float WallSlideSpeed;
    private float NormalGravity;
    private float TrampolineJumpSpeed;
    private float wallcheckDistance;
    private float downRecoilSpeed;
    private float horRecoilSpeed;

    public int WallContact;
    public float LastOnWallTime;
    public float WallJumpTime = 0.5f;
    public float LastWallJumpTime;
    public float LastOnGroundTime;
    public bool DoubleJump;
    public bool doubleJumpActive = false;
    public float fallGravityMult = 1.9f;
    public float maxFallSpeed = 18f;
    public float wallJumpRunlerpAmount;
    private float wallJumpStartTime;
    private bool isWallJumping = false;
    public bool isFacingRight;
    public bool wasFacingRight;
    private float lastJumpedTime = 0f;

    public bool isRecoilingVert = false;
    public bool isRecoilingHor = false;
    public float recoilHorLerpAmount = .2f;
    public float recoilTime = .5f;
    private float recoilStartTime;

    private CameraFollowObjectScript cameraFollowObject;
    private float fallSpeedYDampingChangeThreshold;

    [Header("CameraStuff")]
    [SerializeField] private GameObject cameraFollowGO;

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling,
        doublejumping
    } //Define enum variable to define animation state

    private bool isWallCollide = false;

    [SerializeField] private AudioSource jumpSoundEffect;
    //private MovementState = MovementState.idle//Declare within unity animations instead

    //declare all the variables above^^

    // Start is called before the first frame update
    private void Start()
    {
        //Cache various components initially to improve efficiency
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cameraFollowObject = cameraFollowGO.GetComponent<CameraFollowObjectScript>();
        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedDampingChangeThreshold;

        //initialize all variables from the playerData scriptable object
        maxSpeed = PlayerData.maxSpeed;
        JumpSpeed = PlayerData.jumpSpeed;
        WallJumpSpeedx = PlayerData.wallJumpSpeedx;
        WallJumpSpeedy = PlayerData.wallJumpSpeedy;
        WallSlideSpeed = PlayerData.wallSlideSpeed;
        NormalGravity = PlayerData.normalGravity;
        TrampolineJumpSpeed  = PlayerData.trampolineJumpSpeed;
        wallcheckDistance = PlayerData.wallcheckDistance;
        downRecoilSpeed = PlayerData.downRecoilSpeed;
        horRecoilSpeed = PlayerData.horRecoilSpeed;
        JumpableGround = PlayerData.jumpableGround;
        JumpableWall = PlayerData.jumpableWall;
        //these should not be in start to update them as things get unlocked
       

    }

    // Update is called once per frame
    private void Update()
    {
        #region Timers

        //update all timers here
        LastOnWallTime -= Time.deltaTime;
        LastWallJumpTime -= Time.deltaTime;
        LastOnGroundTime -= Time.deltaTime;

        #endregion

        rb.gravityScale = NormalGravity; //initialize gravity
        #region Running
        
         if (isWallJumping) //if wall jumping currently
         {
             Running(wallJumpRunlerpAmount); //disable Running inputs
         }
         else if (isRecoilingHor)
        {
            Running(recoilHorLerpAmount);
        }
            else //or if not wall jumping
         {
             Running(1f); //enable Running
         }


        #endregion
        #region CameraLerping
        if (rb.velocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.lerpedFromPlayerFalling)//if falling past speed threshold
        {
            CameraManager.instance.LerpYDamping(true);
        }
        if (rb.velocity.y >= 0f && !CameraManager.instance.isLerpingYDamping && CameraManager.instance.lerpedFromPlayerFalling)//if standing still or moving upwards
        {
            CameraManager.instance.lerpedFromPlayerFalling = false;//reset it so this can be called again
            CameraManager.instance.LerpYDamping(false);

        }
        #endregion
        #region Jumping
        if (isMovingEnabled)

        {
            if (IsGrounded()) //if player is grounded
            {
                LastOnGroundTime = coyoteTime; //Reset Last on ground time to coyote time
            }

            if (Input.GetButtonDown("Jump")) //check if jumping and player is touching ground
            {
                if (LastOnGroundTime > 0 && !isJumping) //if grounded
                {
                    isJumping = true;
                    lastJumpedTime = Time.time;
                    jumpSoundEffect.Play(); //play jump sound effect
                    rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse); //adds jump force to player
                    DoubleJump = true; //allow double jump

                    Debug.Log("Jumping");
                }
                else if (PlayerData.doubleJumpActive && DoubleJump) //check if double jump allowed and unlocked
                {
                    jumpSoundEffect.Play(); //play jump sound effect
                    rb.velocity = new Vector2(rb.velocity.x, 0f); //reset velocity to zero in y
                    rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse); //adds jump force to player
                    DoubleJump = false; //disable furrther jumping till grounded again
                }
            }
            if (lastJumpedTime<(Time.time - PlayerData.jumpDisabledWindowTime))
            {
                isJumping = false;
            }
        }
        if (sceneTransitionMovementDirectionVertical==1 && !isMovingEnabled && isSceneTransitionActive)//check if scene transition direction is upwards
        {
            
            rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);//adds jump force to player if movement disabled
            isSceneTransitionActive = false;
        }
        
        if (rb.velocity.y < 0)
            {
                //Higher gravity if falling
                rb.gravityScale = NormalGravity * fallGravityMult;
                //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
            }
        
        #endregion

            #region wallgrab/walljump

            WallContact = IsWallContact(); //call function to check if player in contact with the wall
            WallSlide(); //if in contact with the wall, execute wall slide
        if (isMovingEnabled)
        {
            if (isWallJumping && Time.time - wallJumpStartTime > WallJumpTime)//check if still within wall jump time
            {
                isWallJumping = false;//if not, set wall jumping variable to false
            }

            if (WallContact != 0 && !IsGrounded() &&
                    PlayerData.wallJumpActive) //check if player is touching wall, is not grounded and wall jump power unlocked
            {
                LastOnWallTime = coyoteTime; //reset last on wall time to coyote time
                                             //LastOnWallTime is seperate from LastWallJumpTime to track jump and slide physics seperately

                if (Input.GetButtonDown("Jump")) //check if jump is pressed
                {
                    LastWallJumpTime = coyoteTime; //set last wall jump time to coyote time
                    wallJumpStartTime = Time.time;
                    rb.gravityScale = NormalGravity; //reset gravity
                    jumpSoundEffect.Play(); //play jump sound effect
                    rb.velocity = new Vector2(rb.velocity.x, 0f); //set y velocity to zero so jump works if falling
                    rb.AddForce(Vector2.up * WallJumpSpeedy, ForceMode2D.Impulse); //add wall jump force y
                    rb.AddForce(Vector2.right * (WallJumpSpeedx * WallContact),
                        ForceMode2D.Impulse); //add wall jump force x
                    DoubleJump = true;//enable double jump if unlocked
                    isWallJumping = true;
                }
                //rb.velocity = new Vector2(rb.velocity.x,0f);
                //rb.velocity = new Vector2(rb.velocity.x, WallJumpSpeedy);//adds wall jump to player velocity
                //Debug.Log("WallJump" + rb.totalForce + rb.velocity);
            }
            else
            {
                isWallCollide = false; //reset wall collision parameter to false again
            }
        }

            #endregion

            UpdateAnimationState(); //call function to update the animation state
        #region Recoil Timer
        if (isRecoilingHor && Time.time - recoilStartTime > recoilTime)//check if still within recoil time
        {
            isRecoilingHor = false;//if not, set recoiling variable to false
        }
        #endregion
    }

    #region Animation Control

    void UpdateAnimationState() //function to update the animation state
        {
            MovementState state; //define state as a variable of type MovementState (defined by me in Enum)
            if (dirX > 0f) //If running forwards do this
            {
            //anim.SetBool("Running", true);//Sets the Running variable to switch animations
                
                state = MovementState.running; //set Movement state to running
                //sprite.flipX = false; //flip sprite direction
                Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);//initialize rotator to flip gameobject
                transform.rotation = Quaternion.Euler(rotator);//rotate player in y axis to turn
            if (!isFacingRight)
            {
                isFacingRight = true;
                cameraFollowObject.CallTurn();//turn the camera follow object to move camera
            }
      
            }
            else if (dirX < 0f) //If running backwards do this
            {
                
                state = MovementState.running; //set Movement state to running
                                               //sprite.flipX = true; //flip sprite direction
                Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);//initialize rotator to flip gameobject
                transform.rotation = Quaternion.Euler(rotator);//rotate player in y axis to turn
            if (isFacingRight)
            {
                isFacingRight = false;
                cameraFollowObject.CallTurn();//turn the camera follow object to move camera
            }
        }
            else //If player is idle do this
            {
                state = MovementState.idle; //set Movement state to idle
            }

            if (rb.velocity.y > .1f) //check if y velocity is positive
            {
                state = MovementState.jumping; //set Movement state to jumping
            }

            else if (rb.velocity.y < -.1f) //If the y velocity is negative
            {
                state = MovementState.falling; //set Movement state to falling
            }

            anim.SetInteger("MovementState",
                (int)state); //Update the Movement state parameter within the animator to that within this program
        }

        #endregion

        public bool IsGrounded() //Function to check if the player is touching the ground
        {
            return (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, PlayerData.groundCheckDistance,
                JumpableGround)); //casts box .1 below collision box to only extend below collision
        }

        void OnTriggerEnter2D(Collider2D collision) //Trampoline collision effect
        {
            if (collision.gameObject.CompareTag("Trampoline")) //If There is a collision with the trampoline
            {
                jumpSoundEffect.Play(); //Play the jump sound effect
                rb.velocity = new Vector2(rb.velocity.x, TrampolineJumpSpeed); //adds jump to player velocity
            }
        }

        int IsWallContact() //Function to check if the player is touching the wall
        {
            if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, wallcheckDistance,
                    JumpableWall)) //casts box .1 to right of collision box to only extend below collision
            {
                return 1;
            }

            if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, wallcheckDistance,
                    JumpableWall)) //casts box .1 to right of collision box to only extend below collision
            {
                return -1;
            }

            if (!isWallCollide) //if the wall has not already entered a collision recently
            {
                isWallCollide = true; //then set the colission parameter to be true
            }

            return 0; //returns different integers depending on the wall contact direction

        }


        bool IsWallJumping() //function to get boolean variable to decide if the player is currently wall jumping
        {
            if (LastWallJumpTime > 0 && !IsGrounded())//check if last wall jump was more than coyote time ago
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        void Running(float lerpAmount) //function to execute running (lerpamount=0 will disable running)
        {
        //float dirX = Input.GetAxis("Horizontal"); //will slide a bit including this line instead
        if (isMovingEnabled)
        {
            dirX = Input.GetAxisRaw("Horizontal"); //Get the horizontal movement input value
        }
        else
        {
            dirX = sceneTransitionMovementDirectionHorizontal;
        }
            //Calculate the direction we want to move in and our desired velocity
            float targetSpeed = dirX * maxSpeed;
            //We can reduce are control using Lerp() this smooths changes to direction and speed
            targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);
            //Calculate difference between current velocity and desired velocity
            float speedDif = targetSpeed - rb.velocity.x;
            //Calculate force along x-axis to apply to the player

            float movement = speedDif;

            //rb.velocity = new Vector2(dirX * MoveSpeed, rb.velocity.y); //adds horizontal movement to player velocity
            rb.AddForce(Vector2.right * movement, ForceMode2D.Force); //adds run force to player
        }

        void WallSlide() //function to implement wall slide
        {
            if (WallContact != 0 && !IsGrounded() && PlayerData.wallJumpActive &&
                rb.velocity.y < 0) //check if in contact with the wall, not grounded, wall jump unlocked and falling
                //only implemented if falling to ensure upward velocity doesn't cut to zero when in contact with a wall
            {
                //rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.gravityScale = WallSlideSpeed; //reduce gravity to slide slower
                if (rb.velocity.y < -.1f && !isWallCollide) //if falling and is first contact with the wall
                {
                    rb.velocity =
                        new Vector2(rb.velocity.x, 0f); //reset velocity to zero in y if first collision with wall
                    isWallCollide =
                        true; //set this to true to ensure velocity doesn't get reset to zero again every frame of contact
                
                }
            }
         
        }
    public void Recoil(int attackDirection)
    {
        if (attackDirection == -1)//if attacking down
        {
            Debug.Log("Recoiling");
            isRecoilingVert = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * downRecoilSpeed, ForceMode2D.Impulse); //adds jump force to player
            DoubleJump = true; //allow double jump
            
        }

        if (attackDirection == 0)//implement horizontal recoil
        {
            recoilStartTime = Time.time;
            isRecoilingHor = true;
            if (isFacingRight)
            {
                rb.AddForce(Vector2.left * horRecoilSpeed,
                            ForceMode2D.Impulse); //add force in x
            }
            if (!isFacingRight)
            {
                rb.AddForce(Vector2.right * horRecoilSpeed,
                            ForceMode2D.Impulse); //add force in x
            }
        }

    }
}
