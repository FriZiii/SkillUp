import {
  ChangeDetectorRef,
  Component,
  computed,
  inject,
  input,
  OnChanges,
  OnInit,
  signal,
  SimpleChanges,
} from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { UserProgressService } from '../../services/user-progress-service';
import { UserService } from '../../../user/services/user.service';
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
import { VjsPlayerComponent } from '../../../videojs/videojs.component';
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
import { SuComment } from '../../models/comment.model';
import { CommentComponent } from '../comments/comment/comment.component';
import { FormsModule } from '@angular/forms';
import { CommentService } from '../../services/comment.service';

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
  ],
  templateUrl: './course-walk-through.component.html',
  styleUrl: './course-walk-through.component.css',
})
export class CourseWalkThroughComponent implements OnInit {
  //URL
  courseId = input.required<string>();

  //Services
  coureContentService = inject(CourseContentService);
  courseService = inject(CoursesService);
  userProgressService = inject(UserProgressService);
  assetService = inject(AssetService);
  cdr = inject(ChangeDetectorRef);
  exerciseService = inject(ExerciseService);
  commentService = inject(CommentService);

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
  comments = signal<SuComment[]>([]);

  ngOnInit(): void {
    this.coureContentService.getSectionsByCourseId(this.courseId());

    this.coureContentService
      .getAttachmentsByCoruseId(this.courseId())
      .subscribe((res) => {
        this.attachements.set(res);
        console.log(this.attachements());
      });

    this.userProgressService
      .getAcomplishedElementsForCourse(this.courseId())
      .subscribe((res) => {
        this.userProgressService.accomplishedElements.set(res);
      });

    new Promise((resolve) => setTimeout(resolve, 2000)).then(() => {
      this.currentElement = this.sections()[0].elements[0];
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
    //current element na accomplished
    this._currentElement = value;

    this.commentService.getCommentsByElementId(value.id).subscribe((res) => {
      console.log('init:');
      this.comments.set(res);
      console.log('courseWalk:');
      console.log(this.comments());
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
          console.log(this.fileLink());
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
  }

  /*  comments: SuComment[] = [
    {
        id: '1',
        elementId: 'c1',
        author: { id: '1', firstName: 'Adamsdavid', lastName: 'Lokl', email:'abd@abc.pl', profilePicture: '/assets/avatar.png' },
        content: "I genuinely think that Codewell's community is AMAZING. It's just starting out but the templates on there amazing.",
        createdAt: "2024-12-21T14:38:32.852618Z",
        likesCount: 2,
        isLiked: false,
        replies: [
            {
                id: '2',
                elementId: 'c2',
                author: { id: '1', firstName: 'saramay', lastName: 'Lokl', email:'abd@abc.pl', profilePicture: '/assets/avatar.png' },
                content: "I agree. I've been coding really well (pun intended) ever since I started practicing on their templates hehe.",
                createdAt: new Date(Date.now() - 16 * 60 * 60 * 1000),
                likesCount: 5,
                isLiked: false,
                replies: [
                    {
                        id: '3',
                        elementId: 'c3',
                        author: { id: '1', firstName: 'jess', lastName: 'Lokl', email:'abd@abc.pl', profilePicture: '/assets/avatar.png' },
                        content: 'Okay, this comment wins.',
                        createdAt: new Date(Date.now() - 14 * 60 * 60 * 1000),
                        likesCount: 5,
                        isLiked: false,
                        replies: [
                          {
                            id: '4',
                            elementId: 'c3',
                            author: { id: '1', firstName: 'mateuszek', lastName: 'super', email:'abd@abc.pl', profilePicture: '/assets/avatar.png' },
                            content: 'Okay, this comment wins.',
                            createdAt: new Date(Date.now() - 14 * 60 * 60 * 1000),
                            likesCount: 5,
                            isLiked: false,
                            replies: []
                        }
                        ]
                    }
                ]
            }
        ]
    },
    {
        id: '4',
        elementId: 'c4',
        author: { id: '1', firstName: 'anrzej', lastName: 'duda', email:'abd@abc.pl', profilePicture: '/assets/avatar.png' },
        content: 'Thanks for making this, super helpful',
        createdAt: new Date(Date.now() - 20 * 60 * 60 * 1000),
        likesCount: 2,
        isLiked: false,
        replies: []
    }
]; */

  onCommentAdded(comment: SuComment[]) {
    console.log(comment);
    this.comments.set(comment);
  }
}
