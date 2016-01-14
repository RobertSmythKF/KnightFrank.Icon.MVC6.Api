using KnightFrank.Icon.MVC6.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.Icon.MVC6.Api.Repositories
{
    // NOTE - This interface would typically exist in the core project

    public interface IInstructionRepository
    {
        IEnumerable<Instruction> GetAll();
        Instruction GetById(int id);
        Instruction Add(Instruction instruction);
        Instruction Update(Instruction instruction);
        void Delete(Instruction instrution);
        void Delete(int id);

    }
}
