import {
  Component,
  DestroyRef,
  inject,
  input,
  OnInit,
  signal,
} from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { PasswordService } from '../../services/password.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [
    ButtonModule,
    InputTextModule,
    FormsModule,
    PasswordModule,
    ReactiveFormsModule,
  ],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
})
export class ResetPasswordComponent implements OnInit {
  ngOnInit(): void {
    console.log(this.token());
  }
  passwordService = inject(PasswordService);
  token = input<string | null>(null);
  email = signal('');

  loading = false;
  errorMessage = '';
  destroyRef = inject(DestroyRef);

  sendIntruction() {
    this.loading = true;

    const subscription = this.passwordService
      .sendPasswordResetInstruction(this.email())
      .pipe(finalize(() => (this.loading = false)))
      .subscribe();

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe;
    });
  }

  form: FormGroup = new FormGroup(
    {
      newPassword: new FormControl('', Validators.required),
      newPasswordConfirm: new FormControl('', Validators.required),
    },
    { validators: this.passwordsMatchValidator() }
  );

  passwordsMatchValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      const group = control as FormGroup;
      const newPassword = group.get('newPassword')?.value;
      const confirmPassword = group.get('newPasswordConfirm')?.value;

      this.errorMessage = 'New password and confirm must be equal';
      return newPassword === confirmPassword ? null : { mismatch: true };
    };
  }

  resetPassword() {
    this.loading = true;

    const subscription = this.passwordService
      .resetPassword(this.token()!, this.form.value.newPassword)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe();

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe;
    });
  }
}
