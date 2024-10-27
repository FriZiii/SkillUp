import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { AuthService } from '../../services/auth.service';
import { RouterModule } from '@angular/router';
import { CheckboxModule } from 'primeng/checkbox';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [
    InputTextModule,
    FloatLabelModule,
    PasswordModule,
    ButtonModule,
    ReactiveFormsModule,
    RouterModule,
    CheckboxModule 
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css',
})
export class SignInComponent {
  authService = inject(AuthService);

  signInForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', [Validators.required]),
    remember: new FormControl(false)
  });

  subbmitSignIn() {
    const formValue = this.signInForm.value;
    this.authService.signIn(formValue.email!, formValue.password!).subscribe();
  }
}
