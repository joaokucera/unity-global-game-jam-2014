using UnityEngine;
using System.Collections;

public class Spawn_Enemy_1 : MonoBehaviour {

	public int enemyCount;
	public float spawnDelay;
	float spawnCount;
	bool spawn = false;

	GameObject enemy_1;
	
	// Use this for initialization
	void Start () {
		enemy_1 = Resources.Load<GameObject>("Enemy_1");
	}
	
	// Update is called once per frame
	void Update () {
		if(spawn){
			_spawnEnemies();
		}
	}

	void _spawnEnemies(){
		if(Time.time > spawnCount && enemyCount > 0){
			//print (" "+ spawnCount + " / " + enemyCount + " .");
			Instantiate(enemy_1,transform.position,transform.localRotation);
			spawnCount = Time.time+spawnDelay;
			enemyCount--;
		}
	}

	void OnTriggerEnter(Collider coll){
		if(coll.tag == "Player" && !spawn){
			spawn = true;
		}
	}

}
