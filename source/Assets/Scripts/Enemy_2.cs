using UnityEngine;
using System.Collections;

public class Enemy_2 : EnemyBase
{
    //Player  variables
    Transform playerTransform;
    PlayerControl playerControl;

    //This variables
    public float movementSpeedMax;
	public float movementSpeedMin;
    public float attackDistance;
    public float shootDistanceMax;
	public float shootDistanceMin;
    public bool isShooting;
    public int Health;

    //Projectile
    private Rigidbody projectile;
    public int shootIntervaleMax;
	public int shootIntervaleMin;
    public int numberShoots;
    float shootIntervaleTemp;
    int numberShootsTemp;
    Transform projectileSpawnPoint;
    public float shootSpeed = 50;
    public float timeToDestroyProjectile = 1f;
    private Transform dynamicObjects;

    //Collisions
    bool onPlayerCollision;

    // Use this for initialization
    void Start()
    {
        //shoot
        projectile = Resources.Load<Rigidbody>("ProjectileEnemy");
        dynamicObjects = GameObject.FindGameObjectWithTag("DynamicEmptyObject").transform;

		movementSpeedMax = Random.Range(movementSpeedMin,movementSpeedMax);
		shootDistanceMax = Random.Range(shootDistanceMin,shootDistanceMax);
		shootIntervaleMax = Random.Range(shootIntervaleMin,shootIntervaleMax);
		
        InvokeRepeating("_toShoot", shootIntervaleMax, 1);

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

    //Instantiate shoot
    void _toShoot()
    {
        //if (shootIntervaleTemp > Time.time) {
        if (this.transform.position.x + shootDistanceMax > playerControl.transform.position.x &&
           this.transform.position.x - shootDistanceMax < playerControl.transform.position.x)
        {
            //if (numberShootsTemp > 0) {


            int random = Random.Range(-1, 1);

            Rigidbody instantiatedProjectile;

            if (transform.localScale.x < 0)
            {
                instantiatedProjectile = (Rigidbody)Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.localRotation);
                instantiatedProjectile.transform.rotation = Quaternion.Euler(0, 0, -90);
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(-shootSpeed, random, 0));

            }
            else
            {
                instantiatedProjectile = (Rigidbody)Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                instantiatedProjectile.transform.rotation = Quaternion.Euler(0, 0, 90);
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(shootSpeed, random, 0));
            }


            instantiatedProjectile.transform.parent = dynamicObjects;
            //numberShootsTemp--;

            //}
        }
        //print("In Shoot Distance: " + shootIntervaleTemp + " / " + numberShootsTemp);
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
