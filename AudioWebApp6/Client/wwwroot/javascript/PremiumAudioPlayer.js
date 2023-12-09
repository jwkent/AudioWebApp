export function playbuttonaudio(isplaying) {
    var x = document.getElementById("audioplayer");
    
    if (isplaying === true) {
        x.play();    
    }
    else {
        x.pause();
    }
}