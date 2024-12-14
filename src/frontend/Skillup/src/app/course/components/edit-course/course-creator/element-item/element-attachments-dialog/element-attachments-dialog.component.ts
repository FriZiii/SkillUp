import { Component, computed, inject, input } from '@angular/core';
import { Element } from '../../../../../models/course-content.model';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { ButtonModule } from 'primeng/button';
import { CourseContentService } from '../../../../../services/course-content.service';

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

  //Services
  courseContentService = inject(CourseContentService);

  upload() {
    this.courseContentService.addAttachment(this.element().id, this.selectedFile!).subscribe((res) => {
      this.element().attachments.push(res);
      this.selectedFile = undefined;
    });
  }

  cancel() {
    this.selectedFile = undefined;
  }

  downloadAttachment(attachmentId: string){
    this.courseContentService.getAttachment(attachmentId).subscribe((blob) => {
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(blob);
      a.href = objectUrl;
      a.download = attachmentId;
      a.click();
      URL.revokeObjectURL(objectUrl);
    });
  }

  /* .subscribe((response) => {
    const blob = response.body!;
    const contentDisposition = response.headers.get('content-disposition');
    const filename = this.extractFileName(contentDisposition);

    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;

    // Jeśli nazwa pliku jest w nagłówku, użyj jej, jeśli nie, użyj domyślnej nazwy
    a.setAttribute('download', filename || 'attachment');
    a.click();

    window.URL.revokeObjectURL(url); // Zwolnij zasoby
  }); */

  private extractFileName(contentDisposition: string | null): string | undefined {
    if (!contentDisposition) return undefined;
    const matches = contentDisposition.match(/filename="?([^"]+)"?/);
    return matches?.[1];
  }

  deleteAttachment(attachmentId: string){
    this.courseContentService.deleteAttachment(attachmentId).subscribe((res) => {
      this.element().attachments = this.element().attachments.filter(a => a != attachmentId);
    });
  }
}
