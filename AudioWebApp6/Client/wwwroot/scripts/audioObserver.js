function getStartTime(key) {
    localforage.getItem(key).then((value) => {
        let storedValue = value !== null ? value : 0;
        timeStamper(storedValue);
    }).catch((err) => {
        console.log(err);
    });
}
function timeStamper(storedValue) {
    const audioElement = document.getElementById("audioPlayer");
    audioElement.preload = 'auto';
    audioElement.addEventListener('loadedmetadata', () => audioElement.currentTime = storedValue);
    audioElement.addEventListener('timeupdate', (event) => {
        localforage.setItem(audioElement.getAttribute("src"), audioElement.currentTime).then((value) => { })
            .catch((err) => {
                console.log(err);
            });
    });
    if ('mediaSession' in navigator) {
        navigator.mediaSession.metadata = new MediaMetadata({
            title: audioElement.getAttribute("title"),
            artist: audioElement.getAttribute("artist"),
            album: audioElement.getAttribute("category")
        });
    }
}