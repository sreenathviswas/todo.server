﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo.api.persistence;

namespace todo.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private readonly IRepository<ToDo> _repository;

        public ToDoController(IRepository<ToDo> repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<ToDo>> Get()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ToDo> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<ToDo> Post([FromBody] ToDo todo)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803

            todo.CreatedBy = User.Identity.Name;
            todo.CreatedOn = DateTime.UtcNow;
            todo.ModifiedBy = User.Identity.Name;
            todo.ModifiedOn = DateTime.UtcNow;

            return await _repository.SaveAsync(todo);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ToDo> Put(int id, [FromBody] ToDo todo)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803

            var existingTodo = await _repository.GetAsync(id);

            if(existingTodo == null)
            {
                throw new Exception();
            }

            existingTodo.Content = todo.Content;
            existingTodo.ModifiedBy = User.Identity.Name;
            existingTodo.ModifiedOn = DateTime.UtcNow;

            return await _repository.SaveAsync(todo);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803

            await _repository.RemoveAsync(new ToDo { Id = id });
        }
    }
}
