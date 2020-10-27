

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { StudentComponent } from './student/student.component';
import { CustomFilterModule } from "./custom-filter/custom-filter.module";
import { PagerService } from './services/pager-service.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    StudentComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CustomFilterModule,
    RouterModule.forRoot([
      { path: '', component: StudentComponent, pathMatch: 'full' },
      { path: 'student', component: StudentComponent },
    ])
  ],
  providers: [PagerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
