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
    navigator.clipboard.writeText(link)
}
function sendSMS(source, title) {
    window.open('sms:${phoneNumber}?body=${endcodeURIComponent(message)}');
}
function sendEmail(source, title) {
    window.open(`mailto:${email}?subject=${encodeURIComponent(subject)}&body=${encodeURIComponent(body)}`);
}