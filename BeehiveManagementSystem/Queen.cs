using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal class Queen : Bee
    {
        public const float EGGS_PER_SHIFT = 0.45f;
        public const float HONEY_PER_UNASSIGNED_WORKER = 0.5F;

        
        private IWorker[] workers = new IWorker[0];
        private float eggs = 0;
        private float unassignedWorkers = 3;

        public override float CostPerShift { get { return 2.15f; } }
        public string StatusReport { get; private set; }

        public Queen() : base("Queen")
        {
            AssignBee("Egg Care");
            AssignBee("Honey Manufacturer");
            AssignBee("Nectar Collector");
        }
       
        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach (var worker in workers)
            {
                worker.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(HONEY_PER_UNASSIGNED_WORKER * unassignedWorkers);
            UpdateStatusReport();
        }
        public void AssignBee(string kindOfJob)
        {
            switch (kindOfJob)
            {
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
                default:
                    break;
            }
            UpdateStatusReport();
        }
        private void AddWorker(IWorker worker)
        {
            if(unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length -1] = worker;
            }
        }
        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert) 
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            } 
        }
        private void UpdateStatusReport()
        {
            StatusReport = $"{HoneyVault.StatusReport}\n\nEgg count: {eggs:0.0} " +
                $"\nUnassigned workers: {unassignedWorkers} \n{WorkerCounter("Nectar Collector")}\n" +
                $"{WorkerCounter("Honey Manufacturer")}\n{WorkerCounter("Egg Care")} Egg Care bee\n" +
                $"TOTAL WORKERS {workers.Length}";
        }
        private string WorkerCounter(string jobToCount)
        {
            int counter = 0;
            foreach (var worker in workers)
            {
                if (worker.Job == jobToCount)
                {
                    counter++;
                }
            }
            string s = "s";
            if (counter == 1) s = "";
            return $"{counter} {jobToCount} bee{s}";
        }
        
    }
}
