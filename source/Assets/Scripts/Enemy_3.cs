using UnityEngine;
using System.Collections;

public class Enemy_3 : EnemyBase
{
    //Player  variables
    Transform playerTransform;
    PlayerControl playerControl;

    //This variables
    public float movementSpeedMax;
    public float movementSpeedMin;
    public float knifeAttackDistance;
    public float shootDistanceMax;
    public float shootDistanceMin;
    public bool isShooting;
    public int Health;

    //Projectile
    public GameObject granadeSmokeParticle;
    private Rigidbody granade;
    public int shootIntervaleMax;
    public int shootIntervaleMin;
    public int numberShoots;
    float shootIntervaleTemp;
    int numberShootsTemp;
    Transform projectileSpawnPoint;
    private float granadeSpeed = 8;
    public float timeToDestroyProjectile = 1f;
    private Transform dynamicEmptyObject;
    private float timeToDestroyGranadeSmokeParticle = 0.01f;

    //Collisions
    bool onPlayerCollision;

    // Use this for initialization
    void Start()
    {
        //shoot
        granade = Resources.Load<Rigidbody>("Granade");
        dynamicEmptyObject = GameObject.FindGameObjectWithTag("DynamicEmptyObject").transform;

        movementSpeedMax = Random.Range(movementSpeedMin, movementSpeedMax);
        shootDistanceMax = Random.Range(shootDistanceMin, shootDistanceMax);


        foreach (Transform child in transform)
        {
            if (child.name == ("ProjectileSpawnPoint"))
                projectileSpawnPoint = child;

        }

        //Player
        onPlayerCollision = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerControl = playerTransform.GetComponent<PlayerControl>();

        //Ignore Collision Layer 8 to 8
        Physics.IgnoreLayerCollision(8, 8);


    }

    // Update is called once per frame
    void Update()
    {
        float granadeRandom = Random.Range(shootIntervaleMin, shootIntervaleMax);
        Invoke("Granade", granadeRandom);
        _setMovementOrientation();
    }

    void _setMovementOrientation()
    {

        //To move
        if (!onPlayerCollision && !isShooting)
        {
            _enemyDirection();
            //print("IsMoving");
        }

        //To knife attack
        if (onPlayerCollision)
        {
            _attackPlayer();
            //print("IsAttacking");
        }

        //Shoot Stance
        if (this.transform.position.x + shootDistanceMax > playerControl.transform.position.x &&
            this.transform.position.x - shootDistanceMax < playerControl.transform.position.x)
        {
            print("In Shoot Distance: " + shootIntervaleTemp + " / " + numberShootsTemp);
            isShooting = true;
            _shootStance();
        }
        else
        {
            isShooting = false;
        }
    }

    //Move to player
    void _enemyDirection()
    {
        //print("Movement Not Mirrored");
        if (playerTransform.position.x > this.transform.position.x)
        {
            this.transform.position += new Vector3(movementSpeedMax * Time.deltaTime, 0, 0);
            if (this.transform.localScale.x != 1)
                this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            //print("Movement Mirrored");
            this.transform.position -= new Vector3(movementSpeedMax * Time.deltaTime, 0, 0);
            if (this.transform.localScale.x != -1)
                this.transform.localScale = new Vector3(-1, 1);
        }
    }

    //Knife attack
    void _attackPlayer()
    {

        if (playerTransform.position.x > this.transform.position.x)
        {
            //print("Attack Not Mirrored");
            if (this.transform.localScale.x != 1)
                this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            //print("Attack Mirrored");
            if (this.transform.localScale.x != -1)
                this.transform.localScale = new Vector3(-1, 1);

        }
    }

    //Shooting stance
    void _shootStance()
    {
        //print("Shoot Not Mirrored");
        if (playerTransform.position.x > this.transform.position.x)
        {
            //print("Shoot Not Mirrored");
            if (this.transform.localScale.x != 1)
                this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            //print("Shoot Mirrored");
            if (this.transform.localScale.x != -1)
                this.transform.localScale = new Vector3(-1, 1);
        }
    }


    void Granade()
    {
        if (this.transform.position.x + shootDistanceMax > playerControl.transform.position.x &&
            this.transform.position.x - shootDistanceMax < playerControl.transform.position.x)
        {

            // SHAKE
            //cameraShake.Shake();

            // FIRE
            Rigidbody instantiatedGranade;
            GameObject instantiatedParticlesSmoke;
            float height = 5;

            if (transform.localScale.x < 0)
            {
                //instantiatedParticlesSmoke = (GameObject)Instantiate(granadeSmokeParticle, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

                instantiatedGranade = (Rigidbody)Instantiate(granade, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                instantiatedGranade.transform.rotation = Quaternion.Euler(0, 0, -90);
                instantiatedGranade.velocity = transform.TransformDirection(new Vector3(-granadeSpeed, height, 0));

                instantiatedGranade.transform.parent = dynamicEmptyObject;
                //instantiatedParticlesSmoke.transform.parent = projectileSpawnPoint;

                //				// KICK GUN
                //				playerControl.KickGun(granadeKickForce);
            }
            else
            {
                //instantiatedParticlesSmoke = (GameObject)Instantiate(granadeSmokeParticle, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

                instantiatedGranade = (Rigidbody)Instantiate(granade, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                instantiatedGranade.transform.rotation = Quaternion.Euler(0, 0, 90);
                instantiatedGranade.velocity = transform.TransformDirection(new Vector3(granadeSpeed, height, 0));

                instantiatedGranade.transform.parent = dynamicEmptyObject;
                //instantiatedParticlesSmoke.transform.parent = projectileSpawnPoint;

                //				// KICK GUN
                //				playerControl.KickGun(-granadeKickForce);
            }
        }
        CancelInvoke("Granade");
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == ("Player"))
        {
            //print ("OnPlayerCollision");
            onPlayerCollision = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == ("Player"))
        {
            //print ("OnPlayerCollision");
            onPlayerCollision = false;
        }
    }
}
