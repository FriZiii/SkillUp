import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { SelectButtonModule } from 'primeng/selectbutton';
import { UserService } from '../../../services/user.service';
import { UserDetail } from '../../../models/user.model';
import { ToastHandlerService } from '../../../../core/services/toast-handler.service';
import { ButtonModule } from 'primeng/button';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-edit-user-privacy-settings',
  standalone: true,
  imports: [SelectButtonModule, ReactiveFormsModule, ButtonModule],
  templateUrl: './edit-user-privacy-settings.component.html',
  styleUrl: './edit-user-privacy-settings.component.css',
})
export class EditUserPrivacySettingsComponent implements OnInit {
  //Services
  userService = inject(UserService);
  toastService = inject(ToastHandlerService);

  //Variables
  userDetail = signal<UserDetail | null>(null);
  destroyRef = inject(DestroyRef);
  loading = false;

  //Button Options
  stateOptions: any[] = [
    { label: 'Off', value: false },
    { label: 'On', value: true },
  ];

  //Form
  form: FormGroup = new FormGroup({
    publicProfile: new FormControl('off'),
    publicCourses: new FormControl('off'),
  });

  ngOnInit() {
    this.userService.userDetail.subscribe({
      next: (data) => {
        this.userDetail.set(data);
        this.form.patchValue({
          publicProfile: data?.privacySettings.isAccountPublicForLoggedInUsers,
          publicCourses: data?.privacySettings.showCoursesOnUserProfile,
        });
      },
    });
  }

  onSubmit() {
    this.loading = true;
    const subscription = this.userService
      .editUserPrivacySettings(
        this.userDetail()!.id,
        this.form.value.publicProfile,
        this.form.value.publicCourses
      )
      .pipe(finalize (() => this.loading = false))
      .subscribe({
        next: (res) => {
          this.toastService.showSuccess('Profile editted successfully');
        },
      });

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe;
    });
  }
}
