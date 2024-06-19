function listenForEndOfMessage() {
    const audioElement = document.getElementById("audioPlayer");

    if (audioElement) {
        setListener(audioElement);
    } else {
        setTimeout(() => {
            console.log("Delayed exe after 1 second");
            listenForEndOfMessage();
        }, 1000);
    }
}
function setListener(element) {
    console.log("setting listener for " + element.getAttribute("title")); 
    element.addEventListener('ended', () => {
        console.log("The message has ended...");
        //var completedMessageStore = localforage.createInstance({
        //    name: "Completed"
        //});
        
        //completedMessageStore.setItem(element.getAttribute("category"), element.getAttribute("title"));
        storeCompletedMessage(element);
        
    })
}
function storeCompletedMessage(audioElement) {
    var category = audioElement.getAttribute("category");
    var title = audioElement.getAttribute("title");
    var completedMessages = [];
    var completedStore = localforage.createInstance({
        name: "Complete"
    });
    completedStore.getItem(category).then((value) => {
        if (value == null) {
            console.log("the value: " + value);
            
            completedMessages.push(title);
        } else {
            for (var i = 0; i < value.length; i++) {
                if (value[i] !== title) {
                    completedMessages.push(value[i]);
                } else {
                    completedMessages.pop(value[i]);
                }
            } 
            completedMessages.push(title);
        }
    }).then(() => {
        completedStore.setItem(category, completedMessages); 
    }).catch((err) => {
        console.log(err);
    })
}

