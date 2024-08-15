import { Component } from '@angular/core';
import { Announcement } from '../models/announcement';
import { AnnouncementsService } from '../services/announcements.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-announcements',
  templateUrl: './announcements.component.html',
  styleUrls: ['./announcements.component.css']
})
export class AnnouncementsComponent {
  announcements: Announcement[] = [];
  postAnnouncementForm: FormGroup;
  isPostAnnouncementFormSubmitted: boolean = false;

  constructor(private announcementsService: AnnouncementsService) {
    this.postAnnouncementForm = new FormGroup({
      title: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      dateAdded: new FormControl(new Date())
    });
  }

  loadAnnouncements() {
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

  ngOnInit() {
    this.loadAnnouncements();
  }

  get postAnnouncement_TitleControl(): any {
    return this.postAnnouncementForm.controls['title'];
  }

  get postAnnouncement_DescriptionControl(): any {
    return this.postAnnouncementForm.controls['description'];
  }

  public postAnnouncementSubmitted() {
    this.isPostAnnouncementFormSubmitted = true;

    this.announcementsService.postAnnouncement(this.postAnnouncementForm.value).subscribe({
      next: (response: Announcement) => {
        console.log(response);

        this.loadAnnouncements();

        this.postAnnouncementForm.reset();
      },

      error: (error: any) => {
        console.log(error);
      },

      complete: () => { }
    });
  }
}
