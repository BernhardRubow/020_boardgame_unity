using newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;

namespace newvisionsproject.boardgame.gameLogic
{
    public class nvp_Rule_10_GetsAdditionalRuleOn6 : IRule
    {
        private IRule _nextRule;

        public CheckMovesResult CheckRule(CheckMovesResult result)
        {
            if (result.DiceValue == 6) result.AdditionalRollGranted = true;
            return _nextRule.CheckRule(result);
        }

        public IRule SetNextRule(IRule nextRule)
        {
            _nextRule = nextRule;
            return this;
        }
    }
}