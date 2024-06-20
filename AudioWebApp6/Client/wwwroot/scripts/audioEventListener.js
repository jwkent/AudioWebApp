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
    var completedMessages = [];
    var completedSeries = { category, completedMessages };
    var completedStore = localforage.createInstance({
        name: "CompletedStore"
    });
    completedStore.getItem(series).then((value) => {
        if (value == null) {
            createNew(series, category, title, completedStore);
        } else {
            completedSeries = { category: value.category, completedMessages: value.completedMessages }
            checkForCategory(series, category, title, completedSeries, completedStore)
        }
    }).catch((err) => { console.log(err); })
}
function createNew(key, category, title,instance) {
    var completedMessages = [];
    var completedSeries = { category, completedMessages }
    completedMessages.push(title);
    
    instance.setItem(key, completedSeries);
}
function checkForCategory(key, category, title, completedSeries, instance) {
    console.log(completedSeries);
    // check if the category is in the completedSeries
}
