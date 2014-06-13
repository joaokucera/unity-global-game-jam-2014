using UnityEngine;
using System.Collections;

public class Spawn_Enemy_2 : MonoBehaviour {
	
	public int enemyCount;
	public float spawnDelay;
	float spawnCount;
	bool spawn = false;
	
	GameObject enemy_2;
	
	// Use this for initialization
	void Start () {
		enemy_2 = Resources.Load<GameObject>("Enemy_2");
	}
	
	// Update is called once per frame
	void Update () {
		if(spawn){
			_spawnEnemies();
		}
	}
	
	void _spawnEnemies(){
		if(Time.time > spawnCount && enemyCount > 0){
			print (" "+ spawnCount + " / " + enemyCount + " .");
			Instantiate(enemy_2,transform.position,transform.localRotation);
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
