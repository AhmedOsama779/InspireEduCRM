import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Schools } from '../../services/schools';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-add-school',
  imports: [FormsModule , CommonModule, RouterLink],
  templateUrl: './add-school.html',
  styleUrl: './add-school.css'
})
export class AddSchool {
  name = '';
  type = '';
  city = '';
  address = '';
  principalName = '';
  principalMobile = '';
  errorMessage = '';

  constructor(private schoolsService: Schools, private router: Router) {}

  onSubmit(): void {
    this.errorMessage = '';

    this.schoolsService.create({
      name: this.name,
      type: this.type,
      city: this.city,
      address: this.address,
      principalName: this.principalName,
      principalMobile: this.principalMobile
    }).subscribe({
      next: () => {
        this.router.navigate(['/schools']);
      },
      error: (err) => {
        this.errorMessage = 'Failed to create school. Please check your input.';
        console.error(err);
      }
    });
  }
}