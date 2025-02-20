import { Component, inject, OnInit } from '@angular/core';
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
import { AuthService } from './auth/services/auth.service';
import { FilterHeaderComponent } from './core/components/filter-header/filter-header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    ToastModule,
    FooterComponent,
    FilterHeaderComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Skillup';

  MyPreset = definePreset(Aura, {
    semantic: {
      primary: {
        50: '{green.50}',
        100: '{green.100}',
        200: '{green.200}',
        300: '{fuchsia.200}',
        400: '{fuchsia.300}',
        500: '{green.600}',
        600: '{green.700}',
        700: '{green.800}',
        800: '{fuchsia.700}',
        900: '{fuchsia.800}',
        950: '{fuchsia.900}',
      },
      colorScheme: {
        surface: {
          0: '#ffffff',
          50: '{gray.50}',
          100: '{gray.100}',
          200: '{gray.200}',
          300: '{gray.300}',
          400: '{gray.400}',
          500: '{gray.500}',
          600: '{gray.600}',
          700: '{gray.700}',
          800: '{gray.800}',
          900: '{gray.900}',
          950: '{gray.950}',
        },
      },
    },
    components: {
      popover: {
        gutter: '10',
        arrow: {
          offset: '-99999999', //TODO : Change this
        },
      },
      menubar: {
        root: {
          borderRadius: '0',
        },
      },
    },
  });

  authService = inject(AuthService);

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

  ngOnInit(): void {
    this.authService.autoSignIn();
  }
}
