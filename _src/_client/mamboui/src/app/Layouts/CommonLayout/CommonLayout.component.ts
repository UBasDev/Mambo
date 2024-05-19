import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-common-layout',
  standalone: true,
  imports: [CommonModule, ButtonModule, RouterOutlet, RouterModule],
  template: `
    <p>CommonLayout works!</p>
    <p-button label="Go to homepage" routerLink="/"></p-button>
    <p-button label="Go to Authorize page" routerLink="/authorized1"></p-button>
    <p-button
      label="Go to Unauthorize page"
      routerLink="/unauthorized1"
    ></p-button>
    <router-outlet />
  `,
  styleUrl: './CommonLayout.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommonLayoutComponent {}
