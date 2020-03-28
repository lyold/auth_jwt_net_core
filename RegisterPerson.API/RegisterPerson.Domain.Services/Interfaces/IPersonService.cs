
using RegisterPerson.Domain.Model.Entities;
using System.Collections.Generic;

namespace RegisterPerson.Domain.Services.Interfaces
{ 
    public interface IPersonService
    {
        Person Create(Person person);

        Person Update(Person person);

        void Delete(int id);

        Person Find(int id);

        List<Person> FindAll();
    }
}
