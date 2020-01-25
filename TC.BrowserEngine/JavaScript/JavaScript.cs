using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.JavaScript
{
    class JavaScript
    {
        public static string XhrMonitor()
        {
            return @"var test=function(){if(void 0===t){var t=()=>{var t=XMLHttpRequest.prototype.open;XMLHttpRequest.prototype.open=function(){let s=e();console.log(""this"",this),this.onreadystatechange=(()=>{if(3===this.readyState)if(null===window.localStorage.getItem(""xhrCalls""))window.localStorage.setItem(""xhrCalls"",JSON.stringify([{key:s,value:{responseURl:this.responseURL,readyState:this.readyState,httpStatus:this.status,httpStatusText:this.statusText}}]));else{let t=JSON.parse(window.localStorage.getItem(""xhrCalls""));t.push({key:s,value:{responseURl:this.responseURL,readyState:this.readyState,httpStatus:this.status,httpStatusText:this.statusText}}),window.localStorage.setItem(""xhrCalls"",JSON.stringify(t))}if(4===this.readyState){let t=JSON.parse(window.localStorage.getItem(""xhrCalls"")),e=t.find(t=>t.key===s);e?e.value={responseURl:this.responseURL,readyState:this.readyState,httpStatus:this.status,httpStatusText:this.statusText}:t.push({key:s,value:{responseURl:this.responseURL,readyState:this.readyState,httpStatus:this.status,httpStatusText:this.statusText}}),window.localStorage.setItem(""xhrCalls"",JSON.stringify(t))}console.log(""request completed!"")}),t.apply(this,arguments)}},e=()=>""xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx"".replace(/[xy]/g,function(t){var e=16*Math.random()|0;return(""x""==t?e:3&e|8).toString(16)});t()}};test();";
        }

        public static string getXhrCalls()
        {
            return @"getXhrCalls=()=>{let xhrCalls=JSON.parse(window.localStorage.getItem('xhrCalls'));let allCallsDone=!0;if(xhrCalls){for(let xhrCall of xhrCalls){if(xhrCall.value.readyState!==4){allCallsDone=!1;break}}}return true}; return getXhrCalls()";
        }

    }
}
