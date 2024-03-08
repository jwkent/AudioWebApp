using AudioWebApp.Client.Models;
using System.Collections.ObjectModel;

namespace AudioWebApp.Client.Utilities
{
    public class CollectionFilter
    {
        public ObservableCollection<Series>? SeriesItems {get; set;}
        Dictionary<string, int> volumeMap = new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
            ["ten"] = 10,
            ["eleven"] = 11,
            ["twelve"] = 12,
        };
        public void Reorder(ObservableCollection<Series> collection)
        {
            SeriesItems = new ObservableCollection<Series>(collection);
            
            foreach (var item in collection)
            {
                SeriesItems[GetVolumeNumber(item.Name) - 1] = item;
            }
        }
        private int GetVolumeNumber(string name)
        {
            string[] parts = name.Split(' ');
 
            return volumeMap[parts[1].ToLower()];    
        }
        
    }
}
