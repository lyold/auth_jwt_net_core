
using RegisterPerson.Domain.Model.Entities;
using System.Collections.Generic;

namespace RegisterPerson.DataAccess.Abstract.Entities
{
    public interface IPersonServiceSqlServer
    {
        Person Create(Person person);

        Person Update(Person person);

        void Delete(int id);

        Person Find(int id);

        IEnumerable<Person> FindAll();
    }
}
