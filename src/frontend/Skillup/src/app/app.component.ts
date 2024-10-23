import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { definePreset } from 'primeng/themes';
import { Aura } from 'primeng/themes/aura';
import { SignInComponent } from './auth/components/sign-in/sign-in.component';
import { SignUpComponent } from './auth/components/sign-up/sign-up.component';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './core/components/header/header.component';
import { FooterComponent } from './core/components/footer/footer.component';
import { AddCourseComponent } from './course/components/add-course/add-course.component';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    SignInComponent,
    SignUpComponent,
    RouterOutlet,
    HeaderComponent,
    AddCourseComponent,
    ToastModule,
    FooterComponent
  ],
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
    components: {
      popover: {
        gutter: '10',
        arrow: {
          offset: '-99999999', //TODO : Change this
        },
      }
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
