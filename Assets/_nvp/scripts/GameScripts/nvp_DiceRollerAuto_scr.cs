using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.enums;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.delegates;

public class nvp_DiceRollerAuto_scr : MonoBehaviour , IDiceRoller {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private PlayerColors[] _playerColors;
	private int index = 0;


	// +++ exposed events +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  public event PlayerDiceRollDelegate OnDiceRollHappened;


  // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start () {
		_playerColors = new PlayerColors[4] { PlayerColors.red, PlayerColors.black, PlayerColors.yellow, PlayerColors.green};

		StartCoroutine(RollDice());
	}

	IEnumerator RollDice(){
		while(true){
			yield return new WaitForSeconds(0.3f);
			InvokeEvent(index);
			index++;
			index%=4;
		}
	}


	// +++ custom methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void InvokeEvent(int playerIndex){
		var diceRollEventArgs = new PlayerDiceRoll(){
			playerColor = _playerColors[playerIndex], 
			diceValue = UnityEngine.Random.Range(1, 7)};
			
		OnDiceRollHappened(diceRollEventArgs);
	}
}
