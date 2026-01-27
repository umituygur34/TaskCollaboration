using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCollaboration.Api.api.Models;

namespace TaskCollaboration.api.Services
{
    public interface IWorkTaskService
    {
        public Task<IEnumerable<WorkTask>> GetAllAsync();



    }
}