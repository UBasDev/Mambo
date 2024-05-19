import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-unauthorized1',
  standalone: true,
  imports: [CommonModule],
  template: `<p>Everyone is welcome here!</p>`,
  styleUrl: './Unauthorized1.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Unauthorized1Component {}
