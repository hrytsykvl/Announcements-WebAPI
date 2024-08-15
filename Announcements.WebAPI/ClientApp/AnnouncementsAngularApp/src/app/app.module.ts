import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AnnouncementsComponent } from './announcements/announcements.component';
import { HttpClientModule } from '@angular/common/http';
import { AnnouncementDetailsComponent } from './announcement-details/announcement-details.component';

@NgModule({
  declarations: [
    AppComponent,
    AnnouncementsComponent,
    AnnouncementDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
