import {
  ChangeDetectorRef,
  Component,
  computed,
  inject,
  input,
  OnInit,
  signal,
} from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { UserProgressService } from '../../services/user-progress-service';
import { CourseContentService } from '../../services/course-content.service';
import { AccordionModule } from 'primeng/accordion';
import { SectionItemComponent } from '../edit-course/course-creator/section-item/section-item.component';
import { ElementItemDisplayComponent } from '../displays/element-item-display/element-item-display.component';
import {
  AssetType,
  Attachment,
  Element,
} from '../../models/course-content.model';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AssetService } from '../../services/asset.service';
import { VjsPlayerComponent } from '../../../utils/videojs/videojs.component';
import { CommonModule } from '@angular/common';
import {
  Assignment,
  ExerciseType,
  QuestionAnswer,
  Quiz,
} from '../../models/exercise.model';
import { ExerciseService } from '../../services/exercise.service';
import { SolveQuizComponent } from '../exercises/solve-quiz/solve-quiz.component';
import { SolveQuestionComponent } from '../exercises/solve-question/solve-question.component';
import { Sentence } from '../../models/fill-the-gap/fill-the-gap.models';
import { SolveFillTheGapComponent } from '../exercises/solve-fill-the-gap/solve-fill-the-gap.component';
import { CoursePercentage } from '../../models/user-progress.model';
import { CircleProgressComponent } from '../../../utils/components/circle-progress/circle-progress.component';
import { TabsModule } from 'primeng/tabs';
import { CommentsComponent } from '../comments/comments.component';
import { CommentService } from '../../services/comment.service';
import { AuthorDescriptionComponent } from "../displays/author-description/author-description.component";
import { CourseRatingsListComponent } from "./course-ratings-list/course-ratings-list.component";
import { SkeletonModule } from 'primeng/skeleton';
import { Router } from '@angular/router';

@Component({
  selector: 'app-course-walk-through',
  standalone: true,
  imports: [
    AccordionModule,
    SectionItemComponent,
    ElementItemDisplayComponent,
    PdfViewerModule,
    VjsPlayerComponent,
    CommonModule,
    SolveQuizComponent,
    SolveQuestionComponent,
    SolveFillTheGapComponent,
    CircleProgressComponent,
    TabsModule,
    CommentsComponent,
    AuthorDescriptionComponent,
    CourseRatingsListComponent,
    SkeletonModule
],
  templateUrl: './course-walk-through.component.html',
  styleUrl: './course-walk-through.component.css',
})
export class CourseWalkThroughComponent implements OnInit {
  //URL
  courseId = input.required<string>();
  currentElementId = input<string>();

  //Services
  coureContentService = inject(CourseContentService);
  courseService = inject(CoursesService);
  userProgressService = inject(UserProgressService);
  assetService = inject(AssetService);
  cdr = inject(ChangeDetectorRef);
  exerciseService = inject(ExerciseService);
  commentService = inject(CommentService);
  router = inject(Router);

  //Variables
  AssetType = AssetType;
  ExerciseType = ExerciseType;
  sections = computed(() => this.coureContentService.sections());
  private _currentElement: Element | null = null;
  fileLink = signal('');
  hasLink = false;
  currentAssignment: null | Assignment = null;
  currentQuiz: Quiz[] = [];
  currentQuestion: QuestionAnswer[] = [];
  currentFillTheGap: Sentence[] = [];
  course = computed(() =>
    this.courseService.courses().find((c) => c.id === this.courseId())
  );
  percentage: CoursePercentage | undefined = undefined;
  attachements = signal<Attachment[]>([]);
  comments = computed(() => this.commentService.currentComments());
  loading = true;

  ngOnInit(): void {
    this.coureContentService.getSectionsByCourseId(this.courseId());

    this.coureContentService
      .getAttachmentsByCoruseId(this.courseId())
      .subscribe((res) => {
        this.attachements.set(res);
      });

    this.userProgressService
      .getAcomplishedElementsForCourse(this.courseId())
      .subscribe((res) => {
        this.userProgressService.accomplishedElements.set(res);
      });

    new Promise((resolve) => setTimeout(resolve, 2000)).then(() => {
      if(this.currentElementId !== undefined){
        this.currentElement = this.sections().flatMap(s => s.elements).find(e => e.id === this.currentElementId()) ?? this.sections()[0].elements[0];
        this.loading = false;
      }else{
        this.currentElement = this.sections()[0].elements[0];
        this.loading = false;
      }
    });

    this.userProgressService.getPercentage().subscribe((res) => {
      this.percentage = res.find((x) => x.courseId === this.courseId());
    });

  }

  attachmentForElement(elementId: string) {
    return this.attachements().filter((a) => a.elementId === elementId);
  }

  get currentElement(): Element | null {
    return this._currentElement;
  }

  set currentElement(value: Element) {
    this._currentElement = value;

    this.commentService.getCommentsByElementId(value.id).subscribe((res) => {
      this.commentService.currentComments.set(res);
    });

    if (value.hasAsset) {
      this.assetService.getAsset(value.id, value.type).subscribe((response) => {
        if (
          value.type === AssetType.Video ||
          value.type === AssetType.Article
        ) {
          this.fileLink.set(response.url);
          this.hasLink = true;
          this.cdr.detectChanges();
        } else {
          this.currentAssignment = response;
          this.exerciseService
            .getExercises(
              this.currentAssignment!.assetId,
              this.currentAssignment!.exerciseType
            )
            .subscribe((res) => {
              if (this.currentAssignment!.exerciseType === ExerciseType.Quiz) {
                this.currentQuiz = res;
              }
              if (
                this.currentAssignment!.exerciseType ===
                ExerciseType.QuestionAnswer
              ) {
                this.currentQuestion = res;
              }
              if (
                this.currentAssignment!.exerciseType === ExerciseType.FillTheGap
              ) {
                this.currentFillTheGap = res;
              }
            });
        }
      });
    }
  }

  onElementClicked(element: Element) {
    this.currentElement = element;
    this.hasLink = false;
    this.router.navigate(['course/' + this.courseId() + '/walk-through/' + element.id])
  }
}
