using System.Collections.Generic;
using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace newvisionsproject.boardgame.gameLogic
{
    public class nvp_RuleDefault_class : IRule
    {
        private IRule _nextRule;
        public CheckMovesResult CheckRule(CheckMovesResult result)
        {
            result.CanMove = false;
            result.AdditionalRollGranted = false;
            result.LastActiveRule = "Rule Default.";
            return result;
        }

        public IRule SetNextRule(IRule nextRule)
        {
            _nextRule = nextRule;
            return this;
        }
    }


}