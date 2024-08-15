import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnnouncementsService } from '../services/announcements.service';

@Component({
  selector: 'app-announcement-details',
  templateUrl: './announcement-details.component.html',
  styleUrls: ['./announcement-details.component.css']
})
export class AnnouncementDetailsComponent implements OnInit {
  announcement: any;

  constructor(private route: ActivatedRoute, private announcementsService: AnnouncementsService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id'));
      this.announcementsService.getAnnouncementById(id).subscribe((data) => {
        this.announcement = data;
      });
    });
  }

}
