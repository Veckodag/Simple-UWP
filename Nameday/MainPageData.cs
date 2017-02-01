using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameday
{
  public class MainPageData : INotifyPropertyChanged
  {
    private string _greeting = "Hello World!";
    public event PropertyChangedEventHandler PropertyChanged;
    private ObservableCollection<NamedayModel> _allNamedays = new ObservableCollection<NamedayModel>();
    private NamedayModel _selectedNameday;
    public ObservableCollection<NamedayModel> Namedays { get; set; }

    public MainPageData()
    {
      Namedays = new ObservableCollection<NamedayModel>();

      for (var month = 1; month < 12; month++)
      {
        _allNamedays.Add(new NamedayModel(month, 1, new[] {"Adam"}));
        _allNamedays.Add(new NamedayModel(month, 2, new [] {"Fredrik"}));
        _allNamedays.Add(new NamedayModel(month, 3, new [] {"Dan"}));
        _allNamedays.Add(new NamedayModel(month, 4, new [] {"Erik"}));
        _allNamedays.Add(new NamedayModel(month, 24, new[] {"Eve", "Andrew"}));
      }

      PerformFiltering();
    }

    public string Greeting
    {
      get { return _greeting; }
      set
      {
        if (value == _greeting)
          return;

        _greeting = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Greeting)));
      }
    }

    public NamedayModel SelectedNameday
    {
      get { return _selectedNameday; }
      set
      {
        _selectedNameday = value;

        if (value == null)
          Greeting = "Hello World!";
        else
          Greeting = "Hello " + value.NamesAsString;        
      }
    }

    private string _filter;

    public string Filter
    {
      get { return _filter; }
      set
      {
        if (value == _filter)
          return;

        _filter = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));

        PerformFiltering();
      }
    }

    private void PerformFiltering()
    {
      if (_filter == null)
        _filter = "";

      var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

      var result = _allNamedays.Where(d => d.NamesAsString.ToLowerInvariant().Contains(lowerCaseFilter)).ToList();

      var toRemove = Namedays.Except(result).ToList();

      foreach (var x in toRemove)
        Namedays.Remove(x);

      var resultCount = result.Count;

      for (var i = 0; i < resultCount; i++)
      {
        var resultItem = result[i];
        if (i + 1 > Namedays.Count || !Namedays[i].Equals(resultItem))
          Namedays.Insert(i, resultItem);
      }
    }
  }
}
