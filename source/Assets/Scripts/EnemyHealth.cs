using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int enemyHealth = 10;

	GameObject player;
	PlayerProperties pProperties;
	
	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		pProperties = player.GetComponent<PlayerProperties>();

	}
	
	// Update is called once per frame
	void Update () {
		if(enemyHealth < 1){
			Destroy(gameObject);
			pProperties.bodyCount++;
		}
	
	}
}
