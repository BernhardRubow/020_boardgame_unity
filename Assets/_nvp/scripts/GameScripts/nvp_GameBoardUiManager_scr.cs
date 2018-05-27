using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;

public class nvp_GameBoardUiManager_scr : MonoBehaviour
{

  // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  [SerializeField] private Transform[] _fieldPositions;
  public Transform[] startPositionsRed;
  public Transform[] startPositionsBlack;
  public Transform[] startPositionsYellow;
  public Transform[] startPositionsGreen;
  public Transform[] endPositionsRed;
  public Transform[] endPositionsBlack;
  public Transform[] endPositionsYellow;
  public Transform[] endPositionsGreen;

  [SerializeField] private GameObject _playerMoveSelectorComponent;
  [SerializeField] private GameObject _playerMoveCalculatorComponent;
  private CheckMovesResult _lastCalculatedMoveResult;
  private int _selectedMove;

  // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {

    // subscribe to events
    SubscribeToEvents();
  }

  void Update()
  {

  }

  // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void OnPlayerFigureMoved()
  {
    var playerfigure = _lastCalculatedMoveResult.PlayerFigures.Single(x =>
      x.Color == _lastCalculatedMoveResult.PlayerColor
      && x.Index == _lastCalculatedMoveResult.PossibleMoves[_selectedMove].Index);

    Debug.Log(
      string.Format("Player {0} moves figure {1} to position {2}"
        , _lastCalculatedMoveResult.PossibleMoves[_selectedMove].Color
        , _lastCalculatedMoveResult.PossibleMoves[_selectedMove].Index
        , playerfigure.WorldPosition
      )
    );
  }

  void OnPlayerMoveSected(int index)
  {
    _selectedMove = index;    
  }

  void OnPlayerMovesCalculated(CheckMovesResult result)
  {
    _lastCalculatedMoveResult = result;
  }

  // +++ custom methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void SubscribeToEvents()
  {
    _playerMoveSelectorComponent
      .GetComponent<IPlayerMoveSelector>()
        .OnPlayerMoveSected += OnPlayerMoveSected;

    _playerMoveCalculatorComponent
      .GetComponent<IPlayerMoveCalculator>()
        .OnPlayerMovesCalculated += OnPlayerMovesCalculated;

    _playerMoveCalculatorComponent
      .GetComponent<IPlayerFigureMover>()
        .OnPlayerFigureMoved += OnPlayerFigureMoved;
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
    startPositionsGreen = GetStartPositions("green");

    endPositionsRed = GetStartPositions("red_safe");
    endPositionsBlack = GetStartPositions("black_safe");
    endPositionsYellow = GetStartPositions("yellow_safe");
    endPositionsGreen = GetStartPositions("green_safe");
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

  private void OnSelectTurnButtonClicked(int index)
  {
    Debug.Log(string.Format("Button {0} pressed", index));
  }
}
