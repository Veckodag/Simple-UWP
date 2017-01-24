using System;
using Data;

namespace ViewModels
{
  public class PersonViewModel : NotificationBase<Person>
  {
    public PersonViewModel(Person person = null) : base(person) { }
    public string Name
    {
      get { return This.Name; }
      set { SetProperty(This.Name, value, () => This.Name = value); }
    }
    public int Age
    {
      get { return This.Age; }
      set { SetProperty(This.Age, value, () => This.Age = value); }
    }
  }
}