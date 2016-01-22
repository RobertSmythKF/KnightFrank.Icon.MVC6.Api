using AutoMapper;
using KnightFrank.Icon.MVC6.Api.Models;
using KnightFrank.Icon.MVC6.Api.Repositories;
using KnightFrank.Icon.MVC6.Api.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

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
                // Pull all instructions...
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

        [HttpPost]
        public JsonResult Post([FromBody]InstructionViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var instruction = Mapper.Map<Instruction>(vm);
             
                    _instructionsRepository.Add(instruction);

                    var instructionViewModel = Mapper.Map<InstructionViewModel>(instruction);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(instructionViewModel);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"Invalid model state", Errors = GetModelStateErrors() });
            }
            catch(Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"An error occurred - {ex.Message}" });
            }
        }

        [HttpPut]
        public JsonResult Put([FromBody]InstructionViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var instruction = Mapper.Map<Instruction>(vm);

                    _instructionsRepository.Update(instruction);

                    var instructionViewModel = Mapper.Map<InstructionViewModel>(instruction);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(instructionViewModel);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"Invalid model state", Errors = GetModelStateErrors() });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"An error occurred - {ex.Message}" });
            }
        }

        [HttpDelete]
        public JsonResult Delete([FromBody]InstructionViewModel vm)
        {
            try
            {
                var instruction = Mapper.Map<Instruction>(vm);

                _instructionsRepository.Delete(instruction);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(true);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"An error occurred - {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("{instructionId}")]
        public JsonResult Delete(int instructionId)
        {
            try
            {
                _instructionsRepository.Delete(instructionId);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(true);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"An error occurred - {ex.Message}" });
            }
        }

        [Route("search")]
        public JsonResult Search(SearchInput si)
        {
            // e.g. api/instructions/search?Addr1=test%20street&PCode=SW1

            try
            {
                var instructions = _instructionsRepository.GetAll();

                if (!string.IsNullOrWhiteSpace(si.Title))
                    instructions = instructions.Where(i => i.InstructionTitle.ToLower().Contains(si.Title.ToLower()));

                if (!string.IsNullOrWhiteSpace(si.Addr1))
                    instructions = instructions.Where(i => i.Address1.ToLower().Contains(si.Addr1.ToLower()));

                if (!string.IsNullOrWhiteSpace(si.Addr2))
                    instructions = instructions.Where(i => i.Address2.ToLower().Contains(si.Addr2.ToLower()));

                if (!string.IsNullOrWhiteSpace(si.Addr3))
                    instructions = instructions.Where(i => i.Address3.ToLower().Contains(si.Addr3.ToLower()));

                if (!string.IsNullOrWhiteSpace(si.Town))
                    instructions = instructions.Where(i => i.Town.ToLower().Contains(si.Town.ToLower()));

                if (!string.IsNullOrWhiteSpace(si.County))
                    instructions = instructions.Where(i => i.County.ToLower().Contains(si.County.ToLower()));

                if (!string.IsNullOrWhiteSpace(si.PCode))
                    instructions = instructions.Where(i => i.Postcode.ToLower().Contains(si.PCode.ToLower()));

                return Json(Mapper.Map<IEnumerable<InstructionViewModel>>(instructions));
            }
            catch(Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = $"An error occurred - {ex.Message}" });
            }
        }

        public class SearchInput
        {
            public string Title { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Addr3 { get; set; }
            public string Town { get; set; }
            public string County { get; set; }
            public string PCode { get; set; }
        }

        #region implementation

        private IInstructionRepository _instructionsRepository;
        
        private IEnumerable<string> GetModelStateErrors()
        {
            List<string> errors = new List<string>();

            ModelState.Values.Where(val => val.Errors.Count > 0).ToList().ForEach(
                val => val.Errors.ToList().ForEach(
                    err => errors.Add(err.ErrorMessage)
                )
            );

            return errors;
        }
        
        #endregion

    }
}
