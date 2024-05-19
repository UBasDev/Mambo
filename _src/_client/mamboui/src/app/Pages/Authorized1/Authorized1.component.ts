import { CommonModule } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-authorized1',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <p>Authorized1 works!</p>
    <button (click)="this.getRoles()">GET ROLES</button>
  `,
  styleUrl: './Authorized1.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Authorized1Component {
  constructor(private readonly _httpClient: HttpClient) {}
  getRoles(): void {
    this._httpClient
      .get(
        'https://localhost:5999/api/v1/roles/get-all-roles-without-relation',
        {
          withCredentials: true,
        }
      )
      .subscribe({
        next: (response: any) => {
          console.log(response);
        },
        error: (error: HttpErrorResponse) => {
          console.log(error.error);
        },
      });
  }
}
