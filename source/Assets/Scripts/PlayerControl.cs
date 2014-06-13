using UnityEngine;
using System.Collections;

public enum MoveDirection
{
    LEFT = 0,
    RIGHT = 1
}

[AddComponentMenu("Tools / Scripts / Player Control")]
public class PlayerControl : MonoBehaviour
{
    #region Fields

    public AudioClip jumpClip;

    public float walkSpeed = 1.5f;
    public float runSpeed = 2f;
    public float fallSpeed = 2f;

    public float walkJump = 6.2f;
    public float runJump = 9f;
    public float fallJump = 0.1f;
    public float centerY = -0.5f;

    public float gravity = 20f;
    public float startPosition = 0f;

    public MoveDirection moveDirection = MoveDirection.RIGHT;
    public GameObject particleJump;

    public AudioClip soundJump;
    public AudioClip soundFallJump;

    public Vector3 velocity = Vector3.zero;

    private bool jumpEnable = false;
    private bool runJumpEnable = false;
    private bool fallJumpEnable = false;

    private float afterHitForceDown = 1f;

    private CharacterController controller;
    private AnimationSprite sprite;
    //private PlayerSound playerSoundScript;

    private float scaleX = -1.5f;
    private float scaleY = -1.5f;
    public Transform graphics;

    public float startPosY;

    private LayerMask playerLayerMask;
    private LayerMask platformLayerMask;
    //private LayerMask groundLayerMask;

    private PlayerProperties playerProperties;

    #endregion

    #region Methods

    void Start()
    {
        playerLayerMask = LayerMask.NameToLayer("Player");
        platformLayerMask = LayerMask.NameToLayer("Platforms");
        //groundLayerMask = LayerMask.NameToLayer("Ground");

        controller = GetComponent<CharacterController>();
        controller.center = new Vector3(0, centerY, 0);

        graphics.transform.localScale = new Vector3(scaleX, scaleY, 0.1f);
        //transform.localScale = new Vector3(scaleX, scaleY, 0.1f);

        sprite = GetComponent<AnimationSprite>();
        sprite.AnimateSprite(4, 4, 0, 0, 14, 24);

        //playerSoundScript = GetComponent<PlayerSound>();
        playerProperties = GetComponent<PlayerProperties>();
    }

    void Update()
    {
        Vector3 particlePlacement = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z - 0.1f);

