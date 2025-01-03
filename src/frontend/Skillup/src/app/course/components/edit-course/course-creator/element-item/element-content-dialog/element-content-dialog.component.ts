import { Component, inject, input, OnChanges, OnInit, signal, SimpleChanges } from '@angular/core';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AssetType, Element } from '../../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { AssetService } from '../../../../../services/asset.service';
import { ToastHandlerService } from '../../../../../../core/services/toast-handler.service';
import { finalize } from 'rxjs';
import { Assignment, ExerciseType, QuestionAnswer, Quiz } from '../../../../../models/exercise.model';
import { Sentence } from '../../../../../models/fill-the-gap/fill-the-gap.models';
import { ExerciseService } from '../../../../../services/exercise.service';
import { SolveQuizComponent } from "../../../../exercises/solve-quiz/solve-quiz.component";
import { SolveQuestionComponent } from "../../../../exercises/solve-question/solve-question.component";
import { SolveFillTheGapComponent } from "../../../../exercises/solve-fill-the-gap/solve-fill-the-gap.component";

@Component({
  selector: 'app-element-content-dialog',
  standalone: true,
  imports: [PdfViewerModule, ButtonModule, FileUploadModule, SolveQuizComponent, SolveQuestionComponent, SolveFillTheGapComponent],
  templateUrl: './element-content-dialog.component.html',
  styleUrl: './element-content-dialog.component.css'
})
export class ElementContentDialogComponent implements OnChanges {
  //Variables
 visible = input.required<boolean>();
  element = input.required<Element>();
  fileLink = signal('');
  AssetType = AssetType;
  ExerciseType = ExerciseType;
  loadingUpload = false;

  currentAssignment: null | Assignment = null;
  currentQuiz: Quiz[] = [];
  currentQuestion: QuestionAnswer[] = [];
  currentFillTheGap: Sentence[] = [];

  //Services
  assetService = inject(AssetService);
  toastService = inject(ToastHandlerService);
  exerciseService = inject(ExerciseService);

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['visible'] && changes['visible'].currentValue){
      if(this.element().hasAsset){
        this.assetService.getAsset(this.element().id, this.element().type).subscribe((response) => {
          if(this.element().type === AssetType.Exercise){
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
          else{
            this.fileLink.set(response.url);
          }
          });
      }
    }
  }

  //Accepted File
  getAccept(){
    if(this.element().type === AssetType.Article){
      return '.pdf'
    }
    else{
      return '.mp4'
    }
  }
  //Files
  selectedFile: File | undefined;
  onSelectImage(event: FileSelectEvent) {
    this.selectedFile = event.currentFiles[0];
  }

  upload() {
    this.loadingUpload = true;
    switch (this.element().type){
      case AssetType.Article:
        this.assetService.addArticle(this.element().id, this.selectedFile!).subscribe({
          next: () => {
            this.element().hasAsset = true;
            this.assetService.getAsset(this.element().id, this.element().type)
            .pipe(finalize (() => this.loadingUpload = false))
            .subscribe((response) => {
              this.fileLink.set(response.url);
              });
          },
          error: () => {
            this.toastService.showError("Something went wrong adding element");
          }
        });
        break;
      case AssetType.Video:
        this.assetService.addVideo(this.element().id, this.selectedFile!)
        .pipe(finalize (() => this.loadingUpload = false))
        .subscribe({
          next: () => {
            this.element().hasAsset = true;
            this.assetService.getAsset(this.element().id, this.element().type).subscribe((response) => {
              this.fileLink.set(response.url);
              });
          },
          error: () => {
            this.toastService.showError("Something went wrong adding element");
          }
        });
        break;
    }
  }

  cancel() {
    this.selectedFile = undefined;
  }

  
}
