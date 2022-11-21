window.ScrollToBottom = (elementName) => {
    element = document.getElementById(elementName);
    if (element !== null) {
        element.scrollTop = element.scrollHeight - element.clientHeight;
    }
}