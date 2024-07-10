function listenForErrors(mainLayoutComponent) {
    const audioElement = document.getElementById("audioPlayer");
    const errorElement = document.getElementById("playerErrors");
    if (audioElement === null) {
        setTimeout(() => {
            listenForErrors()
        }, 1000);
    }
    else {
        
        const audiotitle = audioElement.getAttribute('title');

        audioElement.addEventListener("play", (event) => {
            console.log("The media is in play.");
            errorElement.style.visibility = 'hidden';
        });
        audioElement.addEventListener("playing", (event) => {
            errorElement.style.visibility = 'hidden';
        })
        audioElement.addEventListener("canplay", (event) => {
            console.log("The media CanPlay. ");
        })
        audioElement.onplaying = (event) => {
            console.log("The media has began playing.");
            errorElement.style.visibility = 'hidden';
        }
        audioElement.onloadeddata = (event) => {
            console.log("The media meta data is loaded.");
            errorElement.style.visibility = 'hidden';
        }
        
        const controller = new AbortController();
        const errorHandler = (event) => {
            
            if (mainLayoutComponent === undefined) {
                setTimeout(() => {
                    audioElement.dispatchEvent(new Event("error"));
                }, 500);
            } else {
                controller.abort();
                handleAudioError(mainLayoutComponent);
            } 

        }
        //fired when the resource could not be loaded due to an error (for example, a network connectivity problem).
        audioElement.addEventListener("error", errorHandler, { 'signal': controller.signal });
        
        // fired when the user agent is trying to fetch media data, but data is unexpectedly not forthcoming
        audioElement.addEventListener("stalled", (event) => {
            console.log("The media is stalled.");
            errorElement.style.visiblity = 'visible';
            if (audioElement.onplaying) {
                console.log("Stalled Cancelled");
                errorElement.style.visibility = 'hidden';
            }
        });

        // fired when media data loading has been suspended
        audioElement.addEventListener("suspend", (event) => {
            console.log("The media is suspended." + audiotitle);
            errorElement.style.visibility = 'visible';
            if (audioElement.onplaying) {
                console.log("Suspended Cancelled");
                setTimeout(() => { errorElement.style.visibility = 'hidden'; }, 500);
            }
        });

        //fired when playback has stopped because of a temporary lack of data.
        audioElement.addEventListener("waiting", (event) => {
            console.log("The media is waiting." + audiotitle);
            console.log("The media is waiting...lack of data");
            errorElement.style.visibility = 'visible';
            if (audioElement.onplaying) {
                console.log("Waiting Cancelled");
                setTimeout(() => { errorElement.style.visibility = 'hidden'; }, 500);
            }
        });
        // fired when the browser has started to load a resource
        audioElement.addEventListener("loadstart", (event) => {
            errorElement.style.visibility = 'visible';
            if (audioElement.onplaying) {
                console.log("LoadStart Cancelled");
                setTimeout(() => { errorElement.style.visiblity = 'hidden'; }, 500);
            }
        });
    }

}
function handleAudioError(mainLayoutComponent) {
        
    mainLayoutComponent.invokeMethodAsync('AudioReload');  
}
function getStartTime(key) {
    localforage.getItem(key).then((value) => {
        let storedValue = value !== null ? value : 0;
        timeStamper(storedValue);
    }).catch((err) => {
        console.log(err);
    });
}
function timeStamper(storedValue) {

    installIOSPatchIfRequired();
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

    // iOS PWA's demonstrate a bug whereby the DOM elements are seemingly
    // deallocated when media playback stops, and can no longer be restarted.
    // To circumvent this, we create a player that is loaded to a silent mp3 and
    // has playback rate of 0. This causes it to be "playing" indefinitely, without
    // interfering with OS hooks to automatically determine media position.
    function installIOSPatchIfRequired() {
        // https://stackoverflow.com/questions/9038625/detect-if-device-is-ios
        const isIOS = ['iPad', 'iPhone', 'iPod'].includes(navigator.platform)
            // iPad on iOS 13 detection
            || (navigator.userAgent.includes("Mac") && "ontouchend" in document);
        const isPWA = window.matchMedia("(display-mode: standalone)").matches;

        if (isIOS && isPWA) {
            if (!document.getElementById("silencePlayer")) {
                const silenceElement = document.createElement('audio');
                silenceElement.id = "silencePlayer";
                silenceElement.autoplay = true;
                silenceElement.playbackRate = 0.0;
                // 16 samples of silence, encoded as an unsigned 8 bit PCM WAV via Audacity, then converted to base64.
                silenceElement.src = 'data:audio/wav;base64,UklGRjQAAABXQVZFZm10IBAAAAABAAEARKwAAESsAAABAAgAZGF0YRAAAACAgICAgICAgICAgICAgICA';

                const audioElement = document.getElementById("audioPlayer");
                audioElement.parentElement.appendChild(silenceElement);
            }
        }
    }
}