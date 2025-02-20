import { Component, inject, input, OnInit, signal } from '@angular/core';
import { AssetService } from '../../../services/asset.service';
import { Assignment, ExerciseType } from '../../../models/exercise.model';
import { AssetType } from '../../../models/course-content.model';
import { HiddenFormWrapperComponent } from "../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component";
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { QuestionListComponent } from "./question-list/question-list.component";
import { QuizListComponent } from "./quiz-list/quiz-list.component";
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FillTheGapComponent } from "../../fill-the-gap/fill-the-gap.component";
import { FillTheGapCreatorComponent } from "../../fill-the-gap/fill-the-gap-creator/fill-the-gap-creator.component";
import { FillTheGapListComponent } from "./fill-the-gap-list/fill-the-gap-list.component";
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-assignment',
  standalone: true,
  imports: [QuestionListComponent, QuizListComponent, InputTextModule, ButtonModule, FloatLabelModule, FormsModule, FillTheGapComponent, FillTheGapCreatorComponent, FillTheGapListComponent, RouterModule],
  templateUrl: './assignment.component.html',
  styleUrl: './assignment.component.css'
})
export class AssignmentComponent implements OnInit{
  //FromUrl
  elementId = input.required<string>();
  courseId = input.required<string>();

  //Services
  asssetService = inject(AssetService);

  //Variables
  assignment= signal<Assignment | null>(null);
  newInstruction = signal('');
  newQuestion = signal('');
  newAnswer = signal('');
  ExerciseType = ExerciseType;
  editing = false;

  ngOnInit(): void {
    this.asssetService.getAsset(this.elementId(), AssetType.Exercise).subscribe(
      (res) => {
        this.assignment.set(res);
        this.newInstruction.set(this.assignment()!.instruction);
      }
    )
  }

  getTypeText(){
    switch (this.assignment()?.exerciseType){
      case ExerciseType.Quiz:
        return 'Quiz';
      case ExerciseType.QuestionAnswer:
        return 'Question with one answer';
      case ExerciseType.FillTheGap:
        return 'Fill the gap';
      default:
        return '';
    }
  }

  changeEdit(){
    this.editing = !this.editing;
  }

  editAssignment(){
    this.asssetService.editAssignment(this.elementId(), this.newInstruction()).subscribe(
      (res) => {
        this.assignment.update(current => ({ ...current!, instruction: this.newInstruction()}));
        this.changeEdit();
      }
    )
  }

  click(){
    console.log('/course-edit/', this.courseId(), '/creator')
  }
}
