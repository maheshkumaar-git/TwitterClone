using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DataLayer;
using TwitterClone.DataLayer.Models;

namespace TwitterClone.BusinessLayer
{
    public class UserBL
    {
        public void AddUser(Person item)
        {
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                db.People.Add(item);
                db.SaveChanges();
            }
        }

        public void UpdateUser(Person data)
        {
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                if (!string.IsNullOrWhiteSpace(data.UserId))                
                {
                    Person person;
                    person = SearchUser(data.UserId);
                    person.Active = person.Active;
                    person.Email = person.Email;
                    person.FullName = person.FullName;
                    person.Password = person.Password;
                    db.People.Attach(person);
                    db.Entry(person).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }

        public Person SearchUser(string name)
        {
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                Person people = db.People.FirstOrDefault(i => i.UserId.Contains(name));
                return people;
            }
        }
        public Person ValidateUser(string name,string pw)
        {
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                Person people = db.People.FirstOrDefault(i => i.UserId == name && i.Password == pw && i.Active == true);
                return people;
            }
        }
       
    }
}
