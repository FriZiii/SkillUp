import { Component, inject, input, OnInit, signal } from '@angular/core';
import { AccordionModule } from 'primeng/accordion';
import { Section } from '../../../models/course-content.model';
import { CourseContentService } from '../../../services/course-content-service';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-course-creator',
  standalone: true,
  imports: [AccordionModule, ButtonModule, DialogModule, InputTextModule, ReactiveFormsModule, CardModule],
  templateUrl: './course-creator.component.html',
  styleUrl: './course-creator.component.css'
})
export class CourseCreatorComponent implements OnInit {
  //Variables
  courseId = input.required<string>();
  sections = signal<Section[]>([]);
  addSectionVisible = false;
  addElementVisible = false;

  //Services
  courseContentService = inject(CourseContentService);

  ngOnInit(): void {
    this.courseContentService.getSectionsByCourseId(this.courseId()).subscribe({
      next: (res) => {
        this.sections.set(res);
      }
    })
  }


  //New Section
  addSection(){
    this.addSectionVisible = true;
  }

  addSectionForm = new FormGroup({
    title: new FormControl('')
  })

  submitSection(){
    const title = this.addSectionForm.value.title!;
    console.log(this.addSectionForm.value.title);
    this.courseContentService.addSection(title, this.courseId()).subscribe({
      next: (res) => {
        console.log(res);
      }
    })
  }

  //New Element
  addElement(){
    this.addElementVisible = true;
  }
}
