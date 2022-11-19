using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    public static class HoneyVault
    {
        private const float NECTAR_CONVERSATION_RATIO = .19F;
        private const float LOW_LEVEL_WARNING = 10F;
        private static float Honey = 25f;
        private static float Nectar = 100f;
        public static string StatusReport
        {
            get
            {
                string report = $"Vault report:\n{Honey:0.0} units of honey\n{Nectar:0.0} units of nectar";
                string honeyWarn = "LOW HONEY — ADD A HONEY MANUFACTURER";
                string nectarWarn = "LOW NECTAR — ADD A NECTAR COLLECTOR";
                if (Nectar < LOW_LEVEL_WARNING) return $"{report}\n{nectarWarn}";
                else if (Honey < LOW_LEVEL_WARNING) return $"{report}\n{honeyWarn}";
                return report;
            }
                
        }
        
        
        public static void CollectNectar(float amount)
        {
            if (amount > 0)
            {
                Nectar += amount;
            }
        }
        public static void ConvertNectarToHoney(float amount)
        {
            if (amount > Nectar )
            {
                Honey += Nectar * NECTAR_CONVERSATION_RATIO;
                Nectar = 0;
            }
            else
            {
                Nectar -= amount;
                Honey += amount * NECTAR_CONVERSATION_RATIO;
            }
        }
        public static bool ConsumeHoney(float amount)
        {
            if (amount < Honey)
            {
                Honey -= amount;
                return true;
            }
            return false;
        }
    }
}
