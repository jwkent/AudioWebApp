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
function storeCompletedMessage(audioElement) {
    var category = audioElement.getAttribute("category");
    var title = audioElement.getAttribute("title");
    var completedMessages = [];
    var completedStore = localforage.createInstance({
        name: "Completed"
    });
    completedStore.getItem(category).then((value) => {
        if (value == null) {
            completedMessages.push(title);
        } else {
            if (!value.includes(title)) {
                completedMessages = value;
                completedMessages.push(title);
            } else {
                completedMessages = value;
            }
        }
    }).then(() => {
        completedStore.setItem(category, completedMessages); 
    }).catch((err) => {
        console.log(err);
    })
}

