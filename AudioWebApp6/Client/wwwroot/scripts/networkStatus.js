window.networkStatus = {
    onDisconnected: function (callback) {
        window.addEventListener('offline', function () {
            callback.invokeMethodAsync('OnDisconnected');
        });
    },
    onConnected: function (callback) {
        window.addEventListener('online', function () {
            callback.invokeMethodAsync('OnConnected');
        });
    }
};