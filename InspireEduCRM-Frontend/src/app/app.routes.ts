import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { SchoolsList } from './pages/schools-list/schools-list';
import { authGuard } from './guards/auth-guard'; // match your actual filename
import { AddSchool } from './pages/add-school/add-school';
import { SchoolDetail } from './pages/school-detail/school-detail';




export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'schools', component: SchoolsList, canActivate: [authGuard] },
  { path: 'schools/add', component: AddSchool, canActivate: [authGuard] },
  { path: 'schools/:id', component: SchoolDetail, canActivate: [authGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];