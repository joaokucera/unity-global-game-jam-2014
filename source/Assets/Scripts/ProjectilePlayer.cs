using UnityEngine;
using System.Collections;

public class ProjectilePlayer : MonoBehaviour {

	GameObject enemy;
	EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		if(coll.tag == "Enemy"){
			enemy = coll.gameObject;
			enemyHealth = enemy.GetComponent<EnemyHealth>();
			enemyHealth.enemyHealth--;
			Destroy(gameObject);
		}
	}
}
