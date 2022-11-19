using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal class HoneyManufacturer : Bee
    {
        public HoneyManufacturer():base("Honey Manufacturer") {  }
        public override float CostPerShift { get { return 1.7f; } }
        public const float NECTAR_PROCESSED_PER_SHIFT = 33.15f;
        protected override void DoJob()
        {
            HoneyVault.ConvertNectarToHoney(NECTAR_PROCESSED_PER_SHIFT);
        }
    }
}
