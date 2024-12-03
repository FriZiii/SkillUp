import { Component, computed, input } from '@angular/core';
import { AssetType, Element } from '../../../models/course-content.model';
import { DialogModule } from 'primeng/dialog';
import { NgClass } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ElementContentDialogComponent } from "../../edit-course/course-creator/element-item/element-content-dialog/element-content-dialog.component";

@Component({
  selector: 'app-view-element-item',
  standalone: true,
  imports: [DialogModule, NgClass, ButtonModule, ElementContentDialogComponent],
  templateUrl: './view-element-item.component.html',
  styleUrl: './view-element-item.component.css'
})
export class ViewElementItemComponent {
  element = input.required<Element>();
  AssetType = AssetType;
  contentDialogVisible = false;


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

  showContentDialog(){
    if(this.element().hasAsset){
      this.contentDialogVisible = true;
    }
  }
}
