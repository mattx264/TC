
class Main {
    xpathHelper: XpathHelper;
    tempEventElement: HTMLElement;
    tempElementValue: string;
    rightClickElementClicked: HTMLElement;
    constructor() {
       new RequestionMonitor().startMonitor(this);
        this.xpathHelper = new XpathHelper();
        document.addEventListener("click", this.addClickEventListener);
        document.addEventListener("mousedown", this.addRightMouseListener);

        document.addEventListener("keyup", this.addKeyDownEventListener);
        document.addEventListener("dblclick", this.addDoubleClickEventListener);

        chrome.runtime.onMessage.addListener((message: any, sender: any, sendResponse: any) => {
            if (message.method === 'getUrl') {
                this.sendMessage({
                    action: 'goToUrl',
                    value: location.href,
                    xpath: ''
                });
            }
        });
    }
    addKeyDownEventListener = (e: KeyboardEvent) => {
        if (e.code == "Tab") {

            const newEle = document.activeElement as HTMLInputElement;
            if (newEle.nodeName == "INPUT") {
                this.tempEventElement.removeEventListener("blur", this.inputOnBlur);
                this.addEventToEventElement(newEle);
            }
            this.sendMessage({
                action: 'sendKeys', xpath: xpath, value: 'Keys.TAB'
            })
            return;
        } else if (e.keyCode == 13) {//ENTER 
            e.preventDefault();
            const newEle = document.activeElement as HTMLInputElement;
            if (newEle.nodeName == "INPUT") {
                this.tempElementValue = '';
                this.getInputTextAndSend(newEle);
            }
            var xpath = this.xpathHelper.getInputElementXPath(newEle);
            this.sendMessage({
                action: 'sendKeys', xpath: xpath, value: 'Keys.ENTER'
            })
            setTimeout(() => {
                document.querySelector('form').submit();
            }, 100);
        }
        /*when user selected input and put value it createing new blur event so it not possible to detect change in input
         if (this.tempEventElement != null && e.code != "Enter") {
             const newEle = document.activeElement as HTMLInputElement;
             if (newEle.nodeName == "INPUT") {
                 this.tempEventElement.removeEventListener("blur", this.inputOnBlur);
                 this.addEventToEventElement(newEle);
             }
             return;
         }*/
        console.log(e);
    }
    addClickEventListener = (e: Event) => {
        var xpath = this.xpathHelper.getActionElementXPath(e.target as Node);
        if (xpath === '/HTML') {
            xpath = this.xpathHelper.getElementXPath(e.target as Node);
        }
        if (xpath === null) {
            return;
        }
        var data = { action: 'click', xpath: xpath }
        //window.postMessage(data, "*")
        // window.postMessage({ type: "FROM_PAGE", text: "Hello from the webpage!" }, "*");
        var ele = this.xpathHelper.getElementByXPath(xpath, document);
        if (ele.nodeName === "INPUT") {
            if (!ele.isSameNode(this.tempEventElement) && this.tempEventElement != null) {
                this.tempEventElement.removeEventListener("blur", this.inputOnBlur);
            }
            if (!ele.isSameNode(this.tempEventElement)) {
                this.addEventToEventElement(ele);
            }
        } else if (ele.nodeName === "SELECT") {
            if (!ele.isSameNode(this.tempEventElement) && this.tempEventElement != null) {
                this.tempEventElement.removeEventListener("blur", this.inputOnBlur);
            }
            if (!ele.isSameNode(this.tempEventElement)) {
                this.addEventToEventElement(ele);
            }
        }
        else if (this.tempEventElement != null) {
            this.tempEventElement.removeEventListener("blur", this.inputOnBlur);
            this.tempEventElement = null;
        }
        if (ele.nodeName === "BUTTON") {

        }
        this.sendMessage(data);
    }
    addDoubleClickEventListener = (e: MouseEvent) => {
        console.log("double click");
        console.log(e)
    }
    addRightMouseListener = (e: MouseEvent) => {
        if (e.which !== 3) {
            return;
        }
        this.rightClickElementClicked = e.target as HTMLElement;
        this.rightClickElementClicked.classList.add("tc-selected-element");
        //right click 
    }
    inputOnBlur = (event: FocusEvent) => {

        this.getInputTextAndSend(event.currentTarget as HTMLInputElement);

    }
    sendMessage(data: { action: string, xpath: string, value?: string }) {
        console.log(data);
        if (chrome.runtime) {
            chrome.runtime.sendMessage(data, function (response: any) {
                console.log(response);
            });
        }
    }
    private getInputTextAndSend(node: HTMLInputElement) {
        if (this.tempElementValue == (node).value) {
            return;
        }
        this.tempElementValue = (node).value;
        var xpath = this.xpathHelper.getActionElementXPath(this.tempEventElement);
        if (xpath === '/HTML') {
            xpath = this.xpathHelper.getElementXPath(this.tempEventElement);
        }
        let data = { action: 'sendKeys', xpath: xpath, value: (node).value };
        if (node.nodeName === "INPUT") {
            data.action= 'sendKeys';
        } else if (node.nodeName === "SELECT") {
            data.action= 'selectByValue';
        }

        this.sendMessage(data);

    }
    private addEventToEventElement(ele: HTMLInputElement) {

        this.tempElementValue = JSON.parse(JSON.stringify(ele.value));
        ele.addEventListener("blur", this.inputOnBlur);
        this.tempEventElement = ele;
    }
}
// //document.addEventListener('DOMContentLoaded', function () {
new Main();
// //}, false);


