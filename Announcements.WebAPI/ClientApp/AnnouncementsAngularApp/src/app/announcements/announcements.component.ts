import { Component } from '@angular/core';
import { Announcement } from '../models/announcement';
import { AnnouncementsService } from '../services/announcements.service';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-announcements',
  templateUrl: './announcements.component.html',
  styleUrls: ['./announcements.component.css']
})
export class AnnouncementsComponent {
  announcements: Announcement[] = [];
  postAnnouncementForm: FormGroup;
  isPostAnnouncementFormSubmitted: boolean = false;

  putAnnouncementForm: FormGroup;
  editAnnouncementID: number | null = null;

  constructor(private announcementsService: AnnouncementsService, private router: Router) {
    this.postAnnouncementForm = new FormGroup({
      title: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      dateAdded: new FormControl(new Date())
    });

    this.putAnnouncementForm = new FormGroup({
      announcements: new FormArray([])
    });
  }

  get putAnnouncementFormArray(): FormArray {
    return this.putAnnouncementForm.get('announcements') as FormArray;
  }

  loadAnnouncements() {
    this.announcementsService.getAnnouncements()
      .subscribe({
        next: (response: Announcement[]) => {
          this.announcements = response;

          this.announcements.forEach(announcement => {
            this.putAnnouncementFormArray.push(new FormGroup({
              id: new FormControl(announcement.id, [Validators.required]),
              title: new FormControl({ value: announcement.title, disabled: true }, [Validators.required]),
              description: new FormControl({ value: announcement.description, disabled: true }, [Validators.required]),
              dateAdded: new FormControl(announcement.dateAdded, [Validators.required]),
            }));
          });
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

        this.putAnnouncementFormArray.push(new FormGroup({
          id: new FormControl(response.id, [Validators.required]),
          title: new FormControl({ value: response.title, disabled: true }, [Validators.required]),
          description: new FormControl({ value: response.description, disabled: true }, [Validators.required]),
          dateAdded: new FormControl(new Date()),
        }));
        this.announcements.push(new Announcement(response.id, response.title, response.description, response.dateAdded));

        this.isPostAnnouncementFormSubmitted = false;
      },

      error: (error: any) => {
        console.log(error);
      },

      complete: () => { }
    });
  }

  editClicked(announcement: Announcement): void {
    this.editAnnouncementID = announcement.id;
  }

  updateClicked(i: number): void {

    this.announcementsService.putAnnouncement(this.putAnnouncementFormArray.controls[i].value).subscribe({
      next: (response: string) => {
        console.log(response);

        this.editAnnouncementID = null;
        this.putAnnouncementFormArray.controls[i].reset(this.putAnnouncementFormArray.controls[i].value);
      },

      error: (error: any) => {
        console.log(error);
      },

      complete: () => { }
    });
  }

  deleteClicked(announcement: Announcement, i: number): void {
    if (confirm('Are you sure you want to delete this announcement?')) {
      this.announcementsService.deleteAnnouncement(announcement.id).subscribe({
        next: (response: string) => {
          console.log(response);

          this.putAnnouncementFormArray.removeAt(i);
          this.announcements.splice(i, 1);
        },

        error: (error: any) => {
          console.log(error);
        },

        complete: () => { }
      });
    }
  }

  navigateToAnnouncement(id: number): void {
    this.router.navigate(['/announcement', id]);
  }
}
