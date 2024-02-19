function setToFavorites(key, value) {
    var faveStore = localforage.createInstance({
        name: "Favorites"
    });
    faveStore.setItem(key, value);
}

function getAllFavorites() {
    var keyNames = [];
    var faveStore = localforage.createInstance({
        name: "Favorites"
    });
    return faveStore.iterate((value, key, iterationNumber) => {
        const fav = { Name: value.name, Source: value.source, TimeStamp: value.dateTimeStamp };
        keyNames.push(fav);
    }).then(() => {
        return (JSON.stringify(keyNames));
    }).catch((err) => {
        console.log(err);
        return [];
    });
}

function removeFavorite(key) {
    var faveStore = localforage.createInstance({
        name: "Favorites"
    });
    faveStore.removeItem(key);
}