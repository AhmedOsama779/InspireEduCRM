import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface FollowUp {
  id: number;
  leadId: number;
  customerServiceRepId: number;
  contactId: number | null;
  followUpDate: string;
  followUpType: string;
  summary: string;
  nextAction: string;
}

@Injectable({
  providedIn: 'root'
})
export class FollowUps {
  private apiUrl = 'https://localhost:7198/api/FollowUps';

  constructor(private http: HttpClient) {}

  getByLeadId(leadId: number): Observable<FollowUp[]> {
    return this.http.get<FollowUp[]>(`${this.apiUrl}/by-lead/${leadId}`);
  }

  create(followUp: {
    leadId: number;
    contactId: number | null;
    followUpDate: string;
    followUpType: string;
    summary: string;
    nextAction: string;
  }): Observable<FollowUp> {
    return this.http.post<FollowUp>(this.apiUrl, followUp);
  }
}