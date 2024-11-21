import { Component, inject, input, OnChanges, OnInit, signal, SimpleChanges } from '@angular/core';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AssetType, Element } from '../../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { AssetService } from '../../../../../services/asset.service';
import { ToastHandlerService } from '../../../../../../core/services/toast-handler.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-element-content-dialog',
  standalone: true,
  imports: [PdfViewerModule, ButtonModule, FileUploadModule ],
  templateUrl: './element-content-dialog.component.html',
  styleUrl: './element-content-dialog.component.css'
})
export class ElementContentDialogComponent implements OnChanges {
  //Variables
 visible = input.required<boolean>();
  element = input.required<Element>();
  fileLink = signal('');
  AssetType = AssetType;
  loadingUpload = false;

  //Services
  assetService = inject(AssetService);
  toastService = inject(ToastHandlerService);

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['visible'] && changes['visible'].currentValue){

      if(this.element().hasAsset){
        this.assetService.getAsset(this.element().id, this.element().type).subscribe((response) => {
          this.fileLink.set(response.url);
          });
      }

    }
  }

  //Accepted File
  getAccept(){
    if(this.element().type === AssetType.Article){
      return '.pdf'
    }
    else{
      return '.mp4'
    }
  }
  //Files
  selectedFile: File | undefined;
  onSelectImage(event: FileSelectEvent) {
    this.selectedFile = event.currentFiles[0];
  }

  upload() {
    this.loadingUpload = true;
    switch (this.element().type){
      case AssetType.Article:
        this.assetService.addArticle(this.element().id, this.selectedFile!).subscribe({
          next: () => {
            this.element().hasAsset = true;
            this.assetService.getAsset(this.element().id, this.element().type)
            .pipe(finalize (() => this.loadingUpload = false))
            .subscribe((response) => {
              this.fileLink.set(response.url);
              });
          },
          error: () => {
            this.toastService.showError("Something went wrong adding element");
          }
        });
        break;
      case AssetType.Video:
        this.assetService.addVideo(this.element().id, this.selectedFile!)
        .pipe(finalize (() => this.loadingUpload = false))
        .subscribe({
          next: () => {
            this.element().hasAsset = true;
            this.assetService.getAsset(this.element().id, this.element().type).subscribe((response) => {
              this.fileLink.set(response.url);
              });
          },
          error: () => {
            this.toastService.showError("Something went wrong adding element");
          }
        });
        break;
    }
  }

  cancel() {
    this.selectedFile = undefined;
  }

  
}
