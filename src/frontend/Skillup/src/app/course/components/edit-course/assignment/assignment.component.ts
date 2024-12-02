import { Component, inject, input, OnInit, signal } from '@angular/core';
import { AssetService } from '../../../services/asset.service';
import { Assignment, ExerciseType } from '../../../models/exercise.model';
import { AssetType } from '../../../models/course-content.model';
import { HiddenFormWrapperComponent } from "../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component";
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { QuestionListComponent } from "./question-list/question-list.component";
import { QuizListComponent } from "./quiz-list/quiz-list.component";

@Component({
  selector: 'app-assignment',
  standalone: true,
  imports: [QuestionListComponent, QuizListComponent],
  templateUrl: './assignment.component.html',
  styleUrl: './assignment.component.css'
})
export class AssignmentComponent implements OnInit{
  //FromUrl
  elementId = input.required<string>();

  //Services
  asssetService = inject(AssetService);

  //Variables
  assignment= signal<Assignment | null>(null);
  newQuestion = signal('');
  newAnswer = signal('');
  ExerciseType = ExerciseType;

  ngOnInit(): void {
    this.asssetService.getAsset(this.elementId(), AssetType.Exercise).subscribe(
      (res) => {
        console.log(res);
        this.assignment.set(res);
      }
    )
  }


}
