import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { TabsModule } from 'primeng/tabs';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputGroupModule } from 'primeng/inputgroup';
import { ButtonModule } from 'primeng/button';
import { SafeUrl } from '@angular/platform-browser';
import { ImageModule } from 'primeng/image';
import { ImageCroperComponent } from '../../../utils/components/image-croper/image-croper.component';
import {
  FileSelectEvent,
  FileSendEvent,
  FileUploadModule,
  UploadEvent,
} from 'primeng/fileupload';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [
    FileUploadModule,
    ImageCroperComponent,
    ImageModule,
    TabsModule,
    FormsModule,
    InputGroupModule,
    InputGroupAddonModule,
    InputTextModule,
    ButtonModule,
  ],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css',
})
export class EditProfileComponent {
  oldImage: SafeUrl =
    'https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png';

  previewNewImage: SafeUrl | null = null;
  showCroper = false;

  selectedFile: File | undefined;

  onSelectImage(event: FileSelectEvent) {
    this.selectedFile = event.currentFiles[0];
    this.showCroper = true;
  }

  onImageCropped(newCroppedImage: SafeUrl) {
    this.previewNewImage = newCroppedImage;
    this.showCroper = false;
  }
}
