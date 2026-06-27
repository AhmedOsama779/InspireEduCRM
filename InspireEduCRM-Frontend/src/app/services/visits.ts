import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Visit {
  id: number;
  schoolId: number;
  contactId: number;
  salesRepId: number;
  visitDate: string;
  notes: string;
  interestLevel: string;
  bookIds: number[];
}

@Injectable({
  providedIn: 'root'
})
export class Visits {
  private apiUrl = 'https://localhost:7198/api/Visits';

  constructor(private http: HttpClient) {}

  getBySchoolId(schoolId: number): Observable<Visit[]> {
    return this.http.get<Visit[]>(`${this.apiUrl}/by-school/${schoolId}`);
  }

  create(visit: {
    schoolId: number;
    contactId: number;
    visitDate: string;
    notes: string;
    interestLevel: string;
    bookIds: number[];
  }): Observable<Visit> {
    return this.http.post<Visit>(this.apiUrl, visit);
  }
}