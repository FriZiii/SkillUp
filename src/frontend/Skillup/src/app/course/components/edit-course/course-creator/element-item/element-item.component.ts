import { Component, computed, inject, input, OnInit, output, signal } from '@angular/core';
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
import { MenuItem, MenuItemCommandEvent } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { AssetService } from '../../../../services/asset.service';
import { ElementContentDialogComponent } from "./element-content-dialog/element-content-dialog.component";
import { Tooltip } from 'primeng/tooltip';
import { SelectButton } from 'primeng/selectbutton';

@Component({
  selector: 'app-element-item',
  standalone: true,
  imports: [CardModule, ButtonModule, FormsModule, InputTextModule, NgClass, InputTextareaModule, FloatLabelModule, DragDropModule, MenuModule, DialogModule, ElementContentDialogComponent, SelectButton],
  templateUrl: './element-item.component.html',
  styleUrl: './element-item.component.css'
})
export class ElementItemComponent implements OnInit {
  //Variable
  section = input.required<Section>();
  element = input.required<Element>();
  elementTitle = signal('');
  elementDescription = signal('');
  elementFree = signal<boolean>(false);
  isDraggable = output<boolean>();
  AssetType = AssetType;
  contentIcon = signal('');
    //MiniMenu
    items: MenuItem[] = [];

  //Services
  courseContentService = inject(CourseContentService);
  confirmDialogService = inject(ConfirmationDialogHandlerService);
  assetService = inject(AssetService);

  ngOnInit(): void {
    this.elementTitle.set(this.element().title);
    this.elementDescription.set(this.element().description);
    this.elementFree.set(this.element().isFree);

    if(this.element().hasAsset){
      this.contentIcon.set('pi pi-link');
      console.log(this.contentIcon())
    }
    else {
      this.contentIcon.set('pi pi-exclamation-triangle');
      console.log(this.contentIcon())
    }

    this.items = [
      {
          label: 'Options',
          items: [
              {
                  label: 'Content',
                  icon: this.contentIcon(),
                  command: () => {
                    console.log(this.element());
                      this.changecontentDialogVisibility();
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
                  separator: true
              },
              {
                  label: 'Delete',
                  icon: 'pi pi-trash',
                  command: (event: MenuItemCommandEvent) => {
                      this.removeElement(event.originalEvent!);
                  }
              }
          ]
      }
  ]; 
  }

  //Element Icon
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

  //Edit
  editing = false;
  changeEditVisibility(){
    if(this.editing){
      this.editing=false;
      this.isDraggable.emit(false);
    }
    else {
      this.editing=true;
      this.isDraggable.emit(true);
    }
  }

  freeOptions: any[] = [{ label: 'Yes', value: true },{ label: 'No', value: false }];

  //Element actions
  saveElement(){
    this.courseContentService.updateElement(this.section().id, this.element().id, this.elementTitle(), this.elementDescription(), this.elementFree()).subscribe();
    this.changeEditVisibility();
  }
  removeElement(event: Event){
    this.confirmDialogService.confirmDelete(event, () => {
      this.courseContentService.deleteElement(this.section().id, this.element().id).subscribe();
    })
  }


  //Dialog
  contentDialogVisible = false;
  changecontentDialogVisibility(){
    if(this.contentDialogVisible){
      this.contentDialogVisible=false;
      this.isDraggable.emit(false);
    }
    else {
      this.contentDialogVisible=true;
      this.isDraggable.emit(true);
    }
  }

  //MiniMenu
  


}
