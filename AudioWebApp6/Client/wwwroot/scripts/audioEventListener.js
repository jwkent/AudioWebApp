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
    element.addEventListener('ended', () => { storeCompletedMessage(element); })
}

function storeCompletedMessage(audioElement) {
    var category = audioElement.getAttribute("category");
    var title = audioElement.getAttribute("title");
    var series = audioElement.getAttribute("series"); 

    var seriesCollection = [];  

    var categoryTitle;      

    var completedMessages = []; 

    var completedStore = localforage.createInstance({
        name: "CompletedStore"
    });
    completedStore.getItem(series).then((value) => {
        if (value === null) {
            
            createNew(series, category, title, completedStore); 
        } else {
            
            checkAndCreateCategory(series, category, title, value, completedStore)
        }
    }).catch((err) => { console.log(err); })
}
function createNew(key, category, title, instance) {
    var seriesCollection = [];
    var completedMessages = [];
    completedMessages.push(title);
    var object = { categoryTitle: category, messageTitles: completedMessages }
    seriesCollection.push(object);
    instance.setItem(key, seriesCollection);
}
function checkAndCreateCategory(seriesTitle, categoryTitle, messageTitle, storedValues, dbInstance) {
    var data = [];
    data = storedValues;
    const categoryIndex = storedValues.findIndex(category => category.categoryTitle === categoryTitle);

    if (categoryIndex === -1) {

        data.push({ categoryTitle: categoryTitle, messageTitles: messageTitle ? [messageTitle] : [] });
        dbInstance.setItem(seriesTitle, data).then(function () {
            //console.log('category added successfully');
        }).catch(function (err) {
            console.error('Error saving data', err);
        });
    }
    else {
        checkAndCreateMessages(seriesTitle, categoryTitle, messageTitle, storedValues, dbInstance);
    }
}
function checkAndCreateMessages(seriesTitle, categoryTitle, messageTitle, storedValues, dbInstance) {
    var messageData = [];
    const categoryIndex = storedValues.findIndex(category => category.categoryTitle === categoryTitle);
    
    messageData = storedValues[categoryIndex];

    const messageIndex = storedValues[categoryIndex].messageTitles.findIndex(messages => messages === messageTitle);
    if (messageIndex === -1) {
        messageData.messageTitles.push(messageTitle);
        update(seriesTitle, messageData, storedValues, dbInstance);
    } 
}
function update(key, categoryMessages, storedValues, dbInstance) {

    var updatedStore = [];
    const oldCategoryIndex = storedValues.findIndex(category => category.categoryTitle === categoryMessages.categoryTitle);
    
    for (var i = 0; i < storedValues.length; i++) {
        if (i != oldCategoryIndex) {
            var object = { categoryTitle: storedValues[i].categoryTitle, messageTitles: storedValues[i].messageTitles };
            updatedStore.push(object);
        }
    }
    updatedStore.push(categoryMessages);
    
    dbInstance.setItem(key, updatedStore).then(function () {
        //console.log("series updated");
    }).catch(function (err) {
        console.error('error savingData', err);
    })
}
function getCompletedSeries(series) {

    var completedCategories = [];

    var completedStore = localforage.createInstance({
        name: "CompletedStore"
    });
    
    return completedStore.getItem(series).then((value) => { 
        for (var i = 0; i < value.length; i++) {
            const completed = {
                CategoryTitle: value[i].categoryTitle,
                MessageTitles: value[i].messageTitles
            }
            completedCategories.push(completed);
        }
        
        
    }).then(() => {
        //console.log(completedCategories);
        return (JSON.stringify(completedCategories));
    }).catch((err) => {
        console.log(err);
        return [];
    })
}