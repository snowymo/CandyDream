using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTheme : MonoBehaviour {

    public ThemeSelection themeSelector;

	public GameObject[] prefabsInThemes;

	public GameObject curObj;

	public string name;

	public Transform tracked_transform;

	// Use this for initialization
	void Start () {
		//print (currentTheme);
		//print((int)currentTheme);
		curObj = GameObject.Instantiate (prefabsInThemes[(int)themeSelector.currentTheme]);
		curObj.transform.parent = transform;
		curObj.name = name;
		TransformAssignment ta = curObj.AddComponent<TransformAssignment> ();
		ta.tracked_transform = tracked_transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
