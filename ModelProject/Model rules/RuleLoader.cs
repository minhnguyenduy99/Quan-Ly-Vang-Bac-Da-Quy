using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Model_rules
{
    public class RuleLoader
    {
        private const string RULE_FILE_PATH = "";
        public static IEnumerable<object> LoadRules()
        {
            IEnumerable<object> rules = null;
            if (!File.Exists(RULE_FILE_PATH))
            {
                var fileStream = File.Create(RULE_FILE_PATH);
                fileStream.Close();
            }
            using (StreamReader reader = new StreamReader(RULE_FILE_PATH))
            {
                try
                {
                    // Read file here


                    // end read file
                    reader.Close();

                    return rules;
                }
                catch { return null; }
            }
        }
    }
}
