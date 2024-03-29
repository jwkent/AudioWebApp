function audioForward() {
    const audioPlayer = document.getElementById("audioPlayer");
    audioPlayer.currentTime += 10;
}
function audioReverse() {
    const audioPlayer = document.getElementById("audioPlayer");
    audioPlayer.currentTime -= 10;
}
function audioRate() {
    const audioPlayer = document.getElementById("audioPlayer");
    console.log("The AUDIOspeed is " + audioPlayer.playbackRate);
    //audioPlayer.playbackRate = speed;
    //console.log("event fired");
    //console.log("The speed is " + audioPlayer.playbackRate);

}