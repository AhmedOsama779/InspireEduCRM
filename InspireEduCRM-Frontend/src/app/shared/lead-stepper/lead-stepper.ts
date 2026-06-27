import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lead-stepper',
  imports: [CommonModule],
  templateUrl: './lead-stepper.html',
  styleUrl: './lead-stepper.css'
})
export class LeadStepper {
  @Input() currentStage = 'Lead';
  @Output() stageChange = new EventEmitter<string>();

  stages = ['Lead', 'Qualified', 'Interested', 'FollowUp', 'Won'];

  get isLost(): boolean {
    return this.currentStage === 'Lost';
  }

  get currentIndex(): number {
    return this.stages.indexOf(this.currentStage);
  }

  isCompleted(index: number): boolean {
    return !this.isLost && index < this.currentIndex;
  }

  isActive(index: number): boolean {
    return !this.isLost && index === this.currentIndex;
  }

  selectStage(stage: string): void {
    if (stage !== this.currentStage) {
      this.stageChange.emit(stage);
    }
  }

  markLost(): void {
    this.stageChange.emit('Lost');
  }
}