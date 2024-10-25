import { Component, effect, inject, OnInit, signal } from '@angular/core';
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
  user = signal<User | null>(null);
  userDetail = signal<UserDetail | null>(null);

  //Form
  form: FormGroup = new FormGroup({
    name: new FormControl('', {validators: [Validators.required],}),
    surname: new FormControl( '', {validators: [Validators.required],}),
    title: new FormControl('', {validators: [Validators.required],}),
    biography: new FormControl('', {validators: [Validators.required],}),
    website: new FormControl( '', {validators: [Validators.required],}),
    twitter: new FormControl('', {validators: [Validators.required],}),
    facebook: new FormControl('', {validators: [Validators.required],}),
    youtube: new FormControl('', {validators: [Validators.required],}),
    linkedin: new FormControl('', {validators: [Validators.required],}),
  });

  ngOnInit(){
    this.userService.userDeatil.subscribe({
      next: (data) => {
        this.userDetail.set(data);
        this.form.patchValue({
          name: data?.firstName,
          surname: data?.lastName,
          title: data?.title,
          biography: data?.biography,
          website: data?.website,
          twitter: data?.twitter,
          facebook: data?.facebook,
          youtube: data?.youtube,
          linkedin: data?.linkedin,
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
  }
}
