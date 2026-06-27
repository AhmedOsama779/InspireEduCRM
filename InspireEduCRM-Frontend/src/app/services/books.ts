import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Book {
  id: number;
  title: string;
  subject: string;
  grade: string;
}

@Injectable({
  providedIn: 'root'
})
export class Books {
  private apiUrl = 'https://localhost:7198/api/Books';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }
}