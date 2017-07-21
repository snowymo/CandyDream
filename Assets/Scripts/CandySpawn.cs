using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour {

	public GameObject[] candyPrefabs;

	float currentFrame;

	List<GameObject> candies;

	public float randomPosRadius = 2f;

	public Vector3 ceiling;

	// Use this for initialization
	void Start () {
		candies = new List<GameObject> ();

		// Starting in 2 seconds, generateCandy will be launched every 1 seconds
		InvokeRepeating("generateCandy", 2.0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void generateCandy(){
		GameObject candy = GameObject.Instantiate(candyPrefabs[Random.Range(0,candyPrefabs.Length-1)], generatePos(), generateRot(),transform);
//		candy.AddComponent (Rigidbody);
		candy.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		Rigidbody rb = candy.AddComponent<Rigidbody>();
		rb.mass = 2;
		rb.useGravity = true;
//		candy.AddComponent<MeshCollider> ();
		candy.AddComponent<BoxCollider>();
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
