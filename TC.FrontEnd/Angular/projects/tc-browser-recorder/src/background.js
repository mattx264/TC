chrome.browserAction.onClicked.addListener(function (tab) {
  chrome.tabs.query({ currentWindow: true, active: true }, function (tabs) {

    var tabId = tabs[0].id;
    chrome.windows.create({
      //  url: chrome.runtime.getURL("/BrowserExtension/index.html?id=" + tabId),
      url: chrome.runtime.getURL("/index.html?id=" + tabId),
      type: "popup"
    }, function (win) {
      // win represents the Window object from windows API
      // Do something after opening
    });
  });
  // function searchgooglemaps(info) {
  //   var searchstring = info.selectionText;
  //   chrome.tabs.create({ url: "http://maps.google.com/maps?q=" + searchstring })
  // }

  chrome.contextMenus.create({
    id: "test-element",
    title: "Test Element",
    contexts: ["all"]
  });
  var clickedEl = null;

  document.addEventListener("click", function(event){
    //right click
    if(event.button == 2) { 
        clickedEl = event.target;
    }
}, true);
  chrome.contextMenus.onClicked.addListener(function (info, tab) {
    if (info.menuItemId == "test-element") {
      console.log("yay!");
      console.log(clickedEl);
    }
  });
});
