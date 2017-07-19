using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour {

	public GameObject candyPrefab;

	float currentFrame;

	List<GameObject> candies;

	public float randomPosRadius = 15f;

	public Vector3 ceiling;

	// Use this for initialization
	void Start () {
		candies = new List<GameObject> ();

		// Starting in 2 seconds, generateCandy will be launched every 1 seconds
		InvokeRepeating("generateCandy", 2.0f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void generateCandy(){
		GameObject candy = GameObject.Instantiate(candyPrefab, generatePos(), generateRot(),transform);
//		candy.AddComponent (Rigidbody);
		candy.name = "candy" + candies.Count.ToString();
		candies.Add(candy);
	}

	private Vector3 generatePos(){
		return (Random.insideUnitSphere) * randomPosRadius + ceiling;
	}

	private Quaternion generateRot(){
		return Random.rotation;
	}
}
