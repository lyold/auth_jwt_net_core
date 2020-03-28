using RegisterPerson.DataAccess.Abstract.Entities;
using RegisterPerson.Domain.Model.Entities;
using RegisterPerson.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RegisterPerson.Domain.Services.Implementation
{ 
    public class PersonService : IPersonService
    {
        private IPersonServiceSqlServer _personServiceSqlServer;

        public PersonService(IPersonServiceSqlServer personServiceSqlServer)
        {
            _personServiceSqlServer = personServiceSqlServer;
        }

        public Person Create(Person person)
        {
            return _personServiceSqlServer.Create(person);
        }

        public void Delete(int id)
        {
            _personServiceSqlServer.Delete(id);
        }

        public Person Find(int id)
        {
            return _personServiceSqlServer.Find(id);
        }

        public List<Person> FindAll()
        {
            var list = _personServiceSqlServer.FindAll();

            if(list != null)
                return list.ToList();

            return new List<Person>();
        }

        public Person Update(Person person)
        {
            return _personServiceSqlServer.Update(person);
        }
    }
}
