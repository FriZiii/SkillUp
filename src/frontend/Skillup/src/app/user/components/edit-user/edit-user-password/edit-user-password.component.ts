import { Component, DestroyRef, inject } from '@angular/core';
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
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { PasswordService } from '../../../../auth/services/password.service';
import { finalize } from 'rxjs';
import { ToastHandlerService } from '../../../../core/services/toast-handler.service';

@Component({
  selector: 'app-edit-user-password',
  standalone: true,
  imports: [
    FormsModule,
    InputGroupModule,
    InputGroupAddonModule,
    PasswordModule,
    InputTextModule,
    ButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './edit-user-password.component.html',
  styleUrl: './edit-user-password.component.css',
})
export class EditUserPasswordComponent {
  private passwordService = inject(PasswordService);

  loading = false;
  errorMessage = '';

  destroyRef = inject(DestroyRef);

  form: FormGroup = new FormGroup(
    {
      currentPassword: new FormControl('', Validators.required),
      newPassword: new FormControl('',[
        Validators.required,
        Validators.minLength(6),
        Validators.pattern('^(?=.*[A-Z]).*$'),
      ]),
      newPasswordConfirm: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
        Validators.pattern('^(?=.*[A-Z]).*$'),
      ]),
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


  onSubmit() {
    this.loading = true;
    const subscription = this.passwordService
      .changePassword(
        this.form.value.currentPassword,
        this.form.value.newPassword
      )
      .pipe(finalize(() => (this.loading = false)))
      .subscribe();

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe;
    });
  }
  
  get passwordIsInvalid(){
    return this.form.controls['newPassword'].dirty && this.form.controls['newPassword'].invalid
  }

  get passwordsAreEqual(){
    if(this.form.value.newPassword === this.form.value.newPasswordConfirm){
      return true;
    }
    return false;
  }
}
