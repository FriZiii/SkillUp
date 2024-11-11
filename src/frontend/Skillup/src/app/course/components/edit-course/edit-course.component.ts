import { Component, inject, input, OnInit, signal } from '@angular/core';
import { MenuModule } from 'primeng/menu';
import { CourseDetail } from '../../models/course.model';
import { MenuItem } from 'primeng/api';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-edit-course',
  standalone: true,
  imports: [MenuModule, RouterModule, RouterOutlet, NgIf],
  templateUrl: './edit-course.component.html',
  styleUrl: './edit-course.component.css'
})
export class EditCourseComponent implements OnInit{
//Variables
courseId = input.required<string>();
course = signal<CourseDetail | null>(null);
items: MenuItem[] | undefined;
router = inject(Router)

ngOnInit(){
  this.items = [
    {
        label: 'Navigate',
        items: [
            {
                label: 'Creator',
                icon: 'pi pi-palette',
                route: 'creator'
            },
            {
                label: 'Essentials',
                icon: 'pi pi-link',
                route: 'essentials'
            },
            {
                label: 'Pricing',
                icon: 'pi pi-dollar',
                route: 'price'
            },
            {
                label: 'Landing Page',
                icon: 'pi pi-home',
                route: 'landing-page'
            }
        ]
    }
];
}
}
