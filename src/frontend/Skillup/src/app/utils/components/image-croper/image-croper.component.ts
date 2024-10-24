import {
  ImageCroppedEvent,
  ImageCropperComponent,
  LoadedImage,
} from 'ngx-image-cropper';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

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
  @Output() imageCroppedEvent = new EventEmitter<SafeUrl>();
  croperLoading = true;
  croppedImage: SafeUrl = '';

  constructor(private sanitizer: DomSanitizer) {}

  onImageCropped(event: ImageCroppedEvent) {
    this.croppedImage = this.sanitizer.bypassSecurityTrustUrl(event.objectUrl!);
  }

  omImageLoaded(image: LoadedImage) {
    this.croperLoading = false;
  }

  cropImage() {
    this.croperLoading = true;
    this.imageCroppedEvent.emit(this.croppedImage);
  }

  onLoadImageFailed() {
    // handle image load failure
  }
}
