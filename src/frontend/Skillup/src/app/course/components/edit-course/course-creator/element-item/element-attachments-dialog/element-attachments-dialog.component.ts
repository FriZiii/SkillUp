import { Component, input } from '@angular/core';
import { Element } from '../../../../../models/course-content.model';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-element-attachments-dialog',
  standalone: true,
  imports: [FileUploadModule, ButtonModule],
  templateUrl: './element-attachments-dialog.component.html',
  styleUrl: './element-attachments-dialog.component.css'
})
export class ElementAttachmentsDialogComponent {
  element = input.required<Element>();

//Files
  selectedFile: File | undefined;
  onSelectImage(event: FileSelectEvent) {
    this.selectedFile = event.currentFiles[0];
  }

  upload() {
    //send this selectedFile to endpoint
  }

  cancel() {
    this.selectedFile = undefined;
  }
}
