import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonLayoutNavComponent } from './CommonLayoutNav/CommonLayoutNav.component';
import { CommonLayoutFooterComponent } from './CommonLayoutFooter/CommonLayoutFooter.component';

@Component({
  selector: 'app-common-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    CommonLayoutNavComponent,
    CommonLayoutFooterComponent,
  ],
  template: `
    <app-common-layout-nav />
    <!-- <p-button label="Go to homepage" routerLink="/"></p-button>
    <p-button label="Go to Authorize page" routerLink="/authorized1"></p-button>
    <p-button
      label="Go to Unauthorize page"
      routerLink="/unauthorized1"
    ></p-button> -->

    <div class="md:px-10">
      <router-outlet />
      <app-common-layout-footer />
    </div>
  `,
  styleUrl: './CommonLayout.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommonLayoutComponent {}
