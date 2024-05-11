import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-authorized1',
  standalone: true,
  imports: [CommonModule, ButtonModule, RouterModule],
  template: `
    <p>Authorized1 works!</p>
    <p-button label="Go to homepage" routerLink="/"></p-button>
  `,
  styleUrl: './Authorized1.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Authorized1Component {}
