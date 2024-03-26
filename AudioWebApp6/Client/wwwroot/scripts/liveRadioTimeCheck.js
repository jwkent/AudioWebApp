function isBetweenTwoAndThreeOnWeekdaysPacificTime() {

    // Get current date and time in UTC
    var nowUtc = new Date();

    // Get Pacific Timezone offset in milliseconds
    var pacificOffsetMs = -7 * 60 * 60 * 1000; // PST is UTC-7

    // Adjust current time with Pacific Timezone offset
    var nowPacific = new Date(nowUtc.getTime() + pacificOffsetMs);

    // Check if it's Monday through Friday (1-5 represent Monday-Friday in JavaScript)
    var dayOfWeek = nowPacific.getDay(); // Sunday is 0, Monday is 1, ..., Saturday is 6
    if (dayOfWeek >= 1 && dayOfWeek <= 5) {
        // Check if it's between 2:00 and 3:00
        var hours = nowPacific.getHours();
        var minutes = nowPacific.getMinutes();
        console.log("it is not " + hours);
        //if (hours === 2 && minutes >= 0 || hours === 3 && minutes <= 0) {
        if (hours === 12 && minutes >= 0 || hours === 13 && minutes <= 0) {
            // Time is between 2:00 and 3:00
            return true;
        }
    }

    // If not within the specified range or on a weekday, return false
    return false;
}