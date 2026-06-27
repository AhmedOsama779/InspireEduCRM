import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface School {
  id: number;
  name: string;
  type: string;
  city: string;
  address: string;
  principalName: string;
  principalMobile: string;
}

@Injectable({
  providedIn: 'root'
})
export class Schools {
  private apiUrl = 'https://localhost:7198/api/Schools';

  constructor(private http: HttpClient) {}

  getAll(): Observable<School[]> {
    return this.http.get<School[]>(this.apiUrl);
  }

  create(school: Omit<School, 'id'>): Observable<School> {
    return this.http.post<School>(this.apiUrl, school);
  }

  getById(id: number): Observable<School> {
  return this.http.get<School>(`${this.apiUrl}/${id}`);
}
}