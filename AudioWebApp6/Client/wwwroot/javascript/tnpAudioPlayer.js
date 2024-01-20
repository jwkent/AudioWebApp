const playerIconContainer = document.getElementById('play-icon');
const audioPlayerContainer = document.getElementById('audio-player-container');
const seekSlider = document.getElementById('seek-slider');
const volumeSlider = document.getElementById('volume-slider');
const outputContainer = document.getElementById('volume-output');
const muteIconContainer = document.getElementById('mute-icon');
const icon = document.getElementById('play-pause');
const iconVolume = document.getElementById('mute-unmute');

let playState = 'play';
let muteState = 'unmuted';

playerIconContainer.addEventListener('click', () => {
    if (playState === 'play') {
        audio.play();
        requestAnimationFrame(whilePlaying);
        icon.classList.toggle('bi-play-fill');
        icon.classList.toggle('bi-pause-fill');
        playState = 'pause';
    } else {
        audio.pause();
        cancelAnimationFrame(raf);
        icon.classList.toggle('bi-pause-fill');
        icon.classList.toggle('bi-play-fill');
        playState = 'play';
    }
});
muteIconContainer.addEventListener('click', () => {
    if (muteState === 'unmuted') {
        iconVolume.classList.toggle('bi-volume-up-fill');
        iconVolume.classList.toggle('bi-volume-mute-fill');
        audio.muted = true;
        muteState = 'mute';
    } else {
        iconVolume.classList.toggle('bi-volume-mute-fill');
        iconVolume.classList.toggle('bi-volume-up-fill');
        audio.muted = false;
        muteState = 'unmuted';
    }
});

const showRangeProgess = (rangeInput) => {
    if (rangeInput === seekSlider) {
        audioPlayerContainer.style.setProperty('--seek-before-width', rangeInput.value / rangeInput.Max * 100 + '%');
    } else {
        audioPlayerContainer.style.setProperty('--volume-before-width', rangeInput.value / rangeInput.Max * 100 + '%');
    }
};

seekSlider.addEventListener('input', (e) => {
    showRangeProgess(e.target);
});
volumeSlider.addEventListener('input', (e) => {
    showRangeProgess(e.target);
});

/** Functionality of the Audio Player */
const audio = document.querySelector('audio');
audio.volume = volumeSlider.value / 100;
const durationContainer = document.getElementById('duration');
const currentTimeContainer = document.getElementById('current-time');
let raf = null;
const calculateTime = (secs) => {
    const hours = Math.floor(secs / 3600);
    secs %= 3600;
    const minutes = Math.floor(secs / 60);
    const returnedMinutes = minutes < 10 ? `0${minutes}` : `${minutes}`;
    const seconds = Math.floor(secs % 60);
    const returnedSeconds = seconds < 10 ? `0${seconds}` : `${seconds}`;
    return `${hours}:${returnedMinutes}:${returnedSeconds}`;
};

const displayDuration = () => {
    durationContainer.textContent = calculateTime(audio.duration);
};
const setSliderMax = () => {
    seekSlider.max = Math.floor(audio.duration);
};

const displayBufferedAmount = () => {
    if (audio.buffered.length === null || audio.buffered.length === 0) {
        audioPlayerContainer.style.setProperty('--buffered-width', "0%");
    } else {
        if (audio.buffered.length === 1) {
            audioPlayerContainer.style.setProperty('--buffered-width', "100%");
        } else {
            const bufferedAmount =
                Math.floor(audio.buffered.end(audio.buffered.length - 1));
            audioPlayerContainer.style.setProperty('--buffered-width', `${(bufferedAmount / seekSlider.max) * 100}%`);

        }
    }

}
const whilePlaying = () => {
    seekSlider.value = Math.floor(audio.currentTime);
    currentTimeContainer.textContent = calculateTime(seekSlider.value);
    audioPlayerContainer.style.setProperty('--seek-before-width', `${seekSlider.value / seekSlider.max * 100}%`);
    raf = requestAnimationFrame(whilePlaying);
}
if (audio.readyState > 0) {
    displayDuration();
    setSliderMax();
    displayBufferedAmount();
} else {
    audio.addEventListener('loadedmetadata', () => {
        displayDuration();
        setSliderMax();
        displayBufferedAmount();
    });
}
audio.addEventListener('progress', displayBufferedAmount);
audio.addEventListener('loadedmetatdata', () => {
    displayDuration();
    setSliderMax();
    displayBufferedAmount();
});
seekSlider.addEventListener('input', () => {
    currentTimeContainer.textContent = calculateTime(seekSlider.value);
    if (!audio.paused) {
        cancelAnimationFrame(raf);
    }
});
seekSlider.addEventListener('change', () => {
    audio.currentTime = seekSlider.value;
    if (!audio.paused) {
        requestAnimationFrame(whilePlaying);
    }
});
audio.addEventListener('timeupdate', () => {
    seekSlider.value = Math.floor(audio.currentTime);
});
volumeSlider.addEventListener('input', (e) => {
    const value = e.target.value;
    outputContainer.textContent = value;
    audio.volume = value / 100;
});

