import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableContainer } from './ui/table-container/table-container';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ActionButton } from './ui/action-button/action-button';
import { Navbar } from './components/navbar/navbar';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { StatCard } from './ui/stat-card/stat-card';

const components = [TableContainer, ActionButton, Navbar, StatCard]
@NgModule({
  declarations: components,
  imports: [CommonModule, MatCardModule, MatDividerModule, MatIconModule, MatButtonModule, RouterLink, RouterLinkActive],
  exports: components
})
export class SharedModule {}
