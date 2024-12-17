import { CommonModule } from '@angular/common';
import { Component, input } from '@angular/core';

@Component({
  selector: 'app-circle-progress',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './circle-progress.component.html',
  styleUrl: './circle-progress.component.css'
})
export class CircleProgressComponent {
  percent = input.required<number>();
  color = '#4AA764';

  calculateDashOffset(): string {
    const radius = 35; // Promień koła
    const circumference = 2 * Math.PI * radius; // Obwód koła
    const offset = circumference - (this.percent() / 100) * circumference;
    return offset.toString();
  }
}
