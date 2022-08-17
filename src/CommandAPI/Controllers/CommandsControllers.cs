using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace CommandAPI.Controllers
{
    /*controllers de api*/
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repositorio;
        private readonly IMapper _mapper;

        public CommandsController(ICommandAPIRepo repositorio,IMapper mapper  )
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        [HttpGet]
        //public ActionResult<IEnumerable<Command>> GetAllCommands()
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            //return new string[]{"this","is","hard","coded"};
            var commandItems = _repositorio.GetAllCommands();
            ///return Ok(commandItems);
            /* con automapper*/
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }
        
        [HttpGet("{id}",Name="GetCommandById")]
        //public ActionResult<Command> GetCommandById(int id)
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repositorio.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            //return Ok(commandItem);
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repositorio.CreateCommand(commandModel);
            _repositorio.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById),new{Id=commandReadDto.Id},commandReadDto);

        }
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
                var commandModelFromRepo = _repositorio.GetCommandById(id);
                if (commandModelFromRepo == null)
                {
                    return NotFound();
                }

                _mapper.Map(commandUpdateDto,commandModelFromRepo);
                _repositorio.UpdateCommand(commandModelFromRepo);
                _repositorio.SaveChanges();
                return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
                var commandModelFromRepo = _repositorio.GetCommandById(id);
                if (commandModelFromRepo == null)
                {
                    return NotFound();
                }

                var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
                patchDoc.ApplyTo(commandToPatch,ModelState);
                if (!TryValidateModel(commandToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                _mapper.Map(commandToPatch,commandModelFromRepo);
                _repositorio.UpdateCommand(commandModelFromRepo);
                _repositorio.SaveChanges();
                return NoContent();
              
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
                var commandModelFromRepo = _repositorio.GetCommandById(id);
                if (commandModelFromRepo == null)
                {
                    return NotFound();
                }
                _repositorio.DeleteCommand(commandModelFromRepo);
                _repositorio.SaveChanges();
                
                return NoContent();

        }

    }
}