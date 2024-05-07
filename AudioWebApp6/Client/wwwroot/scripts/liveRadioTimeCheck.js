function GetLiveTime() {
  // get weekday and hour (24-based) of Los Angeles in U.S English
  const parts = new Intl.DateTimeFormat("en-US", {
    timeZone: "America/Los_Angeles",
    hour: "numeric",
    hour12: false,
    weekday: "long",
  }).formatToParts(new Date())

  const day = parts.find((p) => p.type === "weekday").value
  const hour = parts.find((p) => p.type === "hour").value

  const isTwoPM = hour === "14"
  const isWeekday = day !== "Saturday" && day !== "Sunday"

  return isWeekday && isTwoPM
}
