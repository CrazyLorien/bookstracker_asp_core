import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent }   from './app.component';
import { HttpModule } from '@angular/http';

//modules
import { HomeModule } from './modules/home/home.module';
import { SharedModule } from './modules/shared/shared.module';
import { PageNotFoundModule } from './modules/page-not-found/page-not-found.module';
import { RoutesModule } from './modules/routes/routes.module';

//services
import { Config } from './modules/shared/services/config';

@NgModule({
    imports: [BrowserModule, HttpModule, HomeModule, PageNotFoundModule, SharedModule, RoutesModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule {
    constructor(private config: Config) {
        this.config.load();
    }
}