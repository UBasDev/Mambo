import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { MenubarModule } from 'primeng/menubar';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-common-layout-nav',
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    RouterModule,
    MenubarModule,
    RippleModule,
    InputTextModule,
  ],
  template: `
    <p-menubar [model]="items">
      <ng-template pTemplate="start">
        <img
          (click)="this.goToPage('/')"
          class="hidden lg:block p-0 m-0 cursor-pointer transition ease-out hover:scale-110"
          height="75px"
          width="75px"
          src="/assets/logo/logo.png"
        />
      </ng-template>
      <ng-template pTemplate="item" let-item let-root="root">
        <a pRipple class="navButton flex items-center p-menuitem-link">
          <span class="ml-2 ">{{ item.label }}</span>
        </a>
      </ng-template>
      <ng-template pTemplate="end">
        <div class="flex items-center md:hidden">
          <img
            (click)="this.goToPage('/')"
            class="block p-0 m-0 cursor-pointer transition ease-out hover:scale-110"
            height="50px"
            width="50px"
            src="/assets/logo/logo.png"
          />
        </div>
        <div class="hidden md:flex items-center gap-x-2">
          <p-button
            size="small"
            severity="success"
            rounded="true"
            label="Get Free"
            routerLink="/getFree"
          ></p-button>
          <input
            type="text"
            pInputText
            placeholder="Search"
            class="w-8rem sm:w-auto"
          />
          <p-button
            size="small"
            label="Sign in"
            routerLink="/signIn"
            icon="pi pi-user"
            iconPos="right"
          ></p-button>
        </div>
      </ng-template>
    </p-menubar>
  `,
  styleUrl: './CommonLayoutNav.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommonLayoutNavComponent implements OnInit {
  private router = inject(Router);
  items: MenuItem[] | undefined;
  ngOnInit() {
    this.items = [
      {
        label: 'Features',
      },
      {
        label: 'Guide',
      },
      {
        label: 'Contact',
      },
      {
        label: 'Pricing',
      },
    ];
  }
  public goToPage(route: string) {
    this.router.navigate([`${route}`]);
  }
}
