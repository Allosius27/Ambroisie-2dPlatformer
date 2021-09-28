using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SciFiProject.Core.UI.Tooltips;

namespace SciFiProject.UI
{
    public class UITooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            
        }
    }
}

