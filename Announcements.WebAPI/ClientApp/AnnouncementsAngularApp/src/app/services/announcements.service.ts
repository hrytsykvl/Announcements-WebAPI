import { Injectable } from '@angular/core';
import { Announcement } from '../models/announcement';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_BASE_URL = 'https://localhost:7193/api/';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementsService {
  constructor(private httpClient: HttpClient) { }

  public getAnnouncements(): Observable<Announcement[]> {
    return this.httpClient.get<Announcement[]>(`${API_BASE_URL}announcements`);
  }

  public getAnnouncementById(id: number): Observable<any> {
    return this.httpClient.get(`${API_BASE_URL}announcements/${id}`);
  }

  public postAnnouncement(announcement: Announcement): Observable<Announcement> {
    return this.httpClient.post<Announcement>(`${API_BASE_URL}announcements`, announcement);
  }

  public putAnnouncement(announcement: Announcement): Observable<string> {
    return this.httpClient.put<string>(`${API_BASE_URL}announcements/${announcement.id}`, announcement);
  }
}
