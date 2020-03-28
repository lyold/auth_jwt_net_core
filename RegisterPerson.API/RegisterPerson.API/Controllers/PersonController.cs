using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterPerson.Domain.Model.Entities;
using RegisterPerson.Domain.Services.Interfaces;

namespace RegisterPerson.API.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            this._personService = personService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return Ok(_personService.Find(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Post(Person person)
        {
            try
            {
                return Ok(_personService.Create(person));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Put(Person person)
        {
            try
            {
                return Ok(_personService.Update(person));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete(int id)
        {
            try
            {
                _personService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
