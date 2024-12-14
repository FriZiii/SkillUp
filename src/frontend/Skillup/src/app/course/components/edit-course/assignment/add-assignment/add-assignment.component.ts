import { Component, inject, input, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { AssetService } from '../../../../services/asset.service';
import { ExerciseType } from '../../../../models/exercise.model';
import { Router } from '@angular/router';
import { SelectModule } from 'primeng/select';
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-add-assignment',
  standalone: true,
  imports: [FormsModule, InputTextModule, ButtonModule, SelectModule, FloatLabelModule],
  templateUrl: './add-assignment.component.html',
  styleUrl: './add-assignment.component.css'
})
export class AddAssignmentComponent {
  //FromUrl
  elementId = input.required<string>();

  //Services
  assetService = inject(AssetService);
  router = inject(Router);

  instruction = signal('');
  selectedType = signal<ExerciseType | null>(null);
  exerciseTypes = [{name: 'Quiz', value: ExerciseType.Quiz},
    {name: 'Question with one answer', value: ExerciseType.QuestionAnswer},
    {name: 'Fill the gap', value: ExerciseType.FillTheGap}
  ];

  submit(){
    this.assetService.addAssignment(this.elementId(), this.selectedType()!, this.instruction()).subscribe(
      (res) => {
        this.router.navigate(['/element-edit/', this.elementId(), 'assignment']);
      }
    );
  }
}
