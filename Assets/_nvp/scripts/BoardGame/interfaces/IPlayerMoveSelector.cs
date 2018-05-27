using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.delegates;

namespace newvisionsproject.boardgame.interfaces{
	public interface IPlayerMoveSelector{
		event PlayerMoveSelectorDelegate OnPlayerMoveSected;
	}
}
