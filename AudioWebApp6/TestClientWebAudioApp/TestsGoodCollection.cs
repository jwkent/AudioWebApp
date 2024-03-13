
using AudioWebApp.Client.Utilities;
using System.Collections.ObjectModel;
using AudioWebApp.Client.Models;
using System.Runtime.CompilerServices;
namespace TestClientWebAudioApp
{
    public class TestsGoodCollection : IDisposable
    {
        ObservableCollection<Series> collectionUnderTest;
        private TestCollectionData _testCollectionData { get; set; }
        private CollectionFilter _filter { get; set; }
 
        public TestsGoodCollection()
        {
            collectionUnderTest = new ObservableCollection<Series>();
            _testCollectionData = new TestCollectionData();
            _filter = new CollectionFilter();
        }
        public void Dispose() 
        { 
            collectionUnderTest.Clear();
        }

        [Fact]
        public void GoodCollection_FilteredAndOriginalCollectionLengthsAreSame_IsTrue()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetGoodCollection();

            //Act
            _filter.Reorder(collectionUnderTest);
            var result = _filter.SeriesItems;

            //Assert
            Assert.True(result.Count == collectionUnderTest.Count);
            Dispose();
        }
        [Fact]
        public void GoodCollection_CollectionsFirstAndLastValues_AsExpected()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetGoodCollection();
            var expectedFirst = "Volume One";
            var expectedLast = "Volume Twelve";
            
            //Act
            _filter.Reorder(collectionUnderTest);
            var result = _filter.SeriesItems;
            var resultFirstItemText = result[0].Name;
            
            var resultLastItemText = result[result.Count - 1].Name;
            

            //Assert
            Assert.Equal(expectedFirst, resultFirstItemText);
            Assert.Equal(expectedLast, resultLastItemText);
            Dispose();
        }
        [Fact]
        public void GoodCollection_FirstPartOfObjectNameValueNotEvaluated_IsTrue()
        {
            //Arrange
            string firstPartOfName = "Issue";
            collectionUnderTest = _testCollectionData.GetGoodCollection(firstPartOfName);
           
            //Act

            _filter.Reorder(collectionUnderTest);
            var result = _filter.SeriesItems;
            var resultFirstItemText = result[0].Name;
            var expectedFirst = $"{firstPartOfName} One";
            var resultLastItemText = result[result.Count - 1].Name;
            var expectedLast = $"{firstPartOfName} Twelve";

            //Assert
            Assert.Equal(expectedFirst, resultFirstItemText);
            Assert.Equal(expectedLast, resultLastItemText);
            Dispose();
        }
    }
}