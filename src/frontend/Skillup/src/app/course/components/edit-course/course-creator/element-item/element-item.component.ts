import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import { CardModule } from 'primeng/card';
import { Element, Section, AssetType } from '../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { NgClass } from '@angular/common';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FloatLabelModule } from 'primeng/floatlabel';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { CourseContentService } from '../../../../services/course-content-service';
import { ConfirmationDialogHandlerService } from '../../../../../core/services/confirmation-handler.service';
import { MenuModule } from 'primeng/menu';
import { MenuItem } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { AssetService } from '../../../../services/asset.service';
import { ElementContentDialogComponent } from "./element-content-dialog/element-content-dialog.component";

@Component({
  selector: 'app-element-item',
  standalone: true,
  imports: [CardModule, ButtonModule, FormsModule, InputTextModule, NgClass, InputTextareaModule, FloatLabelModule, DragDropModule, MenuModule, DialogModule, ElementContentDialogComponent],
  templateUrl: './element-item.component.html',
  styleUrl: './element-item.component.css'
})
export class ElementItemComponent implements OnInit {
  section = input.required<Section>();
  element = input.required<Element>();
  editing = false;
  elementTitle = signal('');
  elementDescription = signal('');
  onEditChange = output<boolean>();
  AssetType = AssetType;

  //Services
  courseContentService = inject(CourseContentService);
  confirmDialogService = inject(ConfirmationDialogHandlerService);
  assetService = inject(AssetService);

  ngOnInit(): void {
    this.elementTitle.set(this.element().title);
    this.elementDescription.set(this.element().description);
  }

  definedIcon(type: AssetType) : string{
    switch (type){
      case AssetType.Article:
        return 'pi pi-book';
        case AssetType.Video:
        return 'pi pi-video';
        case AssetType.Exercise:
        return 'pi pi-objects-column';
    }
  }

  changeEditVisibility(){
    if(this.editing){
      this.editing=false;
      this.onEditChange.emit(false);
    }
    else {
      this.editing=true;
      this.onEditChange.emit(true);
    }
  }

  saveElement(){
    this.courseContentService.updateElement(this.section().id, this.element().id, this.elementTitle(), this.elementDescription()).subscribe();
    this.changeEditVisibility();
  }

  removeElement(event: Event){
    this.confirmDialogService.confirmDelete(event, () => {
      this.courseContentService.deleteElement(this.section().id, this.element().id).subscribe();
    })
  }


  //MiniMenu
  contentDialogVisible = false;

  changecontentDialogVisibility(){
    if(this.contentDialogVisible){
      this.contentDialogVisible=false;
    }
    else {
      this.contentDialogVisible=true;
    }
  }

items: MenuItem[] = [
    {
        label: 'Options',
        items: [
            {
                label: 'Content',
                icon: 'pi pi-link',
                command: () => {
                  console.log(this.element());
                    this.changecontentDialogVisibility();
                    if(this.element().hasAsset === true){
                      this.getFileLink();
                      console.log(this.fileLink());
                    }
                }
            },
            {
                label: 'Attachment',
                icon: 'pi pi-paperclip'
            },
            {
                label: 'Edit',
                icon: 'pi pi-file-edit',
                command: () => {
                    this.changeEditVisibility();
                }
            },
            {
                label: 'Delete',
                icon: 'pi pi-trash',
                command: () => {
                    //this.removeElement();
                }
            }
        ]
    }
]; 

fileLink = signal('');
getFileLink(){
  this.assetService.getAsset(this.element().id, this.element().type).subscribe((response) => {
    this.fileLink.set(response.url);
});

}
}
