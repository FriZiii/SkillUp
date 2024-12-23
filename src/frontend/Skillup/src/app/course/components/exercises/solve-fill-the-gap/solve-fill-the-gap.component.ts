import { Component, input } from '@angular/core';
import { Sentence } from '../../../models/fill-the-gap/fill-the-gap.models';
import { FillTheGapComponent } from "../../fill-the-gap/fill-the-gap.component";

@Component({
  selector: 'app-solve-fill-the-gap',
  standalone: true,
  imports: [FillTheGapComponent],
  templateUrl: './solve-fill-the-gap.component.html',
  styleUrl: './solve-fill-the-gap.component.css'
})
export class SolveFillTheGapComponent {
  sentences = input.required<Sentence[]>();
  instruction = input<string>('Complete exercises');
}
