import { Injectable } from '@angular/core';
import { Announcement } from '../models/announcement';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementsService {
  announcements: Announcement[] = [];

  constructor(private httpClient: HttpClient) {
    this.announcements = [

    ];
  }

  public getCities(): Observable<Announcement[]> {
    return this.httpClient.get<Announcement[]>("https://localhost:7193/api/announcements");
  }
}
