using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;


public class nvp_PlayerMovesDisplay_scr : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] private Text[] _moveTexts;
	[SerializeField] private GameObject _playerMoveCalculatorComponent;
	 


	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		Clear();

		SubscribeToEvents();
	}
	
	void Update () {
		
	}

	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnPlayerMovesCalculate(CheckMovesResult result)
	{
		// Debug.Log(string.Format("can move: {0}", result.CanMove));
		// Debug.Log(string.Format("add move: {0}", result.AdditionalRollGranted));
		// Debug.Log(string.Format("moves   : {0}", result.PossibleMoves.Count));
		Clear();
		for(int i = 0, n = result.PossibleMoves.Count; i < n; i++){
			_moveTexts[i].text = string.Format(
				"Player {0} can move Figur {1} for {2}"
				, result.PossibleMoves[i].Color
				, result.PossibleMoves[i].Index
				, result.PossibleMoves[i].DiceValue
			);
		}
	}
	



	// +++ custom methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void SubscribeToEvents(){
		_playerMoveCalculatorComponent
			.GetComponent<IPlayerMoveCalculator>()
				.OnPlayerMovesCalculated += OnPlayerMovesCalculate;
	}

	void Clear(){
		foreach(var text in _moveTexts) text.text ="";
	}
}
