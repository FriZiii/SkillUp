import { Component, inject, input } from '@angular/core';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AssetType, Element } from '../../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { AssetService } from '../../../../../services/asset.service';

@Component({
  selector: 'app-element-content-dialog',
  standalone: true,
  imports: [PdfViewerModule, ButtonModule, FileUploadModule ],
  templateUrl: './element-content-dialog.component.html',
  styleUrl: './element-content-dialog.component.css'
})
export class ElementContentDialogComponent {
  element = input.required<Element>();
  fileLink = input<string>();

  AssetType = AssetType;
  //Services
  assetService = inject(AssetService);


  //Files
selectedFile: File | undefined;
onSelectImage(event: FileSelectEvent) {
  this.selectedFile = event.currentFiles[0];
}

upload() {
  switch (this.element().type){
    case AssetType.Article:
      this.assetService.addArticle(this.element().id, this.selectedFile!).subscribe();
      break;
    case AssetType.Video:
      this.assetService.addVideo(this.element().id, this.selectedFile!).subscribe();
      break;
  }
}

cancel() {
  this.selectedFile = undefined;
}
}
