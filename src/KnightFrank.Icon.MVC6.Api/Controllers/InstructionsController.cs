using AutoMapper;
using KnightFrank.Icon.MVC6.Api.Models;
using KnightFrank.Icon.MVC6.Api.Repositories;
using KnightFrank.Icon.MVC6.Api.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KnightFrank.Icon.MVC6.Api.Controllers
{
    [Route("/api/Instructions")]
    public class InstructionsController : Controller
    {
        public InstructionsController(IInstructionRepository instructionsRepository)
        {
            _instructionsRepository = instructionsRepository;
        }

        // e.g. api/instructions
        public JsonResult Get()
        {
            try
            {
                var mvInstructionList = Mapper.Map<IEnumerable<InstructionViewModel>>(_instructionsRepository.GetAll());

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(mvInstructionList);
            }
            catch(Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"An error occurred - {ex.Message}" });
            }
        }

        // e.g. api/instructions/1
        [Route("{instructionId}")]
        public JsonResult Get(int instructionId)
        {
            try
            {
                Instruction instruction = _instructionsRepository.GetById(instructionId);

                if (instruction != null)
                {
                    var vmInstruction = Mapper.Map<InstructionViewModel>(instruction);

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(vmInstruction);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Instruction not found" });
            }
            catch(Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"An error occurred - {ex.Message}" });
            }
        }

        [Route("search")]
        public JsonResult Search(SearchInput si)
        {
            // e.g. api/instructions/search?Param1=test&Param2=two

            return Json(si);
        }

        #region implementation

        private IInstructionRepository _instructionsRepository;

        public class SearchInput
        {
            public string Param1 { get; set; }
            public string Param2 { get; set; }
        }

        #endregion

    }
}
