import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AnnouncementsComponent } from './announcements/announcements.component';
import { HttpClientModule } from '@angular/common/http';
import { AnnouncementDetailsComponent } from './announcement-details/announcement-details.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DisableControlDirective } from './directives/disable-control.directive';

@NgModule({
  declarations: [
    AppComponent,
    AnnouncementsComponent,
    AnnouncementDetailsComponent,
    DisableControlDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
