import { Component, input, NgModule, OnInit, signal } from '@angular/core';
import { Section } from '../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

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
  editing = false;

  
  ngOnInit(): void {
    this.sectionTitle.set(this.section().title);
  }
  changeEditVisibility(){
    if(this.editing)
      this.editing=false;
    else
      this.editing=true;
  }

  saveElement(){
    console.log('saving element')
  }

  removeElement(){
    console.log('deleting element')
  }
}
