import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-common-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  template: `
    <p>CommonLayout works!</p>
    <router-outlet />
  `,
  styleUrl: './CommonLayout.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommonLayoutComponent {}
