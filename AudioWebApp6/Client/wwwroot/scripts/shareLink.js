function shareAudioLink(link) {
    navigator.clipboard.writeText(link).then(function () {
        alert("Copied to clipboard");
    })
        .catch(function (error) {
            alert(error);
        });
}
