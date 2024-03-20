function audioForward() {
    const audioPlayer = document.getElementById("audioPlayer");
    audioPlayer.currentTime += 10;
}
function audioReverse() {
    const audioPlayer = document.getElementById("audioPlayer");
    audioPlayer.currentTime -= 10;
}