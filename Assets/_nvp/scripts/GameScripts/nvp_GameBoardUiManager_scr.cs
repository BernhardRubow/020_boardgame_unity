using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class nvp_GameBoardUiManager_scr : MonoBehaviour {

	[SerializeField] private Transform[] _fieldPositions;
	public Transform[] startPositionsRed;
	public Transform[] startPositionsBlack;
	public Transform[] startPositionsYellow;
	public Transform[] startPositionsGreen;
	public Transform[] endPositionsRed;
	public Transform[] endPositionsBlack;
	public Transform[] endPositionsYellow;
	public Transform[] endPositionsGreen;

  [SerializeField] private nvp_SelectTurnUiManager_scr _selectTurnUI;

	// Use this for initialization
	void Start () {
		
    // subscribe to events
    _selectTurnUI.OnButtonClicked += OnSelectTurnButtonClicked;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Reset()
  {
    // get field positions on board
    var fieldList = GameObject.FindGameObjectsWithTag("field");
    _fieldPositions = new Transform[40];
    for (int i = 0, n = 40; i < n; i++)
    {
      _fieldPositions[i] = fieldList.Single(x => x.name.Contains(i.ToString("00"))).transform;
    }
    
    startPositionsRed = GetStartPositions("red");
    startPositionsBlack = GetStartPositions("black");
    startPositionsYellow = GetStartPositions("yellow");
    startPositionsGreen =GetStartPositions("green");
    
    endPositionsRed = GetStartPositions("red_safe");
    endPositionsBlack = GetStartPositions("black_safe");
    endPositionsYellow = GetStartPositions("yellow_safe");
    endPositionsGreen =GetStartPositions("green_safe");
  }

  private Transform[] GetStartPositions(string color)
  {
		var startPositions = new Transform[4];
    for (int i = 0, n = 4; i < n; i++)
    {
      startPositions[i] = GameObject.Find(color + "_" + i.ToString()).transform;
    }
		return startPositions;
  }

  private void OnSelectTurnButtonClicked(int index){
    Debug.Log(string.Format("Button {0} pressed", index));
  }



}
