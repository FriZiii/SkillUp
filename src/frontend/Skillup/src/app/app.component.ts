import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { definePreset } from 'primeng/themes';
import { Aura } from 'primeng/themes/aura';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})


export class AppComponent {
  title = 'Skillup';

  MyPreset = definePreset(Aura, {
    semantic: {
      primary: {
        50: '{yellow.0}',
        100: '{yellow.50}',
        200: '{yellow.100}',
        300: '{yellow.200}',
        400: '{yellow.300}',
        500: '{yellow.400}',
        600: '{yellow.500}',
        700: '{yellow.600}',
        800: '{yellow.700}',
        900: '{yellow.800}',
        950: '{yellow.900}',
      },
    },
  });

  constructor(private config: PrimeNGConfig) {
    this.config.theme.set({
      preset: this.MyPreset,
      options: {
        cssLayer: {
          name: 'primeng',
          order: 'tailwind-base, tailwind-utilities, primeng',
        },
      },
    });
  }
}
