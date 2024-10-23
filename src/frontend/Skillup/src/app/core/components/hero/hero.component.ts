import { Component } from '@angular/core';
import { FilterHeaderComponent } from "../filter-header/filter-header.component";

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [FilterHeaderComponent],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css'
})
export class HeroComponent {

}
