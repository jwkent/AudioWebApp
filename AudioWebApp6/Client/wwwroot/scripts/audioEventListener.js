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
    var completedStore = localforage.createInstance({
        name: "Complete"
    });
    try {
        const existingValues = await completedStore.getItem(audioElement.getAttribute("category"));
        if (existingValues) {
            existingValues.push(audioElement.getAttribute("title"));
            await completedStore.setItem(audioElement.getAttribute("category"), audioElement.getAttribute("title"));

        } else {
            await localforage.setItem(audioElement.getAttribute("category"), [audioElement.getAttribute("title")]);
        }

        console.log(`Value '${value}' updated or stored successfully for key '${key}'.`);
    } catch (error) {
        console.error(`Error updating or storing value for key '${key}': ${error.message}`);
    }
}

