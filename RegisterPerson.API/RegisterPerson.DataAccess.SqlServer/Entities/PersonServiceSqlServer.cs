
using RegisterPerson.DataAccess.Abstract.Entities;
using RegisterPerson.DataAccess.SqlServer.Context;
using RegisterPerson.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegisterPerson.API.Services.Context.Implementation
{
    public class PersonServiceSqlServer : IPersonServiceSqlServer
    {
        private SQLServerContext _context;

        public PersonServiceSqlServer(SQLServerContext context)
        {
            this._context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();


            }catch(Exception e)
            {
                throw e;
            }

            return person;
        }

        public void Delete(int id)
        {
            try
            {
                Person person = Find(id);

                _context.Remove(person);
                _context.SaveChanges();

            }catch(Exception e)
            {
                throw e;
            }
        }

        public Person Find(int id)
        {
            return _context.Person.Where(x=>x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Person> FindAll()
        {
            return _context.Person.ToList(); 
        }

        public Person Update(Person person)
        {
            Person oldPerson = Find(person.Id.GetValueOrDefault());

            try
            {
                oldPerson.Name = person.Name;
                oldPerson.Age = person.Age;

                _context.Update(oldPerson);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            
            return oldPerson;
        }
    }
}
