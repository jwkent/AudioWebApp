using AudioWebApp.Client.Models;
using System.Collections.ObjectModel;

namespace AudioWebApp.Client.Utilities
{
    public class CollectionFilter
    {
        public ObservableCollection<Series>? SeriesItems { get; set; }
        public bool IsValidCollection = false;
        private ObservableCollection<Series>? OriginalCollection;
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
            OriginalCollection = collection;

            if (collection.Count == 0 || collection.Count > volumeMap.Count)
            {
                IsValidCollection = false;
            }
            else
            {
                SeriesItems = new ObservableCollection<Series>(collection);
                IsValidCollection = true;
            }


            if (!IsValidCollection)
            {
                HandleException(new InvalidDataException("Collection invalid for filter too large or null"));
            }
            else
            {
                try
                {
                    foreach (var item in collection)
                    {
                        int volumeNumber = GetVolumeNumber(item.Name);

                        if (volumeNumber > 0 && volumeNumber <= collection.Count)
                        {
                            SeriesItems[volumeNumber - 1] = item;
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid volume number detected");
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }
        private int GetVolumeNumber(string name)
        {
            string[] parts = name.Split(' ');
            if (parts.Length < 2 || parts.Length > 2)
            {
                throw new ArgumentException();
            }
            try
            {
                return volumeMap[parts[1].ToLower()];
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }

        }
       /// <summary>
       /// When Exception is thrown simply return the original collection without filtering.
       /// </summary>
       /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            if (ex is IndexOutOfRangeException || ex is InvalidOperationException || ex is KeyNotFoundException ||  ex is ArgumentException || ex is InvalidDataException)
            {
                SeriesItems = OriginalCollection;
            }
        }
    }
}
