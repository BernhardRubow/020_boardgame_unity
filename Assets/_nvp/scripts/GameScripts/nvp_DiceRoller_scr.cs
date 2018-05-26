using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.enums;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.delegates;

public class nvp_DiceRoller_scr : MonoBehaviour, IDiceRoller {
	private PlayerColors[] _playerColors;
  public event PlayerDiceRollDelegate OnDiceRollHappened;


  // Use this for initialization
  void Start () {
		_playerColors = new PlayerColors[4] { PlayerColors.red, PlayerColors.black, PlayerColors.yellow, PlayerColors.green};
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Alpha1))InvokeEvent(0);
		if(Input.GetKeyUp(KeyCode.Alpha2))InvokeEvent(1);
		if(Input.GetKeyUp(KeyCode.Alpha3))InvokeEvent(2);
		if(Input.GetKeyUp(KeyCode.Alpha4))InvokeEvent(3);
	}

	void InvokeEvent(int playerIndex){
		var dto = new PlayerDiceRoll(){playerColor = _playerColors[playerIndex], diceValue = UnityEngine.Random.Range(1, 7)};
		OnDiceRollHappened(dto);
	}
}
