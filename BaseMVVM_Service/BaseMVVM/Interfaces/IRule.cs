using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM.Interfaces
{
    public interface IRule
    {
        string RuleErrorMessage { get; set; }

        /// <summary>
        /// The default value to applied when applying the rule failed
        /// </summary>
        object DefaultValue { get; set; }
        object LoadRuleObjects();

        /// <summary>
        /// Get the result when applying the rule
        /// </summary>
        /// <param name="obj">objects to applied rule</param>
        /// <returns></returns>
        bool ApplyRuleValid(params object[] obj);
    }
}
