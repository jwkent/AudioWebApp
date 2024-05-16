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
            album: audioElement.getAttribute("category"),
            artwork: [
                {
                    src: "./images/TNPMedia96.png",
                    sizes: "96x96",
                    type: "image/png",
                },
                {
                    src: "./images/TNPMedia128.png",
                    sizes: "128x128",
                    type: "image/png",
                },
                {
                    src: "./images/TNPMedia192.png",
                    sizes: "192x192",
                    type: "image/png",
                },
                {
                    src: "./images/TNPMedia256.png",
                    sizes: "256x256",
                    type: "image/png",
                },
                {
                    src: "./images/TNPMedia384.png",
                    sizes: "384x384",
                    type: "image/png",
                },
                {
                    src: "./images/TNPMedia512.png",
                    sizes: "512x512",
                    type: "image/png",
                },
            ],
        });
        navigator.mediaSession.setActionHandler('play', () => {
            audioElement.play();
        });
        navigator.mediaSession.setActionHandler('pause', () => {
            audioElement.pause();
        });
        navigator.mediaSession.setActionHandler('seekforward', () => {
            audioForward();
        });
        navigator.mediaSession.setActionHandler('seekbackward', () => {
            audioReverse();
        })
    }
}