using System.Collections.Generic;
using System.Linq;
using KnightFrank.Icon.MVC6.Api.Models;
using System;

namespace KnightFrank.Icon.MVC6.Api.Repositories
{
    // NOTE - This class would typically exist in the data project when in production and connected to a database...

    public class DebugInstructionRepository : IInstructionRepository
    {
        // This is an in-memory collection of instructions for test purposes...

        private static List<Instruction> _instructions = new List<Instruction>()
        {
            new Instruction() { Id = 1, InstructionTitle="Test Instruction 1", Address1="1 Test Street", Town="London", Postcode="SW1 1AA" },
            new Instruction() { Id = 2, InstructionTitle="Test Instruction 2", Address1="2 Test Street", Town="London", Postcode="SW2 2AA" },
            new Instruction() { Id = 3, InstructionTitle="Test Instruction 3", Address1="3 Test Street", Town="London", Postcode="SW3 3AA" },
            new Instruction() { Id = 4, InstructionTitle="Test Instruction 4", Address1="4 Test Street", Town="London", Postcode="SW4 4AA" },
            new Instruction() { Id = 5, InstructionTitle="Test Instruction 5", Address1="5 Test Street", Town="London", Postcode="SW5 5AA" },
            new Instruction() { Id = 6, InstructionTitle="Test Instruction 6", Address1="6 Test Street", Town="London", Postcode="SW6 6AA" },
            new Instruction() { Id = 7, InstructionTitle="Test Instruction 7", Address1="7 Test Street", Town="London", Postcode="SW7 7AA" },
            new Instruction() { Id = 8, InstructionTitle="Test Instruction 8", Address1="8 Test Street", Town="London", Postcode="SW8 8AA" },
            new Instruction() { Id = 9, InstructionTitle="Test Instruction 9", Address1="9 Test Street", Town="London", Postcode="SW9 9AA" },
        };

        public Instruction Add(Instruction instruction)
        {
            int nextId = _instructions.Max(i => i.Id) + 1;
            instruction.Id = nextId;

            _instructions.Add(instruction);

            return instruction;
        }

        public void Delete(int id)
        {
            Instruction instructionToDelete = _instructions.FirstOrDefault(i => i.Id == id);

            if (instructionToDelete == null)
                throw new Exception($"Instruction with Id {id} not found");

            if(instructionToDelete != null)
                _instructions.Remove(instructionToDelete);
        }

        public void Delete(Instruction instrution)
        {
            if (instrution == null)
                throw new Exception("Instruction was not supplied");

            Delete(instrution.Id);
        }

        public IEnumerable<Instruction> GetAll()
        {
            return _instructions.OrderBy(i => i.Id);
        }

        public Instruction GetById(int id)
        {
            return _instructions.FirstOrDefault(i => i.Id == id);
        }

        public Instruction Update(Instruction instruction)
        {
            if(instruction.Id > 0)
            {
                Delete(instruction.Id);
                _instructions.Add(instruction);
            }

            return instruction;
        }
    }
}
