import { NgModule } from '@angular/core';

//services 
import { ApiClient } from './services/api-client';
import { Config } from './services/config';

@NgModule({
    imports: [],
    declarations: [],
    bootstrap: [],
    providers: [ApiClient, Config],
    exports: []
})
export class SharedModule { }