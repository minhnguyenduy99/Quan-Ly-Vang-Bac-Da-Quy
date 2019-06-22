using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Model_rules
{
    /// <summary>
    /// Identifies a rule for range of a specified value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueRangeRule<T> : IRule
    {
        public string RuleErrorMessage { get; set; }
        public object DefaultValue { get; set; }
        public T MinValue { get; set; }
        public T MaxValue { get; set; }

        public Comparer<T> Comparer { get; set; }

        /// <summary>
        /// Create an instance of <see cref="ValueRangeRule{T}"/>
        /// </summary>
        /// <param name="minValue">The min range of value</param>
        /// <param name="maxValue">The max range of value</param>
        /// <param name="comparer">A comparer shows how value is compared</param>
        public ValueRangeRule(T minValue, T maxValue, Comparer<T> comparer)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Comparer = comparer;
        }

        public bool ApplyRuleValid(params object[] obj)
        {
            var castObj = obj.Cast<T>().ToArray();
            if (castObj == null)
                return false;

            for(int i = 0; i < castObj.Length; i++)
            {
                if (Comparer.Compare(castObj[i], MinValue) == -1 || Comparer.Compare(castObj[i],MaxValue) == 1)
                {
                    return false;
                }
            }
            return true;
        }

        public object LoadRuleObjects()
        {
            return null;
        }
    }
}
