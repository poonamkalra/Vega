import { ErrorHandler, Inject, NgZone } from "@angular/core";
import { ToastyService } from "ng2-toasty";

//import { ErrorHandler } from "@angular/router/src/router";

export class AppErrorHandler implements ErrorHandler
{
    constructor(private ngZone : NgZone, @Inject(ToastyService) private toastyService: ToastyService)
    {
        
    }

    handleError(error: any): void
    {
        //console.log("ERROR");
        this.ngZone.run(() => {
            this.toastyService.error(
                {
                    title: 'Error',
                    msg: 'An error occured',
                    theme: 'bootstrap',
                    showClose: true,
                    timeout: 5000
                  }
                )
        });

       
    }

}