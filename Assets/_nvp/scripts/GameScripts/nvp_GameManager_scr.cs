using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.gameLogic;


public class nvp_GameManager_scr : MonoBehaviour {

	public GameObject IDiceRoller;
	private IDiceRoller _iDiceRoller;
	private nvp_GameBoard_class _gameLogic;


	// Use this for initialization
	void Start () {
		_iDiceRoller = IDiceRoller.GetComponent<IDiceRoller>();	
		_iDiceRoller.OnDiceRollHappened += OnPlayerDiceRoll;

		_gameLogic = new nvp_GameBoard_class(4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPlayerDiceRoll(PlayerDiceRoll diceRoll){
		Debug.Log(string.Format("dice value: {0}", diceRoll.diceValue));
		Debug.Log(string.Format("PlayerColor: {0}", diceRoll.playerColor));
	}
}
