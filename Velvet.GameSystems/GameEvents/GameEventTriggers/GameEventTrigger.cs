using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{


    public class GameEventTrigger
    {


        public delegate void TriggerConditionMetEventHandler(object sender, EventArgs e);
        public event TriggerConditionMetEventHandler TriggerConditionMet;
        protected virtual void OnTriggerConditionMet()
        {
            TriggerConditionMet?.Invoke(this, EventArgs.Empty);
        }



    }
}
