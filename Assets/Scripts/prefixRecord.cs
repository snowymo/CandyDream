using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefixRecord : MonoBehaviour {

    public string identification;

    public ThemeSelection curTheme;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string generatePrefix()
    {
        return identification + "_" + curTheme.currentTheme.ToString() + "_";
    }
}
