using System;
using System.Collections.Generic;

namespace L3ave.GameLogic
{
    public class Triggers
    {
        private static string IgnoreTrigger = "-";

        private Dictionary<string, bool> triggers;

        public bool this[string name, bool need = true]
        {
            get
            {
                name = name.ToLowerInvariant();

                if (name == IgnoreTrigger)
                {
                    return true;
                }

                if (!triggers.ContainsKey(name))
                {
                    return !need;
                }

                return triggers[name] == need;
            }
            set
            {
                triggers[name.ToLowerInvariant()] = value;
            }
        }

        public Triggers()
        {
            triggers = new Dictionary<string, bool>();
        }

        public bool CheckAll(IEnumerable<TriggerPair> triggers)
        {
            foreach (var trigger in triggers)
            {
                if (!this[trigger.Name, trigger.Value])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
