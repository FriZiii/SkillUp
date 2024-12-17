import { Component, computed, inject, input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Attachment, Element } from '../../../../../models/course-content.model';
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
export class ElementAttachmentsDialogComponent implements OnChanges {
  element = input.required<Element>();
  visible = input.required<boolean>();

  attachments: Attachment[] = []
//Files
  selectedFile: File | undefined;
  onSelectImage(event: FileSelectEvent) {
    this.selectedFile = event.currentFiles[0];
  }
  //Services
  courseContentService = inject(CourseContentService);

  ngOnChanges(changes: SimpleChanges): void {
      if(changes['visible'] && changes['visible'].currentValue){
        this.courseContentService.getAttachmentsByElementId(this.element().id).subscribe((res) => {
          this.attachments = res;
        })
      }
  }

  upload() {
    this.courseContentService.addAttachment(this.element().id, this.selectedFile!).subscribe((res) => {
      this.attachments.push(res);
      this.selectedFile = undefined;
    });
  }

  cancel() {
    this.selectedFile = undefined;
  }

  downloadAttachment(attachment: Attachment){
    this.courseContentService.getAttachment(attachment.id).subscribe((blob) => {
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(blob);
      a.href = objectUrl;
      a.download = attachment.name;
      a.click();
      URL.revokeObjectURL(objectUrl);
    });
  }

  deleteAttachment(attachmentId: string){
    this.courseContentService.deleteAttachment(attachmentId).subscribe((res) => {
      this.attachments = this.attachments.filter(a => a.id != attachmentId);
    });
  }
}
