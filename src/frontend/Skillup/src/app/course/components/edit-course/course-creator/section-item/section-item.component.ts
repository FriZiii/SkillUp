import { Component, inject, input, NgModule, OnInit, output, signal } from '@angular/core';
import { Section } from '../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { CourseContentService } from '../../../../services/course-content-service';

@Component({
  selector: 'app-section-item',
  standalone: true,
  imports: [ButtonModule, NgClass, FormsModule, InputTextModule],
  templateUrl: './section-item.component.html',
  styleUrl: './section-item.component.css'
})
export class SectionItemComponent implements OnInit {
  section = input.required<Section>();
  sectionTitle = signal('');
  onEditChange = output<boolean>();
  editing = false;

  //Services
  courseContentService = inject(CourseContentService);
  
  ngOnInit(): void {
    this.sectionTitle.set(this.section().title);
  }
  changeEditVisibility(){
    if(this.editing)
    {
      this.editing=false;
      this.onEditChange.emit(false);
    }
    else{
      this.editing=true;
      this.onEditChange.emit(true);
    }
  }

  saveElement(){
    this.courseContentService.updateSection(this.section().id, this.sectionTitle()).subscribe();
    this.changeEditVisibility();
  }

  removeElement(){
    this.courseContentService.deleteSection(this.section().id).subscribe();
  }
}
