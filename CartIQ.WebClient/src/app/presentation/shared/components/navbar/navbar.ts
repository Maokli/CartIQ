import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  links = [
    { label: 'Receipts',  path: '/receipts'  },
    { label: 'Analytics', path: '/analytics' },
    { label: 'Nutrients', path: '/nutrients' },
  ];
}
