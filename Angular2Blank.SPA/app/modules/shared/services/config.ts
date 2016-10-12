import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class Config {
    private _config: Object;
    private _env: Object;

    constructor(private http: Http) {
    }

    load() {
        this.http.get('app/config/config.json')
            .subscribe((env_data: any) => {
                this._env = JSON.parse(env_data._body);
            });
    }
    getEnv(key: any) {
        return this._env[key];
    }
    get(key: any) {
        return this._config[key];
    }
};