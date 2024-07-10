using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet]
        public List<Person> GetPersons()
        {
            return _personService.GetPersons();
        }
        [HttpPost]
        public void PostPerson(PostPersonModel model)
        {
            _personService.PostPerson(model);
        }
        [HttpPut]
        public void PutPerson(string id, PutPersonModel model)
        {
            _personService.PutPerson(id, model);
        }
        [HttpDelete]
        public void DeletePerson(string id)
        {
            _personService.DeletePerson(id);
        }
    }
}
