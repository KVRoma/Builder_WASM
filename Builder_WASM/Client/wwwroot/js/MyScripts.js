function openFile(array, dataType, tabName) {
    // Create a Blob object from the array
    var file = new Blob([array], { type: [dataType] });
    // Create a URL for the Blob
    var fileURL = URL.createObjectURL(file);
    // Open the URL in a new tab
    var newWindow = window.open(fileURL, '_blank');
    setTimeout(function () {
        newWindow.document.title = tabName;        
    }, 1000);
}

function saveFile(array, dataType, fileName) {

    var file = new Blob([array], { type: [dataType] });    
    var fileURL = URL.createObjectURL(file);

    var link = document.createElement('a');
    link.href = fileURL;
    link.download = fileName;
    link.dispatchEvent(new MouseEvent('click'));
}