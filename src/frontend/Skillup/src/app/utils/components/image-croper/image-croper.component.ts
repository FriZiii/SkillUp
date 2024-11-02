import {
  ImageCroppedEvent,
  ImageCropperComponent,
  LoadedImage,
  base64ToFile,
} from 'ngx-image-cropper';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ImageCroperResponse } from '../../models/image-croper-response.model';

@Component({
  selector: 'app-image-croper',
  standalone: true,
  imports: [
    ProgressSpinnerModule,
    DialogModule,
    ImageCropperComponent,
    ButtonModule,
  ],
  templateUrl: './image-croper.component.html',
  styleUrl: './image-croper.component.css',
})
export class ImageCroperComponent {
  @Input() imageFile: File | null = null;
  @Input() imageChangedEvent: Event | null = null;
  @Input() croperVisibility: boolean = false;

  croperLoading = true;

  @Output() imageCroppedEvent = new EventEmitter<ImageCroperResponse>();
  croppedImageUrl: SafeUrl = '';
  croppedImageFile: File | null = null;

  constructor(private sanitizer: DomSanitizer) {}

  onImageCropped(event: ImageCroppedEvent) {
    this.croppedImageFile = new File([event.blob!], 'fileName', {
      type: event.blob!.type,
    });
    this.croppedImageUrl = this.sanitizer.bypassSecurityTrustUrl(
      event.objectUrl!
    );
  }

  omImageLoaded(image: LoadedImage) {
    this.croperLoading = false;
  }

  cropImage() {
    const response: ImageCroperResponse = {
      file: this.croppedImageFile!,
      url: this.croppedImageUrl,
    };
    this.imageCroppedEvent.emit(response);
  }

  onLoadImageFailed() {
    // handle image load failure
  }
}
