using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController] 
    public class HomeController : ControllerBase 
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context)
            => Ok(context.ToDos.ToList()); 

        [HttpGet("/{id:int}")]
        public IActionResult GetId( [FromRoute] int id, [FromServices] AppDbContext context)
        {
            var todos = context.ToDos.FirstOrDefault(x => x.Id == id);
            if(todos == null)
                return NotFound();

            return Ok(todos); 
        } 

        [HttpPost("/")]
        public IActionResult Post ([FromBody]ToDoModel todo, [FromServices]AppDbContext context)
        {
            context.ToDos?.Add(todo);
            context.SaveChanges();

            return Created($"/{todo.Id}",todo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] ToDoModel todo, [FromServices] AppDbContext context)
        {
            var model = context.ToDos?.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.ToDos.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var model = context.ToDos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            context.ToDos.Remove(model);
            context.SaveChanges();

            return Ok(model);
        }
    }
}
