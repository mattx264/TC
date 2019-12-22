import { OperatorModel } from "../../../shared/src/lib/models/operatorModel";

class Main {
    xpathHelper: XpathHelper;
    tempEventElement: HTMLElement;
    tempElementValue: string;
    rightClickElementClicked: HTMLElement;
    actionLog: OperatorModel[] = [];
    constructor() {

        new RequestionMonitor().startMonitor(this.sendMessage.bind(this));
        this.xpathHelper = new XpathHelper();
        // click is not always working so let try mousedown
        //document.addEventListener("click", this.addClickEventListener);
        document.addEventListener("mousedown", this.addRightMouseListener);

        document.addEventListener("keyup", this.addKeyUpEventListener);
        document.addEventListener("keydown", this.addKeyDownEventListener);
        document.addEventListener("dblclick", this.addDoubleClickEventListener);

        chrome.runtime.onMessage.addListener((message: any, sender: any, sendResponse: any) => {
            switch (message.method) {
                case 'getUrl':
                    this.sendMessage({
                        action: 'goToUrl',
                        value: location.href,
                        path: null
                    });

                    break;
                case 'startXHRMonitor':

                    break;
                default:
                    throw new Error("Message method not support - add new case " + message.method);
                   
            }
            sendResponse({successful:true})
        });
        this.sendMessageToPopup({ type: 'hello' });
    }
    addKeyDownEventListener = (e: KeyboardEvent) => {
        const activeElement = document.activeElement as HTMLInputElement;
        var xpath = this.xpathHelper.getInputElementXPath(activeElement);

        if (e.keyCode == 13) {//ENTER 
            e.preventDefault();
            this.sendMessage({
                action: 'sendKeys', path: xpath, value: 'Keys.ENTER'
            })
            setTimeout(() => {
                //TODO check if object 'e' can be resubmited. - issue can be when you have more than 1 form
                document.querySelector('form').submit();
            }, 100);
        }
    };
    addKeyUpEventListener = (e: KeyboardEvent) => {
        const activeElement = document.activeElement as HTMLInputElement;
        var xpath = this.xpathHelper.getInputElementXPath(activeElement);
        if (e.code == "Tab" || e.code == "ShiftLeft" || e.code == "ShiftRight"
            || e.code == "ControlRight" || e.code == "ControlLeft" || e.code == "AltRight" || e.code == "AltLeft") {
            this.sendMessage({
                action: 'sendKeys', path: xpath, value: 'Keys.' + e.code.toUpperCase()
            });
            return;
        } else if (e.keyCode == 13) {
            //ENTER is handler in addKeyDownEventListener
            return;
        } else if (e.code === "Backspace") {

            this.sendMessage({
                action: 'sendKeys', path: xpath, value: 'Keys.BACKSPACE'
            })

        }
        else if (activeElement instanceof HTMLInputElement) {

            var xpath = this.xpathHelper.getActionElementXPath(activeElement);
            if (xpath === '/HTML') {
                xpath = this.xpathHelper.getElementXPath(activeElement);
            }
            let data = { action: 'sendKeys', path: xpath, value: e.key };
            this.sendMessage(data);
        }
    }
    addClickEventListener = (e: Event) => {
        this.checkAndMonitorSelectElement(e.target);
        var xpath = this.xpathHelper.getActionElementXPath(e.target as Node);
        if (xpath === '/HTML') {
            xpath = this.xpathHelper.getElementXPath(e.target as Node);
        }
        if (xpath === null) {
            return;
        }
        var data: OperatorModel = { action: 'click', path: xpath, value: null }

        this.tempEventElement = this.xpathHelper.getElementByXPath(xpath, document);


        this.sendMessage(data);
    }
    addDoubleClickEventListener = (e: MouseEvent) => {
        console.log("double click");
        console.log(e)
    }
    addRightMouseListener = (e: MouseEvent) => {
        if (e.which !== 3) {
            this.addClickEventListener(e);
        }
        this.rightClickElementClicked = e.target as HTMLElement;
        this.rightClickElementClicked.classList.add("tc-selected-element");
        //right click 
    }

    sendMessage(data: OperatorModel) {
        if (!chrome.runtime) {
            return;
        }
        if (this.actionLog.length > 0) {
            const prev = this.actionLog[this.actionLog.length - 1];
            if (prev.path === data.path && data.action === 'sendKeys' && prev.action === 'sendKeys') {
                this.sendUpdateMessage(data);
                return;
            }
        }
        this.actionLog.push(data);
        this.sendMessageToPopup({ type: 'insert', data: data });

    }
    sendUpdateMessage(data: OperatorModel) {
        this.sendMessageToPopup({ type: 'appendLastValue', data: data });

    }
    sendMessageToPopup(message) {
        if (chrome.runtime) {
            chrome.runtime.sendMessage(message, function (response: any) {
                console.log(response);
            });
        }
    }
    checkAndMonitorSelectElement(element: any) {
        if (element.nodeName !== "SELECT") {
            return;
        }

        element.addEventListener('change', this.onChangeSelect);
    }
    onChangeSelect = (event) => {
        const xpath = this.xpathHelper.getElementXPath(event.target, document);
        this.sendMessage({ value: event.target.value, action: 'selectByValue', path: xpath });

    }
}
// //document.addEventListener('DOMContentLoaded', function () {
new Main();
// //}, false);


