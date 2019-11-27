class RequestionMonitor {
    constructor() {

    }
    startMonitor(main: Main) {

        var proxied = (<any>window).XMLHttpRequest.prototype.send;
        (<any>window).XMLHttpRequest.prototype.send = function () {
            console.log(arguments);
            //Here is where you can add any code to process the request.
            //If you want to pass the Ajax request object, pass the 'pointer' below
            var pointer = this
            var intervalId = window.setInterval(function () {
                if (pointer.readyState != 4) {
                    return;
                }
                console.log(pointer.responseText);
                //Here is where you can add any code to process the response.
                //If you want to pass the Ajax request object, pass the 'pointer' below
                clearInterval(intervalId);

            }, 1);//I found a delay of 1 to be sufficient, modify it as you need.
            return proxied.apply(this, [].slice.call(arguments));
        };
    }
}