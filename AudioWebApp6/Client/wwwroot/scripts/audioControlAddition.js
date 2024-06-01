function audioForward() {
    const audioPlayer = document.getElementById("audioPlayer");
    audioPlayer.currentTime += 10;
}
function audioReverse() {
    const audioPlayer = document.getElementById("audioPlayer");
    audioPlayer.currentTime -= 10;
}
function audioPlaybackRate(speed) {
    const audioPlayer = document.getElementById("audioPlayer");
    audioPlayer.playbackRate = speed;

}
function shareAudioLink(link) {
    navigator.clipboard.writeText(link).then(function () {
        // Success callback
        console.log('Text copied to clipboard successfully!');
    }).catch(function (error) {
        // Error callback
        console.error('Could not copy text: ', error);
    });
}
function sendSMS(source, title) {
    var phoneNumber = "";
    //console.log("in js sendSMS");
    window.open(`sms:${phoneNumber}?body=${encodeURIComponent(source)}`);
}
