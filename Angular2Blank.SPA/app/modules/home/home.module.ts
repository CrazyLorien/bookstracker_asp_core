import { NgModule } from '@angular/core';

//modules
import { SharedModule } from '../shared/shared.module';

//components
import { HomeComponent } from './home.component';

@NgModule({
    imports: [],
    declarations: [HomeComponent],
    entryComponents: [
        HomeComponent
    ],
    bootstrap: [],
    exports: [HomeComponent]
})
export class HomeModule { }