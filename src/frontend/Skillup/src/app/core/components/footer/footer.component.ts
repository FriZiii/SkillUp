import { Component } from '@angular/core';
import { LogoComponent } from '../../../utils/components/logo/logo.component';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [LogoComponent, LogoComponent, ButtonModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css',
})
export class FooterComponent {}
