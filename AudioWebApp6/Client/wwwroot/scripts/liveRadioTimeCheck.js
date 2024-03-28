function GetLiveTime() {

    // Get the current date and time
    const date = new Date();

    // Get the current timezone offset in minutes
    const timezoneOffset = date.getTimezoneOffset();

    // Calculate the UTC time
    const utcTime = date.getTime() - (timezoneOffset * 60 * 1000);

    // Convert the UTC time to a Date object
    const utcDate = new Date(utcTime);

    // Check if it is the 2PM hour in Los Angeles
    const is2PMHour = (utcDate.getHours() === 14 - (timezoneOffset / 60)); // Adjust 2PM hour based on timezone offset);

    // Check if it's a weekday
    const isWeekday = (utcDate.getDay() >= 1 && utcDate.getDay() <= 5);

    // Return true if it is the 2PM hour and a weekday
    return (is2PMHour && isWeekday);

}
