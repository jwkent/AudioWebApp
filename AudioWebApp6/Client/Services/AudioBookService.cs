using AudioWebApp.Client.Models;
using AudioWebApp.Client.Pages;
using System.Collections.ObjectModel;

namespace AudioWebApp.Client.Services
{
    public class AudioBookService
    {
        public ObservableCollection<AudioBook> AudioBooks { get; set; }

        public async Task GetAudioBooks()
        {
            AudioBooks = new ObservableCollection<AudioBook>
            {
                new AudioBook
                {
                    SeriesTitle = "Empire of the Risen Son:",
                    SeriesSubtitle = "A Treatise on the Kingdom of God - What it is any Why it Matters",
                    BookTitle = "Book One: There is Another King",
                    AudioSource = "https://theos.s3.us-west-1.amazonaws.com/books/1_Complete+Audiobook-Empire_of_the_Risen_Son_Book_1.mp3",
                    ImageSource = "./images/empireoftheson_book1.jpg"
                },
                new AudioBook
                {
                    SeriesTitle = "Empire of the Risen Son:",
                    SeriesSubtitle = "A Treatise on the Kingdom of God - What it is any Why it Matters",
                    BookTitle = "Book Two: All the King's Men",
                    AudioSource = "https://theos.s3.us-west-1.amazonaws.com/books/Empire_of_the_Risen_Son-Book_2-by_Steve_Gregg.mp3",
                    ImageSource = "./images/empireoftheson_book2.jpg"
                }
            };

        }
    }
}
