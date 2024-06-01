function getTitleProperty() {
    const titleElement = document.querySelector("title");
    if (titleElement) {
        return titleElement.innerHTML;
    } else {
        return "The Narrow Path";
    }
}

