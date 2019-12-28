import { ErrorHandler, Inject, NgZone, isDevMode} from "@angular/core";
import { ToastrService } from "ngx-toastr";
import * as Sentry from '@sentry/browser';

Sentry.init({ dsn: 'https://75df67ece7aa4504b9eda4fa5498942e@sentry.io/1766540' });
export class AppErrorHandler implements ErrorHandler{
    constructor(
        private ngZone:NgZone,
        @Inject(ToastrService) private toastr: ToastrService ){

    }
    handleError(error: any): void {
        if (!isDevMode())
            Sentry.captureException(error.originalError || error);
        else
            throw error;
        this.ngZone.run(()=>{
            this.toastr.error("bbbbbbbbbbb","Error");
        })
        console.log("Error");
       }
}
