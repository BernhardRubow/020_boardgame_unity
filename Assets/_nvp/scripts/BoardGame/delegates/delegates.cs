using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.dto;

namespace newvisionsproject.boardgame.delegates {

	public delegate void PlayerDiceRollDelegate(PlayerDiceRoll diceRoll);

	public delegate void PlayerMovesCalculatedDelegate(CheckMovesResult result);

	public delegate void PlayerMoveSelectorDelegate(int index);

	public delegate void ButtonClickedEvent(int index);

	public delegate void PlayerFigureMovedDelegate();
}
