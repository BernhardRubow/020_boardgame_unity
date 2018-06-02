using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

public class nvp_GameBoardUiManager_scr : MonoBehaviour
{

  // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++  
  public Transform[] startPositionsRed;                             // positions in the red house
  public Transform[] startPositionsBlack;                           // positions in the black house
  public Transform[] startPositionsYellow;                          // positions in the yellow house
  public Transform[] startPositionsGreen;                           // positions in the green house
  public Transform[] endPositionsRed;                               // positions in red safe zone 
  public Transform[] endPositionsBlack;                             // positions in black safe zone
  public Transform[] endPositionsYellow;                            // positions in yellow safe zone
  public Transform[] endPositionsGreen;                             // positions in green safe zone
  public Transform[] playerFigures;                                 // reference to player figures
  [SerializeField] private Transform[] _fieldPositions;             // all positions of the track on the gameboard
  [SerializeField] private GameObject _playerMoveSelectorComponent;   // holds IPlayerMoveSelector implementation
  [SerializeField] private GameObject _playerMoveCalculatorComponent; // holds IPlayerMoveCalculator and IPlayerFigureMover
  private CheckMovesResult _lastCalculatedMoveResult;               // stores the last calculated move result for a player
  private int _selectedMoveIndex;                                   // stores the last selected move

  // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    SubscribeToEvents();
  }


  // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void OnPlayerFigureMoved()
  {
    MoveAllPlayerFigures();

  }

  void OnPlayerMoveSected(int index)
  {
    _selectedMoveIndex = index;    
  }

  void OnPlayerMovesCalculated(CheckMovesResult result)
  {
    _lastCalculatedMoveResult = result;
  }

  private void OnSelectTurnButtonClicked(int index)
  {
    Debug.Log(string.Format("Button {0} pressed", index));
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

    startPositionsRed = GetSpecialFieldPositionByName("red");
    startPositionsBlack = GetSpecialFieldPositionByName("black");
    startPositionsYellow = GetSpecialFieldPositionByName("yellow");
    startPositionsGreen = GetSpecialFieldPositionByName("green");

    endPositionsRed = GetSpecialFieldPositionByName("red_safe");
    endPositionsBlack = GetSpecialFieldPositionByName("black_safe");
    endPositionsYellow = GetSpecialFieldPositionByName("yellow_safe");
    endPositionsGreen = GetSpecialFieldPositionByName("green_safe");
  }

  private Transform[] GetSpecialFieldPositionByName(string name)
  {
    var startPositions = new Transform[4];
    for (int i = 0, n = 4; i < n; i++)
    {
      startPositions[i] = GameObject.Find(name + "_" + i.ToString()).transform;
    }
    return startPositions;
  }

  private void MoveAllPlayerFigures()
  {
    foreach (var playerFigure in _lastCalculatedMoveResult.PlayerFigures)
    {

      var playerFigureTransform = GameObject
      .Find(string.Format("player_{0}_{1}", playerFigure.Color, playerFigure.Index))
      .transform;

      if (playerFigure.LocalPosition >= 0 && playerFigure.LocalPosition < 40)
      {
        playerFigureTransform.parent = _fieldPositions[playerFigure.WorldPosition%40];
      }

      if (playerFigure.LocalPosition < 0)
      {
        if (playerFigure.Color == PlayerColors.red) playerFigureTransform.parent = startPositionsRed[playerFigure.Index];
        if (playerFigure.Color == PlayerColors.black) playerFigureTransform.parent = startPositionsBlack[playerFigure.Index];
        if (playerFigure.Color == PlayerColors.yellow) playerFigureTransform.parent = startPositionsYellow[playerFigure.Index];
        if (playerFigure.Color == PlayerColors.green) playerFigureTransform.parent = startPositionsGreen[playerFigure.Index];
      }

      if(playerFigure.LocalPosition >=40 && playerFigure.LocalPosition <= 43){
        var posArray = GetArrayByColor(playerFigure.Color);
        playerFigureTransform.parent = posArray[playerFigure.LocalPosition -40];
      }
      playerFigureTransform.localPosition = Vector3.zero;
      playerFigureTransform.parent = null;
    }
  }

  private Transform[] GetArrayByColor(PlayerColors color)
  {
    switch(color){
      case PlayerColors.red:
        return endPositionsRed;
      case PlayerColors.black:
        return endPositionsBlack;
      case PlayerColors.green:
        return endPositionsGreen;
      case PlayerColors.yellow:
        return endPositionsYellow;
      default:
        return null;
    }
  }
}
