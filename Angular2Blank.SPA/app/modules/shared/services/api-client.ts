import {Injectable} from '@angular/core';
import {Http, RequestOptionsArgs, Headers} from '@angular/http';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class ApiClient {
    http: Http;

    constructor(http: Http) {
        this.http = http;
    }

    public post(url, params, headers?) {
        return Observable.create(obs => {
            this.http.post(url, params, headers)
                .subscribe((result) => {
                        obs.next(result);
                        obs.complete();
                    },
                    error => this.handleError(obs, error));
        });
    }

    public get(url, headers?) {
        return Observable.create(obs => {
            this.http.get(url, { headers: headers })
                .subscribe((result) => {
                        obs.next(result);
                        obs.complete();
                    },
                    error => this.handleError(obs, error));
        });
    }

    private handleError(observer, error) {
        if (error.status === 401) {
            //redirect to login
        } else {
            observer.error(error);
        }
    }
}