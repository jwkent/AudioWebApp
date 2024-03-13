using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioWebApp.Client.Models;
using AudioWebApp.Client.Utilities;

namespace TestClientWebAudioApp
{
    
    public class TestsBadCollection : IDisposable
    {
        ObservableCollection<Series> collectionUnderTest;
        private TestCollectionData _testCollectionData { get; set; }
        private CollectionFilter _filter { get; set; }

        public TestsBadCollection() 
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
        public void BadCollection_EmptyCollection_IsValidCollection_False()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetNullCollection();
            //Act
            _filter.Reorder(collectionUnderTest);
            var actual = _filter.IsValidCollection;

            //Assert
            
            Assert.False(actual);
            Dispose();
        }
        [Fact]
        public void BadCollection_TooLargeForFilter_NotValid_ReturnOriginalCollection()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetTooLongCollection();
            //Act
            _filter.Reorder(collectionUnderTest);
            var actual = _filter.IsValidCollection;
            var actualCollection = _filter.SeriesItems;
            var actualCollectionValue = actualCollection[3];
            var expected = collectionUnderTest[3];
            //Assert
            
            Assert.False(actual);
            Assert.Equal(expected, actualCollectionValue);
            Dispose();
        }
        [Fact]
        public void BadCollection_CapOrLower_ShouldFilterButLeaveNamesUnchanged()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetCapAndLowerCollection();

            //Act
            _filter.Reorder(collectionUnderTest);
            var result = _filter.SeriesItems;
            var expected = "Volume one";
            var expected2 = "Volume Three";
            var expected3 = "Volume SEVeN";
            
            //Assert
            Assert.Equal(expected, result[0].Name);
            Assert.Equal(expected2, result[2].Name);
            Assert.Equal(expected3, result[6].Name);
            Dispose();
        }
        [Fact]
        public void BadCollection_ValueNotInDictionary_ReturnsOriginalCollection()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetValueNotInDictionaryCollection();
            //Act
            _filter.Reorder(collectionUnderTest);
            var actual = _filter.SeriesItems[1];
            var expected = collectionUnderTest[1];
            //Assert
            Assert.Equal(expected, actual);
            
            Dispose();
        }
        [Fact]
        public void BadCollection_NoSpaceInName_ReturnsOriginalCollection()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetNoSpaceInNameCollection();
            
            //Act
            _filter.Reorder(collectionUnderTest);
            var actual = _filter.SeriesItems.Last().Name;
            var expected = collectionUnderTest.Last().Name;
            //Assert
            Assert.Equal(expected, actual);
            
            Dispose();
        }
        [Fact]
        public void BadCollection_ExtraSpaces_ReturnsOriginalColection()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetExtraSpacesInNameCollection();

            //Act
            _filter.Reorder(collectionUnderTest);
            var actual = _filter.SeriesItems.Last().Name;
            var expected = collectionUnderTest.Last().Name;
            //Assert
            Assert.True(actual.Equals(expected));
        }
        [Fact]
        public void BadCollection_MoreThanTwoParts_ReturnsOriginalCollection()
        {
            //Arrange
            collectionUnderTest = _testCollectionData.GetMoreThanTwoPartsInNameCollection();

            //Act
            _filter.Reorder(collectionUnderTest);
            var actual = _filter.SeriesItems[3].Name;
            var expected = collectionUnderTest[3].Name;

            //Assert
            Assert.True(actual.Equals(expected));
        }
    }
}
