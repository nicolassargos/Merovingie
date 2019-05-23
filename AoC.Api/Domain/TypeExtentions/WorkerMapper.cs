using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC.Api.Domain;
using AoC.Common.Descriptors;
using AutoMapper;

namespace AoC.Domain.TypeExtentions
{
    public static class WorkerMapper
    {
        public static WorkerDescriptor ToWorkerDescriptor(this Worker worker)
        {
            return Mapper.Map<WorkerDescriptor>(worker);
        }

        public static Worker ToWorker(this WorkerDescriptor workerDescriptor)
        {
            return Mapper.Map<Worker>(workerDescriptor);
        }
    }
}
