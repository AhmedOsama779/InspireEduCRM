import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Schools, School } from '../../services/schools';
import { Contacts, Contact } from '../../services/contacts';
import { Leads, Lead } from '../../services/leads';
import { Visits, Visit } from '../../services/visits';
import { Books, Book } from '../../services/books';
import { FollowUps, FollowUp } from '../../services/follow-ups'; 
import { LeadStepper } from '../../shared/lead-stepper/lead-stepper';


@Component({
  selector: 'app-school-detail',
  imports: [CommonModule, RouterLink, FormsModule , LeadStepper],
  templateUrl: './school-detail.html',
  styleUrl: './school-detail.css'
})
export class SchoolDetail implements OnInit {
  school: School | null = null;
  contacts: Contact[] = [];
  lead: Lead | null = null;
  visits: Visit[] = [];
  books: Book[] = [];
  schoolId!: number;
  followUps: FollowUp[] = [];
  availableStages = ['Lead', 'Qualified', 'Interested', 'FollowUp', 'Won', 'Lost'];

  // Add Contact form
  newContactName = '';
  newContactPosition = '';
  newContactMobile = '';
  newContactEmail = '';
  showAddContactForm = false;

  // Add Visit form
  newVisitContactId: number | null = null;
  newVisitDate = '';
  newVisitNotes = '';
  newVisitInterestLevel = '';
  newVisitBookIds: number[] = [];
  showAddVisitForm = false;

  // Add FollowUp form
newFollowUpContactId: number | null = null;
newFollowUpDate = '';
newFollowUpType = '';
newFollowUpSummary = '';
newFollowUpNextAction = '';
showAddFollowUpForm = false;

  constructor(
    private route: ActivatedRoute,
    private schoolsService: Schools,
    private contactsService: Contacts,
    private leadsService: Leads,
    private visitsService: Visits,
    private booksService: Books,
    private followUpsService: FollowUps
  ) {}

  ngOnInit(): void {
    this.schoolId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadSchool();
    this.loadContacts();
    this.loadLead();
    this.loadVisits();
    this.loadBooks();
  }

  loadSchool(): void {
    this.schoolsService.getById(this.schoolId).subscribe({
      next: (data) => this.school = data,
      error: (err) => console.error(err)
    });
  }

  loadContacts(): void {
    this.contactsService.getBySchoolId(this.schoolId).subscribe({
      next: (data) => this.contacts = data,
      error: (err) => console.error(err)
    });
  }

  loadLead(): void {
  this.leadsService.getBySchoolId(this.schoolId).subscribe({
    next: (data) => {
      this.lead = data;
      this.loadFollowUps();
    },
    error: (err) => this.lead = null
  });
}

  loadVisits(): void {
    this.visitsService.getBySchoolId(this.schoolId).subscribe({
      next: (data) => this.visits = data,
      error: (err) => console.error(err)
    });
  }

  loadBooks(): void {
    this.booksService.getAll().subscribe({
      next: (data) => this.books = data,
      error: (err) => console.error(err)
    });
  }

  onAddContact(): void {
    this.contactsService.create({
      schoolId: this.schoolId,
      name: this.newContactName,
      position: this.newContactPosition,
      mobile: this.newContactMobile,
      email: this.newContactEmail
    }).subscribe({
      next: () => {
        this.newContactName = '';
        this.newContactPosition = '';
        this.newContactMobile = '';
        this.newContactEmail = '';
        this.showAddContactForm = false;
        this.loadContacts();
      },
      error: (err) => console.error(err)
    });
  }

  toggleBookSelection(bookId: number): void {
    const index = this.newVisitBookIds.indexOf(bookId);
    if (index === -1) {
      this.newVisitBookIds.push(bookId);
    } else {
      this.newVisitBookIds.splice(index, 1);
    }
  }

  isBookSelected(bookId: number): boolean {
    return this.newVisitBookIds.includes(bookId);
  }

  onAddVisit(): void {
    if (!this.newVisitContactId) {
      alert('Please select a contact.');
      return;
    }

    this.visitsService.create({
      schoolId: this.schoolId,
      contactId: this.newVisitContactId,
      visitDate: this.newVisitDate,
      notes: this.newVisitNotes,
      interestLevel: this.newVisitInterestLevel,
      bookIds: this.newVisitBookIds
    }).subscribe({
      next: () => {
        this.newVisitContactId = null;
        this.newVisitDate = '';
        this.newVisitNotes = '';
        this.newVisitInterestLevel = '';
        this.newVisitBookIds = [];
        this.showAddVisitForm = false;
        this.loadVisits();
        this.loadLead(); // a new visit might auto-create a Lead, refresh it
      },
      error: (err) => console.error(err)
    });
  }
  loadFollowUps(): void {
  if (this.lead) {
    this.followUpsService.getByLeadId(this.lead.id).subscribe({
      next: (data) => this.followUps = data,
      error: (err) => console.error(err)
    });
  }
}

onStageChange(newStage: string): void {
  if (!this.lead) return;

  this.leadsService.updateStage(this.lead.id, newStage).subscribe({
    next: (updated) => this.lead = updated,
    error: (err) => console.error(err)
  });
}

onAddFollowUp(): void {
  if (!this.lead) return;

  this.followUpsService.create({
    leadId: this.lead.id,
    contactId: this.newFollowUpContactId,
    followUpDate: this.newFollowUpDate,
    followUpType: this.newFollowUpType,
    summary: this.newFollowUpSummary,
    nextAction: this.newFollowUpNextAction
  }).subscribe({
    next: () => {
      this.newFollowUpContactId = null;
      this.newFollowUpDate = '';
      this.newFollowUpType = '';
      this.newFollowUpSummary = '';
      this.newFollowUpNextAction = '';
      this.showAddFollowUpForm = false;
      this.loadFollowUps();
      this.loadLead(); // stage might have auto-bumped
    },
    error: (err) => console.error(err)
  });
}
}