import { Component, inject, input, OnInit, signal } from '@angular/core';
import { AccordionModule } from 'primeng/accordion';
import { ElementType, Section } from '../../../models/course-content.model';
import { CourseContentService } from '../../../services/course-content-service';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-course-creator',
  standalone: true,
  imports: [AccordionModule, ButtonModule, DialogModule, InputTextModule, ReactiveFormsModule, CardModule, FormsModule],
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
        console.log(res);
      }
    })
  }


  //New Section
  changeAddSectionVisibility(){
    if(this.addSectionVisible === false)
      this.addSectionVisible = true;
    else
    this.addSectionVisible = false;
  }

  newSectionTitle = signal('');
  submitSection(){
    console.log(this.newSectionTitle());
    /* this.courseContentService.addSection(this.newSectionTitle(), this.courseId()).subscribe({
      next: (res) => {
        console.log(res);
      }
    }) */
  }

  //New Element
  addElement(){
    this.addElementVisible = true;
  }


  //DefineIcon
  definedIcon(type: ElementType) : string{
    switch (type){
      case ElementType.Article:
        return 'pi pi-book';
        case ElementType.Video:
        return 'pi pi-video';
        case ElementType.Exercise:
        return 'pi pi-objects-column';
    }
  }
}
