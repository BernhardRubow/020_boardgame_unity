using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.delegates;

namespace newvisionsproject.boardgame.interfaces
{
  public interface IDiceRoller
  {
    event PlayerDiceRollDelegate OnDiceRollHappened;
  }
}
