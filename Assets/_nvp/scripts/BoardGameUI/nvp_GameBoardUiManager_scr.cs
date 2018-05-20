using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class nvp_GameBoardUiManager_scr : MonoBehaviour {

	public Transform[] fieldPositions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Reset(){
		var fieldList = GameObject.FindGameObjectsWithTag("field");
		Debug.Log(fieldList.Count());
		fieldPositions = new Transform[40];
		for(int i = 0, n = 40; i < n; i++){
			fieldPositions[i] = fieldList.Single(x => x.name.Contains(i.ToString("00"))).transform;
		}
	}
}
