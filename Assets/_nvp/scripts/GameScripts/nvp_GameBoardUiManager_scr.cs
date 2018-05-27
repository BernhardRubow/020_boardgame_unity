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
  private int _selectedMoveIndex;

  // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    SubscribeToEvents();
  }


  // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void OnPlayerFigureMoved()
  {
    var moveToMake = _lastCalculatedMoveResult.PossibleMoves[_selectedMoveIndex];

    var playerfigure = _lastCalculatedMoveResult.PlayerFigures.Single(
        x => x.Color == moveToMake.Color
        && x.Index == moveToMake.Index); 

    var playerFigureTransform = GameObject
      .Find(string.Format("player_{0}_{1}",moveToMake.Color,moveToMake.Index))
      .transform;

    playerFigureTransform.parent = _fieldPositions[playerfigure.WorldPosition];
    playerFigureTransform.localPosition = Vector3.zero;
    


    Debug.Log(
      string.Format("Player {0} moves figure {1} to position {2}"
        , moveToMake.Color
        , string.Format("player_{0}_{1}",moveToMake.Color,moveToMake.Index)
        , playerfigure.WorldPosition
      )
    );
  }

  void OnPlayerMoveSected(int index)
  {
    _selectedMoveIndex = index;    
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
