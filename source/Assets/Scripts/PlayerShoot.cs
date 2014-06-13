using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour
{
    public AudioClip shotClip;

    public GameObject gunParticleFire;
    public GameObject gunParticleFlash;
    private ParticleSystem granadeParticleSmoke;

    public Transform leftAimTransform;
    public Transform rightAimTransform;

    private Rigidbody projectile;
    private Rigidbody granade;

    private Transform dynamicEmptyObject;
    private PlayerControl playerControl;

    private float particleSpeed = 50;
    private float granadeSpeed = 8;

    private float timeToDestroyProjectile = 1f;
    private float timeToDestroyGranade = 2f;

    private float timeToDestroyGunParticleFire = 0.1f;
    private float timeToDestroyGunParticleFlash = 0.1f;
    private float timeToDestroyGranadeSmokeParticle = 0.01f;

    //private CameraShake cameraShake;

    public float particleKickForce = 2f;
    public float granadeKickForce = 100f;
    private int granadeAmount = 5;

    private PlayerProperties playerProperties;

    void Start()
    {
        projectile = Resources.Load<Rigidbody>("Projectile");
        granade = Resources.Load<Rigidbody>("Granade");
        granadeParticleSmoke = Resources.Load<ParticleSystem>("GranadeSmokeParticle");

        dynamicEmptyObject = GameObject.FindGameObjectWithTag("DynamicEmptyObject").transform;
        playerControl = GetComponent<PlayerControl>();

        //cameraShake = Camera.main.GetComponent<CameraShake>();

        playerProperties = GetComponent<PlayerProperties>();
    }

    void Update()
    {
        // Machine GUn
        if (Input.GetButtonDown("Fire1"))
        {
            InvokeRepeating("Fire", 0.2f, 0.1f);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            playerProperties.characterState = CharacterState.Walk;
            playerProperties.changeCharacter = true;

            CancelInvoke("Fire");
        }

        // Granade
        if (Input.GetButtonDown("Fire2"))
        {
            Granade();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            playerProperties.characterState = CharacterState.Walk;
            playerProperties.changeCharacter = true;
        }
    }

    void Granade()
    {
        if (granadeAmount > 0)
        {
            //playerProperties.characterState = CharacterState.Shot;
            //playerProperties.changeCharacter = true;

            if (playerControl.velocity.x == 0)
            {
                playerProperties.characterState = CharacterState.IdleShot;
            }
            else if (playerControl.velocity.y != 0)
            {
                playerProperties.characterState = CharacterState.JumpShot;
            }
            else
            {
                playerProperties.characterState = CharacterState.Shot;
            }

            playerProperties.changeCharacter = true;

            // SHAKE
            //cameraShake.Shake();

            // FIRE
            Rigidbody instantiatedGranade;
            ParticleSystem instantiatedParticlesSmoke;
            float height = 2;

            if (playerControl.moveDirection == MoveDirection.LEFT)
            {
                instantiatedParticlesSmoke = (ParticleSystem)Instantiate(granadeParticleSmoke, leftAimTransform.position, leftAimTransform.rotation);
                instantiatedParticlesSmoke.transform.parent = leftAimTransform;

                instantiatedGranade = (Rigidbody)Instantiate(granade, leftAimTransform.position, leftAimTransform.rotation);
                instantiatedGranade.transform.rotation = Quaternion.Euler(0, 0, -90);
                instantiatedGranade.velocity = transform.TransformDirection(new Vector3(-granadeSpeed, height, 0));
                instantiatedGranade.transform.parent = dynamicEmptyObject;

                // KICK GUN
                playerControl.KickGun(granadeKickForce);
            }
            else
            {
                instantiatedParticlesSmoke = (ParticleSystem)Instantiate(granadeParticleSmoke, rightAimTransform.position, rightAimTransform.rotation);
                instantiatedParticlesSmoke.transform.parent = rightAimTransform;

                instantiatedGranade = (Rigidbody)Instantiate(granade, rightAimTransform.position, rightAimTransform.rotation);
                instantiatedGranade.transform.rotation = Quaternion.Euler(0, 0, 90);
                instantiatedGranade.velocity = transform.TransformDirection(new Vector3(granadeSpeed, height, 0));
                instantiatedGranade.transform.parent = dynamicEmptyObject;

                // KICK GUN
                playerControl.KickGun(-granadeKickForce);
            }

            Destroy(instantiatedGranade.gameObject, timeToDestroyGranade);
            Destroy(instantiatedParticlesSmoke.gameObject, timeToDestroyGranadeSmokeParticle);

            granadeAmount--;
        }
        if (granadeAmount <= 0)
        {
            granadeAmount = 0;
        }
    }

    void Fire()
    {
        if (playerControl.velocity.x == 0)
        {
            playerProperties.characterState = CharacterState.IdleShot;
        }
        else if (playerControl.velocity.y != 0)
        {
            playerProperties.characterState = CharacterState.JumpShot;
        }
        else
        {
            playerProperties.characterState = CharacterState.Shot;
        }

        playerProperties.changeCharacter = true;

        // SHAKE
        //cameraShake.Shake();

        // FIRE
        Rigidbody instantiatedProjectile;
        GameObject instantiatedParticlesFire;
        GameObject instantiatedParticlesFlash;

        int random = Random.Range(-5, 5);

        if (playerControl.moveDirection == MoveDirection.LEFT)
        {
            instantiatedParticlesFire = (GameObject)Instantiate(gunParticleFire, leftAimTransform.position, leftAimTransform.rotation);
            instantiatedParticlesFire.transform.parent = leftAimTransform;

            instantiatedParticlesFlash = (GameObject)Instantiate(gunParticleFlash, leftAimTransform.position, leftAimTransform.rotation);
            instantiatedParticlesFlash.transform.rotation = Quaternion.Euler(0, -90, 0);
            instantiatedParticlesFlash.transform.parent = leftAimTransform;

            instantiatedProjectile = (Rigidbody)Instantiate(projectile, leftAimTransform.position, leftAimTransform.rotation);
            instantiatedProjectile.transform.rotation = Quaternion.Euler(0, 0, -90);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(-particleSpeed, random, 0));
            instantiatedProjectile.transform.parent = dynamicEmptyObject;

            // KICK GUN
            playerControl.KickGun(particleKickForce);
        }
        else
        {
            instantiatedParticlesFire = (GameObject)Instantiate(gunParticleFire, rightAimTransform.position, rightAimTransform.rotation);
            instantiatedParticlesFire.transform.parent = rightAimTransform;

            instantiatedParticlesFlash = (GameObject)Instantiate(gunParticleFlash, rightAimTransform.position, rightAimTransform.rotation);
            instantiatedParticlesFlash.transform.rotation = Quaternion.Euler(0, 90, 0);
            instantiatedParticlesFlash.transform.parent = rightAimTransform;

            instantiatedProjectile = (Rigidbody)Instantiate(projectile, rightAimTransform.position, rightAimTransform.rotation);
            instantiatedProjectile.transform.rotation = Quaternion.Euler(0, 0, 90);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(particleSpeed, random, 0));
            instantiatedProjectile.transform.parent = dynamicEmptyObject;

            // KICK GUN
            playerControl.KickGun(-particleKickForce);
        }

        Destroy(instantiatedProjectile.gameObject, timeToDestroyProjectile);
        Destroy(instantiatedParticlesFire, timeToDestroyGunParticleFire);
        Destroy(instantiatedParticlesFlash, timeToDestroyGunParticleFlash);

        audio.PlayOneShot(shotClip);
    }
}