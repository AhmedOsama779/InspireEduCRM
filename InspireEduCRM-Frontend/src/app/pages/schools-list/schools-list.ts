import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Schools, School } from '../../services/schools';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-schools-list',
  imports: [CommonModule , CommonModule, RouterLink],
  templateUrl: './schools-list.html',
  styleUrl: './schools-list.css'
})
export class SchoolsList implements OnInit {
  schools: School[] = [];

  constructor(private schoolsService: Schools) {}

  ngOnInit(): void {
    this.schoolsService.getAll().subscribe({
      next: (data) => this.schools = data,
      error: (err) => console.error(err)
    });
  }
}