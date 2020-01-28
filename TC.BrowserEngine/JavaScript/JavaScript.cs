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

        public static string CheckIfAllXhrCallsDone()
        {
            return @"checkIfAllXhrsCallsDone=(()=>{let e=JSON.parse(window.localStorage.getItem(""xhrCalls"")),l=!0;if(e)for(let a of e)if(4!==a.value.readyState){l=!1;break}return l}),checkIfAllXhrsCallsDone();";
        }

        public static string CheckIfXhrCallDone(string xhrCall)
        {
            return @"checkIfXhrCallIsDone=(()=>{let e=JSON.parse(window.localStorage.getItem(""xhrCalls""));if(e){for(let l of e)if(""" + xhrCall + @"""===l.value.responseURL)return 4===l.value.readyState;return!1}}),checkIfXhrCallIsDone();";
        }

    }
}
