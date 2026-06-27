import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Contact {
  id: number;
  schoolId: number;
  name: string;
  position: string;
  mobile: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class Contacts {
  private apiUrl = 'https://localhost:7198/api/Contacts';

  constructor(private http: HttpClient) {}

  getBySchoolId(schoolId: number): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.apiUrl}/by-school/${schoolId}`);
  }

  create(contact: Omit<Contact, 'id'>): Observable<Contact> {
    return this.http.post<Contact>(this.apiUrl, contact);
  }
}