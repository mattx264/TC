var isInit = false;
var popupWindow = null;
chrome.browserAction.onClicked.addListener(function (tab) {
  chrome.tabs.query({ currentWindow: true, active: true }, function (tabs) {
    try {
      var tabId = tabs[0].id;
      if (isInit === true) {
        chrome.windows.get(popupWindow.id, function (win) {
          if (win !== undefined) {
            alert("Your browser recorder is active. You have to close active window to start new session.");
            return;
          } else {
            isInit = false;
            startNewPopup(tabId);
          }
        });
      } else {
        startNewPopup(tabId);
      }
    } catch (err) {
      console.log("ERROR " + err);
      return;
    }
  });
});
function startNewPopup(tabId) {
  isInit = true;
  chrome.windows.create({
    url: chrome.runtime.getURL("/index.html?id=" + tabId),
    type: "popup"
  }, function (win) {
    popupWindow = win;   
  });
  chrome.contextMenus.create({
    id: "test-element",
    title: "Test Element",
    contexts: ["all"]
  });

  chrome.extension.onConnect.addListener(function (port) {
    const networkFilters = {
      urls: ["<all_urls>"]
    };
    function sendPortMessage(obj) {
      try {
        port.postMessage(obj);
      } catch (err) {
        console.log("ERRROR" + err)
      }
    }
    chrome.webRequest.onBeforeSendHeaders.addListener(function (details) {
      if (details.type === "xmlhttprequest") {

        sendPortMessage({ type: 'xhrStart', data: details });
      }

    }, networkFilters, ['requestHeaders', 'blocking']);

    chrome.webRequest.onCompleted.addListener(function (details) {
     
      if (details.type === "xmlhttprequest") {

        sendPortMessage({ type: 'xhrDone', data: details });
      }
    }, networkFilters);


    port.onDisconnect.addListener(function (data) {
      console.log("Is onDisconnect");
    });

    var clickedEl = null;

    document.addEventListener("click", function (event) {
      //right click
      if (event.button == 2) {
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
}