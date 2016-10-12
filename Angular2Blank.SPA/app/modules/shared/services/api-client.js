"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var http_1 = require('@angular/http');
var Observable_1 = require('rxjs/Observable');
var ApiClient = (function () {
    function ApiClient(http) {
        this.http = http;
    }
    ApiClient.prototype.post = function (url, params, headers) {
        var _this = this;
        return Observable_1.Observable.create(function (obs) {
            _this.http.post(url, params, headers)
                .subscribe(function (result) {
                obs.next(result);
                obs.complete();
            }, function (error) { return _this.handleError(obs, error); });
        });
    };
    ApiClient.prototype.get = function (url, headers) {
        var _this = this;
        return Observable_1.Observable.create(function (obs) {
            _this.http.get(url, { headers: headers })
                .subscribe(function (result) {
                obs.next(result);
                obs.complete();
            }, function (error) { return _this.handleError(obs, error); });
        });
    };
    ApiClient.prototype.handleError = function (observer, error) {
        if (error.status === 401) {
        }
        else {
            observer.error(error);
        }
    };
    ApiClient = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], ApiClient);
    return ApiClient;
}());
exports.ApiClient = ApiClient;
//# sourceMappingURL=api-client.js.map