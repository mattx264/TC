"use strict";
var Main = /** @class */ (function () {
    function Main() {
        var _this = this;
        this.addKeyDownEventListener = function (e) {
            if (e.code == "Tab") {
                var newEle = document.activeElement;
                if (newEle.nodeName == "INPUT") {
                    _this.tempEventElement.removeEventListener("blur", _this.inputOnBlur);
                    _this.addEventToEventElement(newEle);
                }
                return;
            }
            if (_this.tempEventElement != null && e.code != "Enter") {
                var newEle = document.activeElement;
                if (newEle.nodeName == "INPUT") {
                    _this.tempEventElement.removeEventListener("blur", _this.inputOnBlur);
                    _this.addEventToEventElement(newEle);
                }
                return;
            }
            console.log(e);
        };
        this.addClickEventListener = function (e) {
            var xpath = _this.xpathHelper.getActionElementXPath(e.target);
            if (xpath === '/HTML') {
                xpath = _this.xpathHelper.getElementXPath(e.target);
            }
            if (xpath === null) {
                return;
            }
            var data = { action: 'click', xpath: xpath };
            //window.postMessage(data, "*")
            // window.postMessage({ type: "FROM_PAGE", text: "Hello from the webpage!" }, "*");
            var ele = _this.xpathHelper.getElementByXPath(xpath, document);
            if (ele.nodeName === "INPUT") {
                if (!ele.isSameNode(_this.tempEventElement) && _this.tempEventElement != null) {
                    _this.tempEventElement.removeEventListener("blur", _this.inputOnBlur);
                }
                if (!ele.isSameNode(_this.tempEventElement)) {
                    _this.addEventToEventElement(ele);
                }
            }
            else if (_this.tempEventElement != null) {
                _this.tempEventElement.removeEventListener("blur", _this.inputOnBlur);
                _this.tempEventElement = null;
            }
            if (ele.nodeName === "BUTTON") {
            }
            _this.sendMessage(data);
        };
        this.inputOnBlur = function (event) {
            _this.getInputTextAndSend(event.currentTarget);
        };
        console.log("START");
        this.xpathHelper = new XpathHelper();
        document.addEventListener("click", this.addClickEventListener);
        document.addEventListener("keyup", this.addKeyDownEventListener);
        document.querySelector('form').onkeypress = function (e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                var newEle = document.activeElement;
                if (newEle.nodeName == "INPUT") {
                    _this.tempElementValue = '';
                    _this.getInputTextAndSend(newEle);
                }
                var xpath = _this.xpathHelper.getInputElementXPath(newEle);
                _this.sendMessage({
                    action: 'sendKeys', xpath: xpath, value: 'Keys.ENTER'
                });
                setTimeout(function () {
                    document.querySelector('form').submit();
                }, 100);
            }
        };
        // window.addEventListener("beforeunload", function (e) {
        //     e.preventDefault();
        //     // Chrome requires returnValue to be set
        //     e.returnValue = '';
        // });   
        chrome.runtime.onMessage.addListener(function (message, sender, sendResponse) {
            if (message.method === 'getUrl') {
                _this.sendMessage({
                    action: 'goToUrl',
                    value: location.href,
                    xpath: ''
                });
            }
        });
    }
    Main.prototype.sendMessage = function (data) {
        console.log(data);
        if (chrome.runtime) {
            chrome.runtime.sendMessage(data, function (response) {
                console.log(response);
            });
        }
    };
    Main.prototype.getInputTextAndSend = function (node) {
        if (this.tempElementValue == (node).value) {
            return;
        }
        this.tempElementValue = (node).value;
        var xpath = this.xpathHelper.getActionElementXPath(this.tempEventElement);
        if (xpath === '/HTML') {
            xpath = this.xpathHelper.getElementXPath(this.tempEventElement);
        }
        var data = { action: 'sendKeys', xpath: xpath, value: (node).value };
        this.sendMessage(data);
        // this.tempEventElement.removeEventListener("blur", this.inputOnBlur);
        // this.tempEventElement = null;
    };
    Main.prototype.addEventToEventElement = function (ele) {
        // this.tempElementValue = (Object as any).assign({}, ele.value); 
        this.tempElementValue = ele.value;
        ele.addEventListener("blur", this.inputOnBlur);
        this.tempEventElement = ele;
    };
    return Main;
}());
//document.addEventListener('DOMContentLoaded', function () {
new Main();
//}, false);
