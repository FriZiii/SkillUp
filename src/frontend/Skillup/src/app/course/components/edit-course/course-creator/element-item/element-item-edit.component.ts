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
import { CourseContentService } from '../../../../services/course-content.service';
import { ConfirmationDialogHandlerService } from '../../../../../core/services/confirmation-handler.service';
import { MenuModule } from 'primeng/menu';
import { MenuItem, MenuItemCommandEvent } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { AssetService } from '../../../../services/asset.service';
import { ElementContentDialogComponent } from "./element-content-dialog/element-content-dialog.component";
import { Tooltip } from 'primeng/tooltip';
import { SelectButton } from 'primeng/selectbutton';
import { ElementAttachmentsDialogComponent } from "./element-attachments-dialog/element-attachments-dialog.component";
import { CourseReviewService } from '../../../../services/course-review.service';
import { CourseListItem } from '../../../../models/course.model';
import { CourseStatus } from '../../../../models/course-status.model';

@Component({
  selector: 'app-element-item-edit',
  standalone: true,
  imports: [CardModule, ButtonModule, FormsModule, InputTextModule, NgClass, InputTextareaModule, FloatLabelModule, DragDropModule, MenuModule, DialogModule, ElementContentDialogComponent, SelectButton, ElementAttachmentsDialogComponent],
  templateUrl: './element-item-edit.component.html',
  styleUrl: './element-item-edit.component.css'
})
export class ElementItemEditComponent implements OnInit {
  //Variable
  section = input.required<Section>();
  element = input.required<Element>();
  course = input.required<CourseListItem>();
  elementTitle = signal('');
  elementDescription = signal('');
  elementFree = signal<boolean>(false);
  isDraggable = output<boolean>();
  AssetType = AssetType;
  contentIcon = signal('');
    //MiniMenu
    items: MenuItem[] = [];
    CourseStatus = CourseStatus;

  //Services
  courseContentService = inject(CourseContentService);
  confirmDialogService = inject(ConfirmationDialogHandlerService);
  assetService = inject(AssetService);
  reviewService = inject(CourseReviewService);

  //Comments
  latestReview = computed(() => this.reviewService.latestReviewForCourse());
  allReviews = computed(() => this.reviewService.allReviewsForCourse()?.filter(r => r.id != this.latestReview()?.id));
  latestComment = computed(() => this.latestReview()?.comments.find(comment => comment.courseElementId === this.element().id) || null)
  comments = computed(() => this.allReviews()?.flatMap(review => review.comments).filter(comment => comment.courseElementId === this.element().id) || null)

  ngOnInit(): void {
    this.elementTitle.set(this.element().title);
    this.elementDescription.set(this.element().description);
    this.elementFree.set(this.element().isFree);

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

  attachmentsDialogVisible = false;
  changeAttachmentsDialogVisibility(){
    if(this.attachmentsDialogVisible){
      this.attachmentsDialogVisible=false;
      this.isDraggable.emit(false);
    }
    else {
      this.attachmentsDialogVisible=true;
      this.isDraggable.emit(true);
    }
  }

  commentDialogVisible = false;


  //Delete element's content
  deleteContent(event: Event){
    this.confirmDialogService.confirmDelete(event, () => {
      this.assetService.deleteAsset(this.element().id).subscribe({
        next: () => {
          this.element().hasAsset = false;
        }
      });
    })
  }

  openComments(){
    this.commentDialogVisible = true;
  }

  resolveComment(commentId: string){
    this.reviewService.resolveComment(commentId).subscribe(
      (res) => this.reviewService.latestReviewForCourse.set(res)
    );
  }
}