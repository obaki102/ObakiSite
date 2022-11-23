window.ScrollToBottom = (elementName) => {
    element = document.getElementById(elementName);
    if (element !== null) {
        element.scrollTop = element.scrollHeight - element.clientHeight;
    }
}

window.DownloadPdfFile = (fileName, url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}