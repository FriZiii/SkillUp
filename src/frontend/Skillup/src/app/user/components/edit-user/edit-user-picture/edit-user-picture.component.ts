import { Component, inject, OnInit, signal } from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { ImageCroperComponent } from '../../../../utils/components/image-croper/image-croper.component';
import { UserService } from '../../../services/user.service';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-edit-user-picture',
  standalone: true,
  imports: [FileUploadModule, ImageCroperComponent],
  templateUrl: './edit-user-picture.component.html',
  styleUrl: './edit-user-picture.component.css',
})
export class EditUserPictureComponent implements OnInit {
  oldImage: SafeUrl =
    'https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png';

  previewNewImage: SafeUrl | null = null;
  showCroper = false;

  selectedFile: File | undefined;
  userService = inject(UserService);
  user = signal<User | null>(null);

  ngOnInit(): void {
    this.userService.user.subscribe((user) => {
      this.user.set(user);
    });
  }

  onSelectImage(event: FileSelectEvent) {
    this.selectedFile = event.currentFiles[0];
    this.showCroper = true;
  }

  onImageCropped(newCroppedImage: SafeUrl) {
    this.previewNewImage = newCroppedImage;
    this.showCroper = false;
  }
}