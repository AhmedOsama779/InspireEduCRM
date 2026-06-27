import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Lead {
  id: number;
  schoolId: number;
  schoolName: string;
  stage: string;
  createdAt: string;
  updatedAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class Leads {
  private apiUrl = 'https://localhost:7198/api/Leads';

  constructor(private http: HttpClient) {}

  getBySchoolId(schoolId: number): Observable<Lead> {
    return this.http.get<Lead>(`${this.apiUrl}/by-school/${schoolId}`);
  }

  updateStage(id: number, stage: string): Observable<Lead> {
    return this.http.put<Lead>(`${this.apiUrl}/${id}/stage`, { stage });
  }
}