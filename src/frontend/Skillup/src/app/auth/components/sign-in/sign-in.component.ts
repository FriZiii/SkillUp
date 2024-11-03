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
import { finalize } from 'rxjs';

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
  loading = false;

  signInForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.pattern('^(?=.*[A-Z]).*$')]),
    remember: new FormControl(false)
  });

  submitSignIn() {
    const formValue = this.signInForm.value;
    this.loading = true;
    this.authService.signIn(formValue.email!, formValue.password!)
    .pipe(finalize (() => this.loading = false))
    .subscribe();
  }

  get emailIsInvalid(){
    return this.signInForm.controls.email.dirty && this.signInForm.controls.email.invalid
  }
  get passwordIsInvalid(){
    return this.signInForm.controls.password.dirty && this.signInForm.controls.password.invalid
  }
  get formIsInvalid(){
    return this.signInForm.pristine || this.signInForm.controls.email.dirty && this.signInForm.controls.email.invalid || this.signInForm.controls.password.dirty && this.signInForm.controls.password.invalid
  }
}
