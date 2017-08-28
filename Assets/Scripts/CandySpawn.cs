using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour {

	public GameObject[] candyPrefabs;

	float currentFrame;

	List<GameObject> candies;

	public float randomPosRadius = 2f;

	public Vector3 ceiling;

    public float startInNSec;

    public float spawnEveryNSec;

	// Use this for initialization
	void Start () {
		candies = new List<GameObject> ();

		// Starting in 2 seconds, generateCandy will be launched every 1 seconds
		InvokeRepeating("generateCandy", startInNSec, spawnEveryNSec);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void generateCandy(){
		GameObject candy = GameObject.Instantiate(candyPrefabs[Random.Range(0,candyPrefabs.Length-1)], generatePos(), generateRot(),transform);
//		candy.AddComponent (Rigidbody);
		candy.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        Rigidbody rb = candy.GetComponent<Rigidbody>();
        if(rb == null)
		    rb = candy.AddComponent<Rigidbody>();
		rb.mass = 2;
		rb.useGravity = true;
//		candy.AddComponent<MeshCollider> ();
		candy.AddComponent<BoxCollider>();
		candy.name = "candy" + candies.Count.ToString();
		candies.Add(candy);
        //print("candy + " + candy.name + "\t" + candy.transform.position);
	}

	private Vector3 generatePos(){
		//return (Random.insideUnitSphere) * randomPosRadius + ceiling;
        return new Vector3(Random.Range(-2.4f, 2.4f), Random.Range(3.0f, 3.5f), Random.Range(-2.4f, 2.4f));
	}

	private Quaternion generateRot(){
		return Random.rotation;
	}
}
