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
                    Narrator = "Teagan Moore",
                    AudioSource = "https://theos.s3.us-west-1.amazonaws.com/books/1_Complete+Audiobook-Empire_of_the_Risen_Son_Book_1.mp3",
                    ImageSource = "./images/empireoftheson_book1.jpg"
                },
                new AudioBook
                {
                    SeriesTitle = "Empire of the Risen Son:",
                    SeriesSubtitle = "A Treatise on the Kingdom of God - What it is any Why it Matters",
                    BookTitle = "Book Two: All the King's Men",
                    Narrator = "Teagan Moore",
                    AudioSource = "https://theos.s3.us-west-1.amazonaws.com/books/Empire_of_the_Risen_Son-Book_2-by_Steve_Gregg.mp3",
                    ImageSource = "./images/empireoftheson_book2.jpg"
                },
                new AudioBook
                {
                    SeriesTitle = "",
                    SeriesSubtitle = "",
                    BookTitle = "Why Not Full Preterism?: A Partial Preterist response to a Novel Theological Innovation",
                    Narrator = "Teagan Moore",
                    AudioSource = "https://www.thenarrowpath.com/audio/audio-books/why-not-full-preterism/Full-Book_Steve-Gregg_Audiobook_Why-Not-Full-Preterism.mp3",
                    ImageSource = "./images/WhyNotFullPreterism_book.jpg"
                }
            };

        }
    }
}
