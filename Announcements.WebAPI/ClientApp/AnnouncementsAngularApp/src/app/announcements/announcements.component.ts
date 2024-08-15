import { Component } from '@angular/core';
import { Announcement } from '../models/announcement';
import { AnnouncementsService } from '../services/announcements.service';

@Component({
  selector: 'app-announcements',
  templateUrl: './announcements.component.html',
  styleUrls: ['./announcements.component.css']
})
export class AnnouncementsComponent {
  announcements: Announcement[] = [];

  constructor(private announcementsService: AnnouncementsService) {
  }

  ngOnInit() {
    this.announcementsService.getAnnouncements()
      .subscribe({
        next: (response: Announcement[]) => {
          this.announcements = response;
        },
        error: (error: any) => {
          console.log(error);
        },
        complete: () => { }
      });
  }
}
