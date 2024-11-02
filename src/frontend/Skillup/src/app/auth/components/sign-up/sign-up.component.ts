import { Component, inject } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabelModule } from 'primeng/floatlabel';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';
import { CheckboxModule } from 'primeng/checkbox';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    InputTextModule,
    FloatLabelModule,
    PasswordModule,
    ButtonModule,
    CheckboxModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  authService = inject(AuthService);

  signUpForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.pattern('^(?=.*[A-Z]).*$')]),
    passwordConfirm: new FormControl('', [Validators.required, Validators.minLength(6), Validators.pattern('^(?=.*[A-Z]).*$')]),
    marketing: new FormControl(true)
  });

  subbmitSignUp() {
    const formValue = this.signUpForm.value;
    this.authService.signUp(formValue.email!, formValue.password!, formValue.marketing!).subscribe();
  }

  
  get emailIsInvalid(){
    return this.signUpForm.controls.email.dirty && this.signUpForm.controls.email.invalid
  }
  get passwordIsInvalid(){
    return this.signUpForm.controls.password.dirty && this.signUpForm.controls.password.invalid
  }
  get formIsInvalid(){
    return this.signUpForm.pristine || this.signUpForm.controls.email.dirty && this.signUpForm.controls.email.invalid || this.signUpForm.controls.password.dirty && this.signUpForm.controls.password.invalid || !this.passwordsAreEqual;
  }
  
  get passwordsAreEqual(){
    if(this.signUpForm.value.password === this.signUpForm.value.passwordConfirm){
      return true;
    }
    return false;
  }
}

