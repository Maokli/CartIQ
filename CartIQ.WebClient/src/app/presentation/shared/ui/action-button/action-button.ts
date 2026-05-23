import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-action-button',
  templateUrl: './action-button.html',
  styleUrl: './action-button.css',
  standalone: false
})
export class ActionButton {
  @Input() label = 'Add';
  @Input() icon = 'add';
  @Input() color: 'primary' | 'accent' | 'warn' = 'primary';
  @Input() disabled = false;

  @Output() action = new EventEmitter<void>();

  onClick() {
    this.action.emit();
  }
}
