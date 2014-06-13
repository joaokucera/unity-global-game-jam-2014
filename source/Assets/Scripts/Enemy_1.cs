using UnityEngine;
using System.Collections;

public class Enemy_1 : EnemyBase
{
    //Player  variables
    Transform playerTransform;
    //PlayerControl playerControl;

    //This variables
    public float movementSpeedMax;
	public float movementSpeedMin;
    public float attackDistance;

    //Collisions
    bool onPlayerCollision;

    // Use this for initialization
    void Start()
    {
        onPlayerCollision = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //playerControl = playerTransform.GetComponent<PlayerControl>();

		movementSpeedMax = Random.Range(movementSpeedMin,movementSpeedMax);

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
        if (!onPlayerCollision)
        {
            _enemyDirection();
            //print("IsMoving");
        }

        if (onPlayerCollision)
        {
            _attackPlayer();
            //print("IsAttacking");
        }
    }

    void _enemyDirection()
    {
        // print("Movement Not Mirrored");
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

    void _attackPlayer()
    {
        // print("Attack Not Mirrored");
        if (playerTransform.position.x > this.transform.position.x)
        {
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

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == ("Player"))
        {
            //print("OnPlayerCollision");
            onPlayerCollision = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == ("Player"))
        {
            //print("OnPlayerCollision");
            onPlayerCollision = false;
        }
    }
}
