function listenForEndOfMessage() {
    const audioElement = document.getElementById("audioPlayer");

    if (audioElement) {
        setListener(audioElement);
    } else {
        setTimeout(() => {
            listenForEndOfMessage();
        }, 1000);
    }
}
function setListener(element) {
    console.log("setting listener for " + element.getAttribute("title")); 
    element.addEventListener('ended', () => { storeCompletedMessage(element); })
}
//function storeCompletedMessage(audioElement) {
//    var category = audioElement.getAttribute("category");
//    var title = audioElement.getAttribute("title");
//    var completedMessages = [];
//    var completedStore = localforage.createInstance({
//        name: "Completed"
//    });
//    completedStore.getItem(category).then((value) => {
//        if (value == null) {
//            completedMessages.push(title);
//        } else {
//            if (!value.includes(title)) {
//                completedMessages = value;
//                completedMessages.push(title);
//            } else {
//                completedMessages = value;
//            }
//        }
//    }).then(() => {
//        completedStore.setItem(category, completedMessages); 
//    }).catch((err) => {
//        console.log(err);
//    })
//}
function storeCompletedMessage(audioElement) {
    var category = audioElement.getAttribute("category");
    var title = audioElement.getAttribute("title");
    var series = audioElement.getAttribute("series"); // The key used to store the data

    var seriesCollection = [];  // holds category objects [ (category)[messageTitles] ]

    var categoryTitle;      // The category title of the messages

    var completedMessages = []; //  messages

    var completedStore = localforage.createInstance({
        name: "CompletedStore"
    });
    completedStore.getItem(series).then((value) => {
        if (value == null) {
            createNew(series, category, title, completedStore); // if there is no series. Topical, versebyverse, etc make one. Key...
        } else {
            // The series exists ( Topical, Verse by Verse, Books, Teachings. etc)
            // Check if the CATEGORY is stored.
            checkSeriesForCategoryTitle(series, category, title, value, completedStore)
        }
    }).catch((err) => { console.log(err); })
}
function createNew(key, category, title, instance) {
    
    var seriesCollection = [];
    var categoryTitle;
    var completedMessages = [];

    completedMessages.push(title);
    categoryTitle = category;
    var object = { categoryTitle, completedMessages }
    seriesCollection.push(object);
    instance.setItem(key, seriesCollection);
}
function checkSeriesForCategoryTitle(key, category, title, value, instance) {
    console.log(value);
    var categoryTitles = [];    //All the categories Titles (stored in db) for that series
    var newSeriesCollection = [];
    var storedCollection = []
    for (var i = 0; i < value.length; i++) {
        console.log(value[i].categoryTitle);

        var cat = { categoryTitle: value[i].categoryTitle, completedMessages: value[i].completedMessages } // store the values 
        storedCollection.push(cat);
        categoryTitles.push(value[i].categoryTitle); //Use this to see if Category is already in db 
    }
    if (!categoryTitles.includes(category)) {
        console.log("nope: " + category);

        var object = { categoryTitle: category, completedMessages: title };
        storedCollection.push(object);
        
        instance.setItem(key, storedCollection);
    } else {
        console.log("already got this category lets check if the title is there " + title);
        checkCategoryTitle(key, category, title, value, instance);

    }
}
function checkCategoryTitle(key, category, title, value, instance) {
    console.log("In CCT: " + title);
    console.log("Checking Category: " + category);
    // get all the titles for a category;
    completedMessagesTitles = []; // store all the Titles
    storedCollection = []; 
    for (var i = 0; i < value.length; i++) {
        //var cat = { categoryTitle: value[i].categoryTitle, completedMessages: value[i].completedMessages } // store the values;
        //storedCollection.push(cat);
        if (value[i].categoryTitle == category) {       // Matching a CATEGORY here....
            console.log("Category: " + value[i].categoryTitle + " has " + value[i].completedMessages);
            if (!value[i].completedMessages.includes(title)) {
                console.log("nope");
                // add the title to the storedCollection
                // value[i].completedMessages has the list of messages ... Add the message title to that list. 
                console.log(value[i].completedMessages);
                console.log(value[i].completedMessages.length);
                for (var r = 0; r < value[i].completedMessages.length; r++) {
                    console.log("CompM: " + value[i].completedMessages[r]);
                    completedMessagesTitles.push(value[i].completedMessages[r]);
                }
                completedMessagesTitles.push(title); 
                var matchCat = { checkCategoryTitle: value[i].categoryTitle, completedMessages: completedMessagesTitles };
                console.log("MCAt: " + matchCat.completedMessages);
                storedCollection.push(matchCat);
                console.log("SCol: " + storedCollection.completedMessages);
                
            } else {
                console.log("yep");
                // dont bother to touch the value[i].completedMessages array.
                var matchCat = { categoryTitle: value[i].categoryTitle, completedMessages: value[i].completedMessages };
                storedCollection.push(matchCat);
            }
            
        } else {
            var cat = { categoryTitle: value[i].categoryTitle, completedMessages: value[i].completedMessages };
            storedCollection.push(cat);
            //return;
        }
    }
    
    
    

}
