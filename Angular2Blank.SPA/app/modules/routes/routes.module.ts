import { RouterModule }   from '@angular/router';
import { NgModule }      from '@angular/core';

import { HomeComponent } from '../home/home.component';
import { PageNotFoundComponent } from '../page-not-found/page-not-found.component';

@NgModule({
    imports: [
        RouterModule.forRoot([
            { path: '', component: HomeComponent },
            { path: '**', component: PageNotFoundComponent }
        ])
    ],
    exports: [RouterModule]
})
export class RoutesModule { }