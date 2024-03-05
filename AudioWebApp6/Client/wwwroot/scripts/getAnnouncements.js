function getAnnouncements( key) {
    return localforage.getItem(key).then(function (value) {
        if (value !== null) {
            return value
        } else {
            return null
        }
    })
}