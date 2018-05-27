using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.boardgame.delegates;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.interfaces;

public class nvp_SelectTurnUiManager_scr : MonoBehaviour, IPlayerMoveSelector	 {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] private Button[] _selectButtons;
	[SerializeField] private GameObject PlayerMoveCalculatorComponent;
	private IPlayerMoveCalculator _playerMoveCalculator;

  
  // +++ events exposed +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public event PlayerMoveSelectorDelegate OnPlayerMoveSected;


  // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start () {
		SubscribeToEvents();
		
	}


	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void OnSelectButtonClicked(int index){
		DeactivateButtons();
		if(OnPlayerMoveSected != null) OnPlayerMoveSected(index);
	}

	private void OnPlayerMovesCalculated(CheckMovesResult result){
		DeactivateButtons();
		if(result.CanMove) ActivateButtons(result.PossibleMoves.Count);
	}


	// +++ custom methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void SubscribeToEvents(){
		_playerMoveCalculator = PlayerMoveCalculatorComponent
			.GetComponent<IPlayerMoveCalculator>();
		
		_playerMoveCalculator.OnPlayerMovesCalculated += OnPlayerMovesCalculated;
	}

	public void ActivateButtons(int numberOfButtons){

		for(int i = 0, n = _selectButtons.Length; i < n; i++){
			_selectButtons[i].gameObject.SetActive(false);
		}

		for(int i = 0, n = numberOfButtons; i < n; i++){
			_selectButtons[i].gameObject.SetActive(true);
		}
	}

	public void DeactivateButtons(){
		for(int i = 0, n = 4; i < n; i++) {
			_selectButtons[i].gameObject.SetActive(false);
		}
	}
}
