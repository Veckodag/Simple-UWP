using System;
using System.Collections.Generic;
using Data;

namespace Models
{
  public class Organization
  {
    public List<Person> People { get; set; }
    public string Name { get; set; }

    public Organization(string databaseName)
    {
      Name = databaseName;
      People = FakeService.GetPeople();
    }

    public void Add(Person person)
    {
      if (!People.Contains(person))
      {
        People.Add(person);
        FakeService.Write(person);
      }
    }

    public void Delete(Person person)
    {
      if (People.Contains(person))
      {
        People.Remove(person);
        FakeService.Delete(person);
      }
    }

    public void Update(Person person)
    {
      FakeService.Write(person);
    }
  }
}