        if (controller.isGrounded)
        {
            jumpEnable = false;
            runJumpEnable = false;

            startPosY = transform.position.y;
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

            if (fallJumpEnable) // FALL JUMP
            {
                controller.center = new Vector3(0, centerY, 0);
                fallJumpEnable = false;
            }
            if (velocity.x == 0) // IDLE
            {
                if (playerProperties.characterState != CharacterState.Dead) //&&
                    //playerProperties.characterState != CharacterState.IdleShot &&
                    //playerProperties.characterState != CharacterState.Shot &&
                    //playerProperties.characterState != CharacterState.JumpShot)
                {
                    playerProperties.characterState = CharacterState.Idle;
                    playerProperties.changeCharacter = true;
                }

                sprite.AnimateSprite(4, 4, 0, 0, 14, 24);
            }
            if (velocity.x != 0) // WALK
            {
                if (playerProperties.characterState != CharacterState.Dead) //&&
                    //playerProperties.characterState != CharacterState.Shot)
                {
                    playerProperties.characterState = CharacterState.Walk;
                    playerProperties.changeCharacter = true;
                }

                velocity *= walkSpeed;
                sprite.AnimateSprite(4, 4, 0, 0, 14, 24);
            }
            if (velocity.x != 0 && Input.GetButton("Fire3")) // RUN
            {
                if (playerProperties.characterState != CharacterState.Dead)// &&
                    //playerProperties.characterState != CharacterState.Shot)
                {
                    playerProperties.characterState = CharacterState.Run;
                    playerProperties.changeCharacter = true;
                }

                velocity *= runSpeed;
                sprite.AnimateSprite(4, 4, 0, 0, 14, 24);
            }
            //if (velocity.x == 0 && Input.GetAxis("Vertical") < 0) // FALL DOWN
            //{
            //    if (playerProperties.characterState != CharacterState.Dead) //&&
            //        //playerProperties.characterState != CharacterState.JumpShot)
            //    {
            //        Debug.Log("4");
            //        playerProperties.characterState = CharacterState.Jump;
            //        playerProperties.changeCharacter = true;
            //    }

            //    velocity.x = 0;
            //    sprite.AnimateSprite(4, 4, 0, 0, 14, 36);
            //}
            if ((Input.GetButtonDown("Jump") || (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)) &&
                (!Input.GetButton("Fire3") || Input.GetButton("Fire3") && velocity.x == 0) &&
                Input.GetAxis("Vertical") >= 0) // WALK JUMP
            {
                DoJump(walkJump, particlePlacement, ref jumpEnable);
            }
            if ((Input.GetButtonDown("Jump") || (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)) &&
                Input.GetButton("Fire3") && velocity.x != 0) // RUN JUMP
            {
                DoJump(runJump, particlePlacement, ref runJumpEnable);
            }
            if ((Input.GetButtonDown("Jump") || (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)) &&
                velocity.x == 0 &&
                Input.GetAxis("Vertical") < 0) // FALL JUMP
            {
                DoFall(fallJump, ref fallJumpEnable);
            }
        }
        if (!controller.isGrounded)
        {
            velocity.x = Input.GetAxis("Horizontal");

            if (Input.GetButtonUp("Jump") && Input.GetButtonUp("Vertical"))
            {
                velocity.y -= fallSpeed;
            }
            if (jumpEnable) // STANDARD JUMP
            {
                if (playerProperties.characterState != CharacterState.Dead)// &&
                    //playerProperties.characterState != CharacterState.JumpShot)
                {
                    playerProperties.characterState = CharacterState.Jump;
                    playerProperties.changeCharacter = true;
                }

                velocity.x *= walkSpeed;
                sprite.AnimateSprite(4, 4, 0, 0, 14, 24);
            }
            if (runJumpEnable) // RUN JUMP
            {
                if (playerProperties.characterState != CharacterState.Dead)// &&
                    //playerProperties.characterState != CharacterState.JumpShot)
                {
                    playerProperties.characterState = CharacterState.Jump;
                    playerProperties.changeCharacter = true;
                }

                velocity.x *= walkJump / 1.5f;
                sprite.AnimateSprite(4, 4, 0, 0, 14, 24);
            }
            if (fallJumpEnable) // FALL JUMP
            {
                if (playerProperties.characterState != CharacterState.Dead)// &&
                    //playerProperties.characterState != CharacterState.JumpShot)
                {
                    playerProperties.characterState = CharacterState.Jump;
                    playerProperties.changeCharacter = true;
                }

                velocity.x *= walkSpeed;
                sprite.AnimateSprite(4, 4, 0, 0, 14, 24);
            }
        }
        if (velocity.x < 0) // GET LAST MOVE DIRECTION (LEFT)
        {
            graphics.transform.localScale = new Vector3(-scaleX, scaleY, 0.1f);
            moveDirection = MoveDirection.LEFT;
        }
        if (velocity.x > 0) // GET LAST MOVE DIRECTION (RIGHT)
        {
            graphics.transform.localScale = new Vector3(scaleX, scaleY, 0.1f);
            moveDirection = MoveDirection.RIGHT;
        }
        if (controller.collisionFlags == CollisionFlags.Above)
        {
            velocity.y = 0;
            velocity.y -= afterHitForceDown;
        }

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void DoFall(float jump, ref bool kindOfJumpEnable)
    {
        //if (controller.collisionFlags == CollisionFlags.Below)
        //{
        //    if (Physics.Raycast(transform.position, -transform.up, 0.1f, platformLayerMask))
        //    {
        //        //Debug.Log("HEY");

        //        controller.center = -new Vector3(0, jump, 0);

        //        kindOfJumpEnable = true;
        //    }
        //}
    }

    private void DoJump(float jump, Vector3 particlePlacement, ref bool kindOfJumpEnable)
    {
        velocity.y = jump;

        if (particleJump)
        {
            GameObject particle = (GameObject)Instantiate(particleJump, particlePlacement, transform.rotation);
            Destroy(particle, 1f);
        }

        //playerSoundScript.PlaySound(soundJump, 0);
        audio.PlayOneShot(jumpClip);
        kindOfJumpEnable = true;

        JumpPlatforms(true);
        Invoke("ReactiveLayerCollision", 0.5f);
    }

    void ReactiveLayerCollision()
    {
        JumpPlatforms(false);
    }

    private void JumpPlatforms(bool ignoreLayer)
    {
        Physics.IgnoreLayerCollision(playerLayerMask, platformLayerMask, ignoreLayer);
    }

    public void KickGun(float movement)
    {
        controller.Move(new Vector3(movement, 0, 0) * Time.deltaTime);
    }

    #endregion
}
