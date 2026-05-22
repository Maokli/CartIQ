import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-stat-card',
  standalone: false,
  templateUrl: './stat-card.html',
  styleUrl: './stat-card.css',
})
export class StatCard {
  @Input() label = '';
  @Input() value = '';
  @Input() sub = '';
  @Input() subAccent: 'green' | 'amber' | null = null;
}
