using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace newvisionsproject.boardgame.interfaces
{

  public interface IRule
  {
    IRule SetNextRule(IRule nextRule);
    CheckMovesResult CheckRule(CheckMovesResult result);
  }

}
