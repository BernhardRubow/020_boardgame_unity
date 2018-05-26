using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

public class nvp_DiceDisplay_scr : MonoBehaviour {

	public Text displayText;
	public GameObject  iDiceRollerParent;
	private IDiceRoller _iDiceRoller;
	// Use this for initialization
	void Start () {
		_iDiceRoller = iDiceRollerParent.GetComponent<IDiceRoller>();
		_iDiceRoller.OnDiceRollHappened += OnDiceRolled;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDiceRolled(PlayerDiceRoll diceRoll){
		displayText.text = string.Format("Player {0} rolls a {1}", diceRoll.playerColor, diceRoll.diceValue);
		switch(diceRoll.playerColor){
			case PlayerColors.red:
				displayText.color = Color.red;
				break;
				
			case PlayerColors.black:
				displayText.color = Color.black;
				break;
				
			case PlayerColors.yellow:
				displayText.color = Color.yellow;
				break;
				
			case PlayerColors.green:
				displayText.color = Color.green;
				break;
		}
	}
}
