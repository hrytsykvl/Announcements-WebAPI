import { Injectable } from '@angular/core';
import { Announcement } from '../models/announcement';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementsService {
  private apiUrl = 'https://localhost:7193/api/announcements';

  constructor(private httpClient: HttpClient) { }

  public getAnnouncements(): Observable<Announcement[]> {
    return this.httpClient.get<Announcement[]>(`${this.apiUrl}`);
  }

  public getAnnouncementById(id: number): Observable<any> {
    return this.httpClient.get(`${this.apiUrl}/${id}`);
  }
}
