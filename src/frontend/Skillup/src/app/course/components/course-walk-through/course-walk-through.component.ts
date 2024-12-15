import { ChangeDetectorRef, Component, computed, inject, input, OnChanges, OnInit, signal, SimpleChanges } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { UserProgressService } from '../../services/user-progress-service';
import { UserService } from '../../../user/services/user.service';
import { CourseContentService } from '../../services/course-content.service';
import { AccordionModule } from 'primeng/accordion';
import { SectionItemComponent } from "../edit-course/course-creator/section-item/section-item.component";
import { ElementItemDisplayComponent } from '../displays/element-item-display/element-item-display.component';
import { AssetType, Element } from '../../models/course-content.model';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AssetService } from '../../services/asset.service';
import { VjsPlayerComponent } from '../../../videojs/videojs.component';
import { CommonModule } from '@angular/common';
import { Assignment, ExerciseType, QuestionAnswer, Quiz } from '../../models/exercise.model';
import { ExerciseService } from '../../services/exercise.service';
import { SolveQuizComponent } from "../exercises/solve-quiz/solve-quiz.component";
import { SolveQuestionComponent } from "../exercises/solve-question/solve-question.component";
import { Sentence } from '../../models/fill-the-gap/fill-the-gap.models';
import { SolveFillTheGapComponent } from "../exercises/solve-fill-the-gap/solve-fill-the-gap.component";

@Component({
  selector: 'app-course-walk-through',
  standalone: true,
  imports: [AccordionModule, SectionItemComponent, ElementItemDisplayComponent, PdfViewerModule, VjsPlayerComponent, CommonModule, SolveQuizComponent, SolveQuestionComponent, SolveFillTheGapComponent],
  templateUrl: './course-walk-through.component.html',
  styleUrl: './course-walk-through.component.css'
})
export class CourseWalkThroughComponent implements OnInit{
  //URL
  courseId = input.required<string>();

  //Services
  coureContentService = inject(CourseContentService);
  userProgressService = inject(UserProgressService);
  assetService = inject(AssetService);
  cdr = inject(ChangeDetectorRef);
  exerciseService = inject(ExerciseService);

  //Variables
  AssetType = AssetType;
  ExerciseType = ExerciseType;
  sections = computed(() => this.coureContentService.sections());
  private _currentElement: Element | null = null
  fileLink = signal('');
  hasLink = false;
  currentAssignment: null | Assignment = null;
  currentQuiz:  Quiz[] = [];
  currentQuestion:  QuestionAnswer[] = [];
  currentFillTheGap:Sentence[] = [];

  ngOnInit(): void {
    this.coureContentService.getSectionsByCourseId(this.courseId());

    this.userProgressService.getAcomplishedElementsForCourse(this.courseId()).subscribe(
      (res) => {
        this.userProgressService.accomplishedElements.set(res);
    })

    new Promise(resolve => setTimeout(resolve, 2000))
    .then(() => {
        this.currentElement = this.sections()[0].elements[0];
    });
  
  }

  get currentElement(): Element | null {
    return this._currentElement;
  }

  set currentElement(value: Element) {
    this._currentElement = value;
    if(value.hasAsset){
      this.assetService.getAsset(value.id, value.type).subscribe((response) => {
        if(value.type === AssetType.Video || value.type === AssetType.Article){
          this.fileLink.set(response.url);
          this.hasLink = true; // Zapamiętujemy poprzedni link
          this.cdr.detectChanges(); // Wymusza detekcję zmian
          console.log(this.fileLink());
        }
        else{
          this.currentAssignment = response;
          this.exerciseService.getExercises(this.currentAssignment!.assetId, this.currentAssignment!.exerciseType).subscribe((res) => {
            if(this.currentAssignment!.exerciseType === ExerciseType.Quiz){
              this.currentQuiz = res;
            }
            if(this.currentAssignment!.exerciseType === ExerciseType.QuestionAnswer){
              this.currentQuestion = res;
            }
            if(this.currentAssignment!.exerciseType === ExerciseType.FillTheGap){
              this.currentFillTheGap = res;
            }
          });
        }
        
        });
    }
}

  onElementClicked(element: Element){
    this.currentElement = element;
    this.hasLink = false;
  }
}
