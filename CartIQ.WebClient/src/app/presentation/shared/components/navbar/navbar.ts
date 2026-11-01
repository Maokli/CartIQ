import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HlmAvatarImports } from '@spartan-ng/helm/avatar';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, ...HlmAvatarImports],
  templateUrl: './navbar.html',
})
export class Navbar {
  links = [
    { label: 'Receipts', path: '/receipts' },
    { label: 'Analytics', path: '/analytics' },
    { label: 'Nutrients', path: '/nutrients' },
  ];
}
