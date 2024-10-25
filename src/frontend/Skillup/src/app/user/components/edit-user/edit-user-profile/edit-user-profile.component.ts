import { Component, DestroyRef, effect, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { UserService } from '../../../services/user.service';
import { User, UserDetail } from '../../../models/user.model';

@Component({
  selector: 'app-edit-user-profile',
  standalone: true,
  imports: [
    FormsModule,
    InputGroupModule,
    InputGroupAddonModule,
    InputTextModule,
    ButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './edit-user-profile.component.html',
  styleUrl: './edit-user-profile.component.css',
})
export class EditUserProfileComponent implements OnInit {
  //Services
  userService = inject(UserService)

  //Variables
  userDetail = signal<UserDetail | null>(null);
  destroyRef = inject(DestroyRef);

  //Form
  form: FormGroup = new FormGroup({
    name: new FormControl('', {validators: [Validators.required],}),
    surname: new FormControl( '', {validators: [Validators.required],}),
    title: new FormControl('', {validators: [Validators.required],}),
    biography: new FormControl('', {validators: [Validators.required],}),
    website: new FormControl( ''),
    twitter: new FormControl(''),
    facebook: new FormControl(''),
    youtube: new FormControl(''),
    linkedin: new FormControl(''),
  });

  ngOnInit(){
    this.userService.userDeatil.subscribe({
      next: (data) => {
        this.userDetail.set(data);
        this.form.patchValue({
          name: data?.firstName,
          surname: data?.lastName,
          title: data?.details.title,
          biography: data?.details.biography,
          website: data?.socialMediaLinks.website,
          twitter: data?.socialMediaLinks.twitter,
          facebook: data?.socialMediaLinks.facebook,
          youtube: data?.socialMediaLinks.youtube,
          linkedin: data?.socialMediaLinks.linkedin,
        });
      },
    });
    
  }

  onClick(){
    console.log(this.userDetail());
  }

  onSubmit(){
    console.log(this.form.value.name);
    
    console.log(this.form.value.surname);
    console.log(this.userDetail()?.id);

    const subscription = this.userService.editUser(this.userDetail()!.id, {
      firstName: this.form.value.name,
      lastName: this.form.value.surname,
      email: this.userDetail()!.email,
      title: this.form.value.title,
      biography: this.form.value.biography,
      socialMediaLinks: {
        twitter: this.form.value.twitter,
        facebook: this.form.value.facebook,
        website: this.form.value.website,
        linkedin: this.form.value.linkedin,
        youtube: this.form.value.youtube,
        }
      })
    .subscribe({
      next: (res) => {
      }
      })
        
    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe;
    });
  }
}
