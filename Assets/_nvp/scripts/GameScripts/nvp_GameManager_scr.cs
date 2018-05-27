using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.gameLogic;
using newvisionsproject.boardgame.delegates;
using newvisionsproject.boardgame.dto;

public class nvp_GameManager_scr : MonoBehaviour, IPlayerMoveCalculator, IPlayerFigureMover
{

  // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  public GameObject DiceRollerComponent;
  public GameObject MoveSelectorComponent;
  private nvp_GameBoard_class _gameLogic;
  private CheckMovesResult _lastCalculatedMoveResult;


  // +++ events exposed +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  public event PlayerMovesCalculatedDelegate OnPlayerMovesCalculated = delegate { };
	public event PlayerFigureMovedDelegate OnPlayerFigureMoved = delegate {};


  // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    SubscribeToEvents();

    _gameLogic = new nvp_GameBoard_class(4);
  }


  // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnPlayerMoveSected(int index){
		_gameLogic.Move(_lastCalculatedMoveResult, index);

		// invoke event
		OnPlayerFigureMoved();
	}
  void OnPlayerDiceRoll(PlayerDiceRoll diceRoll)
  {
    _lastCalculatedMoveResult = _gameLogic.CheckPossiblePlayerMoves(
      diceRoll.playerColor,
      diceRoll.diceValue
    );

		// invoke event
    OnPlayerMovesCalculated(_lastCalculatedMoveResult);
  }


  // +++ custom methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  private void SubscribeToEvents()
  {
    DiceRollerComponent
			.GetComponent<IDiceRoller>()
    		.OnDiceRollHappened += OnPlayerDiceRoll;

		MoveSelectorComponent
			.GetComponent<IPlayerMoveSelector>()
				.OnPlayerMoveSected += OnPlayerMoveSected;
  }
}
