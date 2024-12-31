import { ChangeDetectorRef, Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { AccordionModule } from 'primeng/accordion';
import { Section } from '../../../models/course-content.model';
import { CourseContentService } from '../../../services/course-content.service';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { FormsModule } from '@angular/forms';
import { HiddenFormWrapperComponent } from '../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component';
import { AddNewElementComponent } from "./element-item/add-new-element/add-new-element.component";
import {CdkDragDrop, CdkDropList, CdkDrag, moveItemInArray, DragDropModule, CdkDragEnter, CdkDragExit} from '@angular/cdk/drag-drop';
import { SectionItemComponent } from "./section-item/section-item.component";
import { FloatLabelModule } from 'primeng/floatlabel';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationDialogHandlerService } from '../../../../core/services/confirmation-handler.service';
import { ElementListComponent } from "./element-list/element-list.component";
import { CourseReviewService } from '../../../services/course-review.service';
import { CoursesService } from '../../../services/course.service';
import { CourseStatus } from '../../../models/course-status.model';
import { CommentDialogForInstructorComponent } from "./comment-dialog-for-instructor/comment-dialog-for-instructor.component";

@Component({
  selector: 'app-course-creator',
  standalone: true,
  imports: [AccordionModule, ButtonModule, DialogModule, InputTextModule, ReactiveFormsModule, CardModule, FormsModule, HiddenFormWrapperComponent, AddNewElementComponent, DragDropModule, SectionItemComponent, FloatLabelModule, ConfirmDialogModule, ElementListComponent, CommentDialogForInstructorComponent],
  templateUrl: './course-creator.component.html',
  styleUrl: './course-creator.component.css'
})
export class CourseCreatorComponent implements OnInit {
  //FromUrl
  courseId = input.required<string>();

  //Services
  courseContentService = inject(CourseContentService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);
  reviewService = inject(CourseReviewService);
  courseService = inject(CoursesService);
  
  //Variables
  sections = computed(() => this.courseContentService.sections());
  changeDetectorRef = inject(ChangeDetectorRef);
  course = computed(() => this.courseService.courses().find(c => c.id === this.courseId()));
  CourseStatus = CourseStatus;
  ifResolved = computed(() => this.reviewService.latestReviewForCourse()?.comments.every(c => c.isResolved === true));

  ngOnInit(): void {
   this.courseContentService.getSectionsByCourseId(this.courseId());
   this.reviewService.getReviewsByCourse(this.courseId()).subscribe(
    (res) => {
      this.reviewService.allReviewsForCourse.set(res);
    }
  );
  this.reviewService.getLatestReviewByCourse(this.courseId()).subscribe(
    (res) => {
      this.reviewService.latestReviewForCourse.set(res);
    });
  }


  //New Section
  newSectionTitle = signal('');
  submitSection(event: Event){
    this.confirmationDialogService.confirmSave(event, () => {
      this.courseContentService.addSection(this.courseId(), this.newSectionTitle(), this.sections().length).subscribe({
        next: (res) => {
          this.newSectionTitle.set('');
        }
      })
    })
  }

  dropElement(event: CdkDragDrop<Section>) {
    moveItemInArray(event.container.data.elements, event.previousIndex, event.currentIndex);
    
    const element = event.container.data.elements.find(e => e.index === event.previousIndex);
    const section = this.sections().find(s => s.elements.find(e => e.id === element!.id));
    this.courseContentService.updateElementIndex(section!.id, element!.id, event.currentIndex).subscribe();

  } 

    dropSection(event: CdkDragDrop<Section[]>) {
      moveItemInArray(this.sections(), event.previousIndex, event.currentIndex);
      console.log(event);
      const section = this.sections().find(s => s.index === event.previousIndex);
      this.courseContentService.updateSectionIndex(section!.id, event.currentIndex).subscribe();
    }

    sectionEdit = false;
    changeSectionEditMode(event: boolean){
      this.sectionEdit = event;
    }

    
    elementEdit = false;
    changeElementEditMode(event: boolean){
      this.elementEdit = event;
    }

    onListEntered(event: CdkDragEnter) {
      // Możesz dodać tutaj logikę lub po prostu wymusić detekcję zmian.
      this.changeDetectorRef.detectChanges();
    }
    
    onListExited(event: CdkDragExit) {
      this.changeDetectorRef.detectChanges();
    }

    submitForReview(event: Event){
      this.confirmationDialogService.confirmSave(event, () => {
        this.reviewService.submitToReview(this.courseId()).subscribe();
      })
    }

    canSubmit(){
      if(this.sections().every(s => s.isPublished === true)){
        return true;
      }
      else{
        return false;
      }
    }


  //Comments for course review
  latestReview = computed(() => this.reviewService.latestReviewForCourse());
  allReviews = computed(() => this.reviewService.allReviewsForCourse()?.filter(r => r.id != this.latestReview()?.id));
  latestComment = computed(() => this.latestReview()?.comments.find(comment => comment.courseElementId === null) || null)
  comments = computed(() => this.allReviews()?.flatMap(review => review.comments).filter(comment => comment.courseElementId === null) || null)
  commentDialogVisible = false;
}
