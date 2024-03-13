using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioWebApp.Client.Models;

namespace TestClientWebAudioApp
{
    public class TestCollectionData
    {
        public ObservableCollection<Series>? TestingCollection;
        public ObservableCollection<Series> GetGoodCollection()
        {
            ObservableCollection<Series> GoodCollection = new ObservableCollection<Series>();
            GoodCollection.Add(new Series { Name = "Volume One" });
            GoodCollection.Add(new Series { Name = "Volume Two" });
            GoodCollection.Add(new Series { Name = "Volume Three" });
            GoodCollection.Add(new Series { Name = "Volume Four" });
            GoodCollection.Add(new Series { Name = "Volume Five" });
            GoodCollection.Add(new Series { Name = "Volume Six" });
            GoodCollection.Add(new Series { Name = "Volume Seven" });
            GoodCollection.Add(new Series { Name = "Volume Eight" });
            GoodCollection.Add(new Series { Name = "Volume Nine" });
            GoodCollection.Add(new Series { Name = "Volume Ten" });
            GoodCollection.Add(new Series { Name = "Volume Eleven" });
            GoodCollection.Add(new Series { Name = "Volume Twelve" });

            TestingCollection = GoodCollection;
            return TestingCollection;

        }
        public ObservableCollection<Series> GetGoodCollection(string value)
        {
            ObservableCollection<Series> GoodCollection = new ObservableCollection<Series>();
            GoodCollection.Add(new Series { Name = $"{value} One" });
            GoodCollection.Add(new Series { Name = $"{value} Two" });
            GoodCollection.Add(new Series { Name = $"{value} Three" });
            GoodCollection.Add(new Series { Name = $"{value} Four" });
            GoodCollection.Add(new Series { Name = $"{value} Five" });
            GoodCollection.Add(new Series { Name = $"{value} Six" });
            GoodCollection.Add(new Series { Name = $"{value} Seven" });
            GoodCollection.Add(new Series { Name = $"{value} Eight" });
            GoodCollection.Add(new Series { Name = $"{value} Nine" });
            GoodCollection.Add(new Series { Name = $"{value} Ten" });
            GoodCollection.Add(new Series { Name = $"{value} Eleven" });
            GoodCollection.Add(new Series { Name = $"{value} Twelve" });

            TestingCollection = GoodCollection;
            return TestingCollection;

        }
        public ObservableCollection<Series> GetTooLongCollection()
        {
            ObservableCollection<Series> TooLongCollection = new ObservableCollection<Series>();
            TooLongCollection.Add(new Series { Name = "Volume One" });
            TooLongCollection.Add(new Series { Name = "Volume Two" });
            TooLongCollection.Add(new Series { Name = "Volume Three" });
            TooLongCollection.Add(new Series { Name = "Volume Four" });
            TooLongCollection.Add(new Series { Name = "Volume Five" });
            TooLongCollection.Add(new Series { Name = "Volume Six" });
            TooLongCollection.Add(new Series { Name = "Volume Seven" });
            TooLongCollection.Add(new Series { Name = "Volume Eight" });
            TooLongCollection.Add(new Series { Name = "Volume Nine" });
            TooLongCollection.Add(new Series { Name = "Volume Ten" });
            TooLongCollection.Add(new Series { Name = "Volume Eleven" });
            TooLongCollection.Add(new Series { Name = "Volume Twelve" });
            TooLongCollection.Add(new Series { Name = "Volume Thirteen" });
            TooLongCollection.Add(new Series { Name = "Volume Fourteen" });

            TestingCollection = TooLongCollection;
            return TestingCollection; 
        }
        public ObservableCollection<Series> GetNullCollection()
        {
            ObservableCollection<Series> NullCollection = new ObservableCollection<Series>();
            TestingCollection = NullCollection;
            return TestingCollection;
        }
        public ObservableCollection<Series> GetCapAndLowerCollection()
        {
            ObservableCollection<Series> RedundantCollection = new ObservableCollection<Series>();
            RedundantCollection.Add(new Series { Name = "Volume Three" });
            RedundantCollection.Add(new Series { Name = "Volume one" });
            RedundantCollection.Add(new Series { Name = "Volume two" });
            RedundantCollection.Add(new Series { Name = "Volume Four" });
            RedundantCollection.Add(new Series { Name = "Volume Five" });
            RedundantCollection.Add(new Series { Name = "Volume six" });
            RedundantCollection.Add(new Series { Name = "Volume SEVeN" });
            RedundantCollection.Add(new Series { Name = "Volume eight" });
            RedundantCollection.Add(new Series { Name = "Volume Nine" });
            RedundantCollection.Add(new Series { Name = "Volume Ten" });

            TestingCollection = RedundantCollection;
            return TestingCollection;
        }

        public ObservableCollection<Series> GetValueNotInDictionaryCollection()
        {
            ObservableCollection<Series> ValueNotInDictionaryCollection = new ObservableCollection<Series>();
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume three" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume twenty-two" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume nineteen" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume Flour" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume Five" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume six" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume SeveN" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume eight" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume Nine" });
            ValueNotInDictionaryCollection.Add(new Series { Name = "Volume Ten" });

            TestingCollection = ValueNotInDictionaryCollection;
            return TestingCollection;
        }
        public ObservableCollection<Series> GetNoSpaceInNameCollection()
        {
            ObservableCollection<Series> NoSpaceInNameCollection = new ObservableCollection<Series>();
            NoSpaceInNameCollection.Add(new Series { Name = "Volume three" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume one" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume two" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume Four" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume Five" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume six" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume Seven" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume eight" });
            NoSpaceInNameCollection.Add(new Series { Name = "Volume Nine" });
            NoSpaceInNameCollection.Add(new Series { Name = "VolumeTen" });

            TestingCollection = NoSpaceInNameCollection;
            return TestingCollection;
        }
        public ObservableCollection<Series> GetExtraSpacesInNameCollection()
        {
            ObservableCollection<Series> ExtraSpacesInNameCollection = new ObservableCollection<Series>();
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume three" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume   one" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume two" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume Four" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume Five" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume six" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume Seven" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume eight" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume Nine" });
            ExtraSpacesInNameCollection.Add(new Series { Name = "Volume  Ten" });

            TestingCollection = ExtraSpacesInNameCollection;
            return TestingCollection;
        }
        public ObservableCollection<Series> GetMoreThanTwoPartsInNameCollection()
        {
            ObservableCollection<Series> MoreThanTwoPartsInNameCollection = new ObservableCollection<Series>();
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume three" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume one" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume two" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume Part Four" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume Five" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume six" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume Seven" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume eight" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume Nine" });
            MoreThanTwoPartsInNameCollection.Add(new Series { Name = "Volume  Ten" });

            TestingCollection = MoreThanTwoPartsInNameCollection;
            return TestingCollection;
        }


    }
}
