import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnnouncementsComponent } from './announcements/announcements.component';
import { AnnouncementDetailsComponent } from './announcement-details/announcement-details.component';

const routes: Routes = [
  { path: "announcements", component: AnnouncementsComponent },
  { path: 'announcement/:id', component: AnnouncementDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
