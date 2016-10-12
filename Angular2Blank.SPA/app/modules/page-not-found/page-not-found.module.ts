import { NgModule } from '@angular/core';

//modules
import { SharedModule } from '../shared/shared.module';

//components
import { PageNotFoundComponent } from './page-not-found.component';

@NgModule({
    imports: [],
    declarations: [PageNotFoundComponent],
    entryComponents: [
        PageNotFoundComponent
    ],
    bootstrap: [],
    exports: [PageNotFoundComponent]
})
export class PageNotFoundModule { }