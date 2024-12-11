import { Component, input, OnInit, output, signal } from '@angular/core';
import { AssetType, Element } from '../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { MenuItem } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { ElementContentDialogComponent } from "../edit-course/course-creator/element-item/element-content-dialog/element-content-dialog.component";
import { ElementAttachmentsDialogComponent } from "../edit-course/course-creator/element-item/element-attachments-dialog/element-attachments-dialog.component";

@Component({
  selector: 'app-element-item-display',
  standalone: true,
  imports: [ButtonModule, MenuModule, DialogModule, ElementContentDialogComponent, ElementAttachmentsDialogComponent],
  templateUrl: './element-item-display.component.html',
  styleUrl: './element-item-display.component.css'
})
export class ElementItemDisplayComponent implements OnInit {
  element = input.required<Element>();
  moderator = input<boolean>(false);
  onComment = output<string>();

  items: MenuItem[] = [];
  contentIcon = signal('');



  ngOnInit(): void {
    if(this.element().hasAsset){
      this.contentIcon.set('pi pi-link');
    }
    else {
      this.contentIcon.set('pi pi-exclamation-triangle');
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
                  icon: 'pi pi-paperclip',
                  command: () => {
                      this.changeAttachmentsDialogVisibility();
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

    //Dialog
    contentDialogVisible = false;
    changecontentDialogVisibility(){
      if(this.contentDialogVisible){
        this.contentDialogVisible=false;
      }
      else {
        this.contentDialogVisible=true;
      }
    }
  
    attachmentsDialogVisible = false;
    changeAttachmentsDialogVisibility(){
      if(this.attachmentsDialogVisible){
        this.attachmentsDialogVisible=false;
      }
      else {
        this.attachmentsDialogVisible=true;
      }
    }

    addComment(){
      this.onComment.emit(this.element().id);
    }
}
