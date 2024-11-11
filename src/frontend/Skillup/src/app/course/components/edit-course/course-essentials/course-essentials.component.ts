import { Component, computed, inject, input, NgModule, OnInit, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FileUploadModule } from 'primeng/fileupload';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { ImageCroperComponent } from '../../../../utils/components/image-croper/image-croper.component';
import { CoursesService } from '../../../services/course.service';
import { CourseDetail } from '../../../models/course.model';
import { SelectModule } from 'primeng/select';
import { CourseLevel } from '../../../models/course-level.model';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-course-essentials',
  standalone: true,
  imports: [InputTextModule, FloatLabelModule, ReactiveFormsModule, ButtonModule, FileUploadModule, ImageCroperComponent, SelectModule, NgFor, FormsModule],
  templateUrl: './course-essentials.component.html',
  styleUrl: './course-essentials.component.css'
})
export class CourseEssentialsComponent implements OnInit {
  //Services
  courseService = inject(CoursesService)
  //Variables
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);
  objectivesSummary = computed(() => this.course()?.objectivesSummary);
  levels = Object.entries(CourseLevel).map(([name, value]) => ({
    name,
    value
}));
newObjective = signal('');

  //Form
  courseDetailForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    level: new FormControl(''),
    objectivesSummary: new FormArray([])
  });


  ngOnInit(): void {
    this.courseService.getCourseById(this.courseId()).subscribe({
      next: (res) => {
        this.course.set(res);
        this.courseDetailForm.patchValue({
          title: res?.title,
          description: res?.description,
        });
        
      this.initializeObjectives();
      }
    })
    console.log(this.courseId())
  }
  
  initializeObjectives() {
    const objectives = this.objectivesSummary();
    if (objectives) {
      objectives.forEach(obj => {
        this.objectivesSummaryArray.push(new FormControl(obj));
      });
    }
  }

  get objectivesSummaryArray(): FormArray {
    return this.courseDetailForm.get('objectivesSummary') as FormArray;
  }

  addObjective() {
    console.log(this.newObjective());
    this.objectivesSummaryArray.push(new FormControl(this.newObjective())); 
    this.newObjective.set(''); 

  }

  removeObjective(index: number) {
    this.objectivesSummaryArray.removeAt(index);
  }

  submitForm(){
    console.log(this.courseDetailForm.value.title);
    console.log(this.courseDetailForm.value.description);
    console.log(this.courseDetailForm.value.level);
    console.log(this.courseDetailForm.value.objectivesSummary);
  }
}